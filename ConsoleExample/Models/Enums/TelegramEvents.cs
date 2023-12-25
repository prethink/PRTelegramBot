using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExample.Models.Enums
{
    public enum TelegramEvents
    {
        [Description(nameof(Initialization))]
        Initialization,
        [Description(nameof(Register))]
        Register,
        [Description(nameof(Message))]
        Message,
        [Description(nameof(Server))]
        Server,
        [Description(nameof(BlockedBot))]
        BlockedBot,
        [Description(nameof(CommandExecute))]
        CommandExecute,
        [Description(nameof(GroupAction))]
        GroupAction,
    }
}
