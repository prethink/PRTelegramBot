using Newtonsoft.Json;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Базовая команда
    /// </summary>
    public class TCommandBase
    {
        /// <summary>
        /// Предыдущая команда
        /// </summary>
        [JsonProperty("0")]
        public THeader LastCommand { get; set; }
        public TCommandBase(THeader data = THeader.None)
        {
            LastCommand = data;
        }
    }
}
