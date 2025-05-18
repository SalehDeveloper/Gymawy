using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Participants
{
    public class ParticipantErrors
    {
        public static readonly Error NotFound = Error.NotFound(
            code: "Participant.NotFound",
            description: "participant not found");

        public static readonly Error OverlappedSession = Error.Conflict(
          code: "Participant.OverlappedSession",
          description: "Participant sessions overlapped with this session");
    }
}
