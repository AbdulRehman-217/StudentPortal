using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace StudenPortal.Utilities
{
    public static class CommonMethods
    {
        public static string GetClaim(string claimType, string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            if (token != null)
            {
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (securityToken != null)
                {
                    var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
                    return stringClaimValue;
                }
            }

            return null;
        }

        public static string GetUniqueIdentifier()
        {
            return Guid.NewGuid().ToString("N");
        }

    }
}
