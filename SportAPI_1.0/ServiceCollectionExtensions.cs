using Microsoft.Extensions.DependencyInjection;
using SportAPI.Interfaces;

namespace SportAPI.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services
                .AddTransient<ImageService>()
                .AddTransient<IWorkoutService, WorkoutService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<ICategoriesService, StatsCategoriesService>();

            return services;
        }
    }
}
