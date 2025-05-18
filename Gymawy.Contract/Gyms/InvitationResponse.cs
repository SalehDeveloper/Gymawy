using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Contract.Gyms
{
    public record InvitationResponse(Guid Id , Guid GymId , Guid TrainerId ,InvitationRespond Responsde , string message );


    public enum InvitationRespond
    {
        Rejected = 0,
        Accepted = 1,
        Pending =2

    }
}
