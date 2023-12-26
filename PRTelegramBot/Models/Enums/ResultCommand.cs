using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Models.Enums
{
    internal enum ResultCommand
    {
        NotFound = 0,
        Executed,
        PrivilegeCheck,
        WrongMessageType,
        WrongChatType
    }
}
