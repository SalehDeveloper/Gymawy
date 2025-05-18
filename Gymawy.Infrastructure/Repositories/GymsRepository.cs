using Gymawy.Domain.Gyms;
using Gymawy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Repositories
{
    public class GymsRepository : BaseRepository<Gym>, IGymsRepository
    {
        public GymsRepository(ApplicationDbContext context) : base(context)
        {
        }

        
    }
}
