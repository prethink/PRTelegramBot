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
        public DateTime Date { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="date">Дата.</param>
        public CalendarTCommand(DateTime date)
            : base(0, Enums.ActionWithLastMessage.Edit)
        {
            Date = date;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="lastCommand">Команда.</param>
        public CalendarTCommand(DateTime date, int lastCommand) : base(lastCommand, Enums.ActionWithLastMessage.Edit)
        {
            Date = date;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public CalendarTCommand() { }

        #endregion
    }
}
