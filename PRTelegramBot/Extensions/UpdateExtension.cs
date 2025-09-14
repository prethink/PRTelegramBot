using PRTelegramBot.Core;
using PRTelegramBot.Models;
using PRTelegramBot.Models.EventsArgs;
using System.Collections.Concurrent;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Методы расширения для update в telegram.
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
                UpdateType.ChatJoinRequest => update.ChatJoinRequest.Chat.Id,
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
        /// Получает идентификатор в формате класса.
        /// </summary>
        /// <param name="update">Update.</param>
        /// <returns>Идентификатор в формате класса</returns>
        public static ChatId GetChatIdClass(this Update update)
        {
            return new ChatId(update.GetChatId());
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
            catch
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
                    bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, ex));
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
            return update.Type switch
            {
                UpdateType.Message => GetFullNameFromChat(update.Message.Chat),
                UpdateType.CallbackQuery => GetFullNameFromChat(update.CallbackQuery.Message.Chat),
                UpdateType.BusinessMessage => GetFullNameFromChat(update.BusinessMessage.Chat),
                UpdateType.ChannelPost => GetFullNameFromChat(update.ChannelPost.Chat),
                UpdateType.ChatBoost => GetFullNameFromChat(update.ChatBoost.Chat),
                UpdateType.ChatJoinRequest => GetFullNameFromChat(update.ChatJoinRequest.Chat),
                UpdateType.ChatMember => GetFullNameFromChat(update.ChatMember.Chat),
                UpdateType.DeletedBusinessMessages => GetFullNameFromChat(update.DeletedBusinessMessages.Chat),
                UpdateType.EditedBusinessMessage => GetFullNameFromChat(update.EditedBusinessMessage.Chat),
                UpdateType.EditedChannelPost => GetFullNameFromChat(update.EditedChannelPost.Chat),
                UpdateType.EditedMessage => GetFullNameFromChat(update.EditedMessage.Chat),
                UpdateType.MessageReaction => GetFullNameFromChat(update.MessageReaction.Chat),
                UpdateType.MessageReactionCount => GetFullNameFromChat(update.MessageReactionCount.Chat),
                UpdateType.MyChatMember => GetFullNameFromChat(update.MyChatMember.Chat),
                UpdateType.PollAnswer => GetFullNameFromChat(update.PollAnswer.VoterChat),
                UpdateType.RemovedChatBoost => GetFullNameFromChat(update.RemovedChatBoost.Chat),
                _ => string.Empty
            };
        }

        /// <summary>
        /// Попытаться получить бота из update.
        /// </summary>
        /// <param name="update">Обновление telegram.</param>
        /// <param name="bot">Возвращаемый объект бота.</param>
        /// <returns>True, если бот найден; иначе False.</returns>
        public static bool TryGetBot(this Update update, out PRBotBase bot)
        {
            return botLink.TryGetValue(update.Id, out bot);
        }

        /// <summary>
        /// Получает идентификатор пользователя из обновления Telegram.
        /// </summary>
        /// <param name="update">Объект обновления Telegram.</param>
        /// <returns>Идентификатор пользователя (UserId).</returns>
        public static long GetUserId(this Update update)
        {
            return update.Type switch
            {
                UpdateType.Message => update.Message.From.Id,
                UpdateType.CallbackQuery => update.CallbackQuery.Message.From.Id,
                UpdateType.BusinessMessage => update.BusinessMessage.From.Id,
                UpdateType.ChannelPost => update.ChannelPost.From.Id,
                UpdateType.ChatJoinRequest => update.ChatJoinRequest.From.Id,
                UpdateType.ChatMember => update.ChatMember.From.Id,
                UpdateType.EditedBusinessMessage => update.EditedBusinessMessage.From.Id,
                UpdateType.EditedChannelPost => update.EditedChannelPost.From.Id,
                UpdateType.EditedMessage => update.EditedMessage.From.Id,
                UpdateType.MyChatMember => update.MyChatMember.From.Id,
                _ => throw new NotImplementedException($"Not implemented get userId for {update.Type}")
            };
        }

        /// <summary>
        /// Связать update с PRBotBase.
        /// </summary>
        /// <param name="update">Обновление telegram.</param>
        /// <param name="bot">Экземпляр PRBotBase.</param>
        /// <returns>True - удалось добавить, False - не удалось.</returns>
        internal static bool AddTelegramClient(this Update update, PRBotBase bot)
        {
            if(update is null) 
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

        /// <summary>
        /// Получить информацию о пользователе из чата.
        /// </summary>
        /// <param name="chat">Чат.</param>
        /// <returns>Информация.</returns>
        private static string GetFullNameFromChat(Chat chat)
        {
            string result = string.Empty;
            result += chat?.Id + " ";
            result += string.IsNullOrEmpty(chat.FirstName) ? string.Empty : chat.FirstName + " ";
            result += string.IsNullOrEmpty(chat?.LastName) ? string.Empty : chat.LastName + " ";
            result += string.IsNullOrEmpty(chat?.Username) ? string.Empty : chat.Username + " ";
            return result;
        }

        #endregion
    }
}
