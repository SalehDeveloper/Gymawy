using Gymawy.Api.Mappers;
using Gymawy.Application.Participants.Commands.SpotSession;
using Gymawy.Application.Sessions.Commands.CreateSession;
using Gymawy.Contract.Sessions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymawy.Api.Controllers
{
    public class SessionsController : ApiController
    {
        private readonly ISender _sender;

        public SessionsController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Roles=Roles.Admin)]
        [HttpPost(ApiEndpoints.Room.AddSession)]
        public async Task<IActionResult> Create( [FromRoute] Guid roomId,[FromBody] CreateSessionRequest request , CancellationToken cancellationToken )
        {
            if (!Domain.Sessions.SessionType.TryFromName(
           request.Type,
           out var subscriptionType))
            {
                return Problem("Invalid session type", statusCode: StatusCodes.Status400BadRequest);
            }

            var command = new CreateSessionCommand(
                roomId,
                request.TrainerId,
                subscriptionType,
                request.Description,
                request.MaxParticipants,
                request.Date,
                request.StartTime,
                request.EndTime, 
                request.SessionFee);

            var result = await _sender.Send(command , cancellationToken);

            return result.Match(
                v => base.Ok(result.Value.MapToResponse()),
                Problem);
        }


        [Authorize(Roles=Roles.Participant)]
        [HttpPost(ApiEndpoints.Session.Spot)]
        public async Task<IActionResult> SpotSessoion([FromRoute] Guid sessionId ,  CancellationToken cancellationToken)
        {
            var command = new SpotSessionCommand(sessionId);

            var result = await _sender.Send(command, cancellationToken);

          return  result.Match(
                v => base.Ok(result.Value.MapToResponse()),
                Problem);
        }
    }
}
