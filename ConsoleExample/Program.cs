using ConsoleExample.Checkers;
using ConsoleExample.Examples;
using ConsoleExample.Middlewares;
using ConsoleExample.Models;
using NLog;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;

// Конфигурация NLog.
NLogConfigurate.Configurate();
// Словарик для логгеров.
Dictionary<string, Logger> LoggersContainer = new Dictionary<string, Logger>();
// Команда для завершения приложения.
const string EXIT_COMMAND = "exit";

//Запуск программы
Console.WriteLine("Запуск программы");
Console.WriteLine($"Для закрытие программы напишите {EXIT_COMMAND}");

var checkerReplyCommand = new InternalChecker(CommandType.Reply, new ReplyExampleChecker());
var adminChecker = new InternalChecker(new List<CommandType>() { CommandType.Reply, CommandType.NextStep, CommandType.Inline, CommandType.DynamicReply, CommandType.Slash }, new AdminExampleChecher());

// Парсинг динамических команд из json файла в формате ключ:значение.
var botJsonProvider = new BotConfigJsonProvider(".\\Configs\\commands.json");
var dynamicCommands = botJsonProvider.GetKeysAndValues();

var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
                    .AddConfigPath(ExampleConstants.MESSAGES_FILE_KEY, ".\\Configs\\messages.json")
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .AddReplyDynamicCommands(dynamicCommands)
                    .AddCommandChecker(checkerReplyCommand)
                    .AddCommandChecker(adminChecker)
                    .AddMiddlewares(new OneMiddleware(), new TwoMiddleware(), new ThreeMiddleware())
                    .Build();

// Подписка на простые логи.
telegram.Events.OnCommonLog += Telegram_OnLogCommon;
// Подписка на логи с ошибками.
telegram.Events.OnErrorLog += Telegram_OnLogError;
// Запуск работы бота.
await telegram.Start();
// Инициализация событий для бота.
InitEvents(telegram);
// Инициализация новых команд для бота.
InitCommands(telegram);

void InitEvents(PRBotBase bot)
{
    // Обработка до всех update 
    bot.Events.UpdateEvents.OnPreUpdate += ExampleEvent.Handler_OnUpdate;

    // Обработка после всех update
    bot.Events.UpdateEvents.OnPostUpdate += ExampleEvent.Handler_OnPostUpdate;

    // Обработка не правильный тип сообщений
    bot.Events.OnWrongTypeMessage += ExampleEvent.OnWrongTypeMessage;

    // Обработка пользователь написал в чат start с deeplink
    bot.Events.OnUserStartWithArgs += ExampleEvent.OnUserStartWithArgs;

    // Обработка проверка привилегий
    bot.Events.OnCheckPrivilege += ExampleEvent.OnCheckPrivilege;

    // Обработка пропущенной  команды
    bot.Events.OnMissingCommand += ExampleEvent.OnMissingCommand;

    // Обработка если произошла ошибка при выполнение команды
    bot.Events.OnErrorCommand += ExampleEvent.OnErrorCommand;

    // Обработка не верного типа чата
    bot.Events.OnWrongTypeChat += ExampleEvent.OnWrongTypeChat;

    // Обработка локаций
    bot.Events.MessageEvents.OnLocationHandle += ExampleEvent.OnLocationHandle;

    // Обработка контактных данных
    bot.Events.MessageEvents.OnContactHandle += ExampleEvent.OnContactHandle;

    // Обработка голосований
    bot.Events.MessageEvents.OnPollHandle += ExampleEvent.OnPollHandle;

    // Обработка WebApps
    bot.Events.MessageEvents.OnWebAppsHandle += ExampleEvent.OnWebAppsHandle;

    // Обработка, когда пользователю отказано в доступе
    bot.Events.OnAccessDenied += ExampleEvent.OnAccessDenied;

    //Обработка сообщения с документом
    bot.Events.MessageEvents.OnDocumentHandle += ExampleEvent.OnDocumentHandle;

    //Обработка сообщения с аудио
    bot.Events.MessageEvents.OnAudioHandle += ExampleEvent.OnAudioHandle;

    //Обработка сообщения с видео
    bot.Events.MessageEvents.OnVideoHandle += ExampleEvent.OnVideoHandle;

    //Обработка сообщения с фото
    bot.Events.MessageEvents.OnPhotoHandle += ExampleEvent.OnPhotoHandle;

    //Обработка сообщения с стикером
    bot.Events.MessageEvents.OnStickerHandle += ExampleEvent.OnStickerHandle;

    //Обработка сообщения с голосовым сообщением
    bot.Events.MessageEvents.OnVoiceHandle += ExampleEvent.OnVoiceHandle;

    //Обработка сообщения с неизвестным типом
    bot.Events.MessageEvents.OnUnknownHandle += ExampleEvent.OnUnknownHandle;

    //Обработка сообщения с местоположением
    bot.Events.MessageEvents.OnVenueHandle += ExampleEvent.OnVenueHandle;

    //Обработка сообщения с игрой
    bot.Events.MessageEvents.OnGameHandle += ExampleEvent.OnGameHandle;

    //Обработка сообщения с видеозаметкой
    bot.Events.MessageEvents.OnVideoNoteHandle += ExampleEvent.OnVideoNoteHandle;

    //Обработка сообщения с игральной костью
    bot.Events.MessageEvents.OnDiceHandle += ExampleEvent.OnDiceHandle;

    //Обработка обновления изменения группы/чата
    bot.Events.UpdateEvents.OnMyChatMemberHandle += ExampleEvent.OnUpdateMyChatMember;
}
void InitCommands(PRBotBase bot)
{
    bot.Register.AddInlineCommand(AddCustomTHeader.TestAddCommand, async (botClient, update) =>
    {
        PRTelegramBot.Helpers.Message.Send(botClient, update, "Тест метода TestAddCommand");
    });

    bot.Register.AddInlineCommand(AddCustomTHeader.TestAddCommandTwo, async (botClient, update) =>
    {
        PRTelegramBot.Helpers.Message.Send(botClient, update, "Тест метода TestAddCommandTwo");
    });
}

