using Telegram.Bot.Types;

namespace PRTelegramBot.Core.Middlewares
{
    public class OneMiddleWare : MiddlewareBase
    {
        public override async Task InvokeAsync(Update update, Func<Task> next)
        {
            Console.WriteLine("Выполнение первого обработчика перед update");
            await base.InvokeAsync(update, next);
        }
    }
}
