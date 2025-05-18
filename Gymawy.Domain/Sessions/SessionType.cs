using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Sessions
{
    public class SessionType : SmartEnum<SessionType>
    {
        public static readonly SessionType Kickboxing = new(nameof(Kickboxing), 0);
        public static readonly SessionType Functional = new(nameof(Functional), 1);
        public static readonly SessionType Zoomba = new(nameof(Zoomba), 2);
        public static readonly SessionType Pilates = new(nameof(Pilates), 3);
        public static readonly SessionType Yoga = new(nameof(Yoga), 4);
        public SessionType(string name, int value) : base(name, value)
        {
        }
    }
}
