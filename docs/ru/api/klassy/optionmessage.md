# OptionMessage

```csharp
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
        /// Идентификатор темы/канала.
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
        /// Идентификатор входящего эфемерного сообщения, на которое отвечаем.
        /// Ответ на эфемерное сообщение сам обязан быть эфемерным, и Telegram принимает
        /// его только в течение 15 секунд после исходного.
        /// </summary>
        public int? ReplyToEphemeralMessageId { get; set; }

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
        public InputFile? Thumbnail { get; set; }

        /// <summary>
        /// Признак наличие спойлера в сообщении.
        /// </summary>
        public bool? HasSpoiler { get; set; }

        /// <summary>
        /// Уникальный идентификатор бизнес-подключения, от имени которого отправляется сообщение.
        /// </summary>
        public string? BusinessConnectionId { get; set; }

        /// <summary>
        /// Уникальный идентификатор эффекта, добавляемого к сообщению. Только личные чаты.
        /// </summary>
        public string? MessageEffectId { get; set; }

        /// <summary>
        /// Разрешает до 1000 сообщений в секунду в обход лимитов рассылки, за плату
        /// в Telegram Stars, списываемую с баланса бота.
        /// </summary>
        public bool AllowPaidBroadcast { get; set; }

        /// <summary>
        /// Идентификатор топика личных сообщений, в который отправляется сообщение.
        /// Обязателен, когда сообщение идёт в чат личных сообщений.
        /// </summary>
        public long? DirectMessagesTopicId { get; set; }

        /// <summary>
        /// Параметры предлагаемого поста. Только чаты личных сообщений.
        /// </summary>
        public SuggestedPostParameters? SuggestedPostParameters { get; set; }

        /// <summary>
        /// Показывает подпись над медиа, а не под ним.
        /// Применяется к фото, копируемым сообщениям и правкам подписи.
        /// </summary>
        public bool ShowCaptionAboveMedia { get; set; }

        /// <summary>
        /// Параметры отправляемого эфемерного сообщения.
        /// Эфемерное сообщение показывается одному пользователю поверх чата и не попадает
        /// в историю. Обычный long приводится к этому типу неявно.
        /// </summary>
        public EphemeralMessageParameters? EphemeralMessageParameters { get; set; }

        #endregion
    }
}

```
