using ConsoleExample.Middlewares;
using ConsoleExample.Services;
using PRTelegramBot.Core;

/****************************************************************************************
 * ######################################################################################
 * 
 * Актуальная документация https://prethink.gitbook.io/prtelegrambot
 * 
 * ######################################################################################
 ****************************************************************************************/

Console.WriteLine("Запуск программы");

var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddConfigPaths(Initializer.GetConfigPaths())
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .AddReplyDynamicCommands(Initializer.GetDynamicCommands())
                    .AddCommandChecker(Initializer.GetCommandChekers())
                    .AddMiddlewares(new OneMiddleware(), new TwoMiddleware(), new ThreeMiddleware())
                    .Build();

// Инициализация событий для бота.
Initializer.InitEvents(telegram);
Initializer.InitLogEvents(telegram);
Initializer.InitMessageEvents(telegram);
Initializer.InitUpdateEvents(telegram);

// Инициализация новых команд для бота.
Initializer.InitCommands(telegram);

// Запуск работы бота.
await telegram.Start();

// Чтобы консольное приложение не закрылось.
while (true)
{
    var result = Console.ReadLine();
    if (result.Equals("exit", StringComparison.OrdinalIgnoreCase))
        Environment.Exit(0);
}