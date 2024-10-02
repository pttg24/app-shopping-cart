using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingCart.Api.Controllers;
using ShoppingCart.Contracts.Dtos;
using ShoppingCart.Infrastructure;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Api.Tests.Controllers;

public class ProductsControllerTests
{
    private readonly ProductsController _controller;
    private readonly Mock<IUnitOfWork> _mockUow;
    private readonly Mock<IProductRepository> _mockProductRepository;

    public ProductsControllerTests()
    {
        _mockUow = new Mock<IUnitOfWork>();
        _mockProductRepository = new Mock<IProductRepository>();

        // Setup IUnitOfWork to return mocked ProductRepository
        _mockUow.Setup(u => u.ProductRepository).Returns(_mockProductRepository.Object);

        _controller = new ProductsController(_mockUow.Object);
    }

    [Fact]
    public async Task GetAllProducts_ReturnsOk_WhenProductsExist()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Product 1", Amount = 10.0 },
            new ProductDto { Id = 2, Name = "Product 2", Amount = 15.0 }
        };

        _mockProductRepository.Setup(r => r.GetAllProducts()).ReturnsAsync(products);

        // Act
        var result = await _controller.GetAllProducts() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var returnedProducts = Assert.IsAssignableFrom<List<ProductDto>>(result.Value);
        Assert.Equal(2, returnedProducts.Count);
    }

    [Fact]
    public async Task GetAllProducts_ReturnsBadRequest_WhenNoProductsExist()
    {
        // Arrange
        var products = new List<ProductDto>(); // Empty list to simulate no products

        _mockProductRepository.Setup(r => r.GetAllProducts()).ReturnsAsync(products);

        // Act
        var result = await _controller.GetAllProducts() as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Products not found!", result.Value);
    }
}
