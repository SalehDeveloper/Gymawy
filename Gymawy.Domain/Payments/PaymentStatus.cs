using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Payments
{
    public class PaymentStatus : SmartEnum<PaymentStatus>
    {
        public static readonly PaymentStatus Pending = new (nameof(Pending) , 0);
        public static readonly PaymentStatus Paid = new(nameof(Paid), 1);
        public static readonly PaymentStatus Failed = new(nameof(Failed), 2);



        public PaymentStatus(string name, int value)
            : base(name, value)
        {
        }
    }
}
