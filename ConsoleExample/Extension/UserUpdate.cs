using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace ConsoleExample.Extension
{
    public static class UserUpdate
    {
        public static UserPrivilege LoadExampleFlagPrivilege(this Update update)
        {
            return UserPrivilege.Registered;
        }
    }
}
