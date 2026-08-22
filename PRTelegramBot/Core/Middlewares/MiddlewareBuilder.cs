using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Core.BotScope;

namespace PRTelegramBot.Core.Middlewares
{
    /// <summary>
    /// Builder for constructing the middleware chain.
    /// </summary>
    public class MiddlewareBuilder
    {
        #region Methods

        /// <summary>
        /// Builds the middleware chain.
        /// </summary>
        /// <param name="bot">Bot.</param>
        /// <returns>The handler chain.</returns>
        public virtual MiddlewareBase Build(PRBotBase bot)
        {
            var combineMiddlewares = new List<MiddlewareBase>(bot.Options.Middlewares);
            var diMiddlewares = CurrentScope.Services?.GetServices<MiddlewareBase>() ?? Enumerable.Empty<MiddlewareBase>();
            combineMiddlewares.AddRange(diMiddlewares);

            if (!combineMiddlewares.Any())
                return new EmptyMiddleware();

            MiddlewareBase current = new EmptyMiddleware();

            var orderedMiddlewares = combineMiddlewares.OrderBy(m => m.ExecutionOrder).ToList();

            if (orderedMiddlewares.Count == 1)
            {
                current = orderedMiddlewares[0];
            }
            else if(orderedMiddlewares.Count > 1)
            {
                current = orderedMiddlewares[0];
                current.SetNext(orderedMiddlewares[1]);
                for (int i = 1; i < orderedMiddlewares.Count; i++) 
                {
                    if(i + 1 < orderedMiddlewares.Count)
                    {
                        orderedMiddlewares[i].SetNext(orderedMiddlewares[i + 1], orderedMiddlewares[i - 1]);
                    }
                    else
                    {
                        orderedMiddlewares[i].SetPrevious(orderedMiddlewares[i - 1]);
                    }
                }
            }

            return current;
        }

        #endregion
    }
}
