using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// The method will only be able to handle a specific chat type.
    /// </summary>
    public sealed class RequiredTypeChatAttribute : Attribute
    {
        #region Fields and properties

        /// <summary>
        /// Collection of chat types.
        /// </summary>
        public List<ChatType> TypesChat { get; private set; } = new List<ChatType>();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="typesChat">Collection of chat types.</param>
        public RequiredTypeChatAttribute(params ChatType[] typesChat)
        {
            TypesChat.AddRange(typesChat.ToList());
        }

        #endregion
    }
}
