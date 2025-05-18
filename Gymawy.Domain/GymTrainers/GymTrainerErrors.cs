using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.GymTrainers
{
    public static class GymTrainerErrors
    {

        public static readonly Error AlreadyAssigned =
            Error.Conflict(
                code: "GymTrainer.AlreadyAssigned",
                description: "trainer already assigned.");
    }
}
