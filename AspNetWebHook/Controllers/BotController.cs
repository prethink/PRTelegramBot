using AspNetWebHook.Filter;
using Microsoft.AspNetCore.Mvc;
using PRTelegramBot.Core;
using Telegram.Bot.Types;

namespace AspNetWebHook.Controllers
{
    public class BotController : Controller
    {
        [HttpPost]
        //[ValidateTelegramBot]
        public async Task<IActionResult> Post(
            [FromBody] Update update)
        {
            if (BotCollection.Instance.BotCount == 1)
            {
                var bot = BotCollection.Instance.GetBots().FirstOrDefault();
                await bot.Handler.HandleUpdateAsync(bot.botClient, update, bot.Options.CancellationToken.Token);
                return Ok();
            }
            else if(BotCollection.Instance.BotCount > 1) 
            {
                //TODO
            }

            return Ok();
        }
    }
}
