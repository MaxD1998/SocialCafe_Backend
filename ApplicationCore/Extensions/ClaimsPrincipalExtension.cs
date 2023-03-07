using System.Security.Claims;

namespace ApplicationCore.Extensions;

public static class ClaimsPrincipalExtension
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (Guid.TryParse(id, out var userId))
            return userId;

        return default;
    }
}