using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Infrastructure;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Api.Controllers;

public class ProductsController : BaseController
{
    private readonly IUnitOfWork _uow;

    public ProductsController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllProducts()
    {
        var products = await _uow.ProductRepository.GetAllProducts();
        if (!products.Any()) return BadRequest("Products not found!");
        return Ok(products);
    }

}