using PRTelegramBot.Core;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Configs;

namespace PRTelegramBot.Attributes
{
    public class ReplyMenuDictionaryHandlerAttribute : ReplyMenuHandlerAttribute
    {
        public ConfigApp config { get; private set; }
        public ReplyMenuDictionaryHandlerAttribute(params string[] commands)
            : this(0, commands) { }

        public ReplyMenuDictionaryHandlerAttribute(long botId, params string[] commands) : base(botId, commands)
        {
            var bot = BotCollection.Instance.GetBotOrNull(botId);
            config = new ConfigApp(bot.Options.ConfigPath);
            //Commands = commands.Select(x => GetNameFromResourse(x)).ToList();
            //for (int i = 0; i < Commands.Count; i++)
            //{
            //    if (Commands[i].Contains("NOT_FOUND"))
            //    {
            //        Commands[i] = commands[i];
            //    }
            //}
        }

        private string GetNameFromResourse(string command)
        {
            return config.GetSettings<TextConfig>().GetButton(command);
        }
    }
}
