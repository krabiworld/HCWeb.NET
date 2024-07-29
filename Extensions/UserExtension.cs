using System.Security.Claims;

namespace HCWeb.NET.Extensions;

public static class UserExtension
{
    public static string Id(this ClaimsPrincipal user)
    {
        var id = user.FindFirst(ClaimTypes.NameIdentifier);
        if (id == null)
        {
            throw new NullReferenceException();
        }

        return id.Value;
    }
}