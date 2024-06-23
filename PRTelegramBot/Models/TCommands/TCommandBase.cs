using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Базовая команда.
    /// </summary>
    public class TCommandBase
    {
        #region Свойства и поля

        /// <summary>
        /// Предыдущая команда.
        /// </summary>
        [JsonPropertyName("lc")]
        public int LastCommand { get; set; }

        #endregion

        #region Методы

        /// <summary>
        /// получить команду нужного типа enum.
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления.</typeparam>
        /// <returns>Команда в enum типе.</returns>
        public TEnum GetLastCommandEnum<TEnum>() where TEnum : Enum
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), LastCommand);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public TCommandBase()
        {
            LastCommand = 0;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        public TCommandBase(int command)
        {
            LastCommand = command;
        }

        #endregion
    }
}