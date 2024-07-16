using PRTelegramBot.Attributes;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Models.EventsArgs;

namespace FastBotTemplateConsole.Commands
{
    internal class StartCommands
    {
        [SlashHandler("/start")]
        public static async Task Start(ITelegramBotClient botClient, Update update)
        {
            
        }

        public static async Task StartWithArguments(StartEventArgs e)
        {
           
        }
    }
}
