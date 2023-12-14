using OnlineShop.Data;

namespace OnlineShop.Domain.Services;

public abstract class BaseService
{
    protected readonly OnlineshopContext _context;

    public BaseService(OnlineshopContext context)
    {
        _context = context;
    }
}