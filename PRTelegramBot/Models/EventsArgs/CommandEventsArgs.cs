using PRTelegramBot.Core;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandEventsArgs : BotEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public Func<ITelegramBotClient, Update, Task> ExecuteMethod { get; private set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        /// <param name="update">Обновление.</param>
        /// <param name="executeMethod"></param>
        public CommandEventsArgs(PRBotBase bot, Update update, Func<ITelegramBotClient, Update, Task> executeMethod) 
            : base(bot, update)
        {
            this.ExecuteMethod = executeMethod;
        }
    }
}
