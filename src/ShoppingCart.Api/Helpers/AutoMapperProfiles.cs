using AutoMapper;
using ShoppingCart.Contracts.Dtos;
using ShoppingCart.Infrastructure.Models;
using ShoppingCart.Infrastructure.Seed;

namespace ShoppingCart.Api.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<Cart, CartDto>();
        CreateMap<CartItem, CartItemDto>();
    }
}
