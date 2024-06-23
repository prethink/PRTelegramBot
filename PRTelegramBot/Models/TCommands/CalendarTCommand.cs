using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Команда для передачи даты из календаря
    /// </summary>
    public class CalendarTCommand : TCommandBase
    {
        /// <summary>
        /// Дата.
        /// </summary>
        [JsonPropertyName("1")]
        public DateTime Date { get; private set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="command">Команда.</param>
        public CalendarTCommand(DateTime date, int command = 0) : base(command) 
        {
            Date = date;
        }
    }
}
