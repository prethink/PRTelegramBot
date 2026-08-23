# Webhook

Пример будет на основе [ASP.NET приложения](../../../AspNetWebHookExample).\
nuget пакеты установленные в примере<br>

<figure><img src="../../.gitbook/assets/изображение (30).png" alt=""><figcaption></figcaption></figure>

Program.cs

Для корректной работы webhook ботов, требуется добавить секретный токен. Если в билдере не указать секретный токен для webhook бота, он будет сгенерирован автоматически.

<pre class="language-csharp"><code class="lang-csharp">...
<strong>builder.Services.AddControllers().AddNewtonsoftJson();
</strong><strong>...
</strong>new PRBotBuilder("5623652365:Token")
    .UseFactory(new PRBotWebHookFactory())
    .SetUrlWebHook("https://domain.ru/botendpoint")
    .SetClearUpdatesOnStart(true)
    .Build();
// Найти экземпляр бота можно через класс BotCollection
...
// Сервис который запустит ботов после запуска приложения
builder.Services.AddHostedService&#x3C;BotHostedService>();
...
// Регистрация маршрута для получения данных через WebHook.
// В данном примере будет https://domain.ru/botendpoint
// Обратите внимание на метод SetUrlWebHook, который используется выше.
app.MapBotWebhookRoute&#x3C;BotController>("/botendpoint");
...
app.Run();
</code></pre>

WebHookExtensions.cs

```csharp
using Microsoft.AspNetCore.Mvc;

namespace AspNetWebHook
{
    /// <summary>
    /// Статический класс, содержащий методы расширения для маршрутизации webhook'ов.
    /// </summary>
    public static class WebHookExtensions
    {
        /// <summary>
        /// Сопоставляет маршрут webhook с указанным действием контроллера.
        /// </summary>
        /// <typeparam name="TContoller">Тип контроллера.</typeparam>
        /// <param name="endpoints">Объект для добавления маршрута.</param>
        /// <param name="route">Шаблон маршрута.</param>
        /// <returns>Строитель для настройки конечной точки действия контроллера.</returns>
        public static ControllerActionEndpointConventionBuilder MapBotWebhookRoute<TContoller>(this IEndpointRouteBuilder endpoints, string route)
            where TContoller : Controller
        {
            // Название контроллера без Controller.
            var controllerName = typeof(TContoller).Name.Replace("Controller", "", StringComparison.Ordinal);

            // Метод, который будет обрабатывать маршрут.
            var actionName = typeof(TContoller).GetMethods()[0].Name;

            return endpoints.MapControllerRoute(
                name: "bot_webhook",
                pattern: route,
                defaults: new { controller = controllerName, action = actionName });
        }
    }
}

```

Constants.cs

```csharp
public class Constants
{
    /// <summary>
    /// Заголовок запроса с секретным токеном.
    /// </summary>
    public const string TELEGRAM_SECRET_TOKEN_HEADER = "X-Telegram-Bot-Api-Secret-Token";
}
```

BotHostedService.cs

Сервис который запускает ботов, после запуска приложения.

```csharp
public class BotHostedService : IHostedService
{
    private readonly IServiceProvider serviceProvider;

    public BotHostedService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        StartBots();
    }

    private async Task StartBots()
    {
        // На всякий случай задержка перед запуском.
        await Task.Delay(2000);
        var bots = BotCollection.Instance.GetBots();
        foreach (var bot in bots)
        {
            // Проброс serviceProvider через DI
            bot.Options.ServiceProvider = serviceProvider;
            // На всякий случай обновление обработчиков.
            bot.ReloadHandlers();
            await bot.StartAsync();

            if (bot.DataRetrieval == DataRetrievalMethod.WebHook)
            {
                // Если бот запускается в формате webhook оповещаем в логах в случае ошибки.
                var webHookResult = await((PRBotWebHook)bot).GetWebHookInfo();
                if (!string.IsNullOrEmpty(webHookResult.LastErrorMessage))
                    bot.Events.OnErrorLogInvoke(new Exception(webHookResult.LastErrorMessage));
            }
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var bots = BotCollection.Instance.GetBots();
        foreach (var bot in bots)
        {
            await bot.Stop();
        }
    }
}
```

ValidateTelegramBotAttribute.cs

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Models.Enums;

namespace AspNetWebHook.Filter
{
    /// <summary>
    /// Проверка заголовка "X-Telegram-Bot-Api-Secret-Token" при обработке webook.
    /// Подробнее: <see href="https://core.telegram.org/bots/api#setwebhook"/> "secret_token"
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ValidateTelegramBotAttribute : TypeFilterAttribute
    {
        public ValidateTelegramBotAttribute() : base(typeof(ValidateTelegramBotFilter)) { }

        private class ValidateTelegramBotFilter : IActionFilter
        {
            public ValidateTelegramBotFilter() { }

            public void OnActionExecuted(ActionExecutedContext context) { }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (!IsValidRequest(context.HttpContext.Request))
                {
                    context.Result = new ObjectResult($"\"{Constants.TELEGRAM_SECRET_TOKEN_HEADER}\" is invalid")
                    {
                        StatusCode = 403
                    };
                }
            }

            /// <summary>
            /// Проверка секретного токена при обработке webhook запроса.
            /// </summary>
            /// <param name="request">Запрос.</param>
            /// <returns>True - запрос валидный, False - невалидный.</returns>
            private bool IsValidRequest(HttpRequest request)
            {
                var bots = BotCollection.Instance.GetBots().Where(x => x.DataRetrieval == DataRetrievalMethod.WebHook);
                if (!bots.Any())
                    return false;

                var isSecretTokenProvided = request.Headers.TryGetValue(Constants.TELEGRAM_SECRET_TOKEN_HEADER, out var secretTokenHeader);
                if (!isSecretTokenProvided) return false;

                foreach (var bot in bots)
                {
                    var secretToken = ((WebHookTelegramOptions)bot.Options).SecretToken;
                    if (string.Equals(secretTokenHeader, secretToken, StringComparison.Ordinal));
                        return true;
                }
                return false;
            }
        }
    }
}

```

BotController.cs

```csharp
public class BotController : Controller
{
    [HttpPost]
    [ValidateTelegramBot]
    public async Task<IActionResult> Post([FromBody] Update update)
    {
        // Получение секретного токена если есть.
        if (Request.Headers.TryGetValue(Constants.TELEGRAM_SECRET_TOKEN_HEADER, out var secretTokenHeader))
        {
            //Выгрузка только webHook ботов.
            var webHookbots = BotCollection.Instance.GetBots().Where(x => x.DataRetrieval == DataRetrievalMethod.WebHook);
            foreach (var bot in webHookbots)
            {
                // Сравнение секретных токенов, если идентичны, выполняем обработку.
                var secretToken = ((WebHookTelegramOptions)bot.Options).SecretToken;
                if (string.Equals(secretTokenHeader, secretToken, StringComparison.Ordinal))
                {
                    await bot.Handler.HandleUpdateAsync(bot.botClient, update, bot.Options.CancellationToken.Token);
                    return Ok();
                }
            }
        }
        return BadRequest();
    }
}
```
