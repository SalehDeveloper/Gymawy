using Gymawy.Domain.Admins;
using Gymawy.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Data.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
           
            builder.HasKey(x=> x.Id);
            builder.Property(x=> x.Id).ValueGeneratedNever();

            builder.HasOne(x => x.User)
                   .WithOne()
                   .HasForeignKey<Admin>(x => x.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x=> x.Subscription)
                   .WithOne(x=> x.Admin)
                   .HasForeignKey<Subscription>(x=> x.AdminId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);
                   
        }
    }
}
