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
        [JsonInclude]
        public int Lc { get; set; }

        /// <summary>
        /// Предыдущая команда.
        /// </summary>
        [JsonIgnore]
        public int LastCommand => Lc;

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
        /// <param name="lc">Команда.</param>
        [JsonConstructor]
        public TCommandBase(int lc)
        {
            Lc = lc;
        }

        #endregion
    }
}
