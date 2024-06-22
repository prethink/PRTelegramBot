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
}
