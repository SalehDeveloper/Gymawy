using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace Gymawy.Application.Authentication.Commands.RegisterUser
{
    public record RegisterUserCommand(
        string FullName , 
        string Email  , 
        string Password ,
        IFormFile Photo):IRequest<ErrorOr<string>>;
    
    
}
