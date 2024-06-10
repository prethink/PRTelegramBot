using PRTelegramBot.Core;
using PRTelegramBot.Models;
using System.Collections.Concurrent;
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
        /// Словарь для работы который хранит идентификатор пользователя и его кеш
        /// </summary>
        static ConcurrentDictionary<long, PRBotBase> botLink = new();

        #endregion

        #region Методы

        /// <summary>
        /// Получает ChatId в зависимости от типа сообщений
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <returns>ChatId</returns>
        /// <exception cref="Exception">Не найден тип сообщения</exception>
        public static long GetChatId(this Update update)
        {
            return update.Type switch
            {
                UpdateType.Message => update.Message.Chat.Id,
                UpdateType.CallbackQuery => update.CallbackQuery.Message.Chat.Id,
                _ => throw new Exception("Failed to obtain chat id")
            };
        }

        /// <summary>
        /// Получает идентификатор сообщения
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <returns>Идентификатор сообщения</returns>
        /// <exception cref="Exception">Не найден тип сообщения</exception>
        public static int GetMessageId(this Update update)
        {
            return update.Type switch
            {
                UpdateType.Message => update.Message.MessageId,
                UpdateType.CallbackQuery => update.CallbackQuery.Message.MessageId,
                _ => throw new Exception("Failed to obtain message id")
            };
        }

        /// <summary>
        /// Информация о пользователе
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <returns>Информация о пользователе</returns>
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
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <param name="telegramBot"></param>
        /// <returns></returns>
        public static bool AddTelegramClient(this Update update, PRBotBase bot)
        {
            return botLink.TryAdd(update.Id, bot);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static string GetKeyMappingUserTelegram(this Update update)
        {
            if (botLink.TryGetValue(update.Id, out PRBotBase bot))
                return new UserBotMapping(bot.BotId, update.GetChatId()).GetKey;

            throw new KeyNotFoundException($"Key update {update.Id} not mapped with prbot.");
        }

        /// <summary>
        /// Очистить маппинг update и telegram bot.
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public static bool ClearTelegramClient(this Update update)
        {
            return botLink.TryRemove(update.Id, out PRBotBase _);
        }

        #endregion
    }
}
