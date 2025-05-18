using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Contract.Gyms
{
    public record GymResponse(Guid GymId  , string Name , string MaxRooms);
    
    
}
