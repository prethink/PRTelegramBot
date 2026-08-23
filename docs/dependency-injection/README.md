---
description: >-
  Начиная с версии библиотеки 0.5.0 появилась возможность задействовать
  dependency injection.
---

# Dependency injection

В данном примере будет показано как создать telegram бота с использование dependency injection в ASP.NET Core.&#x20;

Пример - [https://github.com/prethink/PRTelegramBot/tree/master/AspNetExample](../../AspNetExample)

1. В program.cs вы должны использовать метод AddBotHandlers()

```csharp
builder.Services.AddBotHandlers();
или
builder.Services.AddTransientBotHandlers();
builder.Services.AddSingletonBotHandlers();
builder.Services.AddScopedBotHandlers();
```

Благодаря этому система найдет все классы в сборке которые помечены атрибутом BotHandler и создаст экземпляры классов, после чего пробросит все требуемые зависимости.&#x20;

2. После var app = builder.Build() вы должны инициализировать создание экземпляра класса для telegram ботам и передать в билдер бота зависимость IServiceProvider.

```csharp
var app = builder.Build();
var serviceProvider = app.Services.GetService<IServiceProvider>();
//Создание и запуск бота
var telegram = new PRBotBuilder("Token")
                    .SetServiceProvider(serviceProvider)
                    .Build();
```

Пример - Program.cs

```csharp
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;

var builder = WebApplication.CreateBuilder(args);
//....Добавьте ваши зависимости
//Инициализация классов для работы ботов с DI
builder.Services.AddBotHandlers();

var app = builder.Build();
var serviceProvider = app.Services.GetService<IServiceProvider>();
var telegram = new PRBotBuilder("Token")
                    .SetServiceProvider(serviceProvider)
                    .Build();

await telegram.StartAsync();

app.Run();
```

3. Создайте класс с названием которое вам требуется и добавьте к нему атрибут BotHandler. Внутри класса укажите зависимости которые хотите пробросить и создайте команды обработчики внутри класса.&#x20;

Сигнатура команды обработки:

```csharp
[Атрибут обработки]
public async Task Название метода(IBotContext context)
{
//код
}
```

Пример - BotController.cs

<pre class="language-csharp"><code class="lang-csharp">namespace TestDI.BotController
{
<strong>    [BotHandler]
</strong>    public class BotController
    {
        private readonly ILogger&#x3C;BotHandler> _logger;

        public BotHandler(ILogger&#x3C;BotHandler> logger)
        {
            _logger = logger;
        }

        [ReplyMenuHandler("Test")]
        public async Task TestMethod(IBotContext context)
        {
            await PRTelegramBot.Helpers.Message.Send(context, $"{nameof(TestMethod)} {_logger != null}");
        }

        [SlashHandler("/test")]
        public async Task Slash(IBotContext context)
        {
            await PRTelegramBot.Helpers.Message.Send(context, nameof(Slash));
        }

        [ReplyMenuHandler("inline")]
        public async Task InlineTest(IBotContext context)
        {
            var options = new OptionMessage();
            var menuItemns = MenuGenerator.InlineButtons(1, new List&#x3C;IInlineContent> { 
                new InlineCallback("Test", THeader.CurrentPage), 
                new InlineCallback("TestStatic", THeader.NextPage) 
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItemns);
            await PRTelegramBot.Helpers.Message.Send(context, nameof(InlineTest), options);
        }

        [ReplyMenuHandler("inlinestatic")]
        public async Task StaticInlineTest(IBotContext context)
        {
            var options = new OptionMessage();
            var menuItemns = MenuGenerator.InlineButtons(1, new List&#x3C;IInlineContent> {
                new InlineCallback("Test", THeader.CurrentPage),
                new InlineCallback("TestStatic", THeader.NextPage)
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItemns);
            await PRTelegramBot.Helpers.Message.Send(context, nameof(StaticInlineTest), options);
        }

        [InlineCallbackHandler&#x3C;THeader>(THeader.CurrentPage)]
        public async Task InlineHandler(IBotContext context)
        {
            await PRTelegramBot.Helpers.Message.Send(context, nameof(InlineHandler));
        }

        [InlineCallbackHandler&#x3C;THeader>(THeader.NextPage)]
        public async static Task InlineHandlerStatic(IBotContext context)
        {
            await PRTelegramBot.Helpers.Message.Send(context, nameof(InlineHandlerStatic));
        }

        [ReplyMenuHandler("Test1")]
        public async static Task StaticTestMethod(IBotContext context)
        {
            await PRTelegramBot.Helpers.Message.Send(context, nameof(StaticTestMethod));
        }
    }
}
</code></pre>

