using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface for checking commands before they run.
    /// </summary>
    public interface IInternalCheck
    {
        /// <summary>
        /// Runs a check before the command is executed.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="handler">Handler command.</param>
        /// <returns>The execution result.</returns>
        /// <returns></returns>
        Task<InternalCheckResult> Check(IBotContext context, CommandHandler handler);
    }
}
