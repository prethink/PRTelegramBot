using PRTelegramBot.Configs;

namespace PRTelegramBot.Core.Factory
{
    /// <summary>
    /// Абстрактная фабрика.
    /// </summary>
    public abstract class PRBotFactoryBase
    {
        /// <summary>
        /// Создание экземпляра класса PRBot.
        /// </summary>
        /// <param name="options">Параметры.</param>
        /// <returns>Экземпляр PRBot в зависимости от фабрики.</returns>
        public abstract PRBotBase CreateBot(TelegramOptions options);
    }
}
