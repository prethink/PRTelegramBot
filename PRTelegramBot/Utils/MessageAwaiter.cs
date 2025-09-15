using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Message = Telegram.Bot.Types.Message;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Создает сообщение перед обработкой, и удаляет после.
    /// </summary>
    public class MessageAwaiter : IDisposable
    {
        #region Поля и свойства

        /// <summary>
        /// Контекст бота.
        /// </summary>
        private IBotContext context;

        /// <summary>
        /// Сообщение.
        /// </summary>
        private Message message;

        /// <summary>
        /// Идентификатор чата.
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

        #region Методы

        /// <summary>
        /// Создать сообщение ожидание перед основной обработкой данных.
        /// </summary>
        /// <param name="messageText">Текст сообщения.</param>
        public async Task CreateAwaitMessage(string messageText)
        {
            message = await context.BotClient.SendMessage(chatId, messageText);
        }

        /// <summary>
        /// Удалить сообщение после всех обработок.
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

        #region Конструкторы
        
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        public MessageAwaiter(IBotContext context, long chatId) 
            : this(context, "⏳ Генерирую ответ...") { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="messageAwaiterText">тест сообщения ожидания.</param>
        public MessageAwaiter(IBotContext context, string messageAwaiterText)
        {
            this.context = context;
            this.chatId = new ChatId(context.GetChatId());
            _ = CreateAwaitMessage(messageAwaiterText);
        }

        #endregion
    }
}
