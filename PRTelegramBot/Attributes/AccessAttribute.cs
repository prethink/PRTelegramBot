using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Аттрибут для проверки прав доступа на запуск методов
    /// </summary>
    public class AccessAttribute : Attribute
    {
        public int? Flags { get; private set; }
        public AccessAttribute(int flags)
        {
            Flags = flags;
        }
    }
}
