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
        /// Промежуточный обработчик перед выполнением update.
        /// </summary>
        public MiddlewareBase Middleware { get; }

        /// <summary>
        /// Горячая перезагрузка.
        /// </summary>
        public void HotReload();
    }
}
