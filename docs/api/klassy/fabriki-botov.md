# Фабрики ботов

PRBotFactory - классический бот из telegram.bot

PRBotPollingFactory - фабрика бота для polling.

PRBotWebHookFactory - фабрика бота для webhook.



Пример подстановки фабрики при создание бота

```csharp
new PRBotBuilder("5623652365:Token")
    .UseFactory(new PRBotPollingFactory())
    .Build();
```
