using Gymawy.Domain.Admins;
using Gymawy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Repositories
{
    public class AdminsRepository : BaseRepository<Admin>, IAdminsRepository
    {
        public AdminsRepository(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}
