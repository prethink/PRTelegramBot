using AspNetWebHook.Filter;
using Microsoft.AspNetCore.Mvc;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Models.Enums;
using Telegram.Bot.Types;

namespace AspNetWebHook.Controllers
{
    public class BotController : Controller
    {
        [HttpPost]
        [ValidateTelegramBot]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if (Request.Headers.TryGetValue(Constants.TELEGRAM_SECRET_TOKEN_HEADER, out var secretTokenHeader))
            {
                var webHookbots = BotCollection.Instance.GetBots().Where(x => x.DataRetrieval == DataRetrievalMethod.WebHook);
                foreach (var bot in webHookbots)
                {
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

    public static class RequestExtensions
    {
        public static async Task<string> ReadAsStringAsync(this Stream requestBody, bool leaveOpen = false)
        {
            using StreamReader reader = new(requestBody, leaveOpen: leaveOpen);
            var bodyAsString = await reader.ReadToEndAsync();

            return bodyAsString;
        }
    }
}
