using NLog;
using static PRTelegramBot.Core.TelegramService;
using PRTelegramBot.Extensions;
using PRTelegramBot.Core;
using PRTelegramBot.Configs;
using Telegram.Bot.Types;
using PRTelegramBot.Models;

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

telegram.OnLogCommon += Telegram_OnLogCommon;
telegram.OnLogError += Telegram_OnLogError;
await telegram.Start();

telegram.Handler.Router.OnWrongTypeMessage += Router_OnWrongTypeMessage;
telegram.Handler.Router.OnUserStartWithArgs += Router_OnUserStartWithArgs;
telegram.Handler.Router.OnCheckPrivilege += Router_OnCheckPrivilege;
telegram.Handler.Router.OnMissingCommand += Router_OnMissingCommand;
telegram.Handler.Router.OnWrongTypeChat += Router_OnWrongTypeChat;
telegram.Handler.Router.OnLocationHandle += Router_OnLocationHandle;
telegram.Handler.Router.OnContactHandle += Router_OnContactHandle;
telegram.Handler.Router.OnPollHandle += Router_OnPollHandle;

async Task Router_OnPollHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    //Обработка голосований
}

async Task Router_OnContactHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    //Обработка контактов
}

async Task Router_OnLocationHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
{
    //Обработка локации
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