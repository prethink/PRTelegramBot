using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core
{
    public class BotCollection : IBotCollection
    {
        private static BotCollection instance;

        private List<PRBot> BotList { get; set; } = new List<PRBot>();

        public int nextBotId { get; private set; }

        private BotCollection() { }

        /// <summary>
        /// 
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

        public int GetNextBotId()
        {
            return nextBotId++;
        }

        public void AddBot(PRBot bot) => BotList.Add(bot);

        public void RemoveBot(PRBot bot) => BotList.Remove(bot);

        public void ClearBots() => BotList.Clear();

        public PRBot GetBotByTelegramIdOrNull(long? telegramId) => BotList.SingleOrDefault(x => x.TelegramId == telegramId);

        public PRBot GetBotOrNull(long? botId) => BotList.SingleOrDefault(x => x.BotId == botId);

        public List<PRBot> GetBots() => BotList.ToList();

        public PRBot GetBotOrNull(string botName) => BotList.SingleOrDefault(x => x.BotName.Contains(botName, StringComparison.OrdinalIgnoreCase));
    }
}
