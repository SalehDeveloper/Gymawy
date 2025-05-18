using Gymawy.Domain.Bookings;
using Gymawy.Domain.Participants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Data.Configurations
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable("participants");
            builder.HasKey(p => p.Id);
            builder.Property(x =>x.Id).ValueGeneratedNever();

            builder.HasOne(x => x.User)
                  .WithOne()
                  .HasForeignKey<Participant>(x => x.UserId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Sessions)
                   .WithMany(x => x.Participants)
                   .UsingEntity<Booking>();
        }
    }
}
