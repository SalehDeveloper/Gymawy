using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Contract.Authentication
{
    public record RegisterUserRequest(
        string FullName , 
        string Email , 
        string Password , 
        IFormFile Photo);
    
}
