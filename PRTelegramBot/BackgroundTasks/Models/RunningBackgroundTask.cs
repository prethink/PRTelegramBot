using PRTelegramBot.BackgroundTasks.Interfaces;

namespace PRTelegramBot.BackgroundTasks.Models
{
    /// <summary>
    /// Data of the running task.
    /// </summary>
    public sealed class RunningBackgroundTask : IRunningBackgroundTaskData
    {
        #region Fields and properties

        /// <summary>
        /// Errors.
        /// </summary>
        private List<Exception> errors = new();

        /// <summary>
        /// Number of times the task has run.
        /// </summary>
        private int executeCount = 0;

        /// <summary>
        /// Reference to the method that starts the tasks.
        /// </summary>
        private Func<IPRBackgroundTaskMetadata, IRunningBackgroundTaskData, CancellationToken, Task> startAsync;

        #endregion

        #region IRunningBackgroundTaskData

        /// <inheritdoc />
        public Task Task { get; private set; }

        /// <inheritdoc />
        public CancellationTokenSource CancellationTokenSource { get; private set; }

        /// <inheritdoc />
        public IPRBackgroundTaskMetadata Metadata { get; private set; }

        /// <inheritdoc />
        public IReadOnlyList<Exception> Errors => errors.ToList();

        /// <inheritdoc />
        public int ErrorCount => Errors.Count();

        /// <inheritdoc />
        public int ExecutedCount => executeCount;

        /// <inheritdoc />
        public DateTime? StartDate { get; private set; }

        /// <inheritdoc />
        public DateTime? EndDate { get; private set; }

        /// <inheritdoc />
        public PRTaskStatus Status { get; private set; }


        /// <inheritdoc />
        public PRTaskCompletionResult CompleteStatus { get; private set; }

        /// <inheritdoc />
        public void AddError(Exception ex)
        {
            errors.Add(ex);
        }

        /// <inheritdoc />
        public void IncrementExecutionCount()
        {
            executeCount++;
        }

        /// <inheritdoc />
        public void SetStatus(PRTaskStatus status)
        {
            Status = status;
        }

        /// <inheritdoc />
        public void SetCompleteStatus(PRTaskCompletionResult status)
        {
            CompleteStatus = status;
        }

        /// <inheritdoc />
        public void StartTask()
        {
            StartDate = DateTime.Now;
            Task = startAsync.Invoke(Metadata, this, CancellationTokenSource.Token);
        }

        /// <inheritdoc />
        public void EndTask()
        {
            EndDate = DateTime.Now;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the data of a running background task.
        /// </summary>
        /// <param name="StartAsync">Delegate that starts the task.</param>
        /// <param name="metadata">Background task metadata.</param>
        /// <param name="cancellationTokenSource">Cancellation token source of the task.</param>
        /// <returns>Data of the running task.</returns>
        public static IRunningBackgroundTaskData Create(Func<IPRBackgroundTaskMetadata, IRunningBackgroundTaskData, CancellationToken, Task> StartAsync, IPRBackgroundTaskMetadata metadata, CancellationTokenSource cancellationTokenSource)
        {
            var runningBackgroundTaskData = new RunningBackgroundTask();
            runningBackgroundTaskData.CancellationTokenSource = cancellationTokenSource;
            runningBackgroundTaskData.Metadata = metadata;
            runningBackgroundTaskData.startAsync = StartAsync;
            return runningBackgroundTaskData;
        }

        #endregion
    }
}
