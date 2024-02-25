using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Extensions
{
    public static class ServiceProviderExtension
    {
        public static IServiceCollection AddBotHandlers(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var types = ReflectionUtils.FindClassesWithInstanceMethods();
            foreach ( var type in types) 
            {
                services.AddTransient(type);
            }

            return services;
        }

        public static IServiceCollection AddScopedBotHandlers(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var types = ReflectionUtils.FindClassesWithInstanceMethods();
            foreach (var type in types)
            {
                services.AddScoped(type);
            }

            return services;
        }

        public static IServiceCollection AddTransientBotHandlers(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var types = ReflectionUtils.FindClassesWithInstanceMethods();
            foreach (var type in types)
            {
                services.AddTransient(type);
            }

            return services;
        }

        public static IServiceCollection AddSingletonBotHandlers(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var types = ReflectionUtils.FindClassesWithInstanceMethods();
            foreach (var type in types)
            {
                services.AddSingleton(type);
            }

            return services;
        }
    }
}
