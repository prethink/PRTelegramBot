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
        public TCommandBase(int command = 0)
        {
            LastCommand  = command;
        }

        /// <summary>
        /// получить команду нужнного типа Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetLastCommandEnum<T>() where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), LastCommand);
        }
    }
}
