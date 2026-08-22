using PRTelegramBot.Core;

namespace PRTelegramBot.BackgroundTasks.Interfaces
{
    /// <summary>
    /// Interface for background task metadata.
    /// Holds the information needed to schedule the task and control how it runs
    /// without describing its business logic.
    /// </summary>
    public interface IPRBackgroundTaskMetadata
    {
        /// <summary>
        /// Identifiers of the bots the background task is intended for.
        /// An optional parameter.
        /// Used to separate background tasks per bot when working through DI.
        /// An empty collection, or the presence of <see cref="PRConstants.ALL_BOTS_ID"/>,
        /// means the task applies to every bot.
        /// </summary>
        HashSet<long> BotIds { get; }

        /// <summary>
        /// Unique identifier of the background task.
        /// Used to match the metadata with the task implementation.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Unique name of the background task.
        /// Used for logging, diagnostics and identifying the task.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Delay in seconds before the background task runs for the first time.
        /// A value of <c>null</c>, or a value less than or equal to 0, means the task starts immediately.
        /// </summary>
        int? InitialDelaySeconds { get; }

        /// <summary>
        /// Interval in seconds at which the background task repeats.
        /// The minimum repeat interval is always 1 second.
        /// </summary>
        int? RepeatSeconds { get; }

        /// <summary>
        /// Maximum number of runs of the background task
        /// (including both successful and failed attempts).
        /// A value of <c>null</c> or <c>-1</c> means an unlimited number of runs.
        /// </summary>
        int? MaxRepeatCount { get; }

        /// <summary>
        /// Maximum number of attempts to run the background task when errors occur
        /// (including the first run).
        /// A value of <c>null</c> or <c>-1</c> means no limit.
        /// A value of 1 means a single run with no retries on error.
        /// </summary>
        int? MaxErrorAttempts { get; }
    }
}
