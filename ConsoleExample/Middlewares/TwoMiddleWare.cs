using Telegram.Bot.Types;

namespace PRTelegramBot.Core.Middlewares
{
    public class TwoMiddleWare : MiddlewareBase
    {
        public override async Task InvokeAsync(Update update, Func<Task> next)
        {
            Console.WriteLine("Выполнение второго обработчика перед update");
            await base.InvokeAsync(update, next);
        }
    }
}
