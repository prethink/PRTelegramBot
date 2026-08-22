
using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.BackgroundTasks.Models;
using PRTelegramBot.Core;
using PRTelegramBot.EventBus;
using PRTelegramBot.EventBus.Events;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace PRTelegramBot.BackgroundTasks
{
    /// <summary>
    /// Background task runner.
    /// </summary>
    public sealed class PRBackgroundTaskRunner : IPRBackgroundTaskRunner, IPRTaskRunnerSubscriber
    {
        #region Fields and properties

        /// <summary>
        /// Registry of the running tasks.
        /// </summary>
        private readonly ConcurrentDictionary<Guid, IRunningBackgroundTaskData> activeTasks = new();

        /// <summary>
        /// Finished tasks.
        /// </summary>
        private readonly HashSet<IRunningBackgroundTaskData> completedTasks = new();

        /// <summary>
        /// Background task instances.
        /// </summary>
        private HashSet<IPRBackgroundTask> registeredTaskInstances = new();

        /// <summary>
        /// Background task metadata.
        /// </summary>
        private HashSet<IPRBackgroundTaskMetadata> registeredTaskMetadata = new();

        /// <summary>
        /// Bot.
        /// </summary>
        private PRBotBase bot;

        /// <summary>
        /// The bot's global cancellation token.
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
                    bot.GetLogger<PRBackgroundTaskRunner>().LogInformationInternal($"[{nameof(PRBackgroundTaskRunner)}] {mtd.Name} is already running.");
                    continue;
                }

                var backgroundTask = RunningBackgroundTask.Create(RunTaskAsync, mtd, new CancellationTokenSource());
                backgroundTask.StartTask();
                activeTasks.TryAdd(mtd.Id, backgroundTask);
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
                bot.GetLogger<PRBackgroundTaskRunner>().LogInformationInternal($"[{nameof(PRBackgroundTaskRunner)}] No matching metadata for the task with identifier {backgroundTask.Id}. The task will not be started.");
                return Task.CompletedTask;
            }

            if (ActiveTasks.ContainsKey(metadata.Id))
            {
                bot.GetLogger<PRBackgroundTaskRunner>().LogInformationInternal($"[{nameof(PRBackgroundTaskRunner)}] {metadata.Name} is already running.");
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

            bot.GetLogger<PRBackgroundTaskRunner>().LogInformationInternal($"[{nameof(PRBackgroundTaskRunner)}] All background tasks have been stopped.");
        }

        /// <inheritdoc />
        public async Task StopAsync(Guid taskId)
        {
            if (!activeTasks.TryRemove(taskId, out var runningTask))
            {
                bot.GetLogger<PRBackgroundTaskRunner>().LogInformationInternal($"[{nameof(PRBackgroundTaskRunner)}] The task with identifier '{taskId}' was not found, or has already been stopped.");
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
                bot.GetLogger<PRBackgroundTaskRunner>().LogErrorInternal(ex);
            }
            finally
            {
                runningTask.CancellationTokenSource.Dispose();

                bot.GetLogger<PRBackgroundTaskRunner>().LogInformationInternal($"[{nameof(PRBackgroundTaskRunner)}] Task '{runningTask.Metadata.Name}' has been stopped.");
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
                    bot.GetLogger<PRBackgroundTaskRunner>().LogInformationInternal($"[{nameof(PRBackgroundTaskRunner)}] No metadata found for the task with id '{task.Id}'. The task will not be started.");
                }
            }


            using (var serviceScope = bot.CreateServiceScope())
            {
                foreach (var item in ResolveBackgroundTasks(serviceScope))
                {
                    var mtd = item.GetMetadata(this.registeredTaskMetadata, false);
                    if (mtd == null)
                    {
                        bot.GetLogger<PRBackgroundTaskRunner>().LogInformationInternal($"[{nameof(PRBackgroundTaskRunner)}][DI] No metadata found for the task of type {item.GetType()}. The background task will not be started.");
                        continue;
                    }

                    AddMetadata(mtd);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the metadata to the shared collection.
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
        /// Check whether the metadata is meant for a different bot only.
        /// </summary>
        /// <param name="metadata">Background task metadata.</param>
        /// <returns>True if the metadata belongs to a different bot.</returns>
        private bool IsMetadataForAnotherBot(IPRBackgroundTaskMetadata metadata)
        {
            return metadata.BotIds.Any() 
                && !metadata.BotIds.Contains(PRConstants.ALL_BOTS_ID) 
                && !metadata.BotIds.Contains(bot.BotId);
        }

        /// <summary>
        /// Gets every background task instance registered through DI in the specified scope.
        /// </summary>
        /// <param name="scope">
        /// The scope the services have to be resolved from. May be null.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="IPRBackgroundTask"/> instances. 
        /// If <paramref name="scope"/> is null, or the scope has no <see cref="IServiceProvider"/>, an empty collection is returned.
        /// </returns>
        private IEnumerable<IPRBackgroundTask> ResolveBackgroundTasks(DisposableScope scope)
        {
            if (scope?.ServiceProvider == null)
                return Enumerable.Empty<IPRBackgroundTask>();

            return scope.ServiceProvider.GetServices<IPRBackgroundTask>()
                ?? Enumerable.Empty<IPRBackgroundTask>();
        }

        /// <summary>
        /// Starts the background task.
        /// </summary>
        /// <param name="metadata">Metadata.</param>
        /// <param name="data">Metadata.</param>
        /// <param name="token">Cancellation token.</param>
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
                bot.GetLogger<PRBackgroundTaskRunner>().LogInformationInternal($"[{nameof(PRBackgroundTaskRunner)}][Initialize] Background task  '{metadata.Name}' has been initialized.");
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
                            bot.GetLogger<PRBackgroundTaskRunner>().LogErrorInternal($"[{nameof(PRBackgroundTaskRunner)}][RUN DI] Background task '{metadata.Name}' could not run. No instance was resolved through DI. The task has been stopped.");
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
                    bot.GetLogger<PRBackgroundTaskRunner>().LogErrorInternal(ex);

                    data.AddError(ex);
                    data.SetStatus(PRTaskStatus.Error);
                    if (metadata.MaxErrorAttempts.HasValue && metadata.MaxErrorAttempts != -1 && data.ErrorCount >= metadata.MaxErrorAttempts)
                    {
                        bot.GetLogger<PRBackgroundTaskRunner>().LogErrorInternal($"[{nameof(PRBackgroundTaskRunner)}] Background task '{metadata.Name}' stopped. The error limit has been reached: {data.ErrorCount} > {metadata.MaxErrorAttempts}");
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
                    bot.GetLogger<PRBackgroundTaskRunner>().LogErrorInternal($"[{nameof(PRBackgroundTaskRunner)}] Background task '{metadata.Name}' stopped. The task run limit has been reached.");
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

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public PRBackgroundTaskRunner(PRBotBase bot)
        {
            this.bot = bot;

            this.Subscribe();
        }

        #endregion
    }
}
