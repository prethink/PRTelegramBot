using PRTelegramBot.BackgroundTasks.Interfaces;

namespace PRTelegramBot.BackgroundTasks.Models
{
    public sealed class RunningBackgroundTask : IRunningBackgroundTaskData
    {
        #region Поля и свойства

        private List<Exception> errors = new();

        private int executeCount = 0;

        private Func<IPRBackgroundTaskMetadata, IRunningBackgroundTaskData, CancellationToken, Task> startAsync;

        #endregion

        #region IRunningBackgroundTaskData

        /// <inheritdoc />
        public Task Task { get; protected set; }

        /// <inheritdoc />
        public CancellationTokenSource CancellationTokenSource { get; protected set; }

        /// <inheritdoc />
        public IPRBackgroundTaskMetadata Metadata { get; protected set; }

        /// <inheritdoc />
        public IReadOnlyList<Exception> Errors => errors.ToList();

        /// <inheritdoc />
        public int ErrorCount => Errors.Count();

        /// <inheritdoc />
        public int ExecutedCount => executeCount;

        /// <inheritdoc />
        public DateTime? StartDate { get; protected set; }

        /// <inheritdoc />
        public DateTime? EndDate { get; protected set; }

        /// <inheritdoc />
        public PRTaskStatus Status { get; protected set; }


        /// <inheritdoc />
        public PRTaskCompletionResult CompleteStatus { get; protected set; }

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

        #region Методы

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
