using ShoppingCart.Contracts.Dtos;

namespace ShoppingCart.Infrastructure.Interfaces;

public interface IProductRepository
{
    Task<List<ProductDto>> GetAllProducts();
}
