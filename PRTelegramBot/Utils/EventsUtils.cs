using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Utils
{
    internal class EventsUtils
    {
        public static Task InvokeAllAsync(Func<BotEventArgs, Task>? evt, BotEventArgs e)
        {
            if (evt == null)
                return Task.CompletedTask;

            var handlers = evt.GetInvocationList();

            var tasks = handlers
                .Select(h => ((Func<BotEventArgs, Task>)h)(e))
                .ToArray();

            return Task.WhenAll(tasks);
        }
    }
}
