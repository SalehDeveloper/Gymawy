
using Azure;
using Azure.Core;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Domain.ProfileTypes;
using Gymawy.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Gymawy.Infrastructure.Authentication
{
    public class JwtService : IJwtService
    {
        private const string AccessTokenCookieName = "accessToken";
        private const string RefreshTokenCookieName = "refreshToken";

        private readonly AuthenticationOptions _options;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtService(IOptions<AuthenticationOptions> options, IDateTimeProvider dateTimeProvider, IHttpContextAccessor httpContextAccessor)
        {
            _options = options.Value;
            _dateTimeProvider = dateTimeProvider;
            _httpContextAccessor = httpContextAccessor;
        }

         string IJwtService.AccessTokenCookieName => AccessTokenCookieName;

        string IJwtService.RefreshTokenCookieName => RefreshTokenCookieName;

        public string GenerateAccessToken(User user)
        {
            var roleType = user.GetProfileType().Name;



            var claims = new List<Claim>
            {

                new Claim(JwtRegisteredClaimNames.Sub , user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email , user.Email),
                new Claim("userId" , user.Id.ToString())

            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey ,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_options.AccessTokenDurationInMinutes),
                signingCredentials: signingCredentials
                );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public void SetAccessTokenInCookies(string accessToken)
        {
            var accessTokenOptions = new CookieOptions
            {

                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = _dateTimeProvider.UtcNow.AddMinutes(_options.AccessTokenDurationInMinutes)
            };
            
          var context = _httpContextAccessor.HttpContext;
            if (context is null)
                throw new InvalidOperationException("HttpContext is not available.");
            context.Response.Cookies.Append(AccessTokenCookieName, accessToken, accessTokenOptions);

        }

        public void SetRefreshTokenInCookies(string refreshToken)
        {
            var refreshTokenOptions = new CookieOptions
            {

                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = _dateTimeProvider.UtcNow.AddDays(_options.RefreshTokenDurationInDays)
            };

            var context = _httpContextAccessor.HttpContext;
            if (context is null)
                throw new InvalidOperationException("HttpContext is not available.");

            context.Response.Cookies.Append(RefreshTokenCookieName, refreshToken, refreshTokenOptions);
        }
    }
}
