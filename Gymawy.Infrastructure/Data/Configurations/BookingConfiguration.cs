using Gymawy.Domain.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Gymawy.Infrastructure.Data.Configurations
{
    internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {

            builder.ToTable("bookings");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.AmountPaid).HasColumnType("DECIMAL(10,2)");

            builder.Property(x => x.Status)
                .HasConversion(
                v => v.Value,
                v => BookingStatus.FromValue(v));

          



            builder.HasIndex(x => new { x.SessionId, x.ParticipantId }).IsUnique();

        }
    }
}
