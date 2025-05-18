using Gymawy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Abstractions.Auth
{
    public interface IJwtService
    {
        public string AccessTokenCookieName { get; }
        public string RefreshTokenCookieName { get; }

        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken();
        public void SetAccessTokenInCookies(string acessToken);
        public void SetRefreshTokenInCookies (string refreshToken);


    }
}
