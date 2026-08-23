---
description: >-
  MessageAwaiter позволяет создавать сообщение заглушку перед обработкой данных
  и автоматически удалять его после.
---

# MessageAwaiter

```csharp
using(var messageAwaiter = new MessageAwaiter(context))
{
// Обработка...
}

using(var messageAwaiter = new MessageAwaiter(context, "Текст сообщения"))
{
// Обработка...
}
```

Пример использования

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleExample.Examples
{
    internal class ExampleUtils
    {
        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "Awaiter message".
        /// Сначало будет отправлено сообщение о генерации данных, после двух секунд старое сообщение будет удалено и сразу появится новое. 
        /// </summary>
        [ReplyMenuHandler("Awaiter message")]
        public static async Task AdminExample(IBotContext context)
        {
            using(var messageAwaiter = new MessageAwaiter(context))
            {
                // Симуляция тяжелой операции.
                await Task.Delay(2000);
                await MessageSender.Send(context, $"Генерация данных завершена.");
            }
        }
    }
}

```

Результат выполнения<br>

<figure><img src="../../.gitbook/assets/изображение (35).png" alt=""><figcaption></figcaption></figure>

<figure><img src="../../.gitbook/assets/изображение (36).png" alt=""><figcaption></figcaption></figure>
