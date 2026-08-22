using PRTelegramBot.Core.Middlewares;
using Telegram.Bot.Polling;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Telegram update handler.
    /// </summary>
    public interface IPRUpdateHandler : IUpdateHandler
    {
        /// <summary>
        /// Hot reload.
        /// </summary>
        public void HotReload();
    }
}
