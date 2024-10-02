using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingCart.Api.Controllers;
using ShoppingCart.Contracts.Dtos;
using ShoppingCart.Infrastructure;
using ShoppingCart.Infrastructure.Interfaces;
using System.Security.Claims;

namespace ShoppingCart.Api.Tests.Controllers;

public class OrdersControllerTests
{
    private readonly OrdersController _controller;
    private readonly Mock<IUnitOfWork> _mockUow;
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<ICartRepository> _mockCartRepository;

    public OrdersControllerTests()
    {
        _mockUow = new Mock<IUnitOfWork>();
        _mockOrderRepository = new Mock<IOrderRepository>();
        _mockCartRepository = new Mock<ICartRepository>();

        // Setting up IUnitOfWork to use mocked repositories
        _mockUow.Setup(u => u.OrderRepository).Returns(_mockOrderRepository.Object);
        _mockUow.Setup(u => u.CartRepository).Returns(_mockCartRepository.Object);

        _controller = new OrdersController(_mockUow.Object);

        // Mocking HttpContext and setting User with a default user ID of 1
        var mockHttpContext = new Mock<HttpContext>();
        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "1") };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        mockHttpContext.Setup(c => c.User).Returns(claimsPrincipal);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = mockHttpContext.Object
        };
    }

    [Fact]
    public async Task AddToOrder_ReturnsOk_WhenItemAddedSuccessfully()
    {
        // Arrange
        var productId = 1;
        var count = 2;
        var price = 50.0;
        var cartItems = new List<CartItemDto>
        {
            new CartItemDto { ProductId = productId, Price = price * count, Count = count }
        };

        _mockCartRepository.Setup(r => r.AddToCart(It.IsAny<int>(), productId, count, price))
            .ReturnsAsync(cartItems[0]);
        _mockUow.Setup(u => u.SaveChanges()).ReturnsAsync(true);
        _mockCartRepository.Setup(r => r.GetCart(It.IsAny<int>())).ReturnsAsync(cartItems);

        // Act
        var result = await _controller.AddToOrder(productId, count, price) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var returnedItems = Assert.IsAssignableFrom<List<CartItemDto>>(result.Value);
        Assert.Single(returnedItems);
        Assert.Equal(productId, returnedItems[0].ProductId);
        Assert.Equal(price * count, returnedItems[0].Price);
    }

    [Fact]
    public async Task AddToOrder_ReturnsBadRequest_WhenInputsAreInvalid()
    {
        // Act
        var result = await _controller.AddToOrder(0, 0, 0) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Invalid Inputs", result.Value);
    }

}
