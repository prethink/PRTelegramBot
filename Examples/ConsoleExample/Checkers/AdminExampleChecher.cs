using ConsoleExample.Attributes;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using System.Reflection;
using Telegram.Bot.Types;

namespace ConsoleExample.Checkers
{
    internal class AdminExampleChecher : IInternalCheck
    {
        public async Task<InternalCheckResult> Check(PRBotBase bot, Update update, CommandHandler handler)
        {
            var method = handler.GetMethodInfo();
            var adminAttribute = method.GetCustomAttribute<AdminOnlyExampleAttribute>();
            if(adminAttribute != null)
            {
                var userIsAdmin = await bot.IsAdmin(update.GetChatId());
                if(!userIsAdmin)
                    await PRTelegramBot.Helpers.Message.Send(bot.botClient, update.GetChatId(), "Вы не админ!");

                return userIsAdmin ? InternalCheckResult.Passed : InternalCheckResult.Custom;
            }
            return InternalCheckResult.Passed;
        }
    }
}
