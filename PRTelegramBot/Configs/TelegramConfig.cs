using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Конфигурация телеграм бота
    /// </summary>
    public class TelegramConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<long> Admins { get; set; } = new List<long>();
        /// <summary>
        /// 
        /// </summary>
        public List<long> WhiteListUsers { get; set; } = new List<long>();
        /// <summary>
        /// 
        /// </summary>
        public bool ShowErrorNotFoundNameButton { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool ClearUpdatesOnStart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long BotId { get; set; }
    }
}
