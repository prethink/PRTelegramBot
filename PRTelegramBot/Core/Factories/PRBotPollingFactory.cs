using PRTelegramBot.Configs;
using PRTelegramBot.Core.Factory;

namespace PRTelegramBot.Core.Factories
{
    /// <summary>
    /// Фабрика для создания PRBot с функционалом polling.
    /// </summary>
    public class PRBotPollingFactory : PRBotFactoryBase
    {
        #region Базовый класс

        /// <inheritdoc />
        public override PRBotBase CreateBot(TelegramOptions options)
        {
            return new PRBotPolling(options);
        }

        #endregion
    }
}
