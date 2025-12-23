using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;

namespace PRTelegramBot.Tests.BackgroundTasksTests.Models
{
    /// <summary>
    /// Тестовая реализация фоновой задачи.
    /// Считает количество вызовов ExecuteAsync.
    /// </summary>
    public class TestBackgroundTask : IPRBackgroundTask
    {
        protected int executeCallCount;

        /// <summary>
        /// Идентификатор задачи.
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Идентификатор задачи.
        /// </summary>
        public PRBotBase Bot { get; protected set; }

        /// <summary>
        /// Количество вызовов ExecuteAsync.
        /// </summary>
        public int ExecuteCallCount => executeCallCount;

        /// <summary>
        /// Был ли вызван Initialize.
        /// </summary>
        public bool IsInitialized { get; private set; }

        public TestBackgroundTask(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }

        public virtual Task<bool> CanExecute()
        {
            return Task.FromResult(true);
        }

        public Task Initialize(PRBotBase bot)
        {
            IsInitialized = true;
            this.Bot = bot;
            return Task.CompletedTask;
        }

        public virtual Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Interlocked.Increment(ref executeCallCount);
            return Task.CompletedTask;
        }
    }
}
