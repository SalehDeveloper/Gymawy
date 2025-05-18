using Gymawy.Domain.Payments;
using Gymawy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Repositories
{
    public class PaymentsRepository : BaseRepository<Payment>, IPaymentsRepository
    {
        public PaymentsRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
