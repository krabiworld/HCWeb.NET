using System.Security.Claims;
using HCWeb.NET.Models;

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
    /// Return authenticated user id or -1 if user not authenticated.
    /// </summary>
    /// <returns>int</returns>
    public static int GetId(this ClaimsPrincipal claimsPrincipal)
    {
        var id = claimsPrincipal.Identity?.Name;
        return id == null ? -1 : Convert.ToInt32(id);
    }

    /// <summary>
    /// Returns user object or null if user not authenticated or user not found in database.
    /// </summary>
    /// <param name="claimsPrincipal">The extension</param>
    /// <param name="context">ApplicationContext</param>
    /// <returns>Nullable user</returns>
    public static User? GetUser(this ClaimsPrincipal claimsPrincipal, ApplicationContext context)
    {
        if (!claimsPrincipal.IsAuthenticated()) return null;

        var id = claimsPrincipal.GetId();
        if (id == -1) return null;

        var user = context.Users.Find(id);
        return user;
    }
}