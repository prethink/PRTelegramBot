using PRTelegramBot.Models;
using PRTelegramBot.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

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
