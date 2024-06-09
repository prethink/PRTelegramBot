using PRTelegramBot.Core;

namespace PRTelegramBot.Models.EventsArgs
{
    public class CommonLogEventArgs : BotEventArgs
    {
        public string Message { get; private set; }
        public string Type { get; private set; }
        public ConsoleColor Color { get; private set; }

        public CommonLogEventArgs(PRBot bot, CommonLogEventArgsCreator e) : base(bot, e.Update)
        {
            this.Message = e.Message;
            this.Type = e.Type;
            this.Color = e.Color;
        }
    }
}
