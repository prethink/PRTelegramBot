﻿using Microsoft.Extensions.Logging;
using PRTelegramBot.Core;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Models.Logger
{

    /// <summary>
    /// A logger built on top of the bot's events.
    /// Kept for backward compatibility with the old logging system,
    /// which is built on the <see cref="PRBotBase.Events"/> events.
    /// </summary>
    /// <typeparam name="T">Logger category.</typeparam>
    internal class PRLoggerEvents<T> : ILogger<T>
    {
        #region Fields and properties

        /// <summary>
        /// The bot instance whose events the logging goes through.
        /// </summary>
        private readonly PRBotBase bot;

        /// <summary>
        /// Name of the logging category.
        /// Used when raising the general logging events.
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

        #region Methods

        /// <summary>
        /// Logs an error message through the bot's event.
        /// </summary>
        /// <param name="message">Message text.</param>
        /// <param name="exception">Exception, if any.</param>
        private void LogError(string message, Exception? exception)
        {
            if (exception != null)
                bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, exception));
            else
                bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, message));
        }

        /// <summary>
        /// Logs a warning.
        /// </summary>
        /// <param name="message">Message text.</param>
        private void LogWarning(string message)
        {
            bot.Events.OnCommonLogInvoke(message, categoryName, ConsoleColor.Yellow);
        }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">Message text.</param>
        private void LogInformation(string message)
        {
            bot.Events.OnCommonLogInvoke(message, categoryName, ConsoleColor.White);
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">Message text.</param>
        private void LogDebug(string message)
        {
            bot.Events.OnCommonLogInvoke(message, categoryName, ConsoleColor.DarkGray);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an event-based logger instance for the specified bot.
        /// </summary>
        /// <param name="bot">Bot instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when bot is null.</exception>
        public PRLoggerEvents(PRBotBase bot)
        {
            this.bot = bot ?? throw new ArgumentNullException(nameof(bot));
            categoryName = typeof(T).FullName ?? typeof(T).Name;
        }


        #endregion
    }
}
