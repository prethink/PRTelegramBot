using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Extension methods for ServiceProviderExtension.
    /// </summary>
    public static class ServiceProviderExtension
    {
        #region Methods

        /// <summary>
        /// Adds the bot handlers to DI with a Transient lifetime.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddBotHandlers(this IServiceCollection services)
        {
            return AddTransientBotHandlers(services);
        }

        /// <summary>
        /// Adds the bot handlers to DI with a Scoped lifetime.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddScopedBotHandlers(this IServiceCollection services)
        {
            return AddBotHandlersInDI(services, LifeCycle.Scoped);
        }

        /// <summary>
        /// Adds the bot handlers to DI with a Transient lifetime.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddTransientBotHandlers(this IServiceCollection services)
        {
            return AddBotHandlersInDI(services, LifeCycle.Transient);
        }

        /// <summary>
        /// Adds the bot handlers to DI with a Singleton lifetime.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddSingletonBotHandlers(this IServiceCollection services)
        {
            return AddBotHandlersInDI(services, LifeCycle.Singleton);
        }

        /// <summary>
        /// Adds the bot handlers to DI with the specified lifetime.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="lifeCycle">Service lifetime.</param>
        /// <returns>The updated service collection.</returns>
        private static IServiceCollection AddBotHandlersInDI(this IServiceCollection services, LifeCycle lifeCycle)
        {
            if (services is null)
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

        #endregion
    }
}
