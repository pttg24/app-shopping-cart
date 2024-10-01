using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Contracts.Dtos;
using ShoppingCart.Infrastructure.Interfaces;
using ShoppingCart.Infrastructure.Models;
using System.Linq;

namespace ShoppingCart.Infrastructure.Repositories;

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(ShoppingCartContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductDto>> GetAllProducts()
    {
        var products = await DbContext.Products
            .ProjectTo<ProductDto>(Mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
        return products;
    }
}
