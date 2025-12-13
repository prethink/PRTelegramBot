using PRTelegramBot.Core;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с коллекцией ботов.
    /// Позволяет получать ботов по различным критериям, а также управлять коллекцией.
    /// </summary>
    public interface IBotCollection
    {
        /// <summary>
        /// Количество ботов в коллекции.
        /// </summary>
        long BotCount { get; }

        /// <summary>
        /// Получить бота по Telegram Id.
        /// </summary>
        /// <param name="telegramId">Идентификатор Telegram.</param>
        /// <returns>Экземпляр бота или null, если не найден.</returns>
        PRBotBase? GetBotByTelegramIdOrNull(long? telegramId);

        /// <summary>
        /// Получить бота по его внутреннему Id.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <returns>Экземпляр бота или null, если не найден.</returns>
        PRBotBase? GetBotOrNull(long botId);

        /// <summary>
        /// Получить бота по условию фильтрации.
        /// </summary>
        /// <param name="predicate">Функция фильтрации.</param>
        /// <returns>Экземпляр бота или null.</returns>
        PRBotBase? GetBotOrNull(Func<PRBotBase, bool> predicate);

        /// <summary>
        /// Получить бота по имени или логину.
        /// </summary>
        /// <param name="botName">Название/логин бота.</param>
        /// <returns>Экземпляр бота или null.</returns>
        PRBotBase? GetBotOrNull(string botName);

        /// <summary>
        /// Получить всех ботов.
        /// </summary>
        /// <returns>Список всех ботов.</returns>
        IEnumerable<PRBotBase> GetBots();

        /// <summary>
        /// Получить всех ботов с условием фильтрации.
        /// </summary>
        /// <param name="predicate">Функция фильтрации.</param>
        /// <returns>Список ботов, удовлетворяющих условию.</returns>
        IEnumerable<PRBotBase> GetBots(Func<PRBotBase, bool> predicate);

        /// <summary>
        /// Добавить нового бота в коллекцию.
        /// </summary>
        /// <param name="bot">Экземпляр бота.</param>
        void AddBot(PRBotBase bot);

        /// <summary>
        /// Удалить бота из коллекции.
        /// </summary>
        /// <param name="bot">Экземпляр бота.</param>
        void RemoveBot(PRBotBase bot);

        /// <summary>
        /// Очистить всю коллекцию ботов.
        /// </summary>
        void ClearBots();

        /// <summary>
        /// Получить следующий уникальный идентификатор для нового бота.
        /// </summary>
        /// <returns>Следующий Id.</returns>
        long GetNextId();
    }
}
