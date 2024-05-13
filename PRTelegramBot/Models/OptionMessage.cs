using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Вспомогательный класс который хранит настройки для отправки сообщений в телеграме.
    /// </summary>
    public class OptionMessage
    {
        #region Поля и свойства

        /// <summary>
        /// Добавляет Reply меню.
        /// </summary>
        public ReplyKeyboardMarkup MenuReplyKeyboardMarkup { get; set; }

        /// <summary>
        /// Добавляет Inline меню.
        /// </summary>
        public InlineKeyboardMarkup MenuInlineKeyboardMarkup { get; set; }

        /// <summary>
        /// Тип парсинга.
        /// </summary>
        public ParseMode ParseMode { get; set; } = ParseMode.Html;

        /// <summary>
        /// Очищает меню.
        /// </summary>
        public bool ClearMenu { get; set; }

        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        public int? MessageId { get; set; }

        /// <summary>
        /// Проверят что сообщение есть.
        /// </summary>
        /// <returns>True/False</returns>
        public bool HasMessage => !string.IsNullOrWhiteSpace(Message);

        /// <summary>
        /// 
        /// </summary>
        public int? MessageThreadId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? ProtectedContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<MessageEntity>? Entities { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? DisableWebPagePreview { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? DisableNotification { get; set; }        
        
        /// <summary>
        /// 
        /// </summary>
        public bool? DisableContentTypeDetection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ReplyToMessageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? AllowSendingWithoutReply { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Caption { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public InputFile? thumbnail { get; set; } 
        
        /// <summary>
        /// 
        /// </summary>
        public bool? HasSpoiler { get; set; }

        #endregion
    }
}
