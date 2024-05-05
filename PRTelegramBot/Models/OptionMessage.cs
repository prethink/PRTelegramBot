using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Вспомогательный класс который хранит настройки для отправки сообщений в телеграме
    /// </summary>
    public class OptionMessage
    {
        /// <summary>
        /// Добавляет Reply меню
        /// </summary>
        public ReplyKeyboardMarkup MenuReplyKeyboardMarkup { get; set; }

        /// <summary>
        /// Добавляет Inline меню
        /// </summary>
        public InlineKeyboardMarkup MenuInlineKeyboardMarkup { get; set; }

        /// <summary>
        /// Тип парсинга
        /// </summary>
        public ParseMode ParseMode { get; set; } = ParseMode.Html;

        /// <summary>
        /// Очищает меню
        /// </summary>
        public bool ClearMenu { get; set; } = false;

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        public int? MessageId { get; set; }

        /// <summary>
        /// Проверят что сообщение есть
        /// </summary>
        /// <returns>True/False</returns>
        public bool HasMessage => !string.IsNullOrWhiteSpace(Message);
    }
}
