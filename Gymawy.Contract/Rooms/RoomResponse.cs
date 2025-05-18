using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Contract.Rooms
{
    public record RoomResponse (Guid RoomId , Guid GymId , string Name , string MaxDailySessions );
    
    
}
