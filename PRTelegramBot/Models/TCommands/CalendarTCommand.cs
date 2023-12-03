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
        [JsonProperty("1")]
        public DateTime Date { get; set; }

        public CalendarTCommand(DateTime date, int command = 0) : base(command) 
        {
            Date = date;
        }


    }
}
