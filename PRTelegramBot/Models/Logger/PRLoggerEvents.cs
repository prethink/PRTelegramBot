using Microsoft.Extensions.Logging;
using PRTelegramBot.Core;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Models.Logger
{

    /// <summary>
    /// Логер на основе событий бота.
    /// Используется для обратной совместимости со старой системой логирования,
    /// основанной на событиях <see cref="PRBotBase.Events"/>.
    /// </summary>
    /// <typeparam name="T">Категория логера.</typeparam>
    [Obsolete("Устаревший вариант логирования. Создан для обратной совместимости.")]
    public class PRLoggerEvents<T> : ILogger<T>
    {
        #region Поля и свойства

        /// <summary>
        /// Экземпляр бота, через события которого выполняется логирование.
        /// </summary>
        private readonly PRBotBase bot;

        /// <summary>
        /// Название категории логирования.
        /// Используется при вызове событий общего логирования.
        /// </summary>
        private readonly string categoryName;

        #endregion

        #region ILogger

        /// <inheritdoc />
        public IDisposable BeginScope<TState>(TState state)
        {
            return new DisposableObject(() => { });
        }

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <inheritdoc />
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            var message = formatter(state, exception);

            switch (logLevel)
            {
                case LogLevel.Critical:
                case LogLevel.Error:
                    LogError(message, exception);
                    break;

                case LogLevel.Warning:
                    LogWarning(message);
                    break;

                case LogLevel.Information:
                    LogInformation(message);
                    break;

                case LogLevel.Debug:
                case LogLevel.Trace:
                    LogDebug(message);
                    break;

                default:
                    LogInformation(message);
                    break;
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Логирует сообщение об ошибке через событие бота.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="exception">Исключение (если есть).</param>
        private void LogError(string message, Exception? exception)
        {
            if (exception != null)
                bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, exception));
            else
                bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, message));
        }

        /// <summary>
        /// Логирует предупреждение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        private void LogWarning(string message)
        {
            bot.Events.OnCommonLogInvoke(message, categoryName, ConsoleColor.Yellow);
        }

        /// <summary>
        /// Логирует информационное сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        private void LogInformation(string message)
        {
            bot.Events.OnCommonLogInvoke(message, categoryName, ConsoleColor.White);
        }

        /// <summary>
        /// Логирует отладочное сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        private void LogDebug(string message)
        {
            bot.Events.OnCommonLogInvoke(message, categoryName, ConsoleColor.DarkGray);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Создаёт экземпляр событийного логера для указанного бота.
        /// </summary>
        /// <param name="bot">Экземпляр бота.</param>
        /// <exception cref="ArgumentNullException">Если bot равен null.</exception>
        public PRLoggerEvents(PRBotBase bot)
        {
            this.bot = bot ?? throw new ArgumentNullException(nameof(bot));
            categoryName = typeof(T).FullName ?? typeof(T).Name;
        }


        #endregion
    }
}
