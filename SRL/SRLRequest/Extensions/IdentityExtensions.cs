using IdentityModel;
using System.Security.Claims;

namespace IT.DigitalCompany.Extensions
{
    public static class IdentityExtensions
    {
        internal static ICollection<String> GettWellknowUserClaims()
        {
            return new[] {
                JwtClaimTypes.Name,
                JwtClaimTypes.Email,
                JwtClaimTypes.Role
            };
        }

        internal static String? GetEmail(ClaimsPrincipal principal)
        {
            return principal?.FindFirstValue(JwtClaimTypes.Email);
        }

    }
}
