using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Message = Telegram.Bot.Types.Message;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Sends a message before processing and deletes it afterwards.
    /// </summary>
    public class MessageAwaiter : IDisposable
    {
        #region Fields and properties

        /// <summary>
        /// Bot context.
        /// </summary>
        private IBotContext context;

        /// <summary>
        /// Message.
        /// </summary>
        private Message message;

        /// <summary>
        /// Chat identifier.
        /// </summary>
        private ChatId chatId;

        #endregion

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            _ = DeleteMessage();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends a waiting message before the main data processing.
        /// </summary>
        /// <param name="messageText">Message text.</param>
        public async Task CreateAwaitMessage(string messageText)
        {
            message = await context.BotClient.SendMessage(chatId, messageText);
        }

        /// <summary>
        /// Deletes the message once all processing is done.
        /// </summary>
        public async Task DeleteMessage()
        {
            try
            {
                await context.BotClient.DeleteMessage(chatId, message.MessageId);
            }
            catch (Exception ex) { }
        }

        #endregion

        #region Constructors
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="chatId">Chat identifier.</param>
        public MessageAwaiter(IBotContext context, long chatId) 
            : this(context, "⏳ Generating a reply...") { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="messageAwaiterText">Text of the waiting message.</param>
        public MessageAwaiter(IBotContext context, string messageAwaiterText)
        {
            this.context = context;
            this.chatId = new ChatId(context.GetChatId());
            _ = CreateAwaitMessage(messageAwaiterText);
        }

        #endregion
    }
}
