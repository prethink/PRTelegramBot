using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Класс хранящий всех ботов.
    /// </summary>
    public sealed class BotCollection : IBotCollection
    {
        #region Поля и свойства

        /// <summary>
        /// Глобальный экземпляр настроек.
        /// </summary>
        public static IBotCollection Instance => instance.Value;

        /// <summary>
        /// Lazy инициализация глобального экземпляра настроек.
        /// </summary>
        private static Lazy<IBotCollection> instance = new Lazy<IBotCollection>(() => new BotCollection());

        /// <summary>
        /// Коллекция ботов.
        /// </summary>
        private Dictionary<long, PRBotBase> botList = new();

        #endregion

        #region IBotCollection

        /// <inheritdoc />
        public long BotCount => botList.Count;

        /// <inheritdoc />
        public long GetNextId() => botList.LastOrDefault().Key + 1;

        /// <inheritdoc />
        public void AddBot(PRBotBase bot)  => botList.Add(bot.BotId, bot);

        /// <inheritdoc />
        public void RemoveBot(PRBotBase bot) => botList.Remove(bot.BotId);

        /// <inheritdoc />
        public void ClearBots() => botList.Clear();

        /// <inheritdoc />
        public PRBotBase? GetBotByTelegramIdOrNull(long? telegramId)  => botList.Values.SingleOrDefault(x => x.TelegramId == telegramId);

        /// <inheritdoc />
        public PRBotBase GetBotOrNull(long botId) => botList.Values.SingleOrDefault(x => x.BotId == botId);

        /// <inheritdoc />
        public PRBotBase? GetBotOrNull(Func<PRBotBase, bool> predicate) => botList.Values.SingleOrDefault(predicate);

        /// <inheritdoc />
        public IEnumerable<PRBotBase> GetBots() => botList.Select(x => x.Value).ToList();

        /// <inheritdoc />
        public IEnumerable<PRBotBase> GetBots(Func<PRBotBase, bool> predicate) => botList.Values.Where(predicate).ToList();

        /// <inheritdoc />
        public PRBotBase? GetBotOrNull(string botName) => botList.Values.SingleOrDefault(x => x.BotName.Contains(botName, StringComparison.OrdinalIgnoreCase));

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        private BotCollection() { }

        #endregion
    }
}
