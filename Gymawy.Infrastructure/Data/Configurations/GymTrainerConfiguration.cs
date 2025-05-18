using Gymawy.Domain.GymTrainers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Data.Configurations
{
    public class GymTrainerConfiguration : IEntityTypeConfiguration<GymTrainer>
    {
        public void Configure(EntityTypeBuilder<GymTrainer> builder)
        {
            builder.ToTable("gym_trainer");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.HasIndex(x => new { x.GymId, x.TrainerId }).IsUnique();
        }
    }
}