#region Работа фоновых задач
var tasker = new Tasker(10);
tasker.Start();
#endregion

#region Логи

async Task Telegram_OnLogError(ErrorLogEventArgs e)
{
    Console.ForegroundColor = ConsoleColor.Red;
    string errorMsg = $"{DateTime.Now}: {e.Exception.ToString()}";


    //if (e.Exception is Telegram.Bot.Exceptions.ApiRequestException apiEx)
    //{
    //    errorMsg = $"{DateTime.Now}: {apiEx.ToString()}";
    //    if (apiEx.Message.Contains("Forbidden: bot was blocked by the user"))
    //    {
    //        string msg = $"Пользователь {id.GetValueOrDefault()} заблокировал бота - " + apiEx.ToString();
    //        Telegram_OnLogCommon(msg, "BlockedBot", ConsoleColor.Red);
    //        return;
    //    }
    //    else if (apiEx.Message.Contains("BUTTON_USER_PRIVACY_RESTRICTED"))
    //    {
    //        string msg = $"Пользователь {id.GetValueOrDefault()} заблокировал бота - " + apiEx.ToString();
    //        Telegram_OnLogCommon(msg, "BlockedBot", ConsoleColor.Red);
    //        return;
    //    }
    //    else if (apiEx.Message.Contains("group chat was upgraded to a supergroup chat"))
    //    {
    //        errorMsg += $"\n newChatId: {apiEx?.Parameters?.MigrateToChatId.GetValueOrDefault()}";
    //    }
    //}

    if (LoggersContainer.TryGetValue("Error", out var logger))
    {
        logger.Error(errorMsg);
    }
    else
    {
        var nextLogger = LogManager.GetLogger("Error");
        nextLogger.Error(errorMsg);
        LoggersContainer.Add("Error", nextLogger);
    }
    Console.WriteLine(errorMsg);
    Console.ResetColor();
}

async Task Telegram_OnLogCommon(CommonLogEventArgs e)
{
    Console.ForegroundColor = e.Color;
    string formatMsg = $"{DateTime.Now}: {e.Message}";
    Console.WriteLine(formatMsg);
    Console.ResetColor();

    if(e.Type != null)
    {
        if (LoggersContainer.TryGetValue(e.Type, out var logger))
        {
            logger.Info(formatMsg);
        }
        else
        {
            var nextLogger = LogManager.GetLogger(e.Type);
            nextLogger.Info(formatMsg);
            LoggersContainer.Add(e.Type, nextLogger);
        }
    }


}
#endregion

//Ожидание ввода команды
while (true)
{
    var result = Console.ReadLine();
    if (result.ToLower() == EXIT_COMMAND)
    {
        Environment.Exit(0);
    }
}