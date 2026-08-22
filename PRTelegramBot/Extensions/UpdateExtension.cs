using PRTelegramBot.Core;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Models;
using PRTelegramBot.Models.InlineButtons;
using System.Collections.Concurrent;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Extension methods for Telegram updates.
    /// </summary>
    public static class UpdateExtension
    {
        #region Fields and properties

        /// <summary>
        /// Dictionary that links an update with its bot.
        /// </summary>
        static ConcurrentDictionary<long, PRBotBase> botLink = new();

        #endregion

        #region Methods

        /// <summary>
        /// Gets the chat identifier depending on the message type.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <returns>Chat identifier.</returns>
        /// <exception cref="NotImplementedException">Thrown when handling of the update is not implemented.</exception>
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
        /// Gets the identifier as a class.
        /// </summary>
        /// <param name="update">Update.</param>
        /// <returns>The identifier as a class</returns>
        public static ChatId GetChatIdClass(this Update update)
        {
            return new ChatId(update.GetChatId());
        }

        /// <summary>
        /// Tries to get the chat identifier.
        /// </summary>
        /// <param name="update">Update.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <returns>True if it was retrieved; false otherwise.</returns>
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
        /// Gets the message identifier.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <returns>Message identifier.</returns>
        /// <exception cref="NotImplementedException">Thrown when handling of the update is not implemented.</exception>
        public static int GetMessageId(this Update update)
        {
            return update.Type switch
            {
                UpdateType.Message => update.Message.MessageId,
                UpdateType.CallbackQuery => update.CallbackQuery.Message.MessageId,
                //TODO: messageId still needs work
                _ => throw new NotImplementedException($"Not implemented get messageId for {update.Type}")
            };
        }

        /// <summary>
        /// Whether the identifier belongs to a private user chat.
        /// </summary>
        /// <param name="update">Update.</param>
        /// <returns>True for yes; False for no.</returns>
        public static bool IsUserChatId(this Update update)
        {
            try 
            {
                return update.GetChatId() > 0;
            }
            catch(Exception ex) 
            {
                CurrentScope.Bot.GetLogger(typeof(UpdateExtension)).LogErrorInternal(ex);
                return false;
            }
        }

        /// <summary>
        /// Information about the user.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <returns>Information about the user.</returns>
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
        /// Tries to get the bot from the update.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <param name="bot">The returned bot object.</param>
        /// <returns>True if the bot was found; otherwise False.</returns>
        public static bool TryGetBot(this Update update, out PRBotBase bot)
        {
            return botLink.TryGetValue(update.Id, out bot);
        }

        /// <summary>
        /// Gets the user identifier from the Telegram update.
        /// </summary>
        /// <param name="update">The Telegram update object.</param>
        /// <returns>The user identifier (UserId).</returns>
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
        /// Links the update with a PRBotBase.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <param name="bot">The PRBotBase instance.</param>
        /// <returns>True if it was added; False if it was not.</returns>
        internal static bool AddTelegramClient(this Update update, PRBotBase bot)
        {
            if(update is null) 
                return false;

            return botLink.TryAdd(update.Id, bot);
        }

        /// <summary>
        /// Gets the mapping between the user and the bot.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <returns>The generated id+botkey value</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no key is found for the bot.</exception>
        internal static string GetKeyMappingUserTelegram(this Update update)
        {
            if (botLink.TryGetValue(update.Id, out PRBotBase bot))
                return new UserBotMapping(bot.BotId, update.GetChatId()).GetKey;

            throw new KeyNotFoundException($"Key update {update.Id} not mapped with prbot.");
        }


        internal static string GetInlineKey(this Update update, Enum @enum)
        {
            return update.GetKeyMappingUserTelegram() + "-" + Convert.ToInt32(@enum);
        }

        /// <summary>
        /// Clears the mapping between the update and the Telegram bot.
        /// </summary>
        /// <param name="update">Telegram update.</param>
        /// <returns>True if it was cleared; False if it was not.</returns>
        internal static bool ClearTelegramClient(this Update update)
        {
            return botLink.TryRemove(update.Id, out PRBotBase _);
        }

        /// <summary>
        /// Gets information about the user from the chat.
        /// </summary>
        /// <param name="chat">Chat.</param>
        /// <returns>Information.</returns>
        private static string GetFullNameFromChat(Chat chat)
        {
            List<string> infos = [chat.Id.ToString(), chat.FirstName, chat.LastName, chat.Username];
            infos = infos.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            return string.Join(' ', infos);
        }

        #endregion
    }
}
