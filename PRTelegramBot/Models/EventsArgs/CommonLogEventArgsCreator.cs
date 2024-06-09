using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    public class CommonLogEventArgsCreator : EventArgs
    {
        public string Message { get; private set; }
        public string Type { get; private set; }
        public ConsoleColor Color { get; private set; }
        public Update Update { get; private set; }

        public CommonLogEventArgsCreator(string message, string type)
            : this(message, type, ConsoleColor.White, new Update()) { }

        public CommonLogEventArgsCreator(string message, string type, Update update)
    : this(message, type, ConsoleColor.White, update) { }

        public CommonLogEventArgsCreator(string message, string type, ConsoleColor color)
            : this(message, type, color, new Update()) { }

        public CommonLogEventArgsCreator(string message, string type, ConsoleColor color, Update update)
        {
           this.Message = message;
           this.Type = type;
           this.Color = color;
           this.Update = update;
        }
    }
}
