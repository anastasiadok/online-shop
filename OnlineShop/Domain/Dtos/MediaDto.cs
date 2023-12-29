namespace OnlineShop.Domain.Dtos;

public record MediaDto(Guid MediaId, Guid ProductId, byte[] Bytes, string FileType, string FileName);