using PRTelegramBot.Models.Enums;
using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.CallbackCommands
{
    /// <summary>
    /// Base command.
    /// </summary>
    public class TCommandBase
    {
        #region Properties and fields

        /// <summary>
        /// Callback command header.
        /// </summary>
        [JsonPropertyName("l")]
        public int HeaderCallbackCommand { get; set; }

        /// <summary>
        /// Action to perform on the previous message.
        /// </summary>
        [JsonPropertyName("a")]
        public int ActionWithLastMessage { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// gets the command as the required enum type.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <returns>The command as an enum value.</returns>
        public TEnum GetLastCommandEnum<TEnum>() where TEnum : Enum
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), HeaderCallbackCommand);
        }

        /// <summary>
        /// Action to perform on the last message.
        /// </summary>
        /// <returns>An enum describing what to do with the last message.</returns>
        public ActionWithLastMessage GetActionWithLastMessage()
        {
            return (ActionWithLastMessage)Enum.ToObject(typeof(ActionWithLastMessage), ActionWithLastMessage);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public TCommandBase()
            : this(0) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="command">Command.</param>
        public TCommandBase(int command)
        {
            HeaderCallbackCommand = command;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="command">Command.</param>
        /// <param name="action">Action to perform on the last message.</param>
        public TCommandBase(int command, ActionWithLastMessage action)
            : this (action)
        {
            HeaderCallbackCommand = command;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="action">Action to perform on the last message.</param>
        public TCommandBase(ActionWithLastMessage action)
            : this(0)
        {
            ActionWithLastMessage = (int)action;
        }

        #endregion
    }
}