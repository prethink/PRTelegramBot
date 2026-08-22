using System.ComponentModel;

namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// Example of a user privilege.
    /// </summary>
    [Flags]
    public enum UserPrivilege
    {
        [Description("Guest")]
        Guest = 1,
        [Description("Registered")]
        Registered = 2,
        [Description("Administrator")]
        Admin = 4,
        [Description("VIP")]
        VIP = 8,
        [Description("Moderator")]
        Moderator = 16,
    }
}
