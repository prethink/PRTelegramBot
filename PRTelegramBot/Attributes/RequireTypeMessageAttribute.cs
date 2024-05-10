using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Метод сможет обработать только определенный тип сообщений.
    /// </summary>
    public class RequireTypeMessageAttribute : Attribute
    {
        #region Поля и свойства

        /// <summary>
        /// Типы сообщений.
        /// </summary>
        public List<MessageType> TypeMessages { get; private set; } = new List<MessageType>();

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="typeMessages">Тип сообщений.</param>
        public RequireTypeMessageAttribute(params MessageType[] typeMessages)
        {
            TypeMessages.AddRange(typeMessages.ToList());
        }

        #endregion
    }
}
