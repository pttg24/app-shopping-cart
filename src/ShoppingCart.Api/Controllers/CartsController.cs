using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Api.Extensions;
using ShoppingCart.Contracts;
using ShoppingCart.Domain;
using ShoppingCart.Infrastructure;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Api.Controllers;

public class CartsController : BaseController
{
    private readonly IUnitOfWork _uow;
    public CartsController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpPost]
    public async Task<ActionResult> AddToCart(int productId, int count, double price)
    {
        if (productId == 0 || count == 0)
            return BadRequest("Invalid Inputs");

        var userId = HttpContext.User.GetUserId();
        if (userId == 0) userId = 1;
        var item = await _uow.CartRepository.AddToCart(userId, productId, count, price);
        if (!await _uow.SaveChanges())
        {
            return BadRequest("Failed to add.");
        }
        return Ok(await _uow.CartRepository.GetCart(userId));
    }

    [HttpGet]
    public async Task<ActionResult> GetCartByUserId()
    {
        var userId = HttpContext.User.GetUserId();
        if (userId == 0) userId = 1;
        var result = await _uow.CartRepository.GetCart(userId);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetCartById(int id)
    {
        var result = await _uow.CartRepository.GetCartById(id);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveFromCart([FromBody] int cartItemId)
    {
        var userId = HttpContext.User.GetUserId();
        if (userId == 0) userId = 1;
        var remove = await _uow.CartRepository.RemoveFromCart(userId, cartItemId);
        if (remove && !await _uow.SaveChanges())
        {
            return BadRequest("Failed to remove.");
        }
        return Ok();
    }
}
