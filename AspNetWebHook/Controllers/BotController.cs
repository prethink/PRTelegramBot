using AspNetWebHook.Filter;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace AspNetWebHook.Controllers
{
    public class BotController : Controller
    {
        //[HttpPost]
        //[ValidateTelegramBot]
        //public async Task<IActionResult> Post(
        //    [FromBody] Update update,
        //    [FromServices] UpdateHandlers handleUpdateService,
        //    CancellationToken cancellationToken)
        //{
        //    await handleUpdateService.HandleUpdateAsync(update, cancellationToken);
        //    return Ok();
        //}
    }
}
