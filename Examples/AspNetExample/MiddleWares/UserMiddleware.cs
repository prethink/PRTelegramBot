using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Interfaces;
using System.Diagnostics;

namespace AspNetExample.MiddleWares
{
    public class UserMiddleware : MiddlewareBase
    {
        private readonly AppDbContext db;

        public UserMiddleware(AppDbContext appDbContext)
        {
            this.db = appDbContext;
        }

        public override int ExecutionOrder => 0;

        public override async Task InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
        {
            var usersCount = db.Users.Count();
            Debug.WriteLine($"PreMiddleWare {nameof(UserMiddleware)} - Users - {usersCount}");
            await base.InvokeOnPreUpdateAsync(context, next);
        }

        public override Task InvokeOnPostUpdateAsync(IBotContext context)
        {
            var usersCount = db.Users.Count();
            Console.WriteLine("Выполнение первого обработчика после update");
            Debug.WriteLine($"PostMiddleWare {nameof(UserMiddleware)} - Users - {usersCount}");
            return base.InvokeOnPostUpdateAsync(context);
        }
    }
}
