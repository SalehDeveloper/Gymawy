using Gymawy.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Domain.TrainerInvitations
{
    public interface ITrainerInvitationsRepository : IBaseRepository<TrainerInvitaion>
    {
        Task<bool> IsTrainerAlreadyInvitedAsync(Guid gymId, Guid trainerId, CancellationToken cancellationToken = default);
    }
}
