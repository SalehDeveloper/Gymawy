using Gymawy.Domain.GymTrainers;
using Gymawy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Repositories
{
    public class GymTrainersRepository : BaseRepository<GymTrainer>, IGymTrainersRepository
    {
        public GymTrainersRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
