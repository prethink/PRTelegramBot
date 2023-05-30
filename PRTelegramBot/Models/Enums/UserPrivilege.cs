using System.ComponentModel;

namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Привилегии пользователей
    /// </summary>
    [Flags]
    public enum UserPrivilege
    {
        [Description("Гость")]
        Guest = 0,
        [Description("Зарегистрированный")]
        Registered = 1,
        [Description("Администратор")]
        Admin = 2,
    }
}
