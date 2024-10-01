using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

}
