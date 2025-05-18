using ErrorOr;
using Gymawy.Domain.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand (Guid GymId , string Name )
        :IRequest<ErrorOr<Room>>;
    
    
}
