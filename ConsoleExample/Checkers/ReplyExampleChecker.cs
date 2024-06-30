using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace ConsoleExample.Checkers
{
    internal class ReplyExampleChecker : IInternalCheck
    {
        public async Task<InternalCheckResult> Check(PRBotBase bot, Update update)
        {
            // Что-то проверяем перед выполнением reply команд.
            // InternalCheckResult.Passed - продолжить выполнение команды, любые другие результаты остановят выполнение команды.
            return InternalCheckResult.Passed;
        }
    }
}
