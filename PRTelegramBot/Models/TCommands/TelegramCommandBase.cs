using Newtonsoft.Json;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Базовая команда
    /// </summary>
    public class TelegramCommandBase
    {
        /// <summary>
        /// Предыдущая команда
        /// </summary>
        [JsonProperty("0")]
        public int LastCommand { get; set; }
        public TelegramCommandBase(int command = 0)
        {
            LastCommand  = command;
        }

        /// <summary>
        /// получить команду нужного типа Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetLastCommandEnum<T>() where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), LastCommand);
        }
    }
}
