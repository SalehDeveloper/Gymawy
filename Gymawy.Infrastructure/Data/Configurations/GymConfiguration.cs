using Gymawy.Domain.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Data.Configurations
{
    public class GymConfiguration : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.ToTable("gyms");

            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name).HasColumnType("NVARCHAR(50)").IsRequired();

            builder.HasMany(x=> x.Rooms)
                   .WithOne(x=> x.Gym)
                   .HasForeignKey(x=> x.GymId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

        }
    }
}
