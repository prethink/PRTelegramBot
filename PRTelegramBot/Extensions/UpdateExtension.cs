using PRTelegramBot.Core;
using PRTelegramBot.Models;
using System.Collections.Concurrent;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Методы расширения для update в телеграм.
    /// </summary>
    public static class UpdateExtension
    {
        #region Поля и свойства

        /// <summary>
        /// Словарь для связи update и бота.
        /// </summary>
        static ConcurrentDictionary<long, PRBotBase> botLink = new();

        #endregion

        #region Методы

        /// <summary>
        /// Получает идентификатор чата в зависимости от типа сообщений.
        /// </summary>
        /// <param name="update">Обновление telegram.</param>
        /// <returns>Идентификатор чата.</returns>
        /// <exception cref="NotImplementedException">Выбрасывается если не реализована обработка обновления.</exception>
        public static long GetChatId(this Update update)
        {
            return update.Type switch
            {
                UpdateType.Message => update.Message.Chat.Id,
                UpdateType.CallbackQuery => update.CallbackQuery.Message.Chat.Id,
                _ => throw new NotImplementedException($"Not implemented get chatId for {update.Type}")
            };
        }

        /// <summary>
        /// Получает идентификатор сообщения.
        /// </summary>
        /// <param name="update">Обновление telegram.</param>
        /// <returns>Идентификатор сообщения.</returns>
        /// <exception cref="NotImplementedException">Выбрасывается если не реализована обработка обновления.</exception>
        public static int GetMessageId(this Update update)
        {
            return update.Type switch
            {
                UpdateType.Message => update.Message.MessageId,
                UpdateType.CallbackQuery => update.CallbackQuery.Message.MessageId,
                _ => throw new NotImplementedException($"Not implemented get messageId for {update.Type}")
            };
        }

        /// <summary>
        /// Информация о пользователе.
        /// </summary>
        /// <param name="update">Обновление telegram.</param>
        /// <returns>Информация о пользователе.</returns>
        public static string GetInfoUser(this Update update)
        {
            string result = "";

            result += update?.Message?.Chat?.Id + " ";
            result += string.IsNullOrEmpty(update?.Message?.Chat?.FirstName) ? "" : update.Message.Chat.FirstName + " ";
            result += string.IsNullOrEmpty(update?.Message?.Chat?.LastName) ? "" : update.Message.Chat.LastName + " ";
            result += string.IsNullOrEmpty(update?.Message?.Chat?.Username) ? "" : update.Message.Chat.Username + " ";

            result += update?.CallbackQuery?.Message?.Chat?.Id + " ";
            result += string.IsNullOrEmpty(update?.CallbackQuery?.Message?.Chat?.FirstName) ? "" : update.CallbackQuery.Message.Chat.FirstName + " ";
            result += string.IsNullOrEmpty(update?.CallbackQuery?.Message?.Chat?.LastName) ? "" : update.CallbackQuery.Message.Chat.LastName + " ";
            result += string.IsNullOrEmpty(update?.CallbackQuery?.Message?.Chat?.Username) ? "" : update.CallbackQuery.Message.Chat.Username + " ";

            return result;
        }

        /// <summary>
        /// Попытаться получить бота из update.
        /// </summary>
        /// <param name="update">Обновление телеграм.</param>
        /// <param name="bot">Возвращаемый объект бота.</param>
        /// <returns>True, если бот найден; иначе False.</returns>
        public static bool TryGetBot(this Update update, out PRBotBase bot)
        {
            return botLink.TryGetValue(update.Id, out bot);
        }

        /// <summary>
        /// Связать update с PRBotBase.
        /// </summary>
        /// <param name="update">Обновление telegram.</param>
        /// <param name="bot">Экзпляр PRBotBase.</param>
        /// <returns>True - удалось добавить, False - не удалось.</returns>
        internal static bool AddTelegramClient(this Update update, PRBotBase bot)
        {
            if(update == null) 
                return false;

            return botLink.TryAdd(update.Id, bot);
        }

        /// <summary>
        /// Получить маппинг пользователя и бота.
        /// </summary>
        /// <param name="update">Обновление telegram.</param>
        /// <returns>Сгенерированное значение id+botkey</returns>
        /// <exception cref="KeyNotFoundException">Выбрасывается если ее найден ключ для бота.</exception>
        internal static string GetKeyMappingUserTelegram(this Update update)
        {
            if (botLink.TryGetValue(update.Id, out PRBotBase bot))
                return new UserBotMapping(bot.BotId, update.GetChatId()).GetKey;

            throw new KeyNotFoundException($"Key update {update.Id} not mapped with prbot.");
        }

        /// <summary>
        /// Очистить маппинг update и telegram bot.
        /// </summary>
        /// <param name="update">Обновление telegram.</param>
        /// <returns>True - удалось очистить, False - не удалось.</returns>
        internal static bool ClearTelegramClient(this Update update)
        {
            return botLink.TryRemove(update.Id, out PRBotBase _);
        }

        #endregion
    }
}
