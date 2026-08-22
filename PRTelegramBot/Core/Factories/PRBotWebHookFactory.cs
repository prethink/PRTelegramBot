using PRTelegramBot.Configs;

namespace PRTelegramBot.Core.Factory
{
    /// <summary>
    /// Factory that creates a PRBot with webhook support.
    /// </summary>
    public class PRBotWebHookFactory : PRBotFactoryBase
    {
        #region Base class

        /// <inheritdoc />
        public override PRBotBase CreateBot(TelegramOptions options)
        {
            return new PRBotWebHook(options);
        }

        #endregion
    }
}
