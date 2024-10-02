using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using ShoppingCart.Contracts.Dtos;
using ShoppingCart.Domain;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Repositories;

public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(ShoppingCartContext context, IMapper mapper) : base(context, mapper)
    { 

    }
    public async Task<List<OrderItemDto>> GetOrder(int userId) 
    {
        throw new NotImplementedException();
    }

    public async Task<List<OrderItemDto>> GetOrderById(int id) 
    {
        throw new NotImplementedException();
    }
    public async Task<OrderItemDto> AddToOrder(int userId, int productId, int count, double price) 
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateOrder(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateOrderStatus(int id, JsonPatchDocument<OrderDto> patchdoc)
    {
        //patchDoc.ApplyTo(existingOrder);
        throw new NotImplementedException();
    }

}
