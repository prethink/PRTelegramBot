using Microsoft.Extensions.DependencyInjection;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Фабрика для создание экземпляров классов.
    /// </summary>
    public static class InstanceFactory
    {
        #region Методы

        /// <summary>
        /// Создать экземпляр класса.
        /// </summary>
        /// <param name="type">Тип класса.</param>
        /// <param name="serviceProvider">Сервис провайдер для создание экземпляра с DI.</param>
        /// <returns>Экземпляр класса.</returns>
        public static object GetOrCreate(Type type, IServiceProvider serviceProvider = null)
        {
            object instance = null;

            if (serviceProvider is not null)
            {
                var factory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
                using (var scope = factory.CreateScope())
                    instance = scope.ServiceProvider.GetRequiredService(type);
            }
            else
            {
                instance = ReflectionUtils.CreateInstanceWithNullArguments(type);
            }

            return instance;
        }

        #endregion
    }
}
