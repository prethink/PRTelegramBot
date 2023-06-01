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
        /// Предыдушая команда
        /// </summary>
        [JsonProperty("0")]
        public Header LastCommand { get; set; }
        public TCommandBase(Header data = Header.None)
        {
            LastCommand = data;
        }
    }
}
