using Telegram.Bot;
using Telegram.Bot.Requests;
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
        /// Клиент бота.
        /// </summary>
        private ITelegramBotClient botClient;

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
            message = await botClient.SendTextMessageAsync(chatId, messageText);
        }

        /// <summary>
        /// Удалить сообщение после всех обработок.
        /// </summary>
        public async Task DeleteMessage()
        {
            try
            {
                await botClient.DeleteMessageAsync(new DeleteMessageRequest(chatId, message.MessageId));
            }
            catch (Exception ex) { }
        }

        #endregion

        #region Конструкторы
        
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        public MessageAwaiter(ITelegramBotClient botClient, long chatId) 
            : this(botClient, chatId, "⏳ Генерирую ответ...") { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageAwaiterText">тест сообщения ожидания.</param>
        public MessageAwaiter(ITelegramBotClient botClient, long chatId, string messageAwaiterText)
        {
            this.botClient = botClient;
            this.chatId = new ChatId(chatId);
            _ = CreateAwaitMessage(messageAwaiterText);
        }

        #endregion
    }
}
