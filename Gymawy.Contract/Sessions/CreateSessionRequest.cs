using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Contract.Sessions
{
     public record  CreateSessionRequest(
         Guid TrainerId,
         int MaxParticipants,
         string Description,
         string Type,
         DateOnly Date,
         TimeOnly StartTime,
         TimeOnly EndTime,
         decimal SessionFee);    
    
}
