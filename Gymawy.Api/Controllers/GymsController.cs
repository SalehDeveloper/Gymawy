using Gymawy.Api.Mappers;

using Gymawy.Application.Gyms.Commands.CreateGym;
using Gymawy.Application.Gyms.Commands.InviteTrainer;
using Gymawy.Contract.Gyms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymawy.Api.Controllers
{

    public class GymsController:ApiController
    {
        private readonly ISender _sender;

        public GymsController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost(ApiEndpoints.Gym.Create)]
        public async Task<IActionResult> CreateGym(CreateGymRequest request , CancellationToken cancellationToken )
        {
            var command = new CreateGymCommand(request.Name);

            var result = await _sender.Send(command , cancellationToken);

            return result.Match(
                v => base.Ok(result.Value.MapToGymResponse()),
                Problem);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost( ApiEndpoints.Gym.InviteTrainer)]
        public async Task<IActionResult> InviteTrainer(Guid gymId  , Guid trainerId , CancellationToken cancellationToken)
        {
            var command = new InviteTrainerCommand(gymId, trainerId);
            var result = await _sender.Send(command, cancellationToken);

            return result.Match(
                v => base.Ok(result.Value.MapToResponse("The trainer invitation has been sent. Awaiting their response.")),
                Problem);
        }
    }
}
