using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Contract.Sessions
{
    public record SessionResponse(
        Guid SessionId , 
        Guid RoomId , 
        Guid TrainerId , 
        SessionType Type , 
        string Description , 
        DateOnly Date , 
        TimeOnly StartTime , 
        TimeOnly EndTime,
        decimal SessionFee,
        SessionStatus Status);
    
        
    
}
