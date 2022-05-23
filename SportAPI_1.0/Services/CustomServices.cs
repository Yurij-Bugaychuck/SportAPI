using Microsoft.Extensions.DependencyInjection;
using SportAPI.Interfaces;

namespace SportAPI.Services
{
    public static class CustomServices
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services
                .AddTransient<ImageService>()
                .AddTransient<IWorkoutService, WorkoutService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<ICategoriesService, CategoriesService>();

            return services;
        }
    }
}
