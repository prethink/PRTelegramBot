namespace PRTelegramBot.EventBus
{
    /// <summary>
    /// Вспомогательный контейнер для хранения подписчиков одного типа события.
    /// Обеспечивает безопасное добавление и удаление подписчиков
    /// во время выполнения рассылки событий.
    /// </summary>
    /// <typeparam name="TSubscriber">Тип подписчика.</typeparam>
    internal class SubscribersList<TSubscriber> where TSubscriber : class
    {
        /// <summary>
        /// Флаг, указывающий, что после завершения рассылки
        /// требуется очистка списка от удалённых подписчиков.
        /// </summary>
        private bool needsCleanUp = false;

        /// <summary>
        /// Указывает, что в данный момент выполняется рассылка событий.
        /// Используется для отложенного удаления подписчиков,
        /// чтобы избежать модификации коллекции во время перебора.
        /// </summary>
        public bool Executing;

        /// <summary>
        /// Список подписчиков.
        /// Во время выполнения рассылки элементы могут временно
        /// заменяться на <c>null</c> и удаляться позднее.
        /// </summary>
        public readonly List<TSubscriber> List = new List<TSubscriber>();

        /// <summary>
        /// Добавляет подписчика в список.
        /// </summary>
        /// <param name="subscriber">Экземпляр подписчика.</param>
        public void Add(TSubscriber subscriber)
        {
            List.Add(subscriber);
        }

        /// <summary>
        /// Удаляет подписчика из списка.
        /// Если удаление происходит во время рассылки событий,
        /// подписчик помечается для последующей очистки.
        /// </summary>
        /// <param name="subscriber">Экземпляр подписчика.</param>
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
        /// Очищает список от подписчиков, помеченных на удаление
        /// во время выполнения рассылки событий.
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
