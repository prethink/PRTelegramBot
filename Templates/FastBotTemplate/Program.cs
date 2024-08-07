﻿using FastBotTemplateConsole.Commands;
using FastBotTemplateConsole.Events;
using PRTelegramBot.Core;

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

            _ = bot.Start();

            // Чтобы консолька не закрылась.
            while(true) { }
        }
    }
}
