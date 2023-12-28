using PRTelegramBot.Models;

namespace ConsoleExample.Models
{
    public class UserCache : TelegramCache
    {
        /// <summary>
        /// Временные данные
        /// </summary>
        public string Data { get; set; }

        public override void ClearData()
        {
            base.ClearData();
            Data = "";
        }
    }
}
