using System;
using System.Collections.Generic;
using System.Security.Claims;
using Moq;
using ShoppingCart.Api.Extensions;
using Xunit;

namespace ShoppingCart.Api.Tests.Extensions;

public class AuthExtensionsTests
{
    [Fact]
    public void GetUserName_ReturnsUserName_WhenClaimExists()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "TestUser")
        };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));

        // Act
        var userName = claimsPrincipal.GetUserName();

        // Assert
        Assert.Equal("TestUser", userName);
    }

    [Fact]
    public void GetUserName_ReturnsNull_WhenClaimDoesNotExist()
    {
        // Arrange
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var userName = claimsPrincipal.GetUserName();

        // Assert
        Assert.Null(userName);
    }

    [Fact]
    public void GetUserId_ReturnsUserId_WhenClaimExists()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "123")
        };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));

        // Act
        var userId = claimsPrincipal.GetUserId();

        // Assert
        Assert.Equal(123, userId);
    }

    [Fact]
    public void GetUserId_ReturnsZero_WhenClaimDoesNotExist()
    {
        // Arrange
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

        // Act
        var userId = claimsPrincipal.GetUserId();

        // Assert
        Assert.Equal(0, userId);
    }
}
