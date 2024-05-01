using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Extensions
{
    public static class ServiceProviderExtension
    {
        public enum LifeCycle
        {
            Singleton,
            Scoped,
            Transient
        }
        public static IServiceCollection AddBotHandlers(this IServiceCollection services)
        {
            return AddTransientBotHandlers(services);
        }

        public static IServiceCollection AddScopedBotHandlers(this IServiceCollection services)
        {
            return AddBotHandlersInDI(services, LifeCycle.Scoped);
        }

        public static IServiceCollection AddTransientBotHandlers(this IServiceCollection services)
        {
            return AddBotHandlersInDI(services, LifeCycle.Transient);
        }

        public static IServiceCollection AddSingletonBotHandlers(this IServiceCollection services)
        {
            return AddBotHandlersInDI(services, LifeCycle.Singleton);
        }

        private static IServiceCollection AddBotHandlersInDI(this IServiceCollection services, LifeCycle lifeCycle)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var types = ReflectionUtils.FindClassesWithBotHandlerAttribute();
            foreach (var type in types)
            {
                _ = lifeCycle switch
                {
                    LifeCycle.Singleton => services.AddSingleton(type),
                    LifeCycle.Scoped => services.AddScoped(type),
                    LifeCycle.Transient => services.AddTransient(type),
                    _ => throw new NotImplementedException()
                };
            }

            return services;
        }
    }
}
