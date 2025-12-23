using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.Tests.BackgroundTasksTests.Tests;

namespace PRTelegramBot.Tests.BackgroundTasksTests.Models
{
    [PRBackgroundTask(BackgroundTaskTests.TaskIdOne, nameof(TestBackgroundTaskWithAttribute))]
    internal class TestBackgroundTaskWithAttribute : TestBackgroundTask
    {
        public TestBackgroundTaskWithAttribute(Guid? id = null) : base(id)
        {
           
        }
    }
}
