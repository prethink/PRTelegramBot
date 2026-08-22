using Microsoft.Extensions.DependencyInjection;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Factory that creates class instances.
    /// </summary>
    public static class InstanceFactory
    {
        #region Methods

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="type">Class type.</param>
        /// <param name="serviceProvider">The service provider used to create the instance through DI.</param>
        /// <returns>An instance of the class.</returns>
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
