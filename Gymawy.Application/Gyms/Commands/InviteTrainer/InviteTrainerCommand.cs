using ErrorOr;
using Gymawy.Domain.TrainerInvitations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Gyms.Commands.InviteTrainer
{
    public record InviteTrainerCommand(
        Guid GymId,
        Guid TrainerId):IRequest<ErrorOr<TrainerInvitaion>>;
    
    
}
