using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShoppingCart.Infrastructure.Interfaces;
using ShoppingCart.Infrastructure.Repositories;

namespace ShoppingCart.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShoppingCartContext _context;
    private readonly IMapper _mapper;

    public ICartRepository CartRepository => new CartRepository(_context, _mapper);
    public IOrderRepository OrderRepository => new OrderRepository(_context, _mapper);
    public IProductRepository ProductRepository => new ProductRepository(_context, _mapper);

    public UnitOfWork(ShoppingCartContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }
}
