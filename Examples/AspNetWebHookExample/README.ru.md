# Пример ASP.NET с webhook

[English](README.md) | **Русский**

Два бота получают обновления через webhook: Telegram сам отправляет каждый update на ваш endpoint, вместо того чтобы бот их опрашивал.

Целевая платформа: **net8.0** · Получение обновлений: **webhook**

Вариант с polling — [AspNetExample](../AspNetExample/README.ru.md).

## Что потребуется

Для webhook нужен адрес, до которого достучится Telegram: публичный **HTTPS**-URL с валидным сертификатом. Для разработки подойдёт туннель — ngrok, Cloudflare Tunnel и подобные.

## Запуск

1. Получите токен бота у [@BotFather](https://t.me/BotFather).
2. В `Program.cs` укажите токен и свой публичный адрес:
   ```csharp
   new PRBotBuilder("5623652365:Token")
       .UseFactory(new PRBotWebHookFactory())
       .SetUrlWebHook("https://domain.ru/botendpoint")
       .SetClearUpdatesOnStart(true)
       .Build();
   ```
   `SetUrlWebHook` должен совпадать с маршрутом, который регистрируется ниже — по умолчанию `/botendpoint`.
3. Запустите проект. `BotHostedService` поднимет ботов и зарегистрирует webhook в Telegram.

## Как это собрано

Важны три части, и все три обязательны.

**Контроллеры и Newtonsoft.Json.** Без них update не десериализуется:
```csharp
builder.Services.AddControllers().AddNewtonsoftJson();
```

**Маршрут.** `MapBotWebhookRoute<BotController>` связывает endpoint с действием контроллера:
```csharp
app.MapBotWebhookRoute<BotController>("/botendpoint");
app.MapControllers();
```

**Запуск.** `BotHostedService` срабатывает при старте приложения: передаёт ботам `IServiceProvider`, вызывает `ReloadHandlers()`, запускает их, а затем проверяет у Telegram, принят ли webhook — и если нет, отдаёт `LastErrorMessage` через событие ошибки бота.

## Несколько ботов на одном endpoint

Оба бота используют один маршрут `/botendpoint`. Различаются они по секретному токену: Telegram передаёт его в заголовке `X-Telegram-Bot-Api-Secret-Token`, а `BotController` сравнивает значение с `bot.Options.WebHookOptions.SecretToken` и выбирает нужного бота.

`ValidateTelegramBotAttribute` отсекает запросы без валидного заголовка ещё до входа в действие, так что произвольный POST на ваш endpoint ничего не даст.

Экземпляры ботов доступны отовсюду через `BotCollection.Instance.GetBots()`.

## Что демонстрируется

| Область | Где смотреть |
| --- | --- |
| Приём и маршрутизация update | `Controllers/BotController.cs` |
| Проверка секретного токена | `Filter/ValidateTelegramBotAttribute.cs` |
| Регистрация маршрута webhook | `WebHookExtensions.cs` |
| Запуск ботов как hosted service | `Services/BotHostedService.cs` |
| Два бота рядом | `Program.cs` |

## На что обратить внимание

В примере боты создаются до `app.Build()`, а запускаются только из hosted service. Порядок здесь важен: боты должны уже лежать в `BotCollection` к моменту прихода первого update, но обращаться к Telegram им нельзя, пока приложение не готово обслуживать endpoint.

В продакшене обязательно задайте секретный токен — без него любой, кто узнает ваш URL, сможет слать поддельные обновления:
```csharp
.SetSecretTokenWebHook("your-secret")
```

---

Смотрите также: [основной README](../../README.ru.md) · [документация](https://prethink.gitbook.io/prtelegrambot/ru/)
