# Middleware

Middleware - это строительные блоки, которые оборачивают и расширяют основной обработчик update telegram бота. Каждый компонент middleware принимает botclient, update и делегирует его следующему компоненту в цепочке. Работает аналогично, как в ASP.NET. Обработка вызывается по цепочке перед update и после update.&#x20;

<figure><img src=".gitbook/assets/изображение (41).png" alt=""><figcaption></figcaption></figure>

**InvokeOnPreUpdateAsync** - вызывается до обработки update.

**InvokeOnPostUpdateAsync** - вызывается после обработки update.

Данные методы являются виртуальными, соответственно их можно переопределить.

**ExecutionOrder** - Порядок выполнения middleware в pipeline. Меньшее значение означает более высокий приоритет и раннее выполнение.

Для работы с middleware используется базовый класс [middlewarebase](api/klassy/middlewarebase.md). Для создания своего обработчика нужно от наследоваться от базового класса и переопределить методы  InvokeOnPreUpdateAsync и InvokeOnPostUpdateAsynс. Важно не забыть выполнить методы базового класса.

Пример

```csharp
using PRTelegramBot.Core.Middlewares;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleExample.Middlewares
{
    public class OneMiddleware : MiddlewareBase
    {
        public override int ExecutionOrder => 0;
        
        public override async Task InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
        {
            Console.WriteLine("Выполнение первого обработчика перед update");
            await base.InvokeOnPreUpdateAsync(context, next);
        }

        public override Task InvokeOnPostUpdateAsync(IBotContext context)
        {
            Console.WriteLine("Выполнение первого обработчика после update");
            return base.InvokeOnPostUpdatesAsync(context);
        }
    }
}

```

Подключение своих обработчиков при создание ботов. Обработчики можно добавить через билдер или используя DI.

```csharp
// Через DI.
builder.Services.AddScoped<MiddlewareBase, DIMiddleware>();
builder.Services.AddTransient<MiddlewareBase, UserMiddleware>();
// Через билдер.
var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
                    .AddConfigPath(ExampleConstants.MESSAGES_FILE_KEY, ".\\Configs\\messages.json")
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .AddReplyDynamicCommands(dynamicCommands)
                    .AddMiddlewares(new OneMiddleware(), new TwoMiddleware(), new ThreeMiddleware())
                    .Build();
```

Последовательность выполнения зависит от ExecutionOrder, а не от того, как вы добавили обработчики.

Как видно из кода, первым идет OneMiddleWare (ExecutionOrder 0), после TwoMiddleWare (ExecutionOrder 1) и в конце ThreeMiddleware (ExecutionOrder 2). \
Обработка перед update будет выглядеть так:

* OneMiddleWare
* TwoMiddleWare&#x20;
* ThreeMiddleware

Обработка после update будет выглядеть так:

* ThreeMiddleware
* TwoMiddleWare&#x20;
* OneMiddleWare

Если потребуется, можно прервать выполнение цепочку использовал return;

