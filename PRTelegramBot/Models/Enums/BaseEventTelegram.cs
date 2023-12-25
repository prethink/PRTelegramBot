using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
