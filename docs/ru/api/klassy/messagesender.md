# MessageSender

```csharp
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Services.Messages
{
    public class MessageSender
    {
        #region Методы

        /// <summary>
        /// Сообщение ожидание обработки сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> AwaitAnswerBot(IBotContext context, long chatId, string message = "⏳ Генерирую ответ...", OptionMessage option = null)

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="update">Обновление телерграм.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> Send(IBotContext context, Update update, string text, OptionMessage option = null)

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> Send(IBotContext context, string text, OptionMessage option = null)

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> Send(IBotContext context, long chatId, string text, OptionMessage option = null)

        /// <summary>
        /// Отправка эфемерного сообщения пользователю, от которого пришёл текущий update.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <param name="replaceCallbackQueryMessage">Показать сообщение вместо исходного, а не поверх него.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendEphemeral(IBotContext context, string text, OptionMessage? option = null, bool replaceCallbackQueryMessage = false)

        /// <summary>
        /// Отправка эфемерного сообщения конкретному пользователю.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="receiverUserId">Идентификатор пользователя, который увидит сообщение.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <param name="replaceCallbackQueryMessage">Показать сообщение вместо исходного, а не поверх него.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendEphemeral(IBotContext context, long receiverUserId, string text, OptionMessage? option = null, bool replaceCallbackQueryMessage = false)

        /// <summary>
        /// Отправка rich-сообщения, описанного через HTML.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="html">Содержимое rich-сообщения в виде HTML.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendRichMessage(IBotContext context, string html, OptionMessage? option = null)

        /// <summary>
        /// Отправка rich-сообщения, описанного через HTML, в конкретный чат.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="html">Содержимое rich-сообщения в виде HTML.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendRichMessage(IBotContext context, long chatId, string html, OptionMessage? option = null)

        /// <summary>
        /// Отправка rich-сообщения, собранного вручную.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="richMessage">Отправляемое rich-сообщение.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendRichMessage(IBotContext context, InputRichMessage richMessage, OptionMessage? option = null)

        /// <summary>
        /// Отправка rich-сообщения, собранного вручную, в конкретный чат.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="richMessage">Отправляемое rich-сообщение.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> SendRichMessage(IBotContext context, long chatId, InputRichMessage richMessage, OptionMessage? option = null)

        #endregion
    }
}

```
