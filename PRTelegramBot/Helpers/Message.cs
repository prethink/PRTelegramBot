using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Media;
using PRTelegramBot.Services.Messages;
using Telegram.Bot.Types;

namespace PRTelegramBot.Helpers
{
    /// <summary>
    /// Obsolete facade over the message and media services.
    /// </summary>
    /// <remarks>Kept for backward compatibility. Use MessageSender, MessageEditor, MessageDeleter, MediaSender and MediaEditor instead.</remarks>
    [Obsolete($"This class is obsolete. Look at {nameof(MessageSender)}, {nameof(MessageEditor)}, {nameof(MessageDeleter)}, {nameof(MediaEditor)}, {nameof(MediaSender)} instead")]
    public class Message
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
        [Obsolete($"Use {nameof(MessageCopier)}.{nameof(MessageCopier.CopyMessages)}")]
        public static async Task<List<MessageId>> CopyMessages(IBotContext context, List<Telegram.Bot.Types.Message> messages, long chatId, OptionMessage? option = null)
        {
            return await MessageCopier.CopyMessages(context, messages, chatId, option);
        }

        /// <summary>
        /// Copies the message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="message">Message.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="option">Message parameters.</param>
        /// <returns>Message identifier.</returns>
        [Obsolete($"Use {nameof(MessageCopier)}.{nameof(MessageCopier.CopyMessage)}")]
        public static async Task<MessageId> CopyMessage(IBotContext context, Telegram.Bot.Types.Message message, long chatId, OptionMessage? option = null)
        {
            return await MessageCopier.CopyMessage(context, message, chatId, option);
        }

