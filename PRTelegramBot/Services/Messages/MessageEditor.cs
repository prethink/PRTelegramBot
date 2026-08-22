using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Services.Messages
{
    /// <summary>
    /// Edits messages that have already been sent.
    /// </summary>
    public class MessageEditor
    {
        #region Methods

        /// <summary>
        /// Edits the inline menu.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        public static async Task<Telegram.Bot.Types.Message> EditInline(IBotContext context, long chatId, int messageId, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option) as InlineKeyboardMarkup;

            Telegram.Bot.Types.Message message = null;
            if (option?.MenuInlineKeyboardMarkup is not null)
            {
                message = await context.BotClient.EditMessageReplyMarkup(
                    chatId: chatId,
                    messageId: messageId,
                    replyMarkup: replyMarkup,
                    cancellationToken: option.CancellationToken);
            }

            return message;
        }

        /// <summary>
        /// Edits a message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        public static async Task<Telegram.Bot.Types.Message> Edit(IBotContext context, long chatId, int messageId, string text, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option) as InlineKeyboardMarkup;
            var linkOptions = MessageUtils.CreateLinkPreviewOptionsFromOption(option);
            return await context.BotClient.EditMessageText(
                chatId: chatId,
                messageId: messageId,
                text: text,
                parseMode: option.ParseMode,
                replyMarkup: replyMarkup,
                entities: option.Entities,
                linkPreviewOptions: linkOptions,
                cancellationToken: option.CancellationToken);
        }

        /// <summary>
        /// Edits a message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        public static async Task<Telegram.Bot.Types.Message> Edit(IBotContext context, string text, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            long chatId = context.GetChatId();
            int messageId = context.GetMessageId();

            var editMessage = await Edit(context, chatId, messageId, text, option);
            return editMessage;
        }

        #endregion
    }
}
