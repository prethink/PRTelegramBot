using PRTelegramBot.Configs;

namespace PRTelegramBot.Core.Factory
{
    /// <summary>
    /// Фабрика для создания PRBot с функционалом webhook.
    /// </summary>
    public class PRBotWebHookFactory : PRBotFactoryBase
    {
        #region Базовый класс

        /// <inheritdoc />
        public override PRBotBase CreateBot(TelegramOptions options)
        {
            return new PRBotWebHook(options);
        }

        #endregion
    }
}
