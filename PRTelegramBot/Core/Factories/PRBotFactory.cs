using PRTelegramBot.Configs;

namespace PRTelegramBot.Core.Factory
{
    /// <summary>
    /// Фабрика создания PRBot с функционалом polling.
    /// </summary>
    public class PRBotFactory : PRBotFactoryBase
    {
        #region Базовый класс

        /// <inheritdoc />
        public override PRBotBase CreateBot(TelegramOptions options)
        {
            return new PRBot(options);
        }

        #endregion
    }
}
