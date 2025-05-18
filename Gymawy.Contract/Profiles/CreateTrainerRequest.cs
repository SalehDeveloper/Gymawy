using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Contract.Profiles
{
    public record CreateTrainerRequest(
            string Specialty,
            string Certification,
            string Bio
        );
    
    
}
