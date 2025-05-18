using ErrorOr;
using Gymawy.Domain.TrainerInvitations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Trainers.Commands.RespondToInvitation
{
    public record RespondToInvitationCommand(Guid InvitaionId , InvitationRespond Responde)
        :IRequest<ErrorOr<TrainerInvitaion>>;
    
}
