namespace PRTelegramBot.EventBus
{
    /// <summary>
    /// Helper container that stores the subscribers of a single event type.
    /// Makes adding and removing subscribers safe
    /// while an event broadcast is running.
    /// </summary>
    /// <typeparam name="TSubscriber">Subscriber type.</typeparam>
    internal class SubscribersList<TSubscriber> where TSubscriber : class
    {
        /// <summary>
        /// Flag indicating that once the broadcast finishes,
        /// the list has to be cleaned up of the removed subscribers.
        /// </summary>
        private bool needsCleanUp = false;

        /// <summary>
        /// Indicates that an event broadcast is currently running.
        /// Used for deferred removal of subscribers,
        /// so the collection is not modified while it is being iterated.
        /// </summary>
        public bool Executing;

        /// <summary>
        /// The list of subscribers.
        /// While the broadcast is running, the items may temporarily
        /// be replaced with <c>null</c> and removed later.
        /// </summary>
        public readonly List<TSubscriber> List = new List<TSubscriber>();

        /// <summary>
        /// Adds a subscriber to the list.
        /// </summary>
        /// <param name="subscriber">The subscriber instance.</param>
        public void Add(TSubscriber subscriber)
        {
            List.Add(subscriber);
        }

        /// <summary>
        /// Removes a subscriber from the list.
        /// If the removal happens while events are being broadcast,
        /// the subscriber is marked for later cleanup.
        /// </summary>
        /// <param name="subscriber">The subscriber instance.</param>
        public void Remove(TSubscriber subscriber)
        {
            if (Executing)
            {
                var i = List.IndexOf(subscriber);
                if (i >= 0)
                {
                    needsCleanUp = true;
                    List[i] = null;
                }
            }
            else
            {
                List.Remove(subscriber);
            }
        }

        /// <summary>
        /// Removes the subscribers marked for removal from the list
        /// while an event broadcast is running.
        /// </summary>
        public void Cleanup()
        {
            if (!needsCleanUp)
                return;

            List.RemoveAll(s => s == null);
            needsCleanUp = false;
        }
    }
}
