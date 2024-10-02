using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Contracts.Dtos;
using ShoppingCart.Domain;
using ShoppingCart.Infrastructure.Interfaces;
using ShoppingCart.Infrastructure.Models;

namespace ShoppingCart.Infrastructure.Repositories;

public class CartRepository : BaseRepository, ICartRepository
{
    public CartRepository(ShoppingCartContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<CartItemDto> AddToCart(int userId, int productId, int count, double price)
    {
        var userHasOpenCart = DbContext.Carts.Select(c => c.UserId == userId).FirstOrDefault();

        if (userHasOpenCart)
        {
            var cart = DbContext.Carts.Where(c => c.UserId == userId).Select(c => c.Id).FirstOrDefault();
            var cartHasProduct = await DbContext.CartItems.Where(ci => ci.CartId == cart).AnyAsync(i => i.ProductId == productId);
            if (cartHasProduct) throw new Exception("Item already in cart.");
            var item = new CartItem { CartId = cart, Price = price, ProductId = productId, Count = count };
            DbContext.CartItems.Add(item);
        }
        else
        {
            var newCart = new Cart { UserId = userId, TotalAmount = price * count };
            var newItem = new CartItem { CartId = newCart.Id, Price = price, ProductId = productId, Count = count };
            DbContext.Carts.Add(newCart);
            DbContext.CartItems.Add(newItem);
        }
     
        return new CartItemDto() { 
            Price = price * count,
            ProductId = productId
        };
    }

    public async Task<List<CartItemDto>> GetCart(int userId)
    {
        var cart = DbContext.Carts
            .Where(c => c.UserId == userId)
            .Select(c => c.Id).FirstOrDefault();

        var items = await DbContext.CartItems
            .Where(i => i.CartId == cart)
            .ProjectTo<CartItemDto>(Mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();

        var result = new List<CartItemDto>();
        foreach (var item in items)
        {
            result.Add(new CartItemDto
            {
                CartId = item.CartId,
                ProductId = item.ProductId,
                Price = item.Price,
                Count = item.Count
            });
        }
        return result;
    }

    public async Task<List<CartItemDto>> GetCartById(int id)
    {
        var cart = DbContext.Carts
            .Where(c => c.Id == id)
            .Select(c => c.Id).FirstOrDefault();

        var items = await DbContext.CartItems
            .Where(i => i.CartId == cart)
            .ProjectTo<CartItemDto>(Mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();

        var result = new List<CartItemDto>();
        foreach (var item in items)
        {
            result.Add(new CartItemDto
            {
                CartId = item.CartId,
                ProductId = item.ProductId,
                Price = item.Price,
                Count = item.Count
            });
        }
        return result;
    }

    public async Task<bool> RemoveFromCart(int userId, int cartItemId)
    {
        var cartId = DbContext.Carts
            .Where(c => c.UserId == userId)
            .Select(c => c.Id).FirstOrDefault();

        var cart = await DbContext.Carts
            .Where(i => i.Id == cartId).ToListAsync();

        var cartItem = await DbContext.CartItems
            .Where(i => i.CartId == cartId && i.Id == cartItemId).ToListAsync();

        DbContext.CartItems.RemoveRange(cartItem);
        DbContext.Carts.RemoveRange(cart);
        return true;
    }
}
