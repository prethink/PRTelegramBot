using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Core.BotScope
{
    /// <summary>
    /// Предоставляет доступ к текущему состоянию контекста и бота.
    /// Служит только для чтения. Управление стеком выполняет BotDataScope.
    /// </summary>
    public static class CurrentScope
    {
        #region Поля и свойства

        /// <summary>
        /// Хранит стек контекстов бота для текущего асинхронного потока.
        /// Используется BotDataScope для управления текущим контекстом, 
        /// а CurrentScope / BotScopeInfo для безопасного чтения.
        /// </summary>
        internal static readonly AsyncLocal<Stack<IBotContext>> contextStack = new();

        /// <summary>
        /// Хранит стек экземпляров бота для текущего асинхронного потока.
        /// Используется BotDataScope для управления текущим экземпляром бота, 
        /// а CurrentScope / BotScopeInfo для безопасного чтения.
        /// </summary>
        internal static readonly AsyncLocal<Stack<PRBotBase>> botStack = new();

        /// <summary>
        /// Сервис провайдер.
        /// </summary>
        internal static readonly AsyncLocal<IServiceProvider?> serviceProvider = new();

        /// <summary>
        /// Текущий контекст бота (read-only).
        /// </summary>
        public static IBotContext? Context => contextStack.Value?.Count > 0
            ? contextStack.Value.Peek()
            : null;

        /// <summary>
        /// Текущий бот (read-only).
        /// </summary>
        public static PRBotBase? Bot => botStack.Value?.Count > 0
            ? botStack.Value.Peek()
            : null;

        /// <summary>
        /// Сервисы текущего бота (read-only).
        /// </summary>
        public static IServiceProvider? Services => serviceProvider.Value;

        #endregion
    }
}
