using Newtonsoft.Json;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Команда для передачи даты из календаря
    /// </summary>
    public class CallendarTCommand : TCommandBase
    {
        /// <summary>
        /// Дата
        /// </summary>
        [JsonProperty("2")]
        public DateTime Date { get; set; }

        public CallendarTCommand(DateTime date)
        {
            Date = date;
        }


    }
}
