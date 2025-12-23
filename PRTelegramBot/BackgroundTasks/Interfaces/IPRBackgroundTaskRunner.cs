using PRTelegramBot.BackgroundTasks;

namespace PRTelegramBot.BackgroundTasks.Interfaces
{
    /// <summary>
    /// Интерфейс исполнителя фоновых задач.
    /// Отвечает за запуск, остановку и управление жизненным циклом фоновых задач.
    /// </summary>
    public interface IPRBackgroundTaskRunner
    {
        /// <summary>
        /// Текущий список запущенных задач. Содержит ключ метаданных и ссылку на запущенный Task.
        /// </summary>
        IReadOnlyDictionary<Guid, IRunningBackgroundTaskData> ActiveTasks { get; }

        /// <summary>
        /// Завершенные задачи.
        /// </summary>
        IReadOnlyCollection<IRunningBackgroundTaskData> EndTasks { get; }

        /// <summary>
        /// Экземпляры задач.
        /// </summary>
        IReadOnlyCollection<IPRBackgroundTask> TaskInstance { get; }

        /// <summary>
        /// Метаданные задач.
        /// </summary>
        IReadOnlyCollection<IPRBackgroundTaskMetadata> Metadata { get; }

        /// <summary>
        /// Инициализация фоновых задач.
        /// </summary>
        /// <param name="metadata">Метаданные.</param>
        /// <param name="tasks">Фоновые задачи.</param>
        void Initialize(IEnumerable<IPRBackgroundTaskMetadata> metadata, IEnumerable<IPRBackgroundTask> tasks);

        /// <summary>
        /// Запустить фоновые задачи.
        /// </summary>
        Task StartAsync();

        /// <summary>
        /// Запустить фоновую задачу.
        /// ВАЖНО. Перед вызовом данного метода убедитесь, что метаданные либо уже загружены в раннер, либо сама задача хранит в себе метаданные. 
        /// Например через атрибут <see cref="PRBackgroundTaskAttribute"/> или реализовывает интерфейс <see cref="IPRBackgroundTaskMetadata"/>
        /// </summary>
        /// <param name="backgroundTask">Фоновая задача.</param>
        Task StartAsync(IPRBackgroundTask backgroundTask);

        /// <summary>
        /// Запустить фоновую задачу.
        /// </summary>
        /// <param name="backgroundTask">Фоновая задача.</param>
        /// <param name="metadata">Метаданные.</param>
        Task StartAsync(IPRBackgroundTask backgroundTask, IPRBackgroundTaskMetadata metadata);

        /// <summary>
        /// Останавливает выполнение всех запущенных фоновых задач.
        /// </summary>
        Task StopAsync();

        /// <summary>
        /// Останавливает выполнение указанной фоновой задачи.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи.</param>
        Task StopAsync(Guid taskId);

        /// <summary>
        /// Останавливает выполнение указанной фоновой задачи.
        /// </summary>
        /// <param name="metadata">Метаданные фоновой задачи.</param>
        Task StopAsync(IPRBackgroundTaskMetadata metadata);
    }
}
