using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Trainers
{
    public  class TrainerErrors
    {
        public static readonly Error NotFound = Error.NotFound(
            code:"Trainer.NotFound" ,
            description:"trainer not found");

        public static readonly Error OverlappedSession = Error.Conflict(
          code: "Trainer.OverlappedSession",
          description: "trainer sessions overlapped with this session");
    }
}
