using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;

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
        /// <param name="context">Контекст бота.</param>
        /// <param name="handler">Команда обработчик.</param>
        /// <returns>Результат выполнения.</returns>
        /// <returns></returns>
        Task<InternalCheckResult> Check(IBotContext context, CommandHandler handler);
    }
}
