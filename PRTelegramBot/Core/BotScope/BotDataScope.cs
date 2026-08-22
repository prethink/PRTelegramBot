using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core.BotScope
{
    /// <summary>
    /// Scope for the current bot and its bot context.
    /// Lets any part of the code safely obtain the current <see cref="IBotContext"/> and <see cref="PRBotBase"/>.
    /// Uses <see cref="AsyncLocal{T}"/> so it behaves correctly in asynchronous code.
    /// </summary>
    public sealed class BotDataScope : IDisposable
    {
        #region Fields and properties

        /// <summary>
        /// Service scope for the current bot.
        /// </summary>
        private readonly IServiceScope? scope;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new scope for the current context and bot.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <param name="prBot">Bot instance.</param>
        public BotDataScope(IBotContext context, PRBotBase prBot)
        {
            CurrentScope.contextStack.Value ??= new Stack<IBotContext>();
            CurrentScope.botStack.Value ??= new Stack<PRBotBase>();

            CurrentScope.contextStack.Value.Push(context);
            CurrentScope.botStack.Value.Push(prBot);

            if (CurrentScope.botStack.Value.Count == 1 && prBot.Options?.ServiceProvider != null)
            {
                scope = prBot.Options.ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                CurrentScope.serviceProvider.Value = scope.ServiceProvider;
            }
        }

        /// <summary>
        /// Creates a new scope for the current bot.
        /// </summary>
        /// <param name="prBot">Bot instance.</param>
        public BotDataScope(PRBotBase prBot) 
            : this(prBot.CreateContext(), prBot) { }

        #endregion

        #region IDisposable

        /// <summary>
        /// Disposes the scope and clears the context and the bot for the current thread / asynchronous context.
        /// </summary>
        public void Dispose()
        {
            if (CurrentScope.contextStack.Value?.Count > 0)
                CurrentScope.contextStack.Value.Pop();

            if (CurrentScope.botStack.Value?.Count > 0)
                CurrentScope.botStack.Value.Pop();

            if (CurrentScope.botStack.Value?.Count == 0)
            {
                CurrentScope.serviceProvider.Value = null;
                scope?.Dispose();
            }
        }

        #endregion
    }
}
