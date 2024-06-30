using PRTelegramBot.Core;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс проверки команд перед их выполнением.
    /// </summary>
    public interface IInternalCheck
    {
        /// <summary>
        /// Выполнить проверку перед выполнение команды.
        /// </summary>
        /// <param name="bot">Бот.</param>
        /// <param name="update">Update.</param>
        /// <returns>Результат выполенения.</returns>
        Task<InternalCheckResult> Check(PRBotBase bot, Update update);
    }
}
