using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут для определенных типов чата
    /// </summary>
    internal class RequiredTypeUpdateAttribute : Attribute
    {
        /// <summary>
        /// Коллекция допустимых чатов
        /// </summary>
        public List<ChatType> TypeUpdate { get; set; } = new List<ChatType>();

        public RequiredTypeUpdateAttribute(params ChatType[] types)
        {
            TypeUpdate.AddRange(types.ToList());
        }
    }
}
