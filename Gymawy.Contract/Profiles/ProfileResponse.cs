using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Contract.Profiles
{
    public record ProfileResponse(Guid UserId , string FullName , string Email ,ProfileType Type , bool Active);
    

    
}
