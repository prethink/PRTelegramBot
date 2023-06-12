using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Метод сможет обработать только определенный тип сообщений
    /// </summary>
    public class RequireTypeMessageAttribute : Attribute
    {
        /// <summary>
        /// Тип сообщения
        /// </summary>
        public List<MessageType> TypeMessages { get; private set; } = new List<MessageType>();

        public RequireTypeMessageAttribute(params MessageType[] typeData)
        {
            TypeMessages.AddRange(typeData.ToList());
        }
    }
}
