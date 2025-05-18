using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Trainers;
using Gymawy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Repositories
{
    public class TrainersRepository : BaseRepository<Trainer>, ITrainersRepository
    {
        public TrainersRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
