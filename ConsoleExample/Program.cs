using NLog;
using static PRTelegramBot.Core.TelegramService;
using PRTelegramBot.Extensions;
using PRTelegramBot.Core;
using PRTelegramBot.Configs;
using Telegram.Bot.Types;
using ConsoleExample.Examples;
using System.Reflection;

//Конфигурация NLog
NLogConfigurate.Configurate();
//Словарик для логгеров
Dictionary<string, Logger> LoggersContainer = new Dictionary<string, Logger>();
//Команда для завершения приложения
const string EXIT_COMMAND = "exit";

//Запуск программы
Console.WriteLine("Запуск программы");
Console.WriteLine($"Для закрытие программы напишите {EXIT_COMMAND}");

#region запуск телеграм бота
var telegram = TelegramService.GetInstance();

//Подписка на простые логи
telegram.OnLogCommon                += Telegram_OnLogCommon;
//Подписка на логи с ошибками
telegram.OnLogError                 += Telegram_OnLogError;
//Запуск работы бота
await telegram.Start();

if(telegram.Handler != null)
{
    //Обработка обновление кроме message и callback
    telegram.Handler.OnUpdate                       += Handler_OnUpdate;

    //Обработка не правильный тип сообщений
    telegram.Handler.Router.OnWrongTypeMessage      += ExampleEvent.OnWrongTypeMessage;

    //Обработка пользователь написал в чат start с deeplink
    telegram.Handler.Router.OnUserStartWithArgs     += ExampleEvent.OnUserStartWithArgs;

    //Обработка проверка привилегий
    telegram.Handler.Router.OnCheckPrivilege        += ExampleEvent.OnCheckPrivilege;

    //Обработка пропущенной  команды
    telegram.Handler.Router.OnMissingCommand        += ExampleEvent.OnMissingCommand;

    //Обработка не верного типа чата
    telegram.Handler.Router.OnWrongTypeChat         += ExampleEvent.OnWrongTypeChat;

    //Обработка локаций
    telegram.Handler.Router.OnLocationHandle        += ExampleEvent.OnLocationHandle;

    //Обработка контактных данных
    telegram.Handler.Router.OnContactHandle         += ExampleEvent.OnContactHandle;

    //Обработка голосований
    telegram.Handler.Router.OnPollHandle            += ExampleEvent.OnPollHandle;

    //Обработка WebApps
    telegram.Handler.Router.OnWebAppsHandle         += ExampleEvent.OnWebAppsHandle;

    //Обработка, когда пользователю отказано в доступе
    telegram.Handler.Router.OnAccessDenied          += ExampleEvent.OnAccessDenied;

    //Обработка сообщения с документом
    telegram.Handler.Router.OnDocumentHandle        += ExampleEvent.OnDocumentHandle;

    //Обработка сообщения с аудио
    telegram.Handler.Router.OnAudioHandle           += ExampleEvent.OnAudioHandle;

    //Обработка сообщения с видео
    telegram.Handler.Router.OnVideoHandle           += ExampleEvent.OnVideoHandle;

    //Обработка сообщения с фото
    telegram.Handler.Router.OnPhotoHandle           += ExampleEvent.OnPhotoHandle;

    //Обработка сообщения с стикером
    telegram.Handler.Router.OnStickerHandle         += ExampleEvent.OnStickerHandle;

    //Обработка сообщения с голосовым сообщением
    telegram.Handler.Router.OnVoiceHandle           += ExampleEvent.OnVoiceHandle;

    //Обработка сообщения с неизвестным типом
    telegram.Handler.Router.OnUnknownHandle         += ExampleEvent.OnUnknownHandle;

    //Обработка сообщения с местоположением
    telegram.Handler.Router.OnVenueHandle           += ExampleEvent.OnVenueHandle;

    //Обработка сообщения с игрой
    telegram.Handler.Router.OnGameHandle            += ExampleEvent.OnGameHandle;

    //Обработка сообщения с видеозаметкой
    telegram.Handler.Router.OnVideoNoteHandle       += ExampleEvent.OnVideoNoteHandle;

    //Обработка сообщения с игральной костью
    telegram.Handler.Router.OnDiceHandle            += ExampleEvent.OnDiceHandle;

}

async Task Handler_OnUpdate(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    //Обработка обновление кроме message и callback
}

#endregion

#region Работа фоновых задач
var tasker = new Tasker(10);
tasker.Start();
#endregion



#region Логи
void Telegram_OnLogError(Exception ex, long? id = null)
{
    Console.ForegroundColor = ConsoleColor.Red;
    string errorMsg = $"{DateTime.Now}: {ex.ToString()}";


    if (ex is Telegram.Bot.Exceptions.ApiRequestException apiEx)
    {
        errorMsg = $"{DateTime.Now}: {apiEx.ToString()}";
        if (apiEx.Message.Contains("Forbidden: bot was blocked by the user"))
        {
            string msg = $"Пользователь {id.GetValueOrDefault()} заблокировал бота - " + apiEx.ToString();
            Telegram_OnLogCommon(msg, TelegramEvents.BlockedBot, ConsoleColor.Red);
            return;
        }
        else if (apiEx.Message.Contains("BUTTON_USER_PRIVACY_RESTRICTED"))
        {
            string msg = $"Пользователь {id.GetValueOrDefault()} заблокировал бота - " + apiEx.ToString();
            Telegram_OnLogCommon(msg, TelegramEvents.BlockedBot, ConsoleColor.Red);
            return;
        }
        else if (apiEx.Message.Contains("group chat was upgraded to a supergroup chat"))
        {
            errorMsg += $"\n newChatId: {apiEx?.Parameters?.MigrateToChatId.GetValueOrDefault()}";
        }

    }

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

void Telegram_OnLogCommon(string msg, TelegramEvents eventType, ConsoleColor color = ConsoleColor.Blue)
{
    Console.ForegroundColor = color;
    string formatMsg = $"{DateTime.Now}: {msg}";
    Console.WriteLine(formatMsg);
    Console.ResetColor();

    if (LoggersContainer.TryGetValue(eventType.GetDescription(), out var logger))
    {
        logger.Info(formatMsg);
    }
    else
    {
        var nextLogger = LogManager.GetLogger(eventType.GetDescription());
        nextLogger.Info(formatMsg);
        LoggersContainer.Add(eventType.GetDescription(), nextLogger);
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