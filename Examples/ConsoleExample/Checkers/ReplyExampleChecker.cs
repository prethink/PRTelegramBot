using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;

namespace ConsoleExample.Checkers
{
    internal class ReplyExampleChecker : IInternalCheck
    {
        public async Task<InternalCheckResult> Check(IBotContext context, CommandHandler handler)
        {
            // Perform some check before reply commands run.
            // InternalCheckResult.Passed continues command execution; any other result stops it.
            return InternalCheckResult.Passed;
        }
    }
}
