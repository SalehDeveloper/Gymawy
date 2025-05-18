using ErrorOr;
using Gymawy.Domain.Sessions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Sessions.Commands.CreateSession
{
    public record CreateSessionCommand(
        Guid RoomId , 
        Guid TrainerId , 
        SessionType Type ,
        string Description , 
        int MaxParticipants,
        DateOnly Date , 
        TimeOnly StartTime  , 
        TimeOnly EndTime  , 
        decimal SessionFee 
        ):IRequest<ErrorOr<Session>>;
    
    
}
