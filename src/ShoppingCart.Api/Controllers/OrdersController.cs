using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using ShoppingCart.Api.Extensions;
using ShoppingCart.Contracts.Dtos;
using ShoppingCart.Infrastructure;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Api.Controllers;

[Authorize]
public class OrdersController : BaseController
{
    private readonly IUnitOfWork _uow;

    public OrdersController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpPost]
    public async Task<ActionResult> AddToOrder(int productId, int count, double price)
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
    public async Task<ActionResult> GetOrder()
    {
        var userId = HttpContext.User.GetUserId();
        if (userId == 0) userId = 1;
        var result = await _uow.OrderRepository.GetOrder(userId);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetOrderById(int id)
    {
        var result = await _uow.OrderRepository.GetOrderById(id);
        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateOrder(int id, [FromBody] OrderDto updatedOrder)
    {
        var result = await _uow.OrderRepository.UpdateOrder(id);
        return NoContent();
    }

    [HttpPatch]
    [Route("{id}/status")]
    public async Task<ActionResult> UpdateOrderStatus(int id, [FromBody] JsonPatchDocument<OrderDto> patchDoc)
    {
        var existingOrder = await _uow.OrderRepository.GetOrderById(id);
        if (existingOrder == null)
        {
            return NotFound();
        }

        await _uow.OrderRepository.UpdateOrderStatus(id, patchDoc);
        return NoContent();
    }
}