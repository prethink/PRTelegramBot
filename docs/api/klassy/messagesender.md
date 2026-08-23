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

        #endregion
    }
}

```
