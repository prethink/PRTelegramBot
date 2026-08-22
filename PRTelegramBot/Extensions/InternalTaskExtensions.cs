using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Internal helpers for tasks that are deliberately not awaited.
    /// </summary>
    internal static class InternalTaskExtensions
    {
        #region Methods

        /// <summary>
        /// Lets the task run without awaiting it and logs the exception if it fails.
        /// </summary>
        /// <remarks>
        /// Event handlers are invoked without an await on purpose, so that a slow subscriber
        /// cannot hold up the processing of other updates. Without this helper an exception
        /// thrown by a subscriber would be lost together with the unobserved task.
        /// </remarks>
        /// <param name="task">The task that is deliberately not awaited.</param>
        /// <param name="context">Bot context the logger is resolved from.</param>
        /// <param name="source">Type the log entry is attributed to.</param>
        internal static void FireAndForget(this Task task, IBotContext context, Type source)
        {
            task.ContinueWith(
                faulted => context.Current.GetLogger(source).LogErrorInternal(faulted.Exception!),
                CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default);
        }

        #endregion
    }
}
