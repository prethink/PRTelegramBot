using PRTelegramBot.Models.Enums;
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
        [JsonPropertyName("l")]
        public int LastCommand { get; set; }

        /// <summary>
        /// Действие с предыдущим сообщением.
        /// </summary>
        [JsonPropertyName("a")]
        public int ActionWithLastMessage { get; set; }

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

        /// <summary>
        /// Действие с последним сообщением.
        /// </summary>
        /// <returns>Enum что сделать с последним сообщением.</returns>
        public ActionWithLastMessage GetActionWithLastMessage()
        {
            return (ActionWithLastMessage)Enum.ToObject(typeof(ActionWithLastMessage), ActionWithLastMessage);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public TCommandBase()
            : this(0) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        public TCommandBase(int command)
        {
            LastCommand = command;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="action">Действие с последним сообщением.</param>
        public TCommandBase(int command, ActionWithLastMessage action)
            : this (action)
        {
            LastCommand = command;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="action">Действие с последним сообщением.</param>
        public TCommandBase(ActionWithLastMessage action)
            : this(0)
        {
            ActionWithLastMessage = (int)action;
        }

        #endregion
    }
}