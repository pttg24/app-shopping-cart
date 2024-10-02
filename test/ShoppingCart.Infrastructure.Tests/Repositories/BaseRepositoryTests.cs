using Moq;
using ShoppingCart.Infrastructure.Repositories;
using AutoMapper;

namespace ShoppingCart.Infrastructure.Tests.Repositories;

public class BaseRepositoryTests
{
    private readonly Mock<ShoppingCartContext> _mockDbContext;
    private readonly Mock<IMapper> _mockMapper;
    private readonly BaseRepository _repository;

    public BaseRepositoryTests()
    {
        _mockDbContext = new Mock<ShoppingCartContext>();
        _mockMapper = new Mock<IMapper>();
        _repository = new BaseRepository(_mockDbContext.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task SaveChanges_ReturnsTrue_WhenSaveIsSuccessful()
    {
        // Arrange
        _mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _repository.SaveChanges();

        // Assert
        Assert.True(result);
        _mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SaveChanges_ReturnsFalse_WhenNoChangesAreSaved()
    {
        // Arrange
        _mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

        // Act
        var result = await _repository.SaveChanges();

        // Assert
        Assert.False(result);
        _mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
