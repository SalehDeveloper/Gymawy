using Ardalis.SmartEnum;

namespace Gymawy.Application.Trainers.Commands.RespondToInvitation
{
    public class InvitationRespond : SmartEnum<InvitationRespond>
    {
        public static readonly InvitationRespond Reject = new(nameof(Reject), 0);
        public static readonly InvitationRespond Accept = new(nameof(Accept), 1);
       
        public InvitationRespond(string name, int value) : base(name, value)
        {
        }
    }

}
