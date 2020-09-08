namespace Template.Helpers.Extensions
{
    using System.Linq;
    using System.Security.Claims;

    public static class ClaimsIdentityExtensions
    {
        public static bool HasSpecificClaim(this ClaimsIdentity identity, string claimName)
        {
            return identity.Claims.Any(x => x.Value == claimName && x.Type == ClaimTypes.Role);
        }
    }
}