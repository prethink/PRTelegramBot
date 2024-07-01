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
                UpdateType.BusinessConnection => update.BusinessConnection.UserChatId,
                UpdateType.BusinessMessage => update.BusinessMessage.Chat.Id,
                UpdateType.ChannelPost => update.ChannelPost.Chat.Id,
                UpdateType.ChatBoost => update.ChatBoost.Chat.Id,
                UpdateType.ChatJoinRequest => update.ChatJoinRequest.UserChatId,
                UpdateType.ChatMember => update.ChatMember.Chat.Id,
                UpdateType.DeletedBusinessMessages => update.DeletedBusinessMessages.Chat.Id,
                UpdateType.EditedBusinessMessage => update.EditedBusinessMessage.Chat.Id,
                UpdateType.EditedChannelPost => update.EditedChannelPost.Chat.Id,
                UpdateType.EditedMessage => update.EditedMessage.Chat.Id,
                UpdateType.MessageReaction => update.MessageReaction.Chat.Id,
                UpdateType.MessageReactionCount => update.MessageReactionCount.Chat.Id,
                UpdateType.MyChatMember => update.MyChatMember.Chat.Id,
                UpdateType.PollAnswer => update.PollAnswer.VoterChat.Id,
                UpdateType.RemovedChatBoost => update.RemovedChatBoost.Chat.Id,
                _ => throw new NotImplementedException($"Not implemented get chatId for {update.Type}")
            }; 
        }

        /// <summary>
        /// Попытаться получить идентификатор чата.
        /// </summary>
        /// <param name="update">Update.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <returns>True - удалось получить, false - нет.</returns>
        public static bool TryGetChatId(this Update update, out long chatId)
        {
            chatId = 0;
            try
            {
                chatId = update.GetChatId();
                return true;
            }
            catch(Exception e) 
            {
                return false;
            }
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
                //TODO: Доработка messageId
                _ => throw new NotImplementedException($"Not implemented get messageId for {update.Type}")
            };
        }

        /// <summary>
        /// Является ли идентификатор пользователским чатом.
        /// </summary>
        /// <param name="update">Update.</param>
        /// <returns>True - да, False - нет.</returns>
        public static bool IsUserChatId(this Update update)
        {
            try 
            {
                return update.GetChatId() > 0;
            }
            catch(Exception ex) 
            {
                if(update.TryGetBot(out var bot))
                    bot.Events.OnErrorLogInvoke(ex);
                return false;
            }
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
