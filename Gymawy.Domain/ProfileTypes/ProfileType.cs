using Ardalis.SmartEnum;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.ProfileTypes
{
    public class ProfileType : SmartEnum<ProfileType>
    {
        public static readonly ProfileType Admin = new(nameof(Admin), 0);
        public static readonly ProfileType Trainer = new(nameof(Trainer), 1);
        public static readonly ProfileType Participant = new(nameof(Participant), 2);


        public ProfileType(string name, int value) : base(name, value)
        {
        }
    }
}
