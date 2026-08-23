# MessageCopier

```csharp
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Services.Messages
{
    public class MessageCopier
    {
        #region Методы

        /// <summary>
        /// Копирует коллекцию сообщений.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="messages">Сообщения.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Коллекция идентификаторов сообщений.</returns>
        public static async Task<List<MessageId>> CopyMessages(IBotContext context, List<Message> messages, long chatId, OptionMessage option = null)

        /// <summary>
        /// Копировать сообщение.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Идентификатор сообщения.</returns>
        public static async Task<MessageId> CopyMessage(IBotContext context, Message message, long chatId, OptionMessage option = null)

        #endregion
    }
}

```
