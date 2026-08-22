namespace PRTelegramBot.EventBus
{
    /// <summary>
    /// Event bus.
    /// </summary>
    public static class PREventBus
    {
        #region Fields and properties

        /// <summary>
        /// Event subscribers.
        /// </summary>
        private static Dictionary<Type, SubscribersList<IPRGlobalSubscriber>> subscribers = new Dictionary<Type, SubscribersList<IPRGlobalSubscriber>>();

        #endregion

        #region Methods

        /// <summary>
        /// Subscribes.
        /// </summary>
        /// <param name="subscriber">Subscriber.</param>
        public static void Subscribe(IPRGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = EventBusHelper.GetSubscriberTypes(subscriber);
            foreach (Type type in subscriberTypes)
            {
                if (!subscribers.ContainsKey(type))
                    subscribers[type] = new SubscribersList<IPRGlobalSubscriber>();

                subscribers[type].Add(subscriber);
            }
        }

        /// <summary>
        /// Unsubscribes.
        /// </summary>
        /// <param name="subscriber">Subscriber.</param>
        public static void Unsubscribe(IPRGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = EventBusHelper.GetSubscriberTypes(subscriber);
            foreach (Type type in subscriberTypes)
            {
                if (subscribers.ContainsKey(type))
                    subscribers[type].Remove(subscriber);
            }
        }

        /// <summary>
        /// Raises the event.
        /// </summary>
        /// <typeparam name="TSubscriber">Subscriber type.</typeparam>
        /// <param name="action">The method to invoke.</param>
        public static void RaiseEvent<TSubscriber>(Action<TSubscriber> action) where TSubscriber : class, IPRGlobalSubscriber
        {
            if (!subscribers.ContainsKey(typeof(TSubscriber)))
                return;

            SubscribersList<IPRGlobalSubscriber> sbrs = subscribers[typeof(TSubscriber)];
            sbrs.Executing = true;
            foreach (IPRGlobalSubscriber subscriber in sbrs.List.ToList())
            {
                try
                {
                    action.Invoke(subscriber as TSubscriber);
                }
                catch (Exception e)
                {
                    //Debug.LogError($"{subscribers.GetType()} - {e}");
                }
            }
            sbrs.Executing = false;
            sbrs.Cleanup();
        }

        #endregion
    }
}
