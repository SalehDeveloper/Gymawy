using Ardalis.SmartEnum;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gymawy.Domain.Rooms
{
    public class RoomErrors :SmartEnum<RoomErrors>
    {

        public static readonly Error MaxSessionLimitReached = Error.Validation(
        code: "Room.MaxLimit",
        description: "The maximum number of sessions inside this room has been reached."
          );

        public static readonly Error OverlappedSession = Error.Validation(
            code: "Room.OverlappedSession",
            description: "The session overlaps with an existing session in this room."
        );



        public RoomErrors(string name, int value) 
            : base(name, value)
        {
        }
    }
}
