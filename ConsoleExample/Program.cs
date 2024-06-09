using ConsoleExample.Examples;
using ConsoleExample.Models;
using NLog;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;

//Конфигурация NLog
NLogConfigurate.Configurate();
//Словарик для логгеров
Dictionary<string, Logger> LoggersContainer = new Dictionary<string, Logger>();
//Команда для завершения приложения
const string EXIT_COMMAND = "exit";

//Запуск программы
Console.WriteLine("Запуск программы");
Console.WriteLine($"Для закрытие программы напишите {EXIT_COMMAND}");



var botJsonProvider = new BotConfigJsonProvider(".\\Configs\\commands.json");
var dynamicCommands = botJsonProvider.GetKeysAndValues();
#region запуск telegram бота

var telegram = new PRBotBuilder("")
                    .SetBotId(0)
                    .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
                    .AddConfigPath(ExampleConstants.MESSAGES_FILE_KEY, ".\\Configs\\messages.json")
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .AddReplyDynamicCommands(dynamicCommands)
                    .Build();

var telegramTwo = new PRBot(options =>
{
    // Токен telegram бота берется из BotFather
    options.Token = "555555:TestToken";
    // Перед запуском очищает список обновлений, которые накопились когда бот не работал.
    options.ClearUpdatesOnStart = true;
    // Если есть хоть 1 идентификатор telegram пользователя, могут пользоваться только эти пользователи
    options.WhiteListUsers = new List<long>() { };
    // Идентификатор telegram пользователя
    options.Admins = new List<long>() { };
    // Уникальных идентификатор для бота, используется, чтобы в одном приложение запускать несколько ботов
    options.BotId = 1;
});

//Подписка на простые логи
telegram.Events.OnCommonLog += Telegram_OnLogCommon;
//Подписка на логи с ошибками
telegram.Events.OnErrorLog += Telegram_OnLogError;



//Запуск работы бота

//Подписка на простые логи
telegramTwo.Events.OnCommonLog += Telegram_OnLogCommon;
//Подписка на логи с ошибками
telegramTwo.Events.OnErrorLog += Telegram_OnLogError;
//Запуск работы бота
await telegram.Start();
await telegramTwo.Start();

InitEventsAndCommands(telegram);
InitEventsAndCommands(telegramTwo);

void InitEventsAndCommands(PRBotBase tg)
{
    ////Обработка обновление 
    tg.Events.OnPreUpdate += Handler_OnUpdate;

    //Обработка обновление кроме message и callback
    tg.Events.OnPostMessageUpdate += Handler_OnWithoutMessageUpdate;

    //Обработка не правильный тип сообщений
    tg.Events.OnWrongTypeMessage += ExampleEvent.OnWrongTypeMessage;

    //Обработка пользователь написал в чат start с deeplink
    tg.Events.OnUserStartWithArgs += ExampleEvent.OnUserStartWithArgs;

    //Обработка проверка привилегий
    tg.Events.OnCheckPrivilege += ExampleEvent.OnCheckPrivilege;

    //Обработка пропущенной  команды
    tg.Events.OnMissingCommand += ExampleEvent.OnMissingCommand;

    //Обработка не верного типа чата
    tg.Events.OnWrongTypeChat += ExampleEvent.OnWrongTypeChat;

    //Обработка локаций
    tg.Events.OnLocationHandle += ExampleEvent.OnLocationHandle;

    //Обработка контактных данных
    tg.Events.OnContactHandle += ExampleEvent.OnContactHandle;

    //Обработка голосований
    tg.Events.OnPollHandle += ExampleEvent.OnPollHandle;

    //Обработка WebApps
    tg.Events.OnWebAppsHandle += ExampleEvent.OnWebAppsHandle;

    //Обработка, когда пользователю отказано в доступе
    tg.Events.OnAccessDenied += ExampleEvent.OnAccessDenied;

    //Обработка сообщения с документом
    tg.Events.OnDocumentHandle += ExampleEvent.OnDocumentHandle;

    //Обработка сообщения с аудио
    tg.Events.OnAudioHandle += ExampleEvent.OnAudioHandle;

    //Обработка сообщения с видео
    tg.Events.OnVideoHandle += ExampleEvent.OnVideoHandle;

    //Обработка сообщения с фото
    tg.Events.OnPhotoHandle += ExampleEvent.OnPhotoHandle;

    //Обработка сообщения с стикером
    tg.Events.OnStickerHandle += ExampleEvent.OnStickerHandle;

    //Обработка сообщения с голосовым сообщением
    tg.Events.OnVoiceHandle += ExampleEvent.OnVoiceHandle;

    //Обработка сообщения с неизвестным типом
    tg.Events.OnUnknownHandle += ExampleEvent.OnUnknownHandle;

    //Обработка сообщения с местоположением
    tg.Events.OnVenueHandle += ExampleEvent.OnVenueHandle;

    //Обработка сообщения с игрой
    tg.Events.OnGameHandle += ExampleEvent.OnGameHandle;

    //Обработка сообщения с видеозаметкой
    tg.Events.OnVideoNoteHandle += ExampleEvent.OnVideoNoteHandle;

    //Обработка сообщения с игральной костью
    tg.Events.OnDiceHandle += ExampleEvent.OnDiceHandle;

    tg.Register.AddInlineCommand(AddCustomTHeader.TestAddCommand, async (botClient, update) =>
    {
        PRTelegramBot.Helpers.Message.Send(botClient, update, "Тест метода TestAddCommand");
    });

    tg.Register.AddInlineCommand(AddCustomTHeader.TestAddCommandTwo, async (botClient, update) =>
    {
        PRTelegramBot.Helpers.Message.Send(botClient, update, "Тест метода TestAddCommandTwo");
    });
}

async Task<UpdateResult> Handler_OnUpdate(BotEventArgs e)
{
    return UpdateResult.Continue;
}

async Task Handler_OnWithoutMessageUpdate(BotEventArgs e)
{
    //Обработка обновление кроме message и callback
}


#endregion

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