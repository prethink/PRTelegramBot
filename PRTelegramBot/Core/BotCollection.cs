using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Class that holds all bots.
    /// </summary>
    public sealed class BotCollection : IBotCollection
    {
        #region Fields and properties

        /// <summary>
        /// The global settings instance.
        /// </summary>
        public static IBotCollection Instance => instance.Value;

        /// <summary>
        /// Lazy initialization of the global settings instance.
        /// </summary>
        private static Lazy<IBotCollection> instance = new Lazy<IBotCollection>(() => new BotCollection());

        /// <summary>
        /// Collection of bots.
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
        public PRBotBase? GetBotOrNull(string botName) => botList.Values.SingleOrDefault(x => x.BotName is not null && x.BotName.Contains(botName, StringComparison.OrdinalIgnoreCase));

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        private BotCollection() { }

        #endregion
    }
}
