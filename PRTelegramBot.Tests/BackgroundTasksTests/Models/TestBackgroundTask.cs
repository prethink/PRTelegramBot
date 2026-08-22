using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;

namespace PRTelegramBot.Tests.BackgroundTasksTests.Models
{
    /// <summary>
    /// Test implementation of a background task.
    /// Counts the ExecuteAsync calls.
    /// </summary>
    public class TestBackgroundTask : IPRBackgroundTask
    {
        protected int executeCallCount;

        /// <summary>
        /// Task identifier.
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Task identifier.
        /// </summary>
        public PRBotBase Bot { get; protected set; }

        /// <summary>
        /// Number of ExecuteAsync calls.
        /// </summary>
        public int ExecuteCallCount => executeCallCount;

        /// <summary>
        /// Whether Initialize was called.
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
