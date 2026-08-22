namespace PRTelegramBot.BackgroundTasks.Interfaces
{
    /// <summary>
    /// Interface of the background task runner.
    /// Responsible for starting, stopping and managing the lifetime of background tasks.
    /// </summary>
    public interface IPRBackgroundTaskRunner
    {
        /// <summary>
        /// The current list of running tasks. Holds the metadata key and a reference to the running Task.
        /// </summary>
        IReadOnlyDictionary<Guid, IRunningBackgroundTaskData> ActiveTasks { get; }

        /// <summary>
        /// Finished tasks.
        /// </summary>
        IReadOnlyCollection<IRunningBackgroundTaskData> EndTasks { get; }

        /// <summary>
        /// Task instances.
        /// </summary>
        IReadOnlyCollection<IPRBackgroundTask> TaskInstance { get; }

        /// <summary>
        /// Task metadata.
        /// </summary>
        IReadOnlyCollection<IPRBackgroundTaskMetadata> Metadata { get; }

        /// <summary>
        /// Initializes the background tasks.
        /// </summary>
        /// <param name="metadata">Metadata.</param>
        /// <param name="tasks">Background tasks.</param>
        void Initialize(IEnumerable<IPRBackgroundTaskMetadata> metadata, IEnumerable<IPRBackgroundTask> tasks);

        /// <summary>
        /// Starts the background tasks.
        /// </summary>
        Task StartAsync();

        /// <summary>
        /// Starts the background task.
        /// IMPORTANT. Before calling this method, make sure the metadata is either already loaded into the runner or carried by the task itself. 
        /// For example through the <see cref="PRBackgroundTaskAttribute"/> attribute, or by implementing the <see cref="IPRBackgroundTaskMetadata"/> interface
        /// </summary>
        /// <param name="backgroundTask">Background task.</param>
        Task StartAsync(IPRBackgroundTask backgroundTask);

        /// <summary>
        /// Starts the background task.
        /// </summary>
        /// <param name="backgroundTask">Background task.</param>
        /// <param name="metadata">Metadata.</param>
        Task StartAsync(IPRBackgroundTask backgroundTask, IPRBackgroundTaskMetadata metadata);

        /// <summary>
        /// Stops all running background tasks.
        /// </summary>
        Task StopAsync();

        /// <summary>
        /// Stops the specified background task.
        /// </summary>
        /// <param name="taskId">Task identifier.</param>
        Task StopAsync(Guid taskId);

        /// <summary>
        /// Stops the specified background task.
        /// </summary>
        /// <param name="metadata">Background task metadata.</param>
        Task StopAsync(IPRBackgroundTaskMetadata metadata);
    }
}
