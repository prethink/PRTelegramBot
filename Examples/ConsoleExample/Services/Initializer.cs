using ConsoleExample.Checkers;
using ConsoleExample.Examples.Events;
using ConsoleExample.Models;
using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;

namespace ConsoleExample.Services
{
    /// <summary>
    /// Инициализатор для бота.
    /// </summary>
    public static class Initializer
    {
        /// <summary>
        /// Инициализация событий.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public static void InitEvents(PRBotBase bot)
        {
            // Обработка не правильный тип сообщений
            bot.Events.OnWrongTypeMessage += ExampleEvents.OnWrongTypeMessage;

            // Обработка пользователь написал в чат start с deeplink
            bot.Events.OnUserStartWithArgs += ExampleEvents.OnUserStartWithArgs;

            // Обработка проверка привилегий
            bot.Events.OnCheckPrivilege += ExampleEvents.OnCheckPrivilege;

            // Обработка пропущенной  команды
            bot.Events.OnMissingCommand += ExampleEvents.OnMissingCommand;

            // Обработка если произошла ошибка при выполнение команды
            bot.Events.OnErrorCommand += ExampleEvents.OnErrorCommand;

            // Обработка не верного типа чата
            bot.Events.OnWrongTypeChat += ExampleEvents.OnWrongTypeChat;
        }

        /// <summary>
        /// Инициализация событий логов.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public static void InitLogEvents(PRBotBase bot)
        {
            // Подписка на простые логи.
            bot.Events.OnCommonLog += ExampleLogEvents.OnLogCommon;
            // Подписка на логи с ошибками.
            bot.Events.OnErrorLog += ExampleLogEvents.OnLogError;
        }

        /// <summary>
        /// Инициализация событий для update типа сообщения.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public static void InitMessageEvents(PRBotBase bot)
        {
            // Обработка локаций
            bot.Events.MessageEvents.OnLocationHandle += ExampleMessageEvents.OnLocationHandle;

            // Обработка контактных данных
            bot.Events.MessageEvents.OnContactHandle += ExampleMessageEvents.OnContactHandle;

            // Обработка голосований
            bot.Events.MessageEvents.OnPollHandle += ExampleMessageEvents.OnPollHandle;

            // Обработка WebApps
            bot.Events.MessageEvents.OnWebAppsHandle += ExampleMessageEvents.OnWebAppsHandle;

            // Обработка, когда пользователю отказано в доступе
            bot.Events.OnAccessDenied += ExampleMessageEvents.OnAccessDenied;

            //Обработка сообщения с документом
            bot.Events.MessageEvents.OnDocumentHandle += ExampleMessageEvents.OnDocumentHandle;

            //Обработка сообщения с аудио
            bot.Events.MessageEvents.OnAudioHandle += ExampleMessageEvents.OnAudioHandle;

            //Обработка сообщения с видео
            bot.Events.MessageEvents.OnVideoHandle += ExampleMessageEvents.OnVideoHandle;

            //Обработка сообщения с фото
            bot.Events.MessageEvents.OnPhotoHandle += ExampleMessageEvents.OnPhotoHandle;

            //Обработка сообщения с стикером
            bot.Events.MessageEvents.OnStickerHandle += ExampleMessageEvents.OnStickerHandle;

            //Обработка сообщения с голосовым сообщением
            bot.Events.MessageEvents.OnVoiceHandle += ExampleMessageEvents.OnVoiceHandle;

            //Обработка сообщения с неизвестным типом
            bot.Events.MessageEvents.OnUnknownHandle += ExampleMessageEvents.OnUnknownHandle;

            //Обработка сообщения с местоположением
            bot.Events.MessageEvents.OnVenueHandle += ExampleMessageEvents.OnVenueHandle;

            //Обработка сообщения с игрой
            bot.Events.MessageEvents.OnGameHandle += ExampleMessageEvents.OnGameHandle;

            //Обработка сообщения с видеозаметкой
            bot.Events.MessageEvents.OnVideoNoteHandle += ExampleMessageEvents.OnVideoNoteHandle;

            //Обработка сообщения с игральной костью
            bot.Events.MessageEvents.OnDiceHandle += ExampleMessageEvents.OnDiceHandle;
        }

        /// <summary>
        /// Инициализация событий для типов update.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public static void InitUpdateEvents(PRBotBase bot)
        {
            // Обработка до всех update 
            bot.Events.UpdateEvents.OnPreUpdate += ExampleUpdateEvents.Handler_OnUpdate;

            // Обработка после всех update
            bot.Events.UpdateEvents.OnPostUpdate += ExampleUpdateEvents.Handler_OnPostUpdate;

            //Обработка обновления изменения группы/чата
            bot.Events.UpdateEvents.OnMyChatMemberHandle += ExampleUpdateEvents.OnUpdateMyChatMember;
        }

        /// <summary>
        /// Инициализация новых команд.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public static void InitCommands(PRBotBase bot)
        {
            bot.Register.AddInlineCommand(AddCustomTHeader.TestAddCommand, async (botClient, update) =>
            {
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Тест метода TestAddCommand");
            });

            bot.Register.AddInlineCommand(AddCustomTHeader.TestAddCommandTwo, async (botClient, update) =>
            {
                await PRTelegramBot.Helpers.Message.Send(botClient, update, "Тест метода TestAddCommandTwo");
            });
        }

        /// <summary>
        /// Получить список динамических команд из json файла.
        /// </summary>
        /// <returns>Команды ключ-значение.</returns>
        public static Dictionary<string, string> GetDynamicCommands()
        {
            var botJsonProvider = new BotConfigJsonProvider(".\\Configs\\commands.json");
            return botJsonProvider.GetKeysAndValues();
        }

        /// <summary>
        /// Получить чекеры для команд.
        /// </summary>
        /// <returns>Список чекеров.</returns>
        public static List<InternalChecker> GetCommandChekers()
        {
            var checkerReplyCommand = new InternalChecker(CommandType.Reply, new ReplyExampleChecker());
            var adminChecker = new InternalChecker(new List<CommandType>() { CommandType.Reply, CommandType.NextStep, CommandType.Inline, CommandType.DynamicReply, CommandType.Slash }, new AdminExampleChecher());
            return new List<InternalChecker>() { checkerReplyCommand, adminChecker };
        }

        /// <summary>
        /// Получить список путей конфигурационных файлов.
        /// </summary>
        /// <returns>Путь до файлов ключ-значение.</returns>
        public static Dictionary<string, string> GetConfigPaths()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json");
            dictionary.Add(ExampleConstants.MESSAGES_FILE_KEY, ".\\Configs\\messages.json");
            return dictionary;
        }
    }
}
