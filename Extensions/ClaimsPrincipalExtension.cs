using System.Security.Claims;

namespace HCWeb.NET.Extensions;

public static class ClaimsPrincipalExtension
{
    /// <summary>
    /// Returns true if user authenticated.
    /// </summary>
    /// <returns>bool</returns>
    public static bool IsAuthenticated(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Identity is { IsAuthenticated: true };
    }
    
    /// <summary>
    /// Return authenticated user email or empty string if user not authenticated.
    /// </summary>
    /// <returns>string</returns>
    public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
    {
        var email = claimsPrincipal.Identity?.Name;
        return email ?? "";
    }
}