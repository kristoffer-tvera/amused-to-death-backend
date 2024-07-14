using System.Security.Claims;

namespace AmusedToDeath.Backend.Services;

public static class ClaimsExtensions
{
    public static string? GetBattleTag(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Identity?.Name;
    }

    public static int? GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var sid = claimsPrincipal.FindFirstValue(ClaimTypes.Sid);
        return sid != null ? int.Parse(sid) : null;
    }

    public static string? GetAccessToken(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue("access_token");
    }
}