using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure;

public interface IUnitOfWork
{
    ICartRepository CartRepository { get; }
    IOrderRepository OrderRepository { get; }
    IProductRepository ProductRepository { get; }

    Task<bool> SaveChanges();
    bool HasChanges();
}
