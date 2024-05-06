using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core
{
    public class BotCollection : IBotCollection
    {
        private static BotCollection instance;

        private Dictionary<long, PRBot> BotList = new Dictionary<long, PRBot>();

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

        public static long GetNextId()
            => Instance.BotList.LastOrDefault().Key + 1;

        public void AddBot(PRBot bot) 
            => BotList.Add(bot.BotId, bot);

        public void RemoveBot(PRBot bot) 
            => BotList.Remove(bot.BotId);

        public void ClearBots() 
            => BotList.Clear();

        public PRBot GetBotByTelegramIdOrNull(long? telegramId) 
            => BotList.Values.SingleOrDefault(x => x.TelegramId == telegramId);

        public PRBot GetBotOrNull(long? botId) 
            => BotList.Values.SingleOrDefault(x => x.BotId == botId);

        public List<PRBot> GetBots() 
            => BotList.Select(x => x.Value).ToList();

        public PRBot GetBotOrNull(string botName) 
            => BotList.Values.SingleOrDefault(x => x.BotName.Contains(botName, StringComparison.OrdinalIgnoreCase));
    }
}
