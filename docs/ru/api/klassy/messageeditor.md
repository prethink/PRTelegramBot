# MessageEditor

```csharp
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Services.Messages
{
    public class MessageEditor
    {
        #region Методы

        /// <summary>
        /// Редактирование меню inline.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> EditInline(IBotContext context, long chatId, int messageId, OptionMessage option = null)

        /// <summary>
        /// Редактирование сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> Edit(IBotContext context, long chatId, int messageId, string text, OptionMessage option = null)

        /// <summary>
        /// Редактирование сообщения.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> Edit(IBotContext context, string text, OptionMessage option = null)

        #endregion
    }
}

```
