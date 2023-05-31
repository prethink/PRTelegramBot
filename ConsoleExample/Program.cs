using NLog;
using static PRTelegramBot.Core.TelegramService;
using PRTelegramBot.Extensions;
using PRTelegramBot.Core;
using PRTelegramBot.Configs;
using Telegram.Bot.Types;

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
    //Обработка не правильный тип сообщений
    telegram.Handler.Router.OnWrongTypeMessage      += Router_OnWrongTypeMessage;

    //Обработка пользователь написал в чат start с deeplink
    telegram.Handler.Router.OnUserStartWithArgs     += Router_OnUserStartWithArgs;

    //Обработка проверка привилегий
    telegram.Handler.Router.OnCheckPrivilege        += Router_OnCheckPrivilege;

    //Обработка пропущеной команды
    telegram.Handler.Router.OnMissingCommand        += Router_OnMissingCommand;

    //Обработка не верного типа чата
    telegram.Handler.Router.OnWrongTypeChat         += Router_OnWrongTypeChat;

    //Обработка локаций
    telegram.Handler.Router.OnLocationHandle        += Router_OnLocationHandle;

    //Обработка контактных данных
    telegram.Handler.Router.OnContactHandle         += Router_OnContactHandle;

    //Обработка голосований
    telegram.Handler.Router.OnPollHandle            += Router_OnPollHandle;

    //Обработка WebApps
    telegram.Handler.Router.OnWebAppsHandle         += Router_OnWebAppsHandle;

    //Обработка когда пользователю отказано в доступе
    telegram.Handler.Router.OnAccessDenied          += Router_OnAccessDenied;

    //Обработка сообщения с документом
    telegram.Handler.Router.OnDocumentHandle        += Router_OnDocumentHandle;

    //Обработка сообщения с аудио
    telegram.Handler.Router.OnAudioHandle           += Router_OnAudioHandle;

    //Обработка сообщения с видео
    telegram.Handler.Router.OnVideoHandle           += Router_OnVideoHandle;

    //Обработка сообщения с фото
    telegram.Handler.Router.OnPhotoHandle           += Router_OnPhotoHandle;

    //Обработка сообщения с стикером
    telegram.Handler.Router.OnStickerHandle         += Router_OnStickerHandle;

    //Обработка сообщения с голосовым сообщением
    telegram.Handler.Router.OnVoiceHandle           += Router_OnVoiceHandle;

    //Обработка сообщения с незивестным типом
    telegram.Handler.Router.OnUnknownHandle         += Router_OnUnknownHandle;

    //Обработка сообщения с местоположением
    telegram.Handler.Router.OnVenueHandle           += Router_OnVenueHandle;

    //Обработка сообщения с игрой
    telegram.Handler.Router.OnGameHandle            += Router_OnGameHandle;

    //Обработка сообщения с видеозаметкой
    telegram.Handler.Router.OnVideoNoteHandle       += Router_OnVideoNoteHandle;

    //Обработка сообщения с игральной костью
    telegram.Handler.Router.OnDiceHandle            += Router_OnDiceHandle;

}

async Task Router_OnDiceHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var dice = update.Message.Dice;
    //Обработка данных
}

async Task Router_OnVideoNoteHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var videonote = update.Message.VideoNote;
    //Обработка данных
}

async Task Router_OnGameHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var game = update.Message.Game;
    //Обработка данных
}

async Task Router_OnVenueHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var venue = update.Message.Venue;
    //Обработка данных
}

async Task Router_OnUnknownHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    //Обработка данных
}

async Task Router_OnVoiceHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var voice = update.Message.Voice;
    //Обработка данных
}

async Task Router_OnStickerHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var sticker = update.Message.Sticker;
    //Обработка данных
}

async Task Router_OnPhotoHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var photo = update.Message.Photo;
    //Обработка данных
}

async Task Router_OnVideoHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var video = update.Message.Video;
    //Обработка данных
}

async Task Router_OnAudioHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var audio = update.Message.Audio;
    //Обработка данных
}

async Task Router_OnDocumentHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var document = update.Message.Document;
    //Обработка данных
}

async Task Router_OnAccessDenied(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    //Обработка данных
}

async Task Router_OnWebAppsHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var webApp = update.Message.WebAppData;
    //Обработка данных
}

async Task Router_OnPollHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var poll = update.Message.Poll;
    //Обработка данных
}

async Task Router_OnContactHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var contact = update.Message.Contact;
    //Обработка данных
}

async Task Router_OnLocationHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    var location = update.Message.Location;
    //Обработка данных
}

async Task Router_OnWrongTypeChat(Telegram.Bot.ITelegramBotClient botclient, Telegram.Bot.Types.Update update)
{
    string msg = "Неверный тип чата";
    await PRTelegramBot.Helpers.Message.Send(botclient, update, msg);
}

async Task Router_OnMissingCommand(Telegram.Bot.ITelegramBotClient botclient, Telegram.Bot.Types.Update update)
{
    string msg = "Не найдена команда";
    await PRTelegramBot.Helpers.Message.Send(botclient, update, msg);
}

async Task Router_OnCheckPrivilege(Telegram.Bot.ITelegramBotClient botclient, Telegram.Bot.Types.Update update, PRTelegramBot.Models.Enums.UserPrivilege? requiredPrivilege)
{
    string msg = "Проверка привилегий";
    await PRTelegramBot.Helpers.Message.Send(botclient, update, msg);
}

async Task Router_OnUserStartWithArgs(Telegram.Bot.ITelegramBotClient botclient, Telegram.Bot.Types.Update update, string args)
{
    string msg = "Пользователь отправил старт с аргументом";
    await PRTelegramBot.Helpers.Message.Send(botclient, update, msg);
}
async Task Router_OnWrongTypeMessage(Telegram.Bot.ITelegramBotClient botclient, Telegram.Bot.Types.Update update)
{
    string msg = "Неверный тип сообщения";
    await PRTelegramBot.Helpers.Message.Send(botclient, update, msg);
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