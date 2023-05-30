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
        public CallbackId LastCommand { get; set; }
        public TCommandBase(CallbackId data = CallbackId.None)
        {
            LastCommand = data;
        }
    }
}
