using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Services.Messages
{
    /// <summary>
    /// Sends messages to Telegram.
    /// </summary>
    public class MessageSender
    {
        #region Methods

        /// <summary>
        /// The waiting message shown while the message is processed.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="message">Message text.</param>
        /// <param name="option">Message parameters.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> AwaitAnswerBot(IBotContext context, long chatId, string message = "⏳ Generating a reply...", OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var sentMessage = await Send(context, chatId, message, option);
            return sentMessage;
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="update">Telegram update.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> Send(IBotContext context, Update update, string text, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            var message = await Send(context, update.GetChatId(), text, option);
            return message;
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> Send(IBotContext context, string text, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            var message = await Send(context, context.Update.GetChatId(), text, option);
            return message;
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> Send(IBotContext context, long chatId, string text, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option);
            var replyParams = MessageUtils.CreateReplyParametersFromOptions(option);
            var linkOptions = MessageUtils.CreateLinkPreviewOptionsFromOption(option);

            if (text.Length > PRConstants.MAX_MESSAGE_LENGTH)
            {
                var chunk = MessageUtils.SplitIntoChunks(text, PRConstants.MAX_MESSAGE_LENGTH);
                int count = 0;
                foreach (var item in chunk)
                {
                    count++;
                    if (count < chunk.Count)
                        await Send(context, chatId, item, option);
                    if (count == chunk.Count)
                        text = item;
                }
            }

            return await context.BotClient.SendMessage(
                chatId: chatId,
                text: text,
                parseMode: option.ParseMode,
                replyMarkup: replyMarkup,
                messageThreadId: option.MessageThreadId,
                entities: option.Entities,
                linkPreviewOptions: linkOptions,
                disableNotification: option.DisableNotification,
                protectContent: option.ProtectedContent,
                replyParameters: replyParams,
                messageEffectId: option.MessageEffectId,
                businessConnectionId: option.BusinessConnectionId,
                allowPaidBroadcast: option.AllowPaidBroadcast,
                directMessagesTopicId: option.DirectMessagesTopicId,
                suggestedPostParameters: option.SuggestedPostParameters,
                cancellationToken: option.CancellationToken);
        }

        #endregion
    }
}
