
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

        /// <inheritdoc />
        public IReadOnlyDictionary<Guid, IRunningBackgroundTaskData> ActiveTasks => activeTasks;

        /// <inheritdoc />
        public IReadOnlyCollection<IRunningBackgroundTaskData> EndTasks => completedTasks;

        /// <inheritdoc />
        public IReadOnlyCollection<IPRBackgroundTask> TaskInstance => registeredTaskInstances.ToList();

        /// <inheritdoc />
        public IReadOnlyCollection<IPRBackgroundTaskMetadata> Metadata => registeredTaskMetadata.ToList();

        /// <inheritdoc />
        public Task StartAsync()
        {
            foreach (var mtd in registeredTaskMetadata)
            {
                if (ActiveTasks.ContainsKey(mtd.Id))
                {
                    bot.Events.OnCommonLogInvoke($"[{nameof(PRBackgroundTaskRunner)}] {mtd.Name} уже запущена.");
                    continue;
                }

                activeTasks.TryAdd(mtd.Id, RunningBackgroundTask.Create(RunTaskAsync, mtd, new CancellationTokenSource()));
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task StartAsync(IPRBackgroundTask task)
        {
            var mtd = task.GetMetadata(registeredTaskMetadata);
            return StartAsync(task, mtd);
        }

        /// <inheritdoc />
        public Task StartAsync(IPRBackgroundTask backgroundTask, IPRBackgroundTaskMetadata metadata)
        {
            if (backgroundTask is null)
                throw new ArgumentNullException(nameof(backgroundTask));

            if (metadata is null)
                throw new ArgumentNullException(nameof(metadata));

            this.AddMetadata(metadata);

            if(!this.registeredTaskMetadata.Any(x => x.Id == backgroundTask.Id))
            {
                bot.Events.OnCommonLogInvoke($"[{nameof(PRBackgroundTaskRunner)}] Для задачи с идентификатором {backgroundTask.Id} нет подходящих метаданных. Задача не будет запущена.");
                return Task.CompletedTask;
            }

            if (ActiveTasks.ContainsKey(metadata.Id))
            {
                bot.Events.OnCommonLogInvoke($"[{nameof(PRBackgroundTaskRunner)}] {metadata.Name} уже запущена.");
                return Task.CompletedTask;
            }

            registeredTaskInstances.Add(backgroundTask);
            var taskData = RunningBackgroundTask.Create(RunTaskAsync, metadata, new CancellationTokenSource());
            activeTasks.TryAdd(metadata.Id, taskData);
            taskData.StartTask();
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task StopAsync()
        {
            var metadata = activeTasks.Keys.ToArray();

            foreach (var mtd in metadata)
                await StopAsync(mtd);

            bot.Events.OnCommonLogInvoke( $"[{nameof(PRBackgroundTaskRunner)}] Все фоновые задачи остановлены.");
        }

        /// <inheritdoc />
        public async Task StopAsync(Guid taskId)
        {
            if (!activeTasks.TryRemove(taskId, out var runningTask))
            {
                bot.Events.OnCommonLogInvoke(
                    $"[{nameof(PRBackgroundTaskRunner)}] Задача c идентификатором '{taskId}' не найдена или уже остановлена.");
                return;
            }

            try
            {
                runningTask.CancellationTokenSource.Cancel();

                await runningTask.Task.ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            { }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, ex));
            }
            finally
            {
                runningTask.CancellationTokenSource.Dispose();

                bot.Events.OnCommonLogInvoke($"[{nameof(PRBackgroundTaskRunner)}] Задача '{runningTask.Metadata.Name}' остановлена.");
            }
        }

        /// <inheritdoc />
        public async Task StopAsync(IPRBackgroundTaskMetadata metadata)
        {
            if (metadata is null)
                throw new ArgumentNullException(nameof(metadata));

            await StopAsync(metadata.Id);
        }

        /// <inheritdoc />
        public void Initialize(IEnumerable<IPRBackgroundTaskMetadata> metadata, IEnumerable<IPRBackgroundTask> tasks)
        {
            foreach (var mtd in metadata)
                AddMetadata(mtd);

            foreach (var task in tasks)
            {
                var mtd = task.GetMetadata(this.registeredTaskMetadata, false);

                if (mtd != null && AddMetadata(mtd))
                {
                    registeredTaskInstances.Add(task);
                }
                else
                {
                    bot.Events.OnCommonLogInvoke($"[{nameof(PRBackgroundTaskRunner)}] Не найдены метаданные для задачи с id '{task.Id}'. Задача не будет запущена.");
                }
            }


            using (var serviceScope = bot.CreateServiceScope())
            {
                foreach (var item in ResolveBackgroundTasks(serviceScope))
                {
                    var mtd = item.GetMetadata(this.registeredTaskMetadata, false);
                    if (mtd == null)
                    {
                        bot.Events.OnCommonLogInvoke($"[{nameof(PRBackgroundTaskRunner)}][DI] Не найдены метаданные для задачи с типом {item.GetType()}. Фоновая задача не будет запущена.");
                        continue;
                    }

                    AddMetadata(mtd);
                }
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавить метаданные в общую коллекцию.
        /// </summary>
        /// <param name="metadata"></param>
        private bool AddMetadata(IPRBackgroundTaskMetadata metadata)
        {
            if(IsMetadataForAnotherBot(metadata))
                return false;

            this.registeredTaskMetadata.Add(metadata);
            return true;
        }

        /// <summary>
        /// Проверяем, что метаданные подходят только для другого бота.
        /// </summary>
        /// <param name="metadata">Метаданные фоновой задачи.</param>
        /// <returns>True - если метаданные для другого бота.</returns>
        private bool IsMetadataForAnotherBot(IPRBackgroundTaskMetadata metadata)
        {
            return metadata.BotIds.Any() 
                && !metadata.BotIds.Contains(PRConstants.ALL_BOTS_ID) 
                && !metadata.BotIds.Contains(bot.BotId);
        }

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
        {
            if (scope?.ServiceProvider == null)
                return Enumerable.Empty<IPRBackgroundTask>();

            return scope.ServiceProvider.GetServices<IPRBackgroundTask>()
                ?? Enumerable.Empty<IPRBackgroundTask>();
        }

        /// <summary>
        /// Запустить фоновую задачу.
        /// </summary>
        /// <param name="metadata">Метаданные.</param>
        /// <param name="data">Метаданные.</param>
        /// <param name="token">Токен отмены.</param>
        private async Task RunTaskAsync(IPRBackgroundTaskMetadata metadata, IRunningBackgroundTaskData data, CancellationToken token = default)
        {
            bool isDependencyInjection = true;
            data.SetStatus(PRTaskStatus.Pending);
            if (metadata.InitialDelaySeconds.HasValue && metadata.InitialDelaySeconds > 0)
                await Task.Delay(TimeSpan.FromSeconds(metadata.InitialDelaySeconds.Value), token);

            data.SetStatus(PRTaskStatus.Started);

            var task = registeredTaskInstances.FirstOrDefault(x => x.Id == metadata.Id);
            if (task != null)
            {
                isDependencyInjection = false;
                Debug.WriteLine($"try {metadata.Name} is initialize.");
                bot.Events.OnCommonLogInvoke($"[{nameof(PRBackgroundTaskRunner)}][Initialize] Фоновая задача  '{metadata.Name}' инициализирована.");
                data.SetStatus(PRTaskStatus.Initialize);
                await task.Initialize(bot);
            }

            do
            {
                try
                {
                    data.IncrementExecutionCount();
                    using (var serviceScope = bot.CreateServiceScope())
                    {
                        if (isDependencyInjection)
                        {
                            var diData = ResolveBackgroundTasks(serviceScope);
                            task = diData.SingleOrDefault(x => x.Id == metadata.Id);
                            data.SetStatus(PRTaskStatus.Initialize);
                            await (task?.Initialize(bot) ?? Task.CompletedTask);
                        }

                        if (task == null)
                        {
                            bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, $"[{nameof(PRBackgroundTaskRunner)}][RUN DI] Фоновая задача '{metadata.Name}' не смогла выполниться. Не найден экземпляр выполнения через DI. Выполнение задачи прекращено."));
                            activeTasks.Remove(metadata.Id, out _);
                            break;
                        }

                        var canExecute = await task.CanExecute();
                        if (canExecute)
                        {
                            data.SetStatus(PRTaskStatus.Executing);
                            await task.ExecuteAsync(token);
                        }
                        else
                        {
                            data.SetStatus(PRTaskStatus.Skipped);
                        }

                    }
                }
                catch (OperationCanceledException)
                {
                    activeTasks.Remove(metadata.Id, out _);
                    data.SetStatus(PRTaskStatus.Complete);
                    data.SetCompleteStatus(PRTaskCompletionResult.Canceled);
                    break;
                }
                catch (Exception ex)
                {
                    bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, ex));

                    data.AddError(ex);
                    data.SetStatus(PRTaskStatus.Error);
                    if (metadata.MaxErrorAttempts.HasValue && metadata.MaxErrorAttempts != -1 && data.ErrorCount >= metadata.MaxErrorAttempts)
                    {
                        bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, $"[{nameof(PRBackgroundTaskRunner)}] Выполнение фоновой задачи '{metadata.Name}' прекращено. Достигнут лимит ошибок при выполнение {data.ErrorCount} > {metadata.MaxErrorAttempts}"));
                        activeTasks.Remove(metadata.Id, out _);
                        data.SetStatus(PRTaskStatus.Complete);
                        data.SetCompleteStatus(PRTaskCompletionResult.Failed);
                        break;
                    }

                    if(metadata.MaxErrorAttempts.HasValue)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(metadata.GetRepeatSeconds()), token);
                        continue;
                    }

                }
                var isRepeatLimitReached = metadata.MaxRepeatCount.HasValue && metadata.MaxRepeatCount.Value > -1 && data.ExecutedCount >= metadata.MaxRepeatCount;
                if (isRepeatLimitReached)
                {
                    bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, $"[{nameof(PRBackgroundTaskRunner)}] Выполнение фоновой задачи '{metadata.Name}' прекращено. Достигнут лимит выполнения задачи."));
                    activeTasks.Remove(metadata.Id, out _);
                    data.SetStatus(PRTaskStatus.Complete);
                    data.SetCompleteStatus(PRTaskCompletionResult.Success);
                    break;
                }

                data.SetStatus(PRTaskStatus.WaitingNextRun);
                await Task.Delay(TimeSpan.FromSeconds(metadata.GetRepeatSeconds()), token);
            }
            while (!botToken.IsCancellationRequested && !token.IsCancellationRequested);

            activeTasks.TryRemove(metadata.Id, out var _);
            completedTasks.Add(data);
            data.EndTask();
        }

        #region IPRTaskRunnerSubscriber

        /// <inheritdoc />
        public void StopEvent(IEnumerable<long> botIds, Guid taskId)
        {
            var shouldStop = !botIds.Any() || botIds.Contains(PRConstants.ALL_BOTS_ID) || botIds.Contains(bot.BotId);
            if (!shouldStop)
                return;

            _ = StopAsync(taskId);
        }

        /// <inheritdoc />
        public void StopEvent(Guid taskId)
        {
            StopEvent(Enumerable.Empty<long>(), taskId);
        }

        /// <inheritdoc />
        public void Subscribe()
        {
            PREventBus.Subscribe(this);
        }

        /// <inheritdoc />
        public void Unsubscribe()
        {
            PREventBus.Unsubscribe(this);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Unsubscribe();
        }

        #endregion

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public PRBackgroundTaskRunner(PRBotBase bot)
        {
            this.bot = bot;

            this.Subscribe();
        }

        #endregion
    }
}
