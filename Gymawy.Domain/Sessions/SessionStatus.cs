using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Sessions
{
    public class SessionStatus : SmartEnum<SessionStatus>
    {
      
        public static readonly SessionStatus CommingSoon = new SessionStatus(nameof(CommingSoon) , 0);
        public static readonly SessionStatus Completed = new SessionStatus(nameof(Completed), 1);
        public static readonly SessionStatus Cancelled = new SessionStatus(nameof(Cancelled), 2);

        public SessionStatus(string name, int value) : base(name, value)
        {
        }
    }
}
