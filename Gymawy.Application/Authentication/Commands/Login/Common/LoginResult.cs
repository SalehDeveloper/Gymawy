using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Authentication.Commands.Login.Common
{
    public record LoginResult
        (
         Guid UserId , 
         string Email , 
         string FullName , 
         string PhotoUrl,
         string ProfileType
        );
    
    
}
