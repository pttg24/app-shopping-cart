using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure.Models;

namespace ShoppingCart.Infrastructure;

public partial class ShoppingCartContext : DbContext
{
    public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options) : base(options)
    {

    }

    public ShoppingCartContext()
    {
        
    }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Order>().HasKey(o => o.Id);
        builder.Entity<OrderItem>().HasKey(oi => oi.Id);
        builder.Entity<Cart>().HasKey(c => c.Id);
        builder.Entity<CartItem>().HasKey(ci => ci.Id);

    }

}
