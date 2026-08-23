using FastBotTemplateConsole.Commands;
using FastBotTemplateConsole.Events;
using PRTelegramBot.Builders;

namespace FastBotTemplateConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var bot = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .SetClearUpdatesOnStart(true)
                    .Build();

            bot.Events.OnCommonLog += LogEvents.OnLogCommon;
            bot.Events.OnErrorLog += LogEvents.OnLogError;
            bot.Events.OnUserStartWithArgs += StartCommands.StartWithArguments;

            await bot.StartAsync();

            // Keeps the console from closing.
            await Task.Delay(Timeout.Infinite);
        }
    }
}
