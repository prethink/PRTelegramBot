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
        /// Replaces or removes the keyboard on the message identified by the update.
        /// </summary>
        /// <param name="context">Bot context containing the message or callback query.</param>
        /// <param name="keyboard">Replacement inline keyboard, or null to remove it.</param>
        /// <param name="cancellationToken">Token used to cancel the request.</param>
        public static async Task EditInline(IBotContext context, InlineKeyboardMarkup? keyboard, CancellationToken cancellationToken = default)
        {
            if (context.Update.CallbackQuery?.InlineMessageId is string inlineId)
                await context.BotClient.EditMessageReplyMarkup(inlineId, replyMarkup: keyboard, cancellationToken: cancellationToken);
            else
                await context.BotClient.EditMessageReplyMarkup(context.GetChatId(), context.GetMessageId(), replyMarkup: keyboard, cancellationToken: cancellationToken);
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
        /// <returns>The edited chat message, or null when Telegram acknowledges an edit by inline message ID.</returns>
        public static async Task<Telegram.Bot.Types.Message?> Edit(IBotContext context, string text, OptionMessage? option = null)
        {
            option = MessageUtils.CreateOptionsIfNull(option);

            if (context.Update.CallbackQuery?.InlineMessageId is string inlineId)
            {
                await context.BotClient.EditMessageText(
                    inlineMessageId: inlineId,
                    text: text,
                    parseMode: option.ParseMode,
                    replyMarkup: MessageUtils.GetReplyMarkup(option) as InlineKeyboardMarkup,
                    entities: option.Entities,
                    linkPreviewOptions: MessageUtils.CreateLinkPreviewOptionsFromOption(option),
                    cancellationToken: option.CancellationToken);
                return null;
            }

            return await Edit(context, context.GetChatId(), context.GetMessageId(), text, option);
        }

        #endregion
    }
}
