using PRTelegramBot.Configs;
using PRTelegramBot.Core.Factory;

namespace PRTelegramBot.Core.Factories
{
    /// <summary>
    /// Factory that creates a PRBot with polling support.
    /// </summary>
    public class PRBotPollingFactory : PRBotFactoryBase
    {
        #region Base class

        /// <inheritdoc />
        public override PRBotBase CreateBot(TelegramOptions options)
        {
            return new PRBotPolling(options);
        }

        #endregion
    }
}
