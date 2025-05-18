using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Authentication.Commands.Login.Common
{
    public static  class AuthenticaionErrors 
    {
        public static readonly Error InvalidCredentials = Error.Unauthorized(
             code: "Auth.InvalidCredentials",
             description: "Invalid email or password.");


        public static readonly Error InvalidRefreshToken = Error.Unauthorized(
             code: "Auth.InvalidRefreshToken",
             description: "Invalid RefreshToken.");

    }

}
