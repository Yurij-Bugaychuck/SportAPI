using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Interfaces;
using SportAPI.Services;
namespace SportAPI.Services
{
    public static class CustomServices
    {

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<ImageService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
