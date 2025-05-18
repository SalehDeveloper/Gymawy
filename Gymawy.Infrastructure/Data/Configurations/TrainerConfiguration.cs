using Gymawy.Domain.GymTrainers;
using Gymawy.Domain.Trainers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Data.Configurations
{
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.ToTable("trainers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();


            builder.HasOne(x => x.User)
                   .WithOne()
                   .HasForeignKey<Trainer>(x => x.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Gyms)
                   .WithMany(x => x.Trainers)
                   .UsingEntity<GymTrainer>();

        }
    }
}
