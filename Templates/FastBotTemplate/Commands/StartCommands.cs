using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.EventsArgs;

namespace FastBotTemplateConsole.Commands
{
    internal class StartCommands
    {
        [SlashHandler("/start")]
        public static Task Start(IBotContext context)
        {
            return Task.CompletedTask;
        }

        public static Task StartWithArguments(StartEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
