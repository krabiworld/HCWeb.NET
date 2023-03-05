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
}
