﻿using System.Security.Claims;

namespace ShoppingCart.Api.Extensions;

public static class AuthExtensions
{
    public static string GetUserName(this ClaimsPrincipal claims)
    {
        return claims.FindFirst(ClaimTypes.Name)?.Value;
    }

    public static int GetUserId(this ClaimsPrincipal claims)
    {
        return Convert.ToInt32(claims.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
