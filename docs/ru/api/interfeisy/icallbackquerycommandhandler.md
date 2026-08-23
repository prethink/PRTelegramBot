---
description: Интерфейс обработчика для callbackQuery команд.
---

# ICallbackQueryCommandHandler

```csharp
using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс обработчика для callbackQuery команд.
    /// </summary>
    public interface ICallbackQueryCommandHandler : ICommandHandlerBase<CallbackQuery>
    {
        /// <summary>
        /// Обработка.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="updateType">CallbackQuery класс update.</param>
        /// <returns>Результат обновления.</returns>
        public Task<UpdateResult> Handle(IBotContext context, CallbackQuery updateType);
    }
}

```
