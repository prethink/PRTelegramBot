using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// The method will only be able to handle a specific message type.
    /// </summary>
    public sealed class RequireTypeMessageAttribute : Attribute
    {
        #region Fields and properties

        /// <summary>
        /// Message types.
        /// </summary>
        public List<MessageType> TypeMessages { get; private set; } = new List<MessageType>();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="typeMessages">Message type.</param>
        public RequireTypeMessageAttribute(params MessageType[] typeMessages)
        {
            TypeMessages.AddRange(typeMessages.ToList());
        }

        #endregion
    }
}
