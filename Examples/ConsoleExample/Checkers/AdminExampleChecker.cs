using ConsoleExample.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using System.Reflection;

namespace ConsoleExample.Checkers
{
    internal class AdminExampleChecker : IInternalCheck
    {
        public async Task<InternalCheckResult> Check(IBotContext context, CommandHandler handler)
        {
            var method = handler.Method;
            var adminAttribute = method.GetCustomAttribute<AdminOnlyExampleAttribute>();
            if(adminAttribute != null)
            {
                var userIsAdmin = await context.IsAdmin(context.Update.GetChatId());
                if(!userIsAdmin)
                    await PRTelegramBot.Helpers.Message.Send(context, "Вы не админ!");

                return userIsAdmin ? InternalCheckResult.Passed : InternalCheckResult.Custom;
            }
            return InternalCheckResult.Passed;
        }
    }
}
