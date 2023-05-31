using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


namespace PRTelegramBot.Configs
{
    class Settings
    {
        public bool ShowNotifyRegisterUserForAdmin { get; set; }
        public List<long> Admins { get; set; }
        public int WordLength { get; set; }
        public bool ShowErrorNotFoundNameButton { get; set; }
    } 
}
