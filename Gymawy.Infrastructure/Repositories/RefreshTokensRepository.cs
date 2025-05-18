using Gymawy.Domain.RefreshTokens;
using Gymawy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Repositories
{
    public class RefreshTokensRepository : BaseRepository<RefreshToken>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
