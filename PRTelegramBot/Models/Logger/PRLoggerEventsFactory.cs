using Microsoft.Extensions.Logging;
using PRTelegramBot.Core;

namespace PRTelegramBot.Models.Logger
{
    /// <summary>
    /// 
    /// </summary>
    [Obsolete("Устаревшая фабрика логеров. Используется для обратной совместимости.")]
    public sealed class PRLoggerEventsFactory : ILoggerFactory
    {
        #region Поля и свойства

        /// <summary>
        /// Экземпляр бота, через события которого выполняется логирование.
        /// </summary>
        private readonly PRBotBase bot;

        #endregion

        #region ILoggerFactory

        /// <inheritdoc />
        public ILogger CreateLogger(string categoryName)
        {
            var type = FindType(categoryName) ?? typeof(object);
            var loggerType = typeof(PRLoggerEvents<>).MakeGenericType(type);
            return (ILogger)Activator.CreateInstance(loggerType, bot)!;
        }

        /// <inheritdoc />
        public void AddProvider(ILoggerProvider provider)
        {
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }

        private static Type? FindType(string categoryName)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .Select(a => a.GetType(categoryName, throwOnError: false))
                .FirstOrDefault(t => t != null);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        /// <exception cref="ArgumentNullException">Экземпляр бота.</exception>
        internal PRLoggerEventsFactory(PRBotBase bot)
        {
            this.bot = bot ?? throw new ArgumentNullException(nameof(bot));
        }


        #endregion
    }
}
