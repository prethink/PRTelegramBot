﻿using PRTelegramBot.Configs;

namespace PRTelegramBot.Core.Factories
{
    /// <summary>
    /// Abstract factory.
    /// </summary>
    public abstract class PRBotFactoryBase
    {
        /// <summary>
        /// Creates an instance of the PRBot class.
        /// </summary>
        /// <param name="options">Parameters.</param>
        /// <returns>A PRBot instance produced by the factory.</returns>
        public abstract PRBotBase CreateBot(TelegramOptions options);
    }
}
