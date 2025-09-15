using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
                    var secretToken = bot.Options.WebHookOptions.SecretToken;
                    if (string.Equals(secretTokenHeader, secretToken, StringComparison.Ordinal));
                        return true;
                }
                return false;
            }
        }
    }
}
