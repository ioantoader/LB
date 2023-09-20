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

        internal static String? GetEmail(this ClaimsPrincipal principal)
        {
            var i = principal.IsInRole("abc");
            var email = principal?.FindFirstValue(JwtClaimTypes.Email);
            if(String.IsNullOrWhiteSpace(email))
            {
                email = principal?.FindFirstValue(ClaimTypes.Email);
            }
            return email;            

        }

    }
}
