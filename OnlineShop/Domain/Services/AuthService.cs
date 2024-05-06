using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Domain.Dtos;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace OnlineShop.Domain.Services;

public class AuthService : BaseService, IAuthService
{
    private readonly IConfiguration _configuration;
    public AuthService(OnlineshopContext context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    public async Task<LoginResponseDto> Login(string email, string password)
    {
        var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        if (user is null)
            throw new BadRequestException("User doesn't exist");

        var computedHash = new HMACSHA512().ComputeHash(Encoding.UTF8.GetBytes(password));
        if (computedHash.SequenceEqual(user.PasswordHash))
            throw new BadRequestException("Wrong password");

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
        };

        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            notBefore: now,
            claims: claims,
            expires: now.Add(TimeSpan.FromHours(Convert.ToDouble(_configuration["Jwt:DurationInHours"]))),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])), SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return new() { Email = email, UserId = user.UserId, Token = encodedJwt };
    }

    public async Task Register(UserCreationDto userDto)
    {
        var emailUser = await _context.Users.Where(u => u.Email == userDto.Email).FirstOrDefaultAsync();
        if (emailUser is not null)
            throw new BadRequestException("Email is already in use");

        var user = userDto.Adapt<User>();
        user.UserId = Guid.NewGuid();
        user.PasswordHash = new HMACSHA512().ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
