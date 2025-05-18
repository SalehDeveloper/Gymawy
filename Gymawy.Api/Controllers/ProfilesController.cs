using Gymawy.Api.Mappers;
using Gymawy.Application.Profiles.Commands.CreateAdminProfile;
using Gymawy.Application.Profiles.Commands.CreateParticipantProfile;
using Gymawy.Application.Profiles.Commands.CreateStripeAccount;
using Gymawy.Application.Profiles.Commands.CreateTrainerProfile;
using Gymawy.Application.Profiles.Queries.GetProfileByEmail;
using Gymawy.Contract.Profiles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymawy.Api.Controllers
{
    public class ProfilesController:ApiController
    {
        private readonly ISender _sender;

        public ProfilesController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpPost(ApiEndpoints.Profile.CreateAdminProfile)]
        [ProducesResponseType(typeof(Guid) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateAdminProfile(CancellationToken cancellationToken = default)
        {
            var command = new CreateAdminProfileCommand();  

           var res =  await _sender.Send(command, cancellationToken);

            return res.Match(
                v => base.Ok(res.Value),
                Problem);

        }


        [Authorize]
        [HttpPost(ApiEndpoints.Profile.CreatePartitipantProfile)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreatePartitipantProfile(CancellationToken cancellationToken = default)
        {
            var command = new CreateParticipantCommand();

            var res = await _sender.Send(command , cancellationToken );

            return res.Match(
                v => base.Ok(res.Value),
                Problem);

        }


        [Authorize]
        [HttpPost(ApiEndpoints.Profile.CreateTrainerProfile)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateTrainerProfile([FromBody]CreateTrainerRequest request , CancellationToken cancellationToken = default)
        {
            var command = new CreateTrainerProfileCommand(request.Specialty, request.Certification, request.Bio);

            var res = await _sender.Send(command  , cancellationToken);
            
            return res.Match(
                v => base.Ok(res.Value) , 
                Problem);   
        }

        [HttpGet(ApiEndpoints.Profile.GetByEmail)]
        public async Task<IActionResult> GetByEmail (string email)
        {
            var query = new GetProfileByEmailQuery(email);
        
            var result = await _sender.Send(query); 

          return  result.Match(
                v=> base.Ok(result.Value.MapToProfileResponse()) ,
                Problem);
        }

        [Authorize(Roles =  Roles.Admin)]
        [HttpPost(ApiEndpoints.Profile.CreateStripeAccount)]
        public async Task<IActionResult> CreateStripeAccount(CancellationToken cancellationToken = default)
        {
            var command = new CreateStripeAccountCommand();

            var result = await _sender.Send(command, cancellationToken);

           return result.Match(
                v => base.Ok(result.Value),
                Problem);
        }



    }
}
