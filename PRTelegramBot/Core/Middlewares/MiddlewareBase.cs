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

        /// <summary>
        /// Следующий обработчик.
        /// </summary>
        protected MiddlewareBase previousMiddleware;

        #endregion

        #region Методы

        /// <summary>
        /// Выполнить следующий асинхронный промежуточный обработчик.
        /// </summary>
        /// <param name="update">Update.</param>
        /// <param name="next">Функция которая должна выполниться после обработчиков.</param>
        public virtual async Task InvokeOnPreUpdateAsync(ITelegramBotClient botClient, Update update, Func<Task> next)
        {
            if (nextMiddleware != null)
            {
                await nextMiddleware.InvokeOnPreUpdateAsync(botClient, update, next);
            }
            else
            {
                await next();
                await InvokeOnPostUpdateAsync(botClient, update);
            }
        }

        /// <summary>
        /// Выполнить предыдущий асинхронный промежуточный обработчик.
        /// </summary>
        /// <param name="update">Update.</param>
        public virtual async Task InvokeOnPostUpdateAsync(ITelegramBotClient botClient, Update update)
        {
            if (previousMiddleware != null)
                await previousMiddleware.InvokeOnPostUpdateAsync(botClient, update);
        }

        /// <summary>
        /// Установить следующий обработчик.
        /// </summary>
        /// <param name="next">Следующий обработчик.</param>
        public void SetNext(MiddlewareBase next)
        {
            this.nextMiddleware = next;
        }

        /// <summary>
        /// Установить следующий обработчик.
        /// </summary>
        /// <param name="next">Следующий обработчик.</param>
        /// <param name="previous">Предыдущий обработчик.</param>
        public void SetNext(MiddlewareBase next, MiddlewareBase previous)
        {
            this.nextMiddleware = next;
            this.previousMiddleware = previous;
        }

        /// <summary>
        /// Установить предыдущий обработчик.
        /// </summary>
        /// <param name="previous">Предыдущий обработчик.</param>
        public void SetPrevious(MiddlewareBase previous)
        {
            this.previousMiddleware = previous;
        }

        #endregion
    }
}
