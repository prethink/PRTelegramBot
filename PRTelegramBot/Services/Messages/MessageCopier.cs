using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Services.Messages
{
    /// <summary>
    /// Copies messages between chats.
    /// </summary>
    public class MessageCopier
    {
        #region Methods

        /// <summary>
        /// Copies a collection of messages.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="messages">Messages.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="option">Message parameters.</param>
        /// <returns>Collection of message identifiers.</returns>
        public static async Task<List<MessageId>> CopyMessages(IBotContext context, List<Message> messages, long chatId, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            List<MessageId> messageIds = new List<MessageId>();

            foreach (var message in messages)
                messageIds.Add(await CopyMessage(context, message, chatId, option));

            return messageIds;
        }

        /// <summary>
        /// Copies the message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="message">Message.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="option">Message parameters.</param>
        /// <returns>Message identifier.</returns>
        public static async Task<MessageId> CopyMessage(IBotContext context, Message message, long chatId, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option);
            var replyParams = MessageUtils.CreateReplyParametersFromOptions(option);
            ChatId toMsg = new ChatId(chatId);
            ChatId fromMsg = new ChatId(message.Chat.Id);

            var messageId = await context.BotClient.CopyMessage(
                chatId: toMsg,
                fromChatId: fromMsg,
                messageId: message.MessageId,
                messageThreadId: option.MessageThreadId,
                caption: option.Caption,
                parseMode: option.ParseMode,
                captionEntities: option.Entities,
                disableNotification: option.DisableNotification,
                protectContent: option.ProtectedContent,
                replyParameters: replyParams,
                replyMarkup: replyMarkup,
                messageEffectId: option.MessageEffectId,
                allowPaidBroadcast: option.AllowPaidBroadcast,
                directMessagesTopicId: option.DirectMessagesTopicId,
                suggestedPostParameters: option.SuggestedPostParameters,
                cancellationToken: option.CancellationToken);
            return messageId;
        }

        #endregion
    }
}
