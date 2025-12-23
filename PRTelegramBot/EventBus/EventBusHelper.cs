namespace PRTelegramBot.EventBus
{
    /// <summary>
    /// Вспомогательный класс для работы с подписчиками EventBus.
    /// Отвечает за определение и кэширование типов событий,
    /// на которые подписан глобальный подписчик.
    /// </summary>
    internal static class EventBusHelper
    {
        /// <summary>
        /// Кэш соответствий типа подписчика и списка интерфейсов событий,
        /// которые он реализует.
        /// Используется для ускорения повторных подписок/отписок
        /// и уменьшения количества reflection-вызовов.
        /// </summary>
        private static Dictionary<Type, List<Type>> cashedSubscriberTypes = new Dictionary<Type, List<Type>>();

        /// <summary>
        /// Возвращает список типов подписчиков (интерфейсов),
        /// реализуемых указанным глобальным подписчиком.
        /// </summary>
        /// <param name="globalSubscriber">Экземпляр глобального подписчика.</param>
        /// <returns>
        /// Список интерфейсов, реализующих <see cref="IPRGlobalSubscriber"/>,
        /// которые используются EventBus для маршрутизации событий.
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
