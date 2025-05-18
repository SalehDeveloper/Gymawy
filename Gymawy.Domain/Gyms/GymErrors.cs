using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Gyms
{
    public class GymErrors
    {

        public static readonly Error MaxRoomLimitReached = Error.Validation(
        code: "Gym.MaxLimit",
        description: "The maximum number of rooms per gym allowed by your subscription has been reached."
          );

        public static readonly Error TrainerAlreadyAssigned = Error.Conflict(
        code: "Gym.TrainerAlreadyAssigned",
        description: "This trainer is already assigned to the gym."
          );


    }
}