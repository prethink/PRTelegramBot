using System.Globalization;
using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Команда для передачи даты из календаря.
    /// </summary>
    public class CalendarTCommand : TCommandBase
    {
        #region Поля и свойства

        /// <summary>
        /// Дата.
        /// </summary>
        [JsonPropertyName("1")]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime Date { get; set; }

        [JsonPropertyName("2")]
        public string Culture { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="date">Дата.</param>
        public CalendarTCommand(DateTime date)
            : this(date, CultureInfo.GetCultureInfo("ru-RU", false), 0)
        {
            Date = date;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="headerCallbackCommand">Заголовок callback команды.</param>
        public CalendarTCommand(DateTime date, int headerCallbackCommand)
            : this(date, CultureInfo.GetCultureInfo("ru-RU", false), headerCallbackCommand)
        {
            Date = date;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="headerCallbackCommand">Заголовок callback команды.</param>
        /// <param name="culture">Язык календаря.</param>
        public CalendarTCommand(DateTime date, CultureInfo culture, int headerCallbackCommand)
            : base(headerCallbackCommand, Enums.ActionWithLastMessage.Edit)
        {
            Date = date;
            Culture = culture.Name;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public CalendarTCommand() { }

        #endregion
    }
}
