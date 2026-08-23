# PRBackgroundTaskRunner

```csharp

using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.BackgroundTasks.Models;
using PRTelegramBot.Core;
using PRTelegramBot.EventBus;
using PRTelegramBot.EventBus.Events;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Models.EventsArgs;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace PRTelegramBot.BackgroundTasks
{
    /// <summary>
    /// Обработчик фоновых задач.
    /// </summary>
    public sealed class PRBackgroundTaskRunner : IPRBackgroundTaskRunner, IPRTaskRunnerSubscriber
    {
        #region Поля и свойства

        /// <summary>
        /// Справочник запущенных задач.
        /// </summary>
        private readonly ConcurrentDictionary<Guid, IRunningBackgroundTaskData> activeTasks = new();

        /// <summary>
        /// Завершенные задачи.
        /// </summary>
        private readonly HashSet<IRunningBackgroundTaskData> completedTasks = new();

        /// <summary>
        /// Экземпляры фоновых задач.
        /// </summary>
        private HashSet<IPRBackgroundTask> registeredTaskInstances = new();

        /// <summary>
        /// Метаданные фоновых задач.
        /// </summary>
        private HashSet<IPRBackgroundTaskMetadata> registeredTaskMetadata = new();

        /// <summary>
        /// Бот.
        /// </summary>
        private PRBotBase bot;

        /// <summary>
        /// Глобальный токен бота.
        /// </summary>
        private CancellationToken botToken => bot.Options.CancellationTokenSource.Token;

        #endregion

        #region IPRBackgroundTaskRunner

        /// <summary>
        /// Текущий список запущенных задач. Содержит ключ метаданных и ссылку на запущенный Task.
        /// </summary>
        public IReadOnlyDictionary<Guid, IRunningBackgroundTaskData> ActiveTasks => activeTasks;

        /// <summary>
        /// Завершенные задачи.
        /// </summary>
        public IReadOnlyCollection<IRunningBackgroundTaskData> EndTasks => completedTasks;

        /// <summary>
        /// Экземпляры задач.
        /// </summary>
        public IReadOnlyCollection<IPRBackgroundTask> TaskInstance => registeredTaskInstances.ToList();

        /// <summary>
        /// Метаданные задач.
        /// </summary>
        public IReadOnlyCollection<IPRBackgroundTaskMetadata> Metadata => registeredTaskMetadata.ToList();

        /// <summary>
        /// Запустить фоновые задачи.
        /// </summary>
        public Task StartAsync()

        /// <summary>
        /// Запустить фоновую задачу.
        /// ВАЖНО. Перед вызовом данного метода убедитесь, что метаданные либо уже загружены в раннер, либо сама задача хранит в себе метаданные. 
        /// Например через атрибут <see cref="PRBackgroundTaskAttribute"/> или реализовывает интерфейс <see cref="IPRBackgroundTaskMetadata"/>
        /// </summary>
        /// <param name="backgroundTask">Фоновая задача.</param>
        public Task StartAsync(IPRBackgroundTask task)

        /// <summary>
        /// Запустить фоновую задачу.
        /// </summary>
        /// <param name="backgroundTask">Фоновая задача.</param>
        /// <param name="metadata">Метаданные.</param>
        public Task StartAsync(IPRBackgroundTask backgroundTask, IPRBackgroundTaskMetadata metadata)

        /// <summary>
        /// Останавливает выполнение всех запущенных фоновых задач.
        /// </summary>
        public async Task StopAsync()

        /// <summary>
        /// Останавливает выполнение указанной фоновой задачи.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи.</param>
        public async Task StopAsync(Guid taskId)

        /// <summary>
        /// Останавливает выполнение указанной фоновой задачи.
        /// </summary>
        /// <param name="metadata">Метаданные фоновой задачи.</param>
        public async Task StopAsync(IPRBackgroundTaskMetadata metadata)

        /// <summary>
        /// Инициализация фоновых задач.
        /// </summary>
        /// <param name="metadata">Метаданные.</param>
        /// <param name="tasks">Фоновые задачи.</param>
        public void Initialize(IEnumerable<IPRBackgroundTaskMetadata> metadata, IEnumerable<IPRBackgroundTask> tasks)

        #endregion

        #region Методы

        /// <summary>
        /// Добавить метаданные в общую коллекцию.
        /// </summary>
        /// <param name="metadata"></param>
        private bool AddMetadata(IPRBackgroundTaskMetadata metadata)

        /// <summary>
        /// Проверяем, что метаданные подходят только для другого бота.
        /// </summary>
        /// <param name="metadata">Метаданные фоновой задачи.</param>
        /// <returns>True - если метаданные для другого бота.</returns>
        private bool IsMetadataForAnotherBot(IPRBackgroundTaskMetadata metadata)

        /// <summary>
        /// Получает все зарегистрированные через DI экземпляры фоновых задач из указанного scope.
        /// </summary>
        /// <param name="scope">
        /// Scope, из которого необходимо получить сервисы. Может быть null.
        /// </param>
        /// <returns>
        /// Перечисление экземпляров <see cref="IPRBackgroundTask"/>. 
        /// Если <paramref name="scope"/> равен null или <see cref="IServiceProvider"/> внутри scope отсутствует, возвращается пустая коллекция.
        /// </returns>
        private IEnumerable<IPRBackgroundTask> ResolveBackgroundTasks(DisposableScope scope)

        /// <summary>
        /// Запустить фоновую задачу.
        /// </summary>
        /// <param name="metadata">Метаданные.</param>
        /// <param name="data">Метаданные.</param>
        /// <param name="token">Токен отмены.</param>
        private async Task RunTaskAsync(IPRBackgroundTaskMetadata metadata, IRunningBackgroundTaskData data, CancellationToken token = default)

        #region IPRTaskRunnerSubscriber

        /// <summary>
        /// Событие остановки фоновой задачи.
        /// </summary>
        /// <param name="botIds">Идентификаторы ботов.</param>
        /// <param name="taskId">Идентификатор задачи.</param>
        public void StopEvent(IEnumerable<long> botIds, Guid taskId)

        /// <summary>
        /// Событие остановки фоновой задачи.
        /// </summary>
        /// <param name="taskId">Идентификатор задачи.</param>
        public void StopEvent(Guid taskId)

        /// <summary>
        /// Подписывает экземпляр на события EventBus.
        /// </summary>
        public void Subscribe()

        /// <summary>
        /// Отписывает экземпляр от событий EventBus.
        /// </summary>
        public void Unsubscribe()

        /// <inheritdoc />
        public void Dispose()

        #endregion

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public PRBackgroundTaskRunner(PRBotBase bot)

        #endregion
    }
}

```
