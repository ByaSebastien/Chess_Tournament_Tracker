using System.Security.Claims;

namespace Chess_Tournament_Tracker.API.Extensions
{
    public static class ExtendedUser
    {
        public static Guid GetId(this ClaimsPrincipal claims)
        {
            string? id = claims.FindFirst(ClaimTypes.Sid)?.Value;
            return id is not null ? new Guid(id) : Guid.Empty;
        }
    }
}
