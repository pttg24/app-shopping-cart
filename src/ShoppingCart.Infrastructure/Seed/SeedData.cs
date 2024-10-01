using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoppingCart.Domain;
using ShoppingCart.Infrastructure.Models;
using System.Text.Json;

namespace ShoppingCart.Infrastructure.Seed;

public class SeedData
{
    private readonly IConfiguration _config;
    private readonly ShoppingCartContext _context;
    private readonly IMapper _mapper;
    private static readonly Random Random = new();

    public SeedData(IConfiguration config, ShoppingCartContext context, IMapper mapper)
    {
        _config = config;
        _context = context;
        _mapper = mapper;
    }

    public async Task SeedDatabase()
    {
        await _context.Database.MigrateAsync();
        await SeedProduct();
        await SeedDefaultCartAndOrder();
    }

    async Task SeedProduct()
    {
        if (await _context.Products.AnyAsync()) return;

        var productSeeds = FillProduct();

        if (productSeeds != null)
            foreach (var productSeed in productSeeds)
            {
                var product = _mapper.Map<Models.Product>(productSeed);
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
    }

    async Task SeedDefaultCartAndOrder()
    {
        if (await _context.Carts.AnyAsync()) return;
        if (await _context.Orders.AnyAsync()) return;

        var cartItem = new CartItem() { Id = 1, CartId = 1, ProductId = 1, Price = 0.65, Count = 2 } ;
        var cart = new Cart() { Id = 1, UserId = 1, CartItems = new List<CartItem> { cartItem }, TotalAmount = 1.30 };
        var orderItem = new OrderItem() { Id = 1, OrderId = 1, ProductId = 1, Price = 0.65, Count = 2, Status = "Completed" };
        var order = new Models.Order() { Id = 1, UserId = 1, OrderItems = new List<OrderItem> { orderItem }, TotalAmount = 1.30, Status = "Completed" };

        await _context.CartItems.AddAsync(cartItem);
        await _context.Carts.AddAsync(cart);
        await _context.OrderItems.AddAsync(orderItem);
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    private List<Models.Product> FillProduct() => new List<Models.Product>()
    {
        new() { Id = 1, Name = "Soup", Description = "Traditional soup", Amount = 0.65, Unit = "TIN" },
        new() { Id = 2, Name = "Bread", Description = "Traditional bread", Amount = 0.80, Unit = "LOAF" },
        new() { Id = 3, Name = "Milk", Description = "Vegetal milk", Amount = 1.30, Unit = "BOTTLE" },
        new() { Id = 4, Name = "Apples", Description = "Regional apples", Amount = 1.00, Unit = "BAG" }
    };
}
