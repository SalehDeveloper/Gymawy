using Gymawy.Domain.Users;
using Gymawy.Infrastructure.Data;

namespace Gymawy.Infrastructure.Repositories
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext context)
            : base(context)
        {

        }
    }
}
