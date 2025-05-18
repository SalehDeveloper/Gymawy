using Gymawy.Domain.Subscriptions;
using Gymawy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Repositories
{
    internal class SubscriptionsRepository : BaseRepository<Subscription>, ISubscriptionsRepository
    {
        public SubscriptionsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
