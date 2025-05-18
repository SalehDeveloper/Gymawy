using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Participants.Commands.SpotSession
{
    public record SpotSessionCommand (Guid SessionId):IRequest<ErrorOr<SpotSessionResult>>;
   
}
