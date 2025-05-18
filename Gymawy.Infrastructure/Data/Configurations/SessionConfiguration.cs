using Gymawy.Domain.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Data.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("sessions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Status)
                .HasConversion(
                v => v.Value,
                v => SessionStatus.FromValue(v));


            builder.Property(x => x.Type)
                .HasConversion(
                v => v.Value,
                v => SessionType.FromValue(v));

            builder.Property(x => x.SessionFee).HasColumnType("DECIMAL(10,2)");
           
            builder.HasOne( x=> x.Trainer)
                  .WithMany(x=> x.Sessions)
                  .HasForeignKey( x=> x.TrainerId)
                  .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
