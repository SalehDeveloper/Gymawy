using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Authentication
{
    public static class ClaimsPrincipalsExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal? claims)
        {

            var userId = claims?.FindFirstValue("userId");

            return Guid.TryParse(userId , out Guid parsedUserId)?
                    parsedUserId :
                     throw new ApplicationException("User id is unavailable");
        } 

    }
}
