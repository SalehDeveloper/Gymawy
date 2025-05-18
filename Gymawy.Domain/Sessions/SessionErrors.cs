using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Sessions
{
    public class SessionErrors
    {
        public static readonly Error NotFound = Error.Validation
            (
               code: "Session.NotFound",
               description: "session not found"
            );

        public static readonly Error Full = Error.Validation
            (
               code: "Session.Full",
               description: "session is full"
            );
    }
}
