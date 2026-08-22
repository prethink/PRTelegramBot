namespace PRTelegramBot.EventBus
{
    /// <summary>
    /// Interface of a global subscriber.
    /// Used by the EventBus system.
    /// </summary>
    public interface IPRGlobalSubscriber : IDisposable
    {
        /// <summary>
        /// Subscribes the instance to the EventBus events.
        /// </summary>
        void Subscribe();

        /// <summary>
        /// Unsubscribes the instance from the EventBus events.
        /// </summary>
        void Unsubscribe();
    }
}
