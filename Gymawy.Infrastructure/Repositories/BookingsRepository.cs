using Gymawy.Domain.Bookings;
using Gymawy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Repositories
{
    public class BookingsRepository : BaseRepository<Booking>, IBookingsRepository
    {
        public BookingsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
