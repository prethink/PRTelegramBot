using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.TCommands
{
    /// <summary>
    /// Строковые данные для команд.
    /// </summary>
    public class StringTCommand : TCommandBase
    {
        #region Поля и свойства

        /// <summary>
        /// Текстовые данные.
        /// </summary>
        [JsonPropertyName("1")]
        public string StrData { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="data">Идентификатор сущности.</param>
        public StringTCommand(string data)
            : base(0)
        {
            StrData = data;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="data">Идентификатор сущности.</param>
        /// <param name="lastCommand">Прошлая команда.</param>
        public StringTCommand(string data, int lastCommand)
            : base(lastCommand)
        {
            StrData = data;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="data">Идентификатор сущности.</param>
        /// <param name="action">Действие с прошлым сообщением.</param>
        public StringTCommand(string data, ActionWithLastMessage action)
            : base(action)
        {
            StrData = data;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="data">Идентификатор сущности.</param>
        /// <param name="lastCommand">Прошлая команда.</param>
        /// <param name="action">Действие с прошлым сообщением.</param>
        public StringTCommand(string data, int lastCommand, ActionWithLastMessage action)
            : base(lastCommand, action)
        {
            StrData = data;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public StringTCommand() { }

        #endregion
    }
}
