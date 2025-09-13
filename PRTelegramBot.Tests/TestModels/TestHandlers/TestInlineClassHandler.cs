using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.TestModels.TestHandlers
{
    internal class TestInlineClassHandler : ICallbackQueryCommandHandler
    {
        public Task<UpdateResult> Handle(IBotContext context, CallbackQuery updateType)
        {
            throw new NotImplementedException();
        }
    }
}
