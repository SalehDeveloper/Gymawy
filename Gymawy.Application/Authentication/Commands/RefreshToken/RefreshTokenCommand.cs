using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Authentication.Commands.RefreshToken
{
    public record RefreshTokenCommand():IRequest<ErrorOr<string>>;
    
    
}
