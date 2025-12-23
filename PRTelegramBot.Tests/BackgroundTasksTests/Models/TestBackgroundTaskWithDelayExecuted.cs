namespace PRTelegramBot.Tests.BackgroundTasksTests.Models
{
    internal class TestBackgroundTaskWithDelayExecuted : TestBackgroundTask
    {
        private int executedSeconds;
        private int canExecuteSeconds;

        public override async Task<bool> CanExecute()
        {
            await Task.Delay(canExecuteSeconds * 1000);
            return true;
        }

        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Interlocked.Increment(ref executeCallCount);
            await Task.Delay(executedSeconds * 1000);
        }

        public TestBackgroundTaskWithDelayExecuted(int canExecuteSeconds, int executedSecond, Guid? id = null) : base(id)
        {
            this.executedSeconds = executedSecond;
            this.canExecuteSeconds = canExecuteSeconds;
        }
    }
}
