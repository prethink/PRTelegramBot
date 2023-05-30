using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Аттрибут для проверки прав доступа на запуск методов
    /// </summary>
    internal class AccessAttribute : Attribute
    {
        /// <summary>
        /// Права доступа
        /// </summary>
        public UserPrivilege? RequiredPrivilege { get; set; }

        public AccessAttribute(UserPrivilege privilages)
        {
            RequiredPrivilege = privilages;
        }
    }
}
