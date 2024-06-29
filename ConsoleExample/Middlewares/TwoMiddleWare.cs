using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.Middlewares
{
    public class TwoMiddleWare : MiddlewareBase
    {
        public override async Task InvokeAsync(ITelegramBotClient botClient, Update update, Func<Task> next)
        {
            Console.WriteLine("Выполнение второго обработчика перед update");
            await base.InvokeAsync(botClient,update, next);
        }
    }
}
