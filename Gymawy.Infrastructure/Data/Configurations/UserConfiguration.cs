using Gymawy.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).ValueGeneratedNever();

            builder.Property(x => x.FullName).HasColumnType("NVARCHAR(50)").IsRequired();

            builder.Property(x => x.Password).HasColumnType("NVARCHAR(100)").IsRequired();

            builder.Property(x=> x.Email).IsRequired();

            builder.Property(x => x.PhotoUrl).IsRequired(false);

            builder.HasIndex(x=> x.Email).IsUnique();
        }
    }
}
