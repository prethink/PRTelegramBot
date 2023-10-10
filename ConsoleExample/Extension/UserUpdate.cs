using PRTelegramBot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ConsoleExample.Extension
{
    public static class UserUpdate
    {
        public static UserPrivilege GetFlagPrivilege(this Update update)
        {
            return UserPrivilege.Registered;
        }

        public static List<int> GetIntPrivilege(this Update update)
        {
            return new List<int>() { 1, 2 };
        }
    }
}
