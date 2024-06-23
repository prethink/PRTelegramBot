using PRTelegramBot.Models.CallbackCommands;
using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.TCommands
{
    public class PageTCommand : TCommandBase
    {
        [JsonPropertyName("1")]
        public int Page { get; set; }
        [JsonPropertyName("2")]
        public int Header { get; set; }
        public PageTCommand(int page, Enum enumValueInt,int command = 0) : base(command)
        {
            this.Page = page;
            Header = Convert.ToInt32(enumValueInt);
        }
    }
}
