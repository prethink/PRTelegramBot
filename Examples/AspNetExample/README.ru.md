# Пример ASP.NET (dependency injection)

[English](README.md) | **Русский**

Бот внутри приложения ASP.NET Core, где всё — обработчики, middleware и фоновые задачи — разрешается через DI-контейнер.

Целевая платформа: **net8.0** · Получение обновлений: **polling**

Вариант с webhook — [AspNetWebHookExample](../AspNetWebHookExample/README.ru.md).

## Запуск

1. Получите токен бота у [@BotFather](https://t.me/BotFather).
2. Подставьте токен в `Program.cs`:
   ```csharp
   var prBotInstance = new PRBotBuilder("token")
   ```
3. Запустите проект. Веб-приложение и бот стартуют вместе.

Webhook и публичный URL здесь не нужны — бот сам опрашивает Telegram.

## Как бот подключается к DI

Ключевой вызов — `builder.Services.AddBotHandlers()`. Он находит в сборке классы с атрибутом `[BotHandler]` и регистрирует их, чтобы зависимости приходили через конструктор.

Дальше боту передаётся контейнер:

```csharp
var serviceProvaider = app.Services.GetService<IServiceProvider>();
var prBotInstance = new PRBotBuilder("token")
    .SetServiceProvider(serviceProvaider)
    .AddInlineClassHandler(ClassTHeader.DefaultTestClass, typeof(BotInlineHandlerWithDependency))
    .AddBackgroundTaskMetadata(new ExampleBackgroundTasksMetadata())
    .Build();
```

## Что демонстрируется

| Область | Где смотреть |
| --- | --- |
| Обработчик с внедрёнными зависимостями | `BotController/BotHandlerWithDependency.cs` |
| Обработчик без зависимостей | `BotController/BotHandlerWithoutDependency.cs` |
| Обработчик только со статическими методами | `BotController/BotHandlerOnlyStatic.cs` |
| Экземплярный inline-обработчик через DI | `BotController/BotInlineHandlerWithDependency.cs` |
| Middleware через DI | `MiddleWares/DIMiddleware.cs`, `MiddleWares/UserMiddleware.cs` |
| Фоновые задачи через DI | `BackgroundTasks/` |
| Разные времена жизни сервисов рядом | `Services/ServiceTransient.cs`, `ServiceScoped.cs`, `ServiceSingleton.cs` |
| EF Core (in-memory) внутри обработчика | `AppDbContext.cs` |
| Пошаговые команды с кэшем | `Models/StepCache.cs` |

## Времена жизни сервисов

Три сервиса с разным временем жизни зарегистрированы намеренно:

```csharp
builder.Services.AddTransient<ServiceTransient>();
builder.Services.AddScoped<ServiceScoped>();
builder.Services.AddSingleton<ServiceSingleton>();
```

Каждый update обрабатывается в своём scope, поэтому `Scoped`-сервис живёт ровно столько, сколько обрабатывается один update. Благодаря этому `AddDbContext` можно спокойно использовать прямо в обработчике.

## На что обратить внимание

`IInlineMenuConverter` здесь регистрируется в контейнере, а не в билдере:

```csharp
builder.Services.AddSingleton<IInlineMenuConverter>(new FileInlineConverter());
```

Работают оба способа. Если конвертер задан и там и там, побеждает билдер — порядок разрешения такой: билдер, затем DI, затем реализация по умолчанию.

---

Смотрите также: [основной README](../../README.ru.md) · [документация](https://prethink.gitbook.io/prtelegrambot/)
