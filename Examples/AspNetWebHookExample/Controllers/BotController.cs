using AspNetWebHook.Filter;
using Microsoft.AspNetCore.Mvc;
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
            // Get the secret token, if present.
            if (Request.Headers.TryGetValue(Constants.TELEGRAM_SECRET_TOKEN_HEADER, out var secretTokenHeader))
            {
                //Retrieve webhook bots only.
                var webHookbots = BotCollection.Instance.GetBots().Where(x => x.DataRetrieval == DataRetrievalMethod.WebHook);
                foreach (var bot in webHookbots)
                {
                    // Compare the secret tokens; if they match, process the update.
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
}
