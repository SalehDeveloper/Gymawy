using Gymawy.Api.Mappers;
using Gymawy.Application.Rooms.Commands.CreateRoom;
using Gymawy.Contract.Gyms;
using Gymawy.Contract.Rooms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymawy.Api.Controllers
{
    public class RoomsController  :ApiController
    {
        private readonly ISender _sender;

        public RoomsController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Roles=Roles.Admin)]
        [HttpPost(ApiEndpoints.Gym.AddRoom)]
        public async Task<IActionResult> CreateRoom([FromRoute] Guid gymId ,[FromBody] CreateRoomRequest request)
        {
            
           var command = new CreateRoomCommand(gymId, request.Name);

            var result = await _sender.Send(command);

          return  result.Match(
                v => base.Ok(result.Value.MapToResponse()) 
                ,
                Problem);
        }
    }
}

