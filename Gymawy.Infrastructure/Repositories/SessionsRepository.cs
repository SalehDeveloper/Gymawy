using Gymawy.Domain.Admins;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.Rooms;
using Gymawy.Domain.Sessions;
using Gymawy.Domain.Subscriptions;
using Gymawy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Gymawy.Infrastructure.Repositories
{
    public class SessionsRepository : BaseRepository<Session>, ISessionRepository
    {
        public SessionsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Admin> GetAdminIdForSession(Guid sessionId)
        {
            var session =await _context.Set<Session>()
                 .Include(x => x.Room)
                  .ThenInclude(x => x.Gym)
                   .ThenInclude(x => x.Subscription)
                    .ThenInclude(x => x.Admin)
                      .FirstOrDefaultAsync(x => x.Id == sessionId);

            
            var result = session.Room.Gym.Subscription.Admin;
           
            return result;
               
                
        }
    }
}
