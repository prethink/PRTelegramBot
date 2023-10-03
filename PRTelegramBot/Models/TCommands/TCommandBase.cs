using Newtonsoft.Json;
using PRTelegramBot.Models.Enums;
using System.Reflection.PortableExecutable;

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
        public int LastCommand { get; set; }
        public TCommandBase(THeader data = THeader.None)
        {
            LastCommand  = Convert.ToInt32(data);
        }
    }
}
