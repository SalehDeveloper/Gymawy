using Gymawy.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Data.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("subscriptions");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Id).ValueGeneratedNever();


            builder.Property("_price").HasColumnName("Price").HasColumnType("DECIMAL(10,2)");
            builder.Property("_maxGyms").HasColumnName("MaxGyms").HasColumnType("INT");

            builder.Property(x => x.StartDate)
                .HasColumnName("StartDate")
                .HasColumnType("DATETIME");
           
            builder.Property(x => x.EndDate)
                .HasColumnName("EndDate").HasColumnType("DATETIME");



            builder.Property(x => x.Type)
                .HasConversion(
                v => v.Value,
                v => SubscriptionType.FromValue(v)).IsRequired();


            builder.Property(x => x.Status)
                .HasConversion(
                v => v.Value,
                v => SubscriptionStatus.FromValue(v))
                .IsRequired();


            builder.HasMany(x=> x.Gyms)
                   .WithOne(x=> x.Subscription)
                   .HasForeignKey(x=> x.SubscriptionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

     
        }
    }
}
