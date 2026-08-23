﻿using Microsoft.Extensions.Logging;
using PRTelegramBot.Core;

namespace PRTelegramBot.Models.Logger
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class PRLoggerEventsFactory : ILoggerFactory
    {
        #region Fields and properties

        /// <summary>
        /// The bot instance whose events the logging goes through.
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

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        /// <exception cref="ArgumentNullException">Thrown when the bot instance is null.</exception>
        internal PRLoggerEventsFactory(PRBotBase bot)
        {
            this.bot = bot ?? throw new ArgumentNullException(nameof(bot));
        }


        #endregion
    }
}
