using PRTelegramBot.Configs;

namespace PRTelegramBot.Core.Factory
{
    /// <summary>
    /// Factory that creates a PRBot with polling support.
    /// </summary>
    public class PRBotFactory : PRBotFactoryBase
    {
        #region Base class

        /// <inheritdoc />
        public override PRBotBase CreateBot(TelegramOptions options)
        {
            return new PRBot(options);
        }

        #endregion
    }
}
