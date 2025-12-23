namespace PRTelegramBot.EventBus.Events
{
    /// <summary>
    /// Подписчик раннера фоновой задачи.
    /// </summary>
    public interface IPRTaskRunnerSubscriber : IPRGlobalSubscriber
    {
        /// <summary>
        /// Событие остановки фоновой задачи.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="taskId">Идентификатор задачи.</param>
        void StopEvent(IEnumerable<long> botIds, Guid taskId);

        /// <summary>
        /// Событие остановки фоновой задачи.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи.</param>
        void StopEvent(Guid taskId);
    }
}
