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
 * Актуальная документация https://prethink.gitbook.io/prtelegrambot
 * 
 * ######################################################################################
 ****************************************************************************************/

Console.WriteLine("Запуск программы");

var telegram = new PRBotBuilder("token")
                    .SetBotId(0)
                    .AddConfigPaths(Initializer.GetConfigPaths())
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .AddReplyDynamicCommands(Initializer.GetDynamicCommands())
                    .AddCommandChecker(Initializer.GetCommandChekers())
                    .AddMiddlewares(new OneMiddleware(), new TwoMiddleware(), new ThreeMiddleware())
                    .AddInlineClassHandler(ClassTHeader.DefaultTestClass, typeof(InlineDefaultClassHandler))
                    //Обход ограничения telegram на 64 байта для callback_data.
                    .SetInlineMenuConverter(new FileInlineConverter())
                    // ToonSerializerWrapper использует меньше байт при сериализации данных по сравнению с JsonSerializer.
                    //.SetInlineSerializer(new ToonSerializerWrapper())
                    .SetInitializeAction(() => { Console.WriteLine("Custom initialize complete."); })
                    .AddBackgroundTask(new HelloWorldBackgroundTask())
                    .AddBackgroundTask(new AttributeBackgroundTask())
                    .SetLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    .Build();

// Инициализация событий для бота.
Initializer.InitEvents(telegram);
Initializer.InitLogEvents(telegram);
Initializer.InitMessageEvents(telegram);
Initializer.InitUpdateEvents(telegram);

// Инициализация новых команд для бота.
Initializer.InitCommands(telegram);

// Запуск работы бота.
await telegram.StartAsync();


telegram.Events.OnErrorLog += Events_OnErrorLog;

async Task Events_OnErrorLog(ErrorLogEventArgs arg)
{
    Console.WriteLine(arg.Exception.Message);
}

// Чтобы консольное приложение не закрылось.
while (true)
{
    var result = Console.ReadLine();
    if (result.Equals("exit", StringComparison.OrdinalIgnoreCase))
        Environment.Exit(0);
}