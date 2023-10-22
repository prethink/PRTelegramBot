using Newtonsoft.Json;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Команда для передачи даты из календаря
    /// </summary>
    public class CalendarTCommand : TCommandBase
    {
        /// <summary>
        /// Дата
        /// </summary>
        [JsonProperty("2")]
        public DateTime Date { get; set; }

        public CalendarTCommand(DateTime date)
        {
            Date = date;
        }


    }
}
