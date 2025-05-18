using ErrorOr;
using Gymawy.Api.Mappers;
using Gymawy.Application.Trainers.Commands.RespondToInvitation;
using Gymawy.Contract.Gyms;
using Gymawy.Contract.Invitations;
using Gymawy.Domain.TrainerInvitations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Gymawy.Api.ApiEndpoints;

namespace Gymawy.Api.Controllers
{
    public class InvitationsController:ApiController
    {
        private readonly ISender _sender;

        public InvitationsController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Roles =Roles.Trainer)]
        [HttpPost(ApiEndpoints.Invitation.Respond)]
        public async Task<IActionResult> Respond ([FromRoute] Guid invitationId ,[FromBody ] RespondToInvitationRequest request)
        {
            if (!Application.Trainers.Commands.RespondToInvitation.InvitationRespond.TryFromName(
                 request.Status,
                 out var respondStatus)
                  )
            {
                return Problem(Error.Validation(description: "invalid respond status"));
            }


            string message = respondStatus.Name switch
            {
               nameof ( Application.Trainers.Commands.RespondToInvitation.InvitationRespond.Accept ) => "Invitation accepted. Trainer assigned to the gym successfully.",
               nameof(Application.Trainers.Commands.RespondToInvitation.InvitationRespond.Reject) => "Invitation rejected successfully."
            };

           


            var commnd = new RespondToInvitationCommand(invitationId, respondStatus);

            var result = await _sender.Send(commnd);



            return result.Match(
               v => base.Ok(result.Value.MapToResponse("")) , Problem);

        }

    }
}
