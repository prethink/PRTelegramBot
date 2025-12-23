namespace PRTelegramBot.EventBus
{
    /// <summary>
    /// Шина событий.
    /// </summary>
    public static class PREventBus
    {
        #region Поля и свойства

        /// <summary>
        /// Подписчики событий.
        /// </summary>
        private static Dictionary<Type, SubscribersList<IPRGlobalSubscriber>> subscribers = new Dictionary<Type, SubscribersList<IPRGlobalSubscriber>>();

        #endregion

        #region Методы

        /// <summary>
        /// Подписаться.
        /// </summary>
        /// <param name="subscriber">Подписчик.</param>
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
        /// Отписаться.
        /// </summary>
        /// <param name="subscriber">Подписчик.</param>
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
        /// Вызвать событие.
        /// </summary>
        /// <typeparam name="TSubscriber">Тип подписчика.</typeparam>
        /// <param name="action">Метод вызова.</param>
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
