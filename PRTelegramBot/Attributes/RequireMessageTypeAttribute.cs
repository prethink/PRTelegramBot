﻿using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// The method will only be able to handle a specific message type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public sealed class RequireMessageTypeAttribute : Attribute
    {
        #region Fields and properties

        /// <summary>
        /// Message types.
        /// </summary>
        public List<MessageType> MessageTypes { get; private set; } = new List<MessageType>();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messageTypes">Message type.</param>
        public RequireMessageTypeAttribute(params MessageType[] messageTypes)
        {
            MessageTypes.AddRange(messageTypes.ToList());
        }

        #endregion
    }
}
