using Gymawy.Domain.TrainerInvitations;
using Gymawy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Repositories
{
    public class TrainerInviationsRepository : BaseRepository<TrainerInvitaion>, ITrainerInvitationsRepository
    {
        

        public TrainerInviationsRepository(ApplicationDbContext context) 
            : base(context)
        {
        }

        public async Task<bool> IsTrainerAlreadyInvitedAsync(Guid gymId, Guid trainerId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TrainerInvitaion>()
                .AnyAsync(x => x.GymId == gymId && x.TrainerId == trainerId && x.Status == InvitationStatus.Pending); 
               
                
        }
    }
}
