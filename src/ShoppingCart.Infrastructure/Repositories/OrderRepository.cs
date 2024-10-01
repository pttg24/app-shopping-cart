using AutoMapper;
using ShoppingCart.Contracts.Dtos;
using ShoppingCart.Domain;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Repositories;

public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(ShoppingCartContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
