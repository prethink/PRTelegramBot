using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    internal interface IHandleTypeUpdate
    {
        public Task<UpdateResult> Handle(Update update);
    }
}
