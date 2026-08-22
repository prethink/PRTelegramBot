using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core.BotScope
{
    /// <summary>
    /// Provides access to the current state of the context and the bot.
    /// Read-only. The stack is managed by BotDataScope.
    /// </summary>
    public static class CurrentScope
    {
        #region Fields and properties

        /// <summary>
        /// Holds the stack of bot contexts for the current asynchronous flow.
        /// BotDataScope uses it to manage the current context, 
        /// while CurrentScope / BotScopeInfo are used for safe reads.
        /// </summary>
        internal static readonly AsyncLocal<Stack<IBotContext>> contextStack = new();

        /// <summary>
        /// Holds the stack of bot instances for the current asynchronous flow.
        /// BotDataScope uses it to manage the current bot instance, 
        /// while CurrentScope / BotScopeInfo are used for safe reads.
        /// </summary>
        internal static readonly AsyncLocal<Stack<PRBotBase>> botStack = new();

        /// <summary>
        /// Service provider.
        /// </summary>
        internal static readonly AsyncLocal<IServiceProvider?> serviceProvider = new();

        /// <summary>
        /// The current bot context (read-only).
        /// </summary>
        public static IBotContext? Context => contextStack.Value?.Count > 0
            ? contextStack.Value.Peek()
            : null;

        /// <summary>
        /// The current bot (read-only).
        /// </summary>
        public static PRBotBase? Bot => botStack.Value?.Count > 0
            ? botStack.Value.Peek()
            : null;

        /// <summary>
        /// Services of the current bot (read-only).
        /// </summary>
        public static IServiceProvider? Services => serviceProvider.Value;

        #endregion
    }
}
