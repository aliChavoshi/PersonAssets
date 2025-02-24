using System.Security.Claims;

namespace PersonAssets.Extension;

public static class ClaimsPrincipleExtension
{
    public static string GetId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
    }

    public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
    }
}