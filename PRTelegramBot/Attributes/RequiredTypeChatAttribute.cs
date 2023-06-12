using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для определенных типов чата
    /// </summary>
    public class RequiredTypeChatAttribute : Attribute
    {
        /// <summary>
        /// Коллекция допустимых чатов
        /// </summary>
        public List<ChatType> TypeUpdate { get; private set; } = new List<ChatType>();

        public RequiredTypeChatAttribute(params ChatType[] types)
        {
            TypeUpdate.AddRange(types.ToList());
        }
    }
}
