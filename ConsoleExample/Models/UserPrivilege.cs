using System.ComponentModel;

namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Пример привилегии пользователя.
    /// </summary>
    [Flags]
    public enum UserPrivilege
    {
        [Description("Гость")]
        Guest = 1,
        [Description("Зарегистрированный")]
        Registered = 2,
        [Description("Администратор")]
        Admin = 4,
        [Description("VIP")]
        VIP = 8,
        [Description("Модератор")]
        Moderator = 16,
    }
}
