using Gymawy.Application.Abstractions.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Authentication
{
    public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
    {
       private readonly AuthenticationOptions _options;

        public JwtBearerOptionsSetup(IOptions< AuthenticationOptions> options)
        {
            _options = options.Value;
        }

        public void Configure(JwtBearerOptions options)
        {
            options.Audience = _options.Audience;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = _options.Issuer,
                ValidateIssuer = true,

                ValidAudience = _options.Audience,
                ValidateAudience = true,

                ValidateLifetime = true,
                RequireExpirationTime = true,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
                ValidateIssuerSigningKey = true,

                RoleClaimType = ClaimTypes.Role



            };

            options.Events = new JwtBearerEvents
            {

                OnMessageReceived = context =>
                {

                    var accessToken = context.Request.Cookies["accessToken"];
                    if (!string.IsNullOrEmpty(accessToken))
                        context.Token = accessToken; 

                    return Task.CompletedTask;
                }
            };
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            Configure(options);
        }
    }
}
