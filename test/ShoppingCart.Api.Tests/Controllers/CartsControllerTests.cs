using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingCart.Api.Controllers;
using ShoppingCart.Infrastructure;
using ShoppingCart.Infrastructure.Interfaces;
using ShoppingCart.Contracts.Dtos;
using System.Security.Claims;

namespace ShoppingCart.Api.Tests.Controllers;

public class CartsControllerTests
{
    private readonly CartsController _controller;
    private readonly Mock<IUnitOfWork> _mockUow;
    private readonly Mock<ICartRepository> _mockCartRepository;

    public CartsControllerTests()
    {
        _mockUow = new Mock<IUnitOfWork>();
        _mockCartRepository = new Mock<ICartRepository>();

        // Setting up IUnitOfWork to use the mocked ICartRepository
        _mockUow.Setup(u => u.CartRepository).Returns(_mockCartRepository.Object);

        _controller = new CartsController(_mockUow.Object);

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
    public async Task AddToCart_ReturnsOk_WhenItemAddedSuccessfully()
    {
        // Arrange
        var productId = 1;
        var count = 2;
        var price = 5.0;
        var cartItems = new List<CartItemDto>
        {
            new CartItemDto { ProductId = productId, Price = price * count, Count = count }
        };

        _mockCartRepository.Setup(r => r.AddToCart(It.IsAny<int>(), productId, count, price))
            .ReturnsAsync(cartItems[0]);
        _mockUow.Setup(u => u.SaveChanges()).ReturnsAsync(true);
        _mockCartRepository.Setup(r => r.GetCart(It.IsAny<int>())).ReturnsAsync(cartItems);

        // Act
        var result = await _controller.AddToCart(productId, count, price) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var returnedItems = Assert.IsAssignableFrom<List<CartItemDto>>(result.Value);
        Assert.Single(returnedItems);
        Assert.Equal(productId, returnedItems[0].ProductId);
        Assert.Equal(price * count, returnedItems[0].Price);
    }

    [Fact]
    public async Task AddToCart_ReturnsBadRequest_WhenInputsAreInvalid()
    {
        // Act
        var result = await _controller.AddToCart(0, 0, 0) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Invalid Inputs", result.Value);
    }

    [Fact]
    public async Task GetCartByUserId_ReturnsOk_WhenCartExists()
    {
        // Arrange
        var cartItems = new List<CartItemDto>
        {
            new CartItemDto { ProductId = 1, Price = 10.0, Count = 2 }
        };

        _mockCartRepository.Setup(r => r.GetCart(It.IsAny<int>())).ReturnsAsync(cartItems);

        // Act
        var result = await _controller.GetCartByUserId() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var returnedItems = Assert.IsAssignableFrom<List<CartItemDto>>(result.Value);
        Assert.Single(returnedItems);
    }

    [Fact]
    public async Task GetCartById_ReturnsOk_WhenCartExists()
    {
        // Arrange
        var cartItems = new List<CartItemDto>
        {
            new CartItemDto { ProductId = 1, Price = 10.0, Count = 2 }
        };

        _mockCartRepository.Setup(r => r.GetCartById(It.IsAny<int>())).ReturnsAsync(cartItems);

        // Act
        var result = await _controller.GetCartById(1) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var returnedItems = Assert.IsAssignableFrom<List<CartItemDto>>(result.Value);
        Assert.Single(returnedItems);
    }

    [Fact]
    public async Task RemoveFromCart_ReturnsOk_WhenItemRemovedSuccessfully()
    {
        // Arrange
        var cartItemId = 1;
        _mockCartRepository.Setup(r => r.RemoveFromCart(It.IsAny<int>(), cartItemId)).ReturnsAsync(true);
        _mockUow.Setup(u => u.SaveChanges()).ReturnsAsync(true);

        // Act
        var result = await _controller.RemoveFromCart(cartItemId) as OkResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task RemoveFromCart_ReturnsBadRequest_WhenRemoveFails()
    {
        // Arrange
        var cartItemId = 1;
        _mockCartRepository.Setup(r => r.RemoveFromCart(It.IsAny<int>(), cartItemId)).ReturnsAsync(true);
        _mockUow.Setup(u => u.SaveChanges()).ReturnsAsync(false);

        // Act
        var result = await _controller.RemoveFromCart(cartItemId) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Failed to remove.", result.Value);
    }
}