        /// <summary>
        /// The waiting message shown while the message is processed.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="message">Message text.</param>
        /// <param name="option">Message parameters.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MessageSender)}.{nameof(MessageSender.AwaitAnswerBot)}")]
        public static async Task<Telegram.Bot.Types.Message> AwaitAnswerBot(IBotContext context, long chatId, string message = "⏳ Generating a reply...", OptionMessage? option = null)
        {
            return await MessageSender.AwaitAnswerBot(context, chatId, message, option);
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="update">Telegram update.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MessageSender)}.{nameof(MessageSender.Send)}")]
        public static async Task<Telegram.Bot.Types.Message> Send(IBotContext context, Update update, string text, OptionMessage? option = null)
        {
            return await MessageSender.Send(context, update, text, option);
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MessageSender)}.{nameof(MessageSender.Send)}")]
        public static async Task<Telegram.Bot.Types.Message> Send(IBotContext context, string text, OptionMessage? option = null)
        {
            return await MessageSender.Send(context, text, option);
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MessageSender)}.{nameof(MessageSender.Send)}")]
        public static async Task<Telegram.Bot.Types.Message> Send(IBotContext context, long chatId, string text, OptionMessage? option = null)
        {
            return await MessageSender.Send(context, chatId, text, option);
        }

        /// <summary>
        /// Sends a group of photos.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="filepaths">Paths to the files.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Collection of messages.</returns>
        [Obsolete($"Use {nameof(MediaSender)}.{nameof(MediaSender.SendPhotoGroup)}")]
        public static async Task<Telegram.Bot.Types.Message[]> SendPhotoGroup(IBotContext context, long chatId, string text, List<string> filepaths, OptionMessage? option = null)
        {
            return await MediaSender.SendPhotoGroup(context, chatId, text, filepaths, option);
        }

        /// <summary>
        /// Sends a message with a photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="filePath">Path to the file.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MediaSender)}.{nameof(MediaSender.SendPhoto)}")]
        public static async Task<Telegram.Bot.Types.Message> SendPhoto(IBotContext context, long chatId, string text, string filePath, OptionMessage? option = null)
        {
            return await MediaSender.SendPhoto(context, chatId, text, filePath, option);
        }

        /// <summary>
        /// Sends a file.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="filePath">Path to the file.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MediaSender)}.{nameof(MediaSender.SendFile)}")]
        public static async Task<Telegram.Bot.Types.Message> SendFile(IBotContext context, long chatId, string text, string filePath, OptionMessage? option = null)
        {
            return await MediaSender.SendFile(context, chatId, text, filePath, option);
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
        [Obsolete($"Use {nameof(MessageEditor)}.{nameof(MessageEditor.Edit)}")]
        public static async Task<Telegram.Bot.Types.Message> Edit(IBotContext context, long chatId, int messageId, string text, OptionMessage? option = null)
        {
            return await MessageEditor.Edit(context, chatId, messageId, text, option);
        }

        /// <summary>
        /// Edits a message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MessageEditor)}.{nameof(MessageEditor.Edit)}")]
        public static async Task<Telegram.Bot.Types.Message> Edit(IBotContext context, string text, OptionMessage? option = null)
        {
            return await MessageEditor.Edit(context, text, option);
        }

        /// <summary>
        /// Edits the caption under the photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="option">Message parameters.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MediaEditor)}.{nameof(MediaEditor.EditCaption)}")]
        public static async Task<Telegram.Bot.Types.Message> EditCaption(IBotContext context, long chatId, int messageId, string text, OptionMessage? option = null)
        {
            return await MediaEditor.EditCaption(context, chatId, messageId, text, option);
        }

        /// <summary>
        /// Edits a photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="photoPath">Path to the photo.</param>
        /// <param name="option">Message setting.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MediaEditor)}.{nameof(MediaEditor.EditPhoto)}")]
        public static async Task<Telegram.Bot.Types.Message> EditPhoto(IBotContext context, long chatId, int messageId, string photoPath, OptionMessage? option = null)
        {
            return await MediaEditor.EditPhoto(context, chatId, messageId, photoPath, option);
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="option">Message setting.</param>
        [Obsolete($"Use {nameof(MessageDeleter)}.{nameof(MessageDeleter.DeleteMessage)}")]
        public static async Task DeleteMessage(IBotContext context, long chatId, int messageId, OptionMessage? option = null)
        {
            await MessageDeleter.DeleteMessage(context, chatId, messageId, option);
        }

        /// <summary>
        /// Sends a message with a photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="stream">Stream.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MediaSender)}.{nameof(MediaSender.SendPhoto)}")]
        public static async Task<Telegram.Bot.Types.Message> SendPhoto(IBotContext context, long chatId, string text, Stream stream, OptionMessage? option = null)
        {
            return await MediaSender.SendPhoto(context, chatId, text, stream, option);
        }

        /// <summary>
        /// Sends a message with a photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="msg">Text.</param>
        /// <param name="url">url.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MediaSender)}.{nameof(MediaSender.SendPhotoWithUrl)}")]
        public static async Task<Telegram.Bot.Types.Message> SendPhotoWithUrl(IBotContext context, long chatId, string msg, string url, OptionMessage? option = null)
        {
            return await MediaSender.SendPhotoWithUrl(context, chatId, msg, url, option);
        }

        /// <summary>
        /// Sends a message with a photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="msg">Text.</param>
        /// <param name="url">url.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MediaSender)}.{nameof(MediaSender.SendMediaWithUrl)}")]
        public static async Task<Telegram.Bot.Types.Message> SendMediaWithUrl(IBotContext context, long chatId, string msg, string url, OptionMessage? option = null)
        {
            return await MediaSender.SendMediaWithUrl(context, chatId, msg, url, option);
        }

        /// <summary>
        /// Edits the inline menu.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MessageEditor)}.{nameof(MessageEditor.EditInline)}")]
        public static async Task<Telegram.Bot.Types.Message> EditInline(IBotContext context, long chatId, int messageId, OptionMessage? option = null)
        {
            return await MessageEditor.EditInline(context, chatId, messageId, option);  
        }

        /// <summary>
        /// Edits a photo. 
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="stream">Stream.</param>
        /// <param name="filename">File name.</param>
        /// <param name="option">Message options.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MediaEditor)}.{nameof(MediaEditor.EditPhoto)}")]
        public static async Task<Telegram.Bot.Types.Message> EditPhoto(IBotContext context, long chatId, int messageId, Stream stream, string filename = "file", OptionMessage? option = null)
        {
            return await MediaEditor.EditPhoto(context, chatId, messageId, stream, filename, option);
        }

        /// <summary>
        /// Edits the inline menu together with the photo.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        /// <param name="messageId">Message identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="media">Media.</param>
        /// <param name="option">Message parameters.</param>
        /// <returns>Message.</returns>
        [Obsolete($"Use {nameof(MediaEditor)}.{nameof(MediaEditor.EditWithPhoto)}")]
        public static async Task<Telegram.Bot.Types.Message> EditWithPhoto(IBotContext context, long chatId, int messageId, string text, InputMedia media, OptionMessage? option = null)
        {
            return await MediaEditor.EditWithPhoto(context, chatId, messageId, text, media, option);
        }


        /// <summary>
        /// Shows a notification to the user.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="callbackQueryId">Callback identifier.</param>
        /// <param name="text">Text.</param>
        /// <param name="showAlert">Whether to show an alert.</param>
        /// <param name="url">.</param>
        /// <param name="cacheTime">.</param>
        /// <returns>Task</returns>
        [Obsolete($"Use {nameof(MessageNotification)}.{nameof(MessageNotification.NotifyFromCallBack)}")]
        public static async Task NotifyFromCallBack(
            IBotContext context,
            string callbackQueryId,
            string text,
            bool showAlert = true,
            string? url = null,
            int? cacheTime = null)
        {
            await MessageNotification.NotifyFromCallBack(context, callbackQueryId, text, showAlert, url, cacheTime);
        }

        #endregion
    }
}
