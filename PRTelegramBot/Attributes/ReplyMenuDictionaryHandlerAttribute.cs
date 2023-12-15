using PRTelegramBot.Models;
using PRTelegramBot.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Attributes
{
    public class ReplyMenuDictionaryHandlerAttribute : ReplyMenuHandlerAttribute
    {
        public ConfigApp config {get;private set;}
        public ReplyMenuDictionaryHandlerAttribute( params string[] commands) : base(commands)
        {
            Init(commands);
        }

        public ReplyMenuDictionaryHandlerAttribute(long botId, params string[] commands) : base(botId, commands) 
        {
            Init(commands);
        }

        public override void Init( params string[] commands)
        {
            config = new ConfigApp("Configs\\appconfig.json");
            base.Init(commands);

            Commands = commands.Select(x => GetNameFromResourse(x)).ToList();
            for (int i = 0; i < Commands.Count; i++)
            {
                if (Commands[i].Contains("NOT_FOUND"))
                {
                    Commands[i] = commands[i];
                }
            }
        }

        private string GetNameFromResourse(string command)
        {
            return config.GetSettings<TextConfig>().GetButton(command);
        }
    }
}
