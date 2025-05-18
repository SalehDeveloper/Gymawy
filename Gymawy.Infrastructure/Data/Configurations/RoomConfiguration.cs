using Gymawy.Domain.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Data.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("rooms");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name).HasColumnType("NVARCHAR(100)").IsRequired();

            builder.HasMany(x=> x.Sessions)
                   .WithOne(x=> x.Room)
                   .HasForeignKey(x=> x.RoomId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);




        }
    }
}
