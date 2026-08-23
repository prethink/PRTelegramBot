# MediaEditor

```csharp
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Services.Media
{
    public class MediaEditor
    {
        #region Методы

        /// <summary>
        /// Редактирование фото. 
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="stream">Поток.</param>
        /// <param name="filename">Название файла.</param>
        /// <param name="option">Настройки сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Telegram.Bot.Types.Message> EditPhoto(IBotContext context, long chatId, int messageId, Stream stream, string filename = "file", OptionMessage option = null)

        /// <summary>
        /// Редактировать inline меню вместе с фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="text">Текст.</param>
        /// <param name="media">Медиа.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> EditWithPhoto(IBotContext context, long chatId, int messageId, string text, InputMedia media, OptionMessage option = null)

        /// <summary>
        /// Редактирование фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="photoPath">Путь к фото.</param>
        /// <param name="option">Настройка сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> EditPhoto(IBotContext context, long chatId, int messageId, string photoPath, OptionMessage option = null)

        /// <summary>
        /// Редактировать текст под фото.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="chatId">Идентификатор чата.</param>
        /// <param name="messageId">Идентификатор сообщения.</param>
        /// <param name="text">Текст.</param>
        /// <param name="option">Параметры сообщения.</param>
        /// <returns>Сообщение.</returns>
        public static async Task<Message> EditCaption(IBotContext context, long chatId, int messageId, string text, OptionMessage option = null)

        #endregion
    }
}

```
