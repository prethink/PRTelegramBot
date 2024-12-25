using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.TestModels.TestHandlers
{
    internal class TestInlineClassHandler : ICallbackQueryCommandHandler
    {
        public Task<UpdateResult> Handle(PRBotBase bot, Update update, CallbackQuery updateType)
        {
            throw new NotImplementedException();
        }
    }
}
