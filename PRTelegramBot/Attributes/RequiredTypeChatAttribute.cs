using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Метод сможет обработать только определенный тип чата.
    /// </summary>
    public sealed class RequiredTypeChatAttribute : Attribute
    {
        #region Поля и свойства

        /// <summary>
        /// Коллекция типов чатов.
        /// </summary>
        public List<ChatType> TypesChat { get; private set; } = new List<ChatType>();

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="typesChat">Коллекция типов чатов.</param>
        public RequiredTypeChatAttribute(params ChatType[] typesChat)
        {
            TypesChat.AddRange(typesChat.ToList());
        }

        #endregion
    }
}
