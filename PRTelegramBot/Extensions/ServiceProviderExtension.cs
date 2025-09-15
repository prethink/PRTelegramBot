using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Методы расширения для ServiceProviderExtension.
    /// </summary>
    public static class ServiceProviderExtension
    {
        #region Методы

        /// <summary>
        /// Добавить обработчики ботов с временным временем жизни (Transient) в DI.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
        public static IServiceCollection AddBotHandlers(this IServiceCollection services)
        {
            return AddTransientBotHandlers(services);
        }

        /// <summary>
        /// Добавить обработчики ботов с областью видимости (Scoped) в DI.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
        public static IServiceCollection AddScopedBotHandlers(this IServiceCollection services)
        {
            return AddBotHandlersInDI(services, LifeCycle.Scoped);
        }

        /// <summary>
        /// Добавить обработчики ботов с временным временем жизни (Transient) в DI.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
        public static IServiceCollection AddTransientBotHandlers(this IServiceCollection services)
        {
            return AddBotHandlersInDI(services, LifeCycle.Transient);
        }

        /// <summary>
        /// Добавить обработчики ботов с одиночным временем жизни (Singleton) в DI.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
        public static IServiceCollection AddSingletonBotHandlers(this IServiceCollection services)
        {
            return AddBotHandlersInDI(services, LifeCycle.Singleton);
        }

        /// <summary>
        /// Добавить обработчики ботов в DI с указанным временем жизни.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <param name="lifeCycle">Время жизни сервиса.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
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
