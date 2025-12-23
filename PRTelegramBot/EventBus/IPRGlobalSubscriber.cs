namespace PRTelegramBot.EventBus
{
    /// <summary>
    /// Интерфейс глобального подписчика.
    /// Используется для EventBus (шина событий) системы.
    /// </summary>
    public interface IPRGlobalSubscriber : IDisposable
    {
        /// <summary>
        /// Подписывает экземпляр на события EventBus.
        /// </summary>
        void Subscribe();

        /// <summary>
        /// Отписывает экземпляр от событий EventBus.
        /// </summary>
        void Unsubscribe();
    }
}
