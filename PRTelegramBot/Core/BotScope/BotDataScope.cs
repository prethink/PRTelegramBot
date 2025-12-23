using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core.BotScope
{
    /// <summary>
    /// Scope для текущего бота и контекста бота.
    /// Позволяет в любом месте кода безопасно получить текущий <see cref="IBotContext"/> и <see cref="PRBotBase"/>.
    /// Использует <see cref="AsyncLocal{T}"/> для корректной работы в асинхронном коде.
    /// </summary>
    public sealed class BotDataScope : IDisposable
    {
        #region Поля и свойства

        /// <summary>
        /// Scope сервисов для текущего бота.
        /// </summary>
        private readonly IServiceScope? scope;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Создаёт новый scope для текущего контекста и бота.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="prBot">Экземпляр бота.</param>
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
        /// Создаёт новый scope для текущего бота.
        /// </summary>
        /// <param name="prBot">Экземпляр бота.</param>
        public BotDataScope(PRBotBase prBot) 
            : this(prBot.CreateContext(), prBot) { }

        #endregion

        #region IDisposable

        /// <summary>
        /// Освобождает scope и очищает контекст и бота для текущего потока/асинхронного контекста.
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
