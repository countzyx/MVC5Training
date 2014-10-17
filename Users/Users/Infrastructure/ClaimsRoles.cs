using System.Collections.Generic;
using System.Security.Claims;

namespace Users.Infrastructure {
    public class ClaimsRoles {
        public static IEnumerable<Claim> CreateRolesFromClaims(ClaimsIdentity user) {
            var claims = new List<Claim>();
            if (user.HasClaim(c => c.Type == ClaimTypes.StateOrProvince && c.Issuer == "RemoteClaims" && c.Value == "DC")
                && user.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Employees")) {
                claims.Add(new Claim(ClaimTypes.Role, "DCStaff"));
            }

            return claims;
        }
    }
}