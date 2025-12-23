using PRTelegramBot.Tests.BackgroundTasksTests.Tests;

namespace PRTelegramBot.Tests.BackgroundTasksTests.Models
{
    internal class TestDIBackgroundTask : TestBackgroundTask
    {
        public TestDIBackgroundTask()
        {
            Id = Guid.Parse(BackgroundTaskTests.TaskIdOne);
        }
    }
}
