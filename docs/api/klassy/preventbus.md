# PREventBus

```csharp
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

        /// <summary>
        /// Отписаться.
        /// </summary>
        /// <param name="subscriber">Подписчик.</param>
        public static void Unsubscribe(IPRGlobalSubscriber subscriber)

        /// <summary>
        /// Вызвать событие.
        /// </summary>
        /// <typeparam name="TSubscriber">Тип подписчика.</typeparam>
        /// <param name="action">Метод вызова.</param>
        public static void RaiseEvent<TSubscriber>(Action<TSubscriber> action) where TSubscriber : class, IPRGlobalSubscriber

        #endregion
    }
}

```
