using ConsoleExample.BackgroundTask;
using ConsoleExample.Examples.InlineClassHandlers;
using ConsoleExample.Middlewares;
using ConsoleExample.Models.CommandHeaders;
using ConsoleExample.Services;
using Microsoft.Extensions.Logging;
using PRTelegramBot.Builders;
using PRTelegramBot.Converters.Inline;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Wrappers;

/****************************************************************************************
 * ######################################################################################
 * 
 * Up-to-date documentation: https://prethink.gitbook.io/prtelegrambot
 * 
 * ######################################################################################
 ****************************************************************************************/

Console.WriteLine("Starting the program");

var telegram = new PRBotBuilder("token")
                    .SetBotId(0)
                    .AddConfigPaths(Initializer.GetConfigPaths())
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .AddReplyDynamicCommands(Initializer.GetDynamicCommands())
                    .AddCommandChecker(Initializer.GetCommandChekers())
                    .AddMiddlewares(new OneMiddleware(), new TwoMiddleware(), new ThreeMiddleware())
                    .AddInlineClassHandler(ClassTHeader.DefaultTestClass, typeof(InlineDefaultClassHandler))
                    //Works around Telegram's 64-byte limit on callback_data.
                    .SetInlineMenuConverter(new FileInlineConverter())
                    // ToonSerializerWrapper uses fewer bytes when serializing data than JsonSerializer.
                    //.SetInlineSerializer(new ToonSerializerWrapper())
                    .SetInitializeAction(() => { Console.WriteLine("Custom initialize complete."); })
                    .AddBackgroundTask(new HelloWorldBackgroundTask())
                    .AddBackgroundTask(new AttributeBackgroundTask())
                    .SetLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    .Build();

// Initialize events for the bot.
Initializer.InitEvents(telegram);
Initializer.InitLogEvents(telegram);
Initializer.InitMessageEvents(telegram);
Initializer.InitUpdateEvents(telegram);

// Initialize new commands for the bot.
Initializer.InitCommands(telegram);

// Start the bot.
await telegram.StartAsync();


telegram.Events.OnErrorLog += Events_OnErrorLog;

async Task Events_OnErrorLog(ErrorLogEventArgs arg)
{
    Console.WriteLine(arg.Exception.Message);
}

// Keeps the console application from closing.
while (true)
{
    var result = Console.ReadLine();
    if (result.Equals("exit", StringComparison.OrdinalIgnoreCase))
        Environment.Exit(0);
}