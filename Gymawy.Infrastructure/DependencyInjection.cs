using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Clock;
using Gymawy.Application.Abstractions.Email;
using Gymawy.Application.Abstractions.Storage;
using Gymawy.Application.Abstractions.Stripe;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Bookings;
using Gymawy.Domain.Gyms;
using Gymawy.Domain.GymTrainers;
using Gymawy.Domain.Participants;
using Gymawy.Domain.Payments;
using Gymawy.Domain.RefreshTokens;
using Gymawy.Domain.Rooms;
using Gymawy.Domain.Sessions;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.TrainerInvitations;
using Gymawy.Domain.Trainers;
using Gymawy.Domain.Users;
using Gymawy.Infrastructure.Authentication;
using Gymawy.Infrastructure.Authentication.PasswordHasher;
using Gymawy.Infrastructure.Authorization;
using Gymawy.Infrastructure.Clock;
using Gymawy.Infrastructure.Data;
using Gymawy.Infrastructure.Email;
using Gymawy.Infrastructure.Repositories;
using Gymawy.Infrastructure.Storage;
using Gymawy.Infrastructure.Stripe;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure ( this IServiceCollection services , IConfiguration configuration)
        {

            AddPersistence (services, configuration);
            AddAuthentication(services , configuration);
            AddAuthorization(services , configuration);


            return services;
        }


        private static void AddPersistence (this IServiceCollection services , IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(cfg => cfg.UseSqlServer(configuration.GetConnectionString("Database")));

            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.Configure<EmailOptions>(configuration.GetSection("EmailConfiguration"));
            services.Configure<CloudinaryOptions>(configuration.GetSection("Cloudinary"));
            services.Configure<StripeOptions>(configuration.GetSection("Stripe"));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IAdminsRepository, AdminsRepository>();
            services.AddScoped<IParticipantsRepository, ParticipantsRepository>();
            services.AddScoped<ITrainersRepository, TrainersRepository>();
            services.AddScoped<IGymsRepository, GymsRepository>();
            services.AddScoped<IRoomsRepository, RoomsRepository>();
            services.AddScoped<ISessionRepository, SessionsRepository>();
            services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
            services.AddScoped<IGymTrainersRepository, GymTrainersRepository>();
            services.AddScoped<IBookingsRepository, BookingsRepository>();
            services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
            services.AddScoped<IPaymentsRepository, PaymentsRepository>();
            services.AddScoped<IStripeService, StripeService>();
            services.AddScoped<ITrainerInvitationsRepository, TrainerInviationsRepository>();


        }
        private static void AddAuthentication (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.Configure<Application.Abstractions.Auth.AuthenticationOptions>(configuration.GetSection("Authentication"));
            services.ConfigureOptions<JwtBearerOptionsSetup>();
            services.AddScoped<IJwtService , JwtService>();
            services.AddHttpContextAccessor();

            services.AddScoped<IUserContext, UserContext>();

        }

        public static void AddAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuthorizationService>();
            services.AddTransient<IClaimsTransformation, CustomeClaimsTransformation>();
        }

    }
}
