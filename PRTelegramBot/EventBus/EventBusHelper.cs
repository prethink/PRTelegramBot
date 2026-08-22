namespace PRTelegramBot.EventBus
{
    /// <summary>
    /// Helper class for working with EventBus subscribers.
    /// Responsible for determining and caching the event types,
    /// the global subscriber is subscribed to.
    /// </summary>
    internal static class EventBusHelper
    {
        /// <summary>
        /// Cache that maps a subscriber type to its list of event interfaces,
        /// that it implements.
        /// Used to speed up repeated subscribe/unsubscribe calls
        /// and to cut down the number of reflection calls.
        /// </summary>
        private static Dictionary<Type, List<Type>> cashedSubscriberTypes = new Dictionary<Type, List<Type>>();

        /// <summary>
        /// Returns the list of subscriber types (interfaces),
        /// implemented by the specified global subscriber.
        /// </summary>
        /// <param name="globalSubscriber">The global subscriber instance.</param>
        /// <returns>
        /// The list of interfaces that implement <see cref="IPRGlobalSubscriber"/>,
        /// that EventBus uses to route the events.
        /// </returns>
        public static List<Type> GetSubscriberTypes(IPRGlobalSubscriber globalSubscriber)
        {
            Type type = globalSubscriber.GetType();
            if (cashedSubscriberTypes.ContainsKey(type))
                return cashedSubscriberTypes[type];

            List<Type> subscriberTypes = type
                .GetInterfaces()
                .Where(t => t.GetInterfaces()
                    .Contains(typeof(IPRGlobalSubscriber)))
                .ToList();

            cashedSubscriberTypes[type] = subscriberTypes;
            return subscriberTypes;
        }
    }
}
