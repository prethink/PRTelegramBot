using PRTelegramBot.Core;

namespace PRTelegramBot.BackgroundTasks.Interfaces
{
    /// <summary>
    /// Interface of a background task.
    /// </summary>
    public interface IPRBackgroundTask
    {
        /// <summary>
        /// Task identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Checks whether the background task can run right now.
        /// The framework calls this method before every execution attempt.
        /// Returning false means execution is skipped and
        /// the check is repeated at the next scheduled run.
        /// </summary>
        Task<bool> CanExecute();

        /// <summary>
        /// Starts running the background task.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task ExecuteAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Sets the bot instance so its context and services can be accessed.
        /// The framework calls this method when the background task is initialized.
        /// </summary>
        /// <param name="bot">Instance of the bot base class.</param>
        Task Initialize(PRBotBase bot);
    }
}
