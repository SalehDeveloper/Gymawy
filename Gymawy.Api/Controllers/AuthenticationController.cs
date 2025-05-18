using ErrorOr;
using Gymawy.Api.Mappers;
using Gymawy.Application.Authentication.Commands;
using Gymawy.Application.Authentication.Commands.ConfirmEmail;
using Gymawy.Application.Authentication.Commands.Login;
using Gymawy.Application.Authentication.Commands.RefreshToken;
using Gymawy.Application.Authentication.Commands.RegisterUser;
using Gymawy.Application.Authentication.Commands.ResendEmailCode;
using Gymawy.Contract.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Gymawy.Api.Controllers
{
   
    public class AuthenticationController : ApiController
    {
        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost(ApiEndpoints.Authenation.Regiser)]
        [ProducesResponseType( typeof(string) , StatusCodes.Status200OK )]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser ([FromForm] RegisterUserRequest request , CancellationToken cancellationToken = default)
        {
            var command = new RegisterUserCommand(request.FullName, request.Email, request.Password, request.Photo);

           var result = await _sender.Send(command , cancellationToken);

            return result.Match(
                res => base.Ok(result.Value),
                Problem);

                


        }
        
        [HttpPost(ApiEndpoints.Authenation.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request , CancellationToken cancellationToken =default)
        {
            var command = new ConfirmEmailCommand(request.email, request.code);

            var result =  await _sender.Send(command , cancellationToken);

           return result.Match( 
                res => base.Ok(result.Value), 
                Problem);


        }

      
        
        
        [HttpPost(ApiEndpoints.Authenation.ResendEmailCode)]
        public async Task<IActionResult> ResendEmailCode([FromBody] string email, CancellationToken cancellationToken = default)
        {
            var command= new ResendEmailCodeCommand(email);
            var result = await _sender.Send(command, cancellationToken);

           return  result.Match(
                res => base.Ok(result.Value) , 
                Problem);   
        }

        [HttpPost(ApiEndpoints.Authenation.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request , CancellationToken cancellationToken =default)
        {
            var command = new LoginCommand(request.Email, request.password);

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(
                res => base.Ok(result.Value.MapToLoginResponse()),
                Problem);


        }

        [HttpPost(ApiEndpoints.Authenation.RefreshToken)]
        public async Task<IActionResult> RefreshToken (CancellationToken  cancellationToken = default)
        {
            var command = new RefreshTokenCommand();

            var result = await _sender.Send(command, cancellationToken);

          return  result.Match(
                res => base.Ok(result.Value),
                Problem);
        }

    }
}
