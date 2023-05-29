using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Метод сможет обработать только определенный тип сообщений
    /// </summary>
    internal class RequireDateAttribute : Attribute
    {
        /// <summary>
        /// Тип сообщения
        /// </summary>
        public MessageType TypeData { get; set; }

        public RequireDateAttribute(MessageType typeData)
        {
            TypeData = typeData;
        }
    }
}
