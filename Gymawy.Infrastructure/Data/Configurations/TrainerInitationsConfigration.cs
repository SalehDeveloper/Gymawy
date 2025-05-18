using Gymawy.Domain.TrainerInvitations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Data.Configurations
{
    public class TrainerInitationsConfigration : IEntityTypeConfiguration<TrainerInvitaion>
    {
        public void Configure(EntityTypeBuilder<TrainerInvitaion> builder)
        {
            builder.ToTable("TrainerInvitations");

            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Id).ValueGeneratedNever();


            builder.Property(x => x.ExpirationDateUtc)
                .IsRequired();

            builder.Property(x => x.CreatedOnUtc)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.Property(x => x.Status)
              .HasConversion(
              v => v.Value,
              v => InvitationStatus.FromValue(v)).IsRequired();
            

            builder.HasOne(x=> x.Gym)
                   .WithMany(g=> g.TrainerInvitaions)
                   .HasForeignKey(x=> x.GymId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Trainer)
            .WithMany(g=> g.TrainerInvitaions) 
            .HasForeignKey(x => x.TrainerId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
