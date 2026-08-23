---
description: Интерфейс обработчика для message команд.
---

# IMessageCommandHandler

```csharp
using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс обработчика для message команд.
    /// </summary>
    public interface IMessageCommandHandler : ICommandHandlerBase<Message>
    {
        /// <summary>
        /// Обработка.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="updateType">Messageкласс update.</param>
        /// <returns>Результат обновления.</returns>
        public Task<UpdateResult> Handle(IBotContext context, Message updateType);
    }
}

```
