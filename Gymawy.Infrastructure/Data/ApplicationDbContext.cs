using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Bookings;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.Participants;
using Gymawy.Domain.Rooms;
using Gymawy.Domain.Sessions;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.Trainers;
using Gymawy.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext , IUnitOfWork
    {
      



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options) 
        {
        }

        public async Task CompleteAsync()
        {
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);


        }
    }
}
