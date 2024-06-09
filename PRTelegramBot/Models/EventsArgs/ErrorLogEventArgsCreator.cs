using Telegram.Bot.Types;

namespace PRTelegramBot.Models.EventsArgs
{
    public class ErrorLogEventArgsCreator : EventArgs
    {
        public Exception Exception { get; private set; }
        public Update Update { get; private set; }

        public ErrorLogEventArgsCreator(Exception exception)
            : this(exception, new Update()) { }

        public ErrorLogEventArgsCreator(Exception exception, Update update)
        {
            Exception = exception;
            Update = update;
        }
    }
}
