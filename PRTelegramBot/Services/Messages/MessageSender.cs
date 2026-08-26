using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Extensions;
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
                ephemeralMessageParameters: option.EphemeralMessageParameters,
                cancellationToken: option.CancellationToken);
        }


        /// <summary>
        /// Sends an ephemeral message to the user the current update came from.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message setting.</param>
        /// <param name="replaceCallbackQueryMessage">
        /// Shows the message in place of the one whose button was pressed, instead of over it.
        /// Needs a group: a private chat has no shared timeline to replace anything on, and
        /// Telegram answers <c>MESSAGE_ID_INVALID</c> there.
        /// Ignored when the update is not a callback query, and it must stay false for a callback
        /// query that came from an ephemeral message — edit those with the ephemeral edit methods.
        /// </param>
        /// <returns>Message.</returns>
        /// <remarks>
        /// An ephemeral message is shown as an overlay to one user only and is never written to
        /// the chat history, so nobody else in the chat sees it. It is the natural way to answer
        /// a button press privately in a group. They were designed for shared chats, but the
        /// everyday case — answering a button press — works in a private chat with the bot too.
        /// <para>
        /// Telegram accepts one only under these conditions, or answers <c>BOT_NOT_ADMIN</c>:
        /// </para>
        /// <list type="bullet">
        /// <item>within 15 seconds of a callback query, quoting its id — the usual case, and
        /// this method takes the id from the update for you;</item>
        /// <item>within 15 seconds of an incoming ephemeral message, replying to it — also
        /// filled in from the update, through <see cref="OptionMessage.ReplyToEphemeralMessageId"/>;</item>
        /// <item>at any time, to any non-bot member, <b>if the bot is an administrator of the
        /// chat</b> — the only way to start an ephemeral exchange rather than continue one, and
        /// unavailable in a private chat, which has no administrators.</item>
        /// </list>
        /// </remarks>
        public static async Task<Message> SendEphemeral(IBotContext context, string text, OptionMessage? option = null, bool replaceCallbackQueryMessage = false)
        {
            return await SendEphemeral(context, context.Update.GetUserId(), text, option, replaceCallbackQueryMessage);
        }

        /// <summary>
        /// Sends an ephemeral message to a specific user.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="receiverUserId">Identifier of the user who will see the message.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message setting.</param>
        /// <param name="replaceCallbackQueryMessage">
        /// Shows the message in place of the one whose button was pressed, instead of over it.
        /// </param>
        /// <returns>Message.</returns>
        /// <remarks>
        /// Delivery is not guaranteed — a user who is offline may never see the message.
        /// <para>
        /// Writing to somebody who did not trigger the update means there is no callback query to
        /// quote, so this needs the bot to be an administrator of the chat. Without that Telegram
        /// answers <c>BOT_NOT_ADMIN</c>.
        /// </para>
        /// </remarks>
        public static async Task<Message> SendEphemeral(IBotContext context, long receiverUserId, string text, OptionMessage? option = null, bool replaceCallbackQueryMessage = false)
        {
            option = MessageUtils.CreateOptionsIfNull(option);
            var callbackQueryId = context.Update.CallbackQuery?.Id;

            option.EphemeralMessageParameters = new EphemeralMessageParameters
            {
                ReceiverUserId = receiverUserId,
                CallbackQueryId = callbackQueryId,
                ReplaceCallbackQueryMessage = callbackQueryId is not null && replaceCallbackQueryMessage,
            };

            // Replying inside an ephemeral overlay is the second way Telegram lets a bot that
            // is not an administrator send one, so carry the id over when the caller has not
            // chosen a reply target of their own.
            option.ReplyToEphemeralMessageId ??= context.Update.Message?.EphemeralMessageId;

            return await Send(context, context.Update.GetChatId(), text, option);
        }

        /// <summary>
        /// Sends a rich message described with HTML.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="html">Rich message content as HTML.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Message.</returns>
        /// <remarks>
        /// A rich message is a different kind of message from a formatted one: it carries
        /// headings, lists, tables, quotations, dividers and embedded media as structured
        /// blocks rather than as entities over a single run of text.
        /// The HTML accepted here is the rich message dialect, not the one `ParseMode.Html`
        /// understands — see the Bot API's rich message formatting options for the tag list.
        /// </remarks>
        public static async Task<Message> SendRichMessage(IBotContext context, string html, OptionMessage? option = null)
        {
            return await SendRichMessage(context, context.Update.GetChatId(), html, option);
        }

        /// <summary>
        /// Sends a rich message described with HTML to a specific chat.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="html">Rich message content as HTML.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Message.</returns>
        /// <remarks>
        /// The HTML is run through <c>HtmlText.ToInputRichMessage</c>, which also resolves the media
        /// references a round-tripped message carries, so HTML that came back from
        /// <c>RichMessage.ToHtml()</c> can be edited and sent again with its media intact.
        /// </remarks>
        public static async Task<Message> SendRichMessage(IBotContext context, long chatId, string html, OptionMessage? option = null)
        {
            ArgumentNullException.ThrowIfNull(html);
            return await SendRichMessage(context, chatId, HtmlText.ToInputRichMessage(html), option);
        }

        /// <summary>
        /// Sends a rich message that was built by hand.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="richMessage">Rich message to send.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Message.</returns>
        public static async Task<Message> SendRichMessage(IBotContext context, InputRichMessage richMessage, OptionMessage? option = null)
        {
            return await SendRichMessage(context, context.Update.GetChatId(), richMessage, option);
        }

        /// <summary>
        /// Sends a rich message that was built by hand to a specific chat.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="richMessage">Rich message to send.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Message.</returns>
        /// <remarks>
        /// Every option a rich message can carry is mapped the same way it is for an ordinary
        /// message. The ones that describe formatted text — <c>ParseMode</c>, <c>Entities</c>
        /// and <c>DisableWebPagePreview</c> — have no counterpart here, because the blocks
        /// carry their own structure.
        /// </remarks>
        public static async Task<Message> SendRichMessage(IBotContext context, long chatId, InputRichMessage richMessage, OptionMessage? option = null)
        {
            ArgumentNullException.ThrowIfNull(richMessage);

            option = MessageUtils.CreateOptionsIfNull(option);
            var replyMarkup = MessageUtils.GetReplyMarkup(option);
            var replyParams = MessageUtils.CreateReplyParametersFromOptions(option);

            return await context.BotClient.SendRichMessage(
                chatId: chatId,
                richMessage: richMessage,
                replyParameters: replyParams,
                replyMarkup: replyMarkup,
                messageThreadId: option.MessageThreadId,
                disableNotification: option.DisableNotification,
                protectContent: option.ProtectedContent,
                messageEffectId: option.MessageEffectId,
                businessConnectionId: option.BusinessConnectionId,
                allowPaidBroadcast: option.AllowPaidBroadcast,
                directMessagesTopicId: option.DirectMessagesTopicId,
                suggestedPostParameters: option.SuggestedPostParameters,
                ephemeralMessageParameters: option.EphemeralMessageParameters,
                cancellationToken: option.CancellationToken);
        }
        #endregion
    }
}
