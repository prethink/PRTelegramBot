namespace PRTelegramBot.EventBus.Events
{
    /// <summary>
    /// Subscriber of the background task runner.
    /// </summary>
    public interface IPRTaskRunnerSubscriber : IPRGlobalSubscriber
    {
        /// <summary>
        /// Event raised when a background task stops.
        /// </summary>
        /// <param name="botIds">Bot identifiers.</param>
        /// <param name="taskId">Task identifier.</param>
        void StopEvent(IEnumerable<long> botIds, Guid taskId);

        /// <summary>
        /// Event raised when a background task stops.
        /// </summary>
        /// <param name="taskId">Task identifier.</param>
        void StopEvent(Guid taskId);
    }
}
