using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.TrainerInvitations
{
    public  class TrainerInvitationErrors
    {
        public static readonly Error Expired =
            Error.Conflict(
                code: "TrainerInvitation.Expired",
                description: "The invitation has expired.");

        public static readonly Error InvalidState =
            Error.Conflict(
                code: "TrainerInvitation.InvalidState",
                description: "Invitation invalid state.");

        public static readonly Error AlreadyInvited =
            Error.Conflict(
                code: "TrainerInvitation.AlreadyInvited",
                description: "trainer already invited.");

    }
}
