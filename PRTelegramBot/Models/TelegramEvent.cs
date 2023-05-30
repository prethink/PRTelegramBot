using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models
{
    public class TelegramEvent
    {
        public ITelegramBotClient TelegramBot { get; set; }
        public Update Update { get; set; }

        public TelegramEvent(ITelegramBotClient telegramBot, Update update)
        {
            TelegramBot = telegramBot;
            Update = update;
        }
    }
}
