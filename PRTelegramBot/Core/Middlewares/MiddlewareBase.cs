using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core.Middlewares
{
    /// <summary>
    /// Базовый промежуточный обработчик.
    /// </summary>
    public abstract class MiddlewareBase
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

        /// <summary>
        /// Порядок выполнения middleware в pipeline.
        /// Меньшее значение означает более высокий приоритет и раннее выполнение.
        /// </summary>
        public abstract int ExecutionOrder { get; }

        #endregion

        #region Методы

        /// <summary>
        /// Выполнить следующий асинхронный промежуточный обработчик.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="next">Функция которая должна выполниться после обработчиков.</param>
        public virtual async Task InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
        {
            if (nextMiddleware is not null)
            {
                await nextMiddleware.InvokeOnPreUpdateAsync(context, next);
            }
            else
            {
                await next();
                await InvokeOnPostUpdateAsync(context);
            }
        }

        /// <summary>
        /// Выполнить предыдущий асинхронный промежуточный обработчик.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        public virtual async Task InvokeOnPostUpdateAsync(IBotContext context)
        {
            if (previousMiddleware is not null)
                await previousMiddleware.InvokeOnPostUpdateAsync(context);
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
