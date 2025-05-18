using Gymawy.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Authorization
{
    public class CustomeClaimsTransformation : IClaimsTransformation
    { 
        private readonly IServiceProvider _serviceProvider;

        public CustomeClaimsTransformation(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity is not { IsAuthenticated: true } ||
                principal.HasClaim(claim => claim.Type == ClaimTypes.Role) && 
                principal.HasClaim(claim => claim.Type == JwtRegisteredClaimNames.Sub))
            {
                return principal;
            }

            using IServiceScope scope = _serviceProvider.CreateScope();

            var authorizationService = scope.ServiceProvider.GetRequiredService<AuthorizationService>();

            var userId = principal.GetUserId();

            UserRoleResponse userRole = await authorizationService.GetRoleForUserAsync(userId);

            var claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, userRole.UserId.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, userRole.RoleName));

            principal.AddIdentity(claimsIdentity);

            return principal;
        }
    }
}
