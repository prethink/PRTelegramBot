# IInternalCheck

```csharp
using PRTelegramBot.Core;
using PRTelegramBot.Models;
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
        /// <param name="context">Контекст бота.</param>
        /// <param name="handler">Команда обработчик.</param>
        /// <returns>Результат выполенения.</returns>
        Task<InternalCheckResult> Check(IBotContext context, CommandHandler handler);
    }
}
```
