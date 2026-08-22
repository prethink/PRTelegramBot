namespace PRTelegramBot.BackgroundTasks.Models
{
    /// <summary>
    /// Background task status.
    /// </summary>
    public enum PRTaskStatus
    {
        /// <summary>
        /// Created, but not started.
        /// </summary>
        Pending,     
        /// <summary>
        /// Waiting for its start time.
        /// </summary>
        Scheduled,        
        /// <summary>
        /// Started running.
        /// </summary>
        Started,
        /// <summary>
        /// Initialization.
        /// </summary>
        Initialize,
        /// <summary>
        /// The task is running.
        /// </summary>
        Executing,          
        /// <summary>
        /// Temporarily paused.
        /// </summary>
        Paused,           
        /// <summary>
        /// Retrying after an error.
        /// </summary>
        Retrying,         
        /// <summary>
        /// Between repeat runs.
        /// </summary>
        WaitingNextRun,
        /// <summary>
        /// The run was skipped.
        /// </summary>
        Skipped,
        /// <summary>
        /// An error occurred.
        /// </summary>
        Error,
        /// <summary>
        /// Cancelled.
        /// </summary>
        Complete
    }

    /// <summary>
    /// The task completion statuses.
    /// </summary>
    public enum PRTaskCompletionResult
    {
        /// <summary>
        /// No status.
        /// </summary>
        None,
        /// <summary>
        /// Completed successfully.
        /// </summary>
        Success,
        /// <summary>
        /// Finished with an error.
        /// </summary>
        Failed,
        /// <summary>
        /// Cancelled.
        /// </summary>
        Canceled
    }
}
