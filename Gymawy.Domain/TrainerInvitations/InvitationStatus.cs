using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.TrainerInvitations
{
    public class InvitationStatus : SmartEnum<InvitationStatus>
    {
        public static readonly InvitationStatus Pending = new(nameof(Pending), 0);
        public static readonly InvitationStatus Accepted = new(nameof(Accepted), 1);
        public static readonly InvitationStatus Rejected = new(nameof(Rejected), 2);
        public static readonly InvitationStatus Expired = new(nameof(Rejected), 3);


        public InvitationStatus(string name, int value) 
            : base(name, value)
        {
        }
    }
}
