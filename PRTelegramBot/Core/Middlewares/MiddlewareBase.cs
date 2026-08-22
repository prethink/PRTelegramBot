using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core.Middlewares
{
    /// <summary>
    /// Base middleware handler.
    /// </summary>
    public abstract class MiddlewareBase
    {
        #region Fields and properties

        /// <summary>
        /// The next handler.
        /// </summary>
        protected MiddlewareBase nextMiddleware;

        /// <summary>
        /// The next handler.
        /// </summary>
        protected MiddlewareBase previousMiddleware;

        /// <summary>
        /// The order the middleware runs in within the pipeline.
        /// A lower value means a higher priority and earlier execution.
        /// </summary>
        public abstract int ExecutionOrder { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the next asynchronous middleware handler.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="next">The function to run after the handlers.</param>
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
        /// Executes the previous asynchronous middleware handler.
        /// </summary>
        /// <param name="context">Bot context.</param>
        public virtual async Task InvokeOnPostUpdateAsync(IBotContext context)
        {
            if (previousMiddleware is not null)
                await previousMiddleware.InvokeOnPostUpdateAsync(context);
        }

        /// <summary>
        /// Sets the next handler.
        /// </summary>
        /// <param name="next">The next handler.</param>
        public void SetNext(MiddlewareBase next)
        {
            this.nextMiddleware = next;
        }

        /// <summary>
        /// Sets the next handler.
        /// </summary>
        /// <param name="next">The next handler.</param>
        /// <param name="previous">The previous handler.</param>
        public void SetNext(MiddlewareBase next, MiddlewareBase previous)
        {
            this.nextMiddleware = next;
            this.previousMiddleware = previous;
        }

        /// <summary>
        /// Sets the previous handler.
        /// </summary>
        /// <param name="previous">The previous handler.</param>
        public void SetPrevious(MiddlewareBase previous)
        {
            this.previousMiddleware = previous;
        }

        #endregion
    }
}
