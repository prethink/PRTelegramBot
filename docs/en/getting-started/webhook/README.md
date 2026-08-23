---
description: Running a bot behind a public URL instead of polling.
---

# Webhook

The example below is based on the [ASP.NET webhook example](https://github.com/prethink/PRTelegramBot/tree/master/Examples/AspNetWebHookExample), which runs two bots on a single endpoint.

A webhook bot needs a **secret token**. It is the only thing that proves a request really came from Telegram. If you do not set one on the builder, the framework generates it for you.

## Program.cs

```csharp
...
builder.Services.AddControllers().AddNewtonsoftJson();
...
new PRBotBuilder("5623652365:Token")
    .UseFactory(new PRBotWebHookFactory())
    .SetUrlWebHook("https://domain.ru/botendpoint")
    .SetClearUpdatesOnStart(true)
    .Build();
// The bot instance can be found later through the BotCollection class.
...
// The service that starts the bots once the application is up.
builder.Services.AddHostedService<BotHostedService>();
...
// Registers the route that receives updates over the webhook.
// With the code above that is https://domain.ru/botendpoint —
// it must match the URL passed to SetUrlWebHook.
app.MapBotWebhookRoute<BotController>("/botendpoint");
...
app.Run();
```

## WebHookExtensions.cs

```csharp
using Microsoft.AspNetCore.Mvc;

namespace AspNetWebHook
{
    /// <summary>
    /// Extension methods for routing webhooks.
    /// </summary>
    public static class WebHookExtensions
    {
        /// <summary>
        /// Maps a webhook route to the given controller action.
        /// </summary>
        /// <typeparam name="TContoller">Controller type.</typeparam>
        /// <param name="endpoints">The object the route is added to.</param>
        /// <param name="route">Route template.</param>
        /// <returns>A builder for configuring the controller action endpoint.</returns>
        public static ControllerActionEndpointConventionBuilder MapBotWebhookRoute<TContoller>(this IEndpointRouteBuilder endpoints, string route)
            where TContoller : Controller
        {
            // The controller name without the "Controller" suffix.
            var controllerName = typeof(TContoller).Name.Replace("Controller", "", StringComparison.Ordinal);

            // The method that will handle the route.
            var actionName = typeof(TContoller).GetMethods()[0].Name;

            return endpoints.MapControllerRoute(
                name: "bot_webhook",
                pattern: route,
                defaults: new { controller = controllerName, action = actionName });
        }
    }
}
```

## Constants.cs

```csharp
public class Constants
{
    /// <summary>
    /// The request header carrying the secret token.
    /// </summary>
    public const string TELEGRAM_SECRET_TOKEN_HEADER = "X-Telegram-Bot-Api-Secret-Token";
}
```

## BotHostedService.cs

Starts the bots once the application has started.

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
        // A short delay before starting, just in case.
        await Task.Delay(2000);
        var bots = BotCollection.Instance.GetBots();
        foreach (var bot in bots)
        {
            // Pass the serviceProvider through for DI.
            bot.Options.ServiceProvider = serviceProvider;
            // Refresh the handlers, just in case.
            bot.ReloadHandlers();
            await bot.StartAsync();

            if (bot.DataRetrieval == DataRetrievalMethod.WebHook)
            {
                // For a webhook bot, report a failure through the log.
                var webHookResult = await ((PRBotWebHook)bot).GetWebHookInfo();
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

## ValidateTelegramBotAttribute.cs

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Models.Enums;

namespace AspNetWebHook.Filter
{
    /// <summary>
    /// Checks the "X-Telegram-Bot-Api-Secret-Token" header while handling a webhook request.
    /// See <see href="https://core.telegram.org/bots/api#setwebhook"/>, "secret_token".
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
            /// Validates the secret token of an incoming webhook request.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <returns>True if the request is valid, False otherwise.</returns>
            private bool IsValidRequest(HttpRequest request)
            {
                var bots = BotCollection.Instance.GetBots().Where(x => x.DataRetrieval == DataRetrievalMethod.WebHook);
                if (!bots.Any())
                    return false;

                var isSecretTokenProvided = request.Headers.TryGetValue(Constants.TELEGRAM_SECRET_TOKEN_HEADER, out var secretTokenHeader);
                if (!isSecretTokenProvided) return false;

                foreach (var bot in bots)
                {
                    var secretToken = bot.Options.WebHookOptions.SecretToken;
                    if (string.Equals(secretTokenHeader, secretToken, StringComparison.Ordinal))
                        return true;
                }
                return false;
            }
        }
    }
}
```

{% hint style="warning" %}
Note the shape of that last comparison. A stray semicolon after the `if` turns the check into an empty statement and makes `return true` unconditional — the filter would then accept any request that merely carries the header, whatever its value. This exact typo lived in the example project until version 1.0.0.
{% endhint %}

## BotController.cs

```csharp
public class BotController : Controller
{
    [HttpPost]
    [ValidateTelegramBot]
    public async Task<IActionResult> Post([FromBody] Update update)
    {
        // Read the secret token, if present.
        if (Request.Headers.TryGetValue(Constants.TELEGRAM_SECRET_TOKEN_HEADER, out var secretTokenHeader))
        {
            // Only the webhook bots.
            var webHookbots = BotCollection.Instance.GetBots().Where(x => x.DataRetrieval == DataRetrievalMethod.WebHook);
            foreach (var bot in webHookbots)
            {
                // Compare the secret tokens; on a match, handle the update.
                var secretToken = bot.Options.WebHookOptions.SecretToken;
                if (string.Equals(secretTokenHeader, secretToken, StringComparison.Ordinal))
                {
                    await bot.Handler.HandleUpdateAsync(bot.BotClient, update, bot.Options.CancellationTokenSource.Token);
                    return Ok();
                }
            }
        }
        return BadRequest();
    }
}
```

This is what lets several bots share one endpoint: the secret token decides which bot an update belongs to.
