using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// The method will only be able to handle a specific chat type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public sealed class RequireChatTypeAttribute : Attribute
    {
        #region Fields and properties

        /// <summary>
        /// Collection of chat types.
        /// </summary>
        public List<ChatType> ChatTypes { get; private set; } = new List<ChatType>();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="chatTypes">Collection of chat types.</param>
        public RequireChatTypeAttribute(params ChatType[] chatTypes)
        {
            ChatTypes.AddRange(chatTypes.ToList());
        }

        #endregion
    }
}
