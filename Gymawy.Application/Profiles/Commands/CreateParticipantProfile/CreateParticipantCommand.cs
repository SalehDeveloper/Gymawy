using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Profiles.Commands.CreateParticipantProfile
{
    public record CreateParticipantCommand():IRequest<ErrorOr<Guid>>;
    
}
