using System.Security.Claims;

namespace Api.Extensions;

public static class ClaimsPrincipalExtension
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (int.TryParse(id, out var userId))
            return userId;

        return default;
    }
}