using PRTelegramBot.BackgroundTasks.Models;

namespace PRTelegramBot.BackgroundTasks.Interfaces
{
    /// <summary>
    /// Interface for the data of a running task.
    /// </summary>
    public interface IRunningBackgroundTaskData
    {
        /// <summary>
        /// Task.
        /// </summary>
        Task Task { get; }

        /// <summary>
        /// Metadata.
        /// </summary>
        IPRBackgroundTaskMetadata Metadata { get; }

        /// <summary>
        /// Errors
        /// </summary>
        IReadOnlyList<Exception> Errors { get; }

        /// <summary>
        /// Number of errors.
        /// </summary>
        int ErrorCount { get; }

        /// <summary>
        /// Number of runs
        /// </summary>
        int ExecutedCount { get; }

        /// <summary>
        /// Date and time the task started.
        /// </summary>
        DateTime? StartDate { get; }

        /// <summary>
        /// Date and time the task finished.
        /// </summary>
        DateTime? EndDate { get; }

        /// <summary>
        /// Task status.
        /// </summary>
        PRTaskStatus Status { get; }

        /// <summary>
        /// The task's completion status.
        /// </summary>
        PRTaskCompletionResult CompleteStatus { get; }

        /// <summary>
        /// Increments the task's run counter.
        /// </summary>
        void IncrementExecutionCount();

        /// <summary>
        /// Records an error.
        /// </summary>
        /// <param name="ex">Exception.</param>
        void AddError(Exception ex);

        /// <summary>
        /// Sets the task status.
        /// </summary>
        /// <param name="status">Status.</param>
        void SetStatus(PRTaskStatus status);

        /// <summary>
        /// Sets the task's completion status.
        /// </summary>
        /// <param name="status">Status.</param>
        void SetCompleteStatus(PRTaskCompletionResult status);

        /// <summary>
        /// Starts the task.
        /// </summary>
        void StartTask();

        /// <summary>
        /// Finishes the task.
        /// </summary>
        void EndTask();

        /// <summary>
        /// The cancellation token source.
        /// </summary>
        CancellationTokenSource CancellationTokenSource { get; }
    }
}
