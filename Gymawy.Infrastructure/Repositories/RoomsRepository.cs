using Gymawy.Domain.Rooms;
using Gymawy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Repositories
{
    public class RoomsRepository : BaseRepository<Room>, IRoomsRepository
    {
        public RoomsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
