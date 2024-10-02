using Microsoft.AspNetCore.JsonPatch;
using ShoppingCart.Contracts.Dtos;
using ShoppingCart.Domain;

namespace ShoppingCart.Infrastructure.Interfaces;

public interface IOrderRepository
{
    Task<List<OrderItemDto>> GetOrder(int userId);
    Task<List<OrderItemDto>> GetOrderById(int id);
    Task<OrderItemDto> AddToOrder(int userId, int productId, int count, double price);
    Task<bool> UpdateOrder(int id);
    Task<bool> UpdateOrderStatus(int id, JsonPatchDocument<OrderDto> patchDoc);
}
