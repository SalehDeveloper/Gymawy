using Gymawy.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Id).ValueGeneratedNever();


            builder.Property(x => x.Status)
                .HasConversion(
                v => v.Value,
                v => PaymentStatus.FromValue(v))
                .IsRequired(); ;

            builder.Property(x => x.Amount).HasColumnType("DECIMAL(10,2)");

            builder.HasOne(p => p.Subscription)
                   .WithMany(s => s.Payments)
                   .HasForeignKey(p => p.SubscriptionId)
                   .OnDelete(DeleteBehavior.Restrict);
        
        
        
        
        }
    }
}
