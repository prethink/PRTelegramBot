using Newtonsoft.Json;
using PRTelegramBot.Models.CallbackCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Models.TCommands
{
    public class PageTCommand : TelegramCommandBase
    {
        [JsonProperty("1")]
        public int Page { get; set; }
        [JsonProperty("2")]
        public int Header { get; set; }
        public PageTCommand(int page, Enum enumValueInt,int command = 0) : base(command)
        {
            this.Page = page;
            Header = Convert.ToInt32(enumValueInt);
        }
    }
}
