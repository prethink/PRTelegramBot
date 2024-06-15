using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Вспомогательный класс который хранит настройки для отправки сообщений в телеграме.
    /// </summary>
    public sealed class OptionMessage
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
        /// <returns>True - есть сообщение, False - нет сообщения.</returns>
        public bool HasMessage => !string.IsNullOrWhiteSpace(Message);

        /// <summary>
        /// Проверяет, что сообщение есть.
        /// </summary>
        public int? MessageThreadId { get; set; }

        /// <summary>
        /// Указывает, что контент сообщения защищен.
        /// </summary>
        public bool? ProtectedContent { get; set; }

        /// <summary>
        /// Токен отмены.
        /// </summary>
        public CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// Сущности сообщения.
        /// </summary>
        public IEnumerable<MessageEntity>? Entities { get; set; }

        /// <summary>
        /// Отключает предварительный просмотр веб-страниц.
        /// </summary>
        public bool? DisableWebPagePreview { get; set; }

        /// <summary>
        /// Отключает уведомления.
        /// </summary>
        public bool? DisableNotification { get; set; }

        /// <summary>
        /// Отключает обнаружение типа контента.
        /// </summary>
        public bool? DisableContentTypeDetection { get; set; }

        /// <summary>
        /// Идентификатор сообщения, на которое следует ответить.
        /// </summary>
        public int? ReplyToMessageId { get; set; }

        /// <summary>
        /// Разрешает отправку без ответа.
        /// </summary>
        public bool? AllowSendingWithoutReply { get; set; }

        /// <summary>
        /// Заголовок сообщения.
        /// </summary>
        public string? Caption { get; set; }

        /// <summary>
        /// Миниатюра сообщения.
        /// </summary>
        public InputFile? thumbnail { get; set; }

        /// <summary>
        /// Признак наличие спойлера в сообщении.
        /// </summary>
        public bool? HasSpoiler { get; set; }

        #endregion
    }
}
