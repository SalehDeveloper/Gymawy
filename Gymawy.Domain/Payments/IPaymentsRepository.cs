using Gymawy.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.Payments
{
    public interface IPaymentsRepository : IBaseRepository<Payment>
    {
    }
}
