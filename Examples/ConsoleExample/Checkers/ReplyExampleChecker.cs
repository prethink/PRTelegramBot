using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;

namespace ConsoleExample.Checkers
{
    internal class ReplyExampleChecker : IInternalCheck
    {
        public async Task<InternalCheckResult> Check(IBotContext context, CommandHandler handler)
        {
            // Что-то проверяем перед выполнением reply команд.
            // InternalCheckResult.Passed - продолжить выполнение команды, любые другие результаты остановят выполнение команды.
            return InternalCheckResult.Passed;
        }
    }
}
