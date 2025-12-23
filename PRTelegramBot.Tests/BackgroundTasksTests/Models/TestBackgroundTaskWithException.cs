
namespace PRTelegramBot.Tests.BackgroundTasksTests.Models
{
    internal class TestBackgroundTaskWithException : TestBackgroundTask
    {
        public override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Interlocked.Increment(ref executeCallCount);
            throw new Exception("Error Boom");
        }

        public TestBackgroundTaskWithException(Guid? id = null) : base(id)
        {

        }
    }
}
