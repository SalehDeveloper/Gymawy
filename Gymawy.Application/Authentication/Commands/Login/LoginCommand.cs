using ErrorOr;
using Gymawy.Application.Authentication.Commands.Login.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Authentication.Commands.Login
{
    public record LoginCommand
        (
          string Email , 
          string Password 
        ):IRequest<ErrorOr<LoginResult>>;
    
    
}
