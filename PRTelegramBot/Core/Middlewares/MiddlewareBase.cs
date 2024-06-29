using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.Middlewares
{
    /// <summary>
    /// Базовый промежуточный обработчик.
    /// </summary>
    public class MiddlewareBase
    {
        #region Поля и свойства

        /// <summary>
        /// Следующий обработчик.
        /// </summary>
        protected MiddlewareBase nextMiddleware;

        #endregion

        #region Методы

        /// <summary>
        /// Выполнить асинхронный промежуточный обработчик.
        /// </summary>
        /// <param name="update">Update.</param>
        /// <param name="next">Функция которая должна выполниться после обработчиков.</param>
        public virtual async Task InvokeAsync(ITelegramBotClient botClient, Update update, Func<Task> next)
        {
            if (nextMiddleware != null)
            {
                await nextMiddleware.InvokeAsync(botClient, update, next);
            }
            else
            {
                await next();
            }
        }

        /// <summary>
        /// Установить следующий обработчик.
        /// </summary>
        /// <param name="nextMiddleware">Следующий обработчик.</param>
        public void SetNext(MiddlewareBase nextMiddleware)
        {
            this.nextMiddleware = nextMiddleware;
        }

        #endregion
    }
}
