using PRTelegramBot.Core.Middlewares;
using Telegram.Bot.Polling;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Обработчик обновлений telegram.
    /// </summary>
    public interface IPRUpdateHandler : IUpdateHandler
    {
        /// <summary>
        /// Горячая перезагрузка.
        /// </summary>
        public void HotReload();
    }
}
