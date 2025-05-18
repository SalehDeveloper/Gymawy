using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Contract.Authentication
{
    public record LoginRequest(
        string Email , 
        string password);
    
    
}
