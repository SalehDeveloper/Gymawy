using Gymawy.Domain.Participants;
using Gymawy.Infrastructure.Data;


namespace Gymawy.Infrastructure.Repositories
{
    public class ParticipantsRepository : BaseRepository<Participant>, IParticipantsRepository
    {
        public ParticipantsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
