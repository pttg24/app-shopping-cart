using ShoppingCart.Contracts.Dtos;
using ShoppingCart.Domain;

namespace ShoppingCart.Infrastructure.Interfaces;

public interface ICartRepository
{
    Task<List<CartItemDto>> GetCart(int userId);
    Task<List<CartItemDto>> GetCartById(int id);
    Task<CartItemDto> AddToCart(int userId, int productId, int count, double price);
    Task<bool> RemoveFromCart(int userId, int cartItemId);
}
