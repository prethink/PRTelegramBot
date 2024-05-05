using System.ComponentModel;

namespace PRTelegramBot.Models.Enums
{
    public enum BaseEventTelegram
    {
        [Description(nameof(None))]
        None = 0,
        [Description(nameof(Initialization))]
        Initialization,
        [Description(nameof(Update))]
        Update,
        [Description(nameof(CallBackCommand))]
        CallBackCommand,
    }
}
