using FluentValidation;
using Gymawy.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication (this IServiceCollection services)
        {

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });


            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

            return services;
        }
    }
}
