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
        /// Заголовок callback команды.
        /// </summary>
        [JsonPropertyName("l")]
        public int HeaderCallbackCommand { get; set; }

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
            return (TEnum)Enum.ToObject(typeof(TEnum), HeaderCallbackCommand);
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
            HeaderCallbackCommand = command;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="action">Действие с последним сообщением.</param>
        public TCommandBase(int command, ActionWithLastMessage action)
            : this (action)
        {
            HeaderCallbackCommand = command;
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