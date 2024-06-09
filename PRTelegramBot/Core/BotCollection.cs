using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Класс хранящий всех ботов.
    /// </summary>
    public sealed class BotCollection 
    {
        #region Поля и свойства

        /// <summary>
        /// Экземпляр класса.
        /// </summary>
        private static BotCollection instance;

        /// <summary>
        /// Коллекция ботов.
        /// </summary>
        private Dictionary<long, PRBotBase> BotList = new Dictionary<long, PRBotBase>();

        /// <summary>
        /// Singleton экземпляр.
        /// </summary>
        public static BotCollection Instance
        {
            get
            {
                if (instance == null)
                    instance = new BotCollection();
                return instance;
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получить следующий идентификатор для бота.
        /// </summary>
        /// <returns>Идентификатор бота.</returns>
        public static long GetNextId()
            => Instance.BotList.LastOrDefault().Key + 1;

        /// <summary>
        /// Добавить бота в коллекцию.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public void AddBot(PRBotBase bot)
            => BotList.Add(bot.BotId, bot);

        /// <summary>
        /// Удалить бота из коллекции.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public void RemoveBot(PRBotBase bot)
            => BotList.Remove(bot.BotId);

        /// <summary>
        /// Очистить всех ботов.
        /// </summary>
        public void ClearBots()
            => BotList.Clear();

        /// <summary>
        /// Получить бота по telegram id.
        /// </summary>
        /// <param name="telegramId">Идентификатор telegram.</param>
        /// <returns>Экземпляр класса бота или null.</returns>
        public PRBotBase GetBotByTelegramIdOrNull(long? telegramId)
            => BotList.Values.SingleOrDefault(x => x.TelegramId == telegramId);

        /// <summary>
        /// Получить экземпляр бота.
        /// </summary>
        /// <param name="botId">Идентификатор бота.</param>
        /// <returns>Экземпляр класса бота или null.</returns>
        public PRBotBase GetBotOrNull(long botId)
            => BotList.Values.SingleOrDefault(x => x.BotId == botId);

        /// <summary>
        /// Получить экземпляр бота.
        /// </summary>
        /// <param name="predicate">Выражение для фильтрации.</param>
        /// <returns>Экземпляр класса бота или null.</returns>
        public PRBotBase GetBotOrNull(Func<PRBotBase, bool> predicate)
            => BotList.Values.SingleOrDefault(predicate);

        /// <summary>
        /// Получить всех ботов.
        /// </summary>
        /// <returns>Коллекция ботов.</returns>
        public List<PRBotBase> GetBots()
            => BotList.Select(x => x.Value).ToList();

        /// <summary>
        /// Получить экземпляр бота.
        /// </summary>
        /// <param name="botName">Название/логин бота.</param>
        /// <returns>Экземпляр класса бота или null.</returns>
        public PRBotBase GetBotOrNull(string botName)
            => BotList.Values.SingleOrDefault(x => x.BotName.Contains(botName, StringComparison.OrdinalIgnoreCase));

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        private BotCollection() { }

        #endregion
    }
}
