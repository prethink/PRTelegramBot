namespace PRTelegramBot.Models.Enums
{
    /// <summary>
    /// The result of executing the step.
    /// </summary>
    public enum ExecuteStepResult
    {
        /// <summary>
        /// The step completed successfully.
        /// </summary>
        Success,
        /// <summary>
        /// The step could not be executed.
        /// </summary>
        Failure,
        /// <summary>
        /// The time window for executing the step has expired.
        /// </summary>
        ExpiredTime
    }
}
