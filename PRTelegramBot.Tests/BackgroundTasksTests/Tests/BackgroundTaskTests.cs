using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.BackgroundTasks.Models;
using PRTelegramBot.Builders;
using PRTelegramBot.Core;
using PRTelegramBot.Tests.BackgroundTasksTests.Factories;
using PRTelegramBot.Tests.BackgroundTasksTests.Models;

namespace PRTelegramBot.Tests.BackgroundTasksTests.Tests
{
    internal class BackgroundTaskTests
    {
        private PRBackgroundTaskRunner taskRunner;
        private PRBotBase bot;

        public const string TaskIdOne = "11111111-1111-1111-1111-111111111111";
        public const string TaskIdTwo = "22222222-2222-2222-2222-222222222222";

        [SetUp]
        public void SetUP()
        {
            BotCollection.Instance.ClearBots();
            bot = new PRBotBuilder("5555:Token").Build();
            taskRunner = new PRBackgroundTaskRunner(bot);
        }

        [TearDown]
        public void Teardown()
        {
            taskRunner.Dispose();
            BotCollection.Instance.ClearBots();
        }

        [Test]
        public async Task StopAsync_ShouldStopAllTasks()
        {
            var taskOneGuid = Guid.Parse(TaskIdOne);
            var metadataTaskOne = TaskMetadataFactory.CreateOneTime(taskOneGuid);
            var testTaskOne = new TestBackgroundTask(taskOneGuid);

            var twoOneGuid = Guid.Parse(TaskIdTwo);
            var metadataTaskTwo = TaskMetadataFactory.CreateOneTime(twoOneGuid);
            var testTaskTwo = new TestBackgroundTask(twoOneGuid);

            taskRunner.Initialize([metadataTaskOne, metadataTaskTwo], [testTaskOne, testTaskTwo]);
            await taskRunner.StartAsync();

            taskRunner.ActiveTasks.Count.Should().Be(2);
            taskRunner.Metadata.Count.Should().Be(2);
            taskRunner.TaskInstance.Count.Should().Be(2);

            await taskRunner.StopAsync();

            taskRunner.ActiveTasks.Count.Should().Be(0);
        }

        [Test]
        public async Task StopAsync_ShouldStopSpecifiedTask()
        {
            var taskOneGuid = Guid.Parse(TaskIdOne);
            var metadataTaskOne = TaskMetadataFactory.CreateOneTime(taskOneGuid);
            var testTaskOne = new TestBackgroundTask(taskOneGuid);

            var twoOneGuid = Guid.Parse(TaskIdTwo);
            var metadataTaskTwo = TaskMetadataFactory.CreateOneTime(twoOneGuid);
            var testTaskTwo = new TestBackgroundTask(twoOneGuid);

            taskRunner.Initialize([metadataTaskOne, metadataTaskTwo], [testTaskOne, testTaskTwo]);
            await taskRunner.StartAsync();

            taskRunner.ActiveTasks.Count.Should().Be(2);
            taskRunner.Metadata.Count.Should().Be(2);
            taskRunner.TaskInstance.Count.Should().Be(2);

            await taskRunner.StopAsync(metadataTaskTwo);

            taskRunner.ActiveTasks.Count.Should().Be(1);
        }

        [Test]
        public void Initialize_TaskWithoutMetadata_ShouldNotBeAdded()
        {
            var task = new TestBackgroundTask(Guid.Parse(TaskIdOne));

            taskRunner.Initialize([], [task]);

            taskRunner.ActiveTasks.Count.Should().Be(0);
            taskRunner.Metadata.Count.Should().Be(0);
            taskRunner.TaskInstance.Count.Should().Be(0);
        }

        [Test]
        public void Initialize_TaskWithDifferentMetadata_ShouldNotBeAdded()
        {
            var task = new TestBackgroundTask(Guid.Parse(TaskIdOne));
            var metadata = TaskMetadataFactory.CreateOneTime(Guid.Parse(TaskIdTwo));

            taskRunner.Initialize([metadata], [task]);

            taskRunner.ActiveTasks.Count.Should().Be(0);
            taskRunner.Metadata.Count.Should().Be(1);
            taskRunner.TaskInstance.Count.Should().Be(0);
        }

        [Test]
        public void Initialize_TaskWithMatchingMetadata_ShouldBeAdded()
        {
            var task = new TestBackgroundTask(Guid.Parse(TaskIdOne));
            var metadata = TaskMetadataFactory.CreateOneTime(Guid.Parse(TaskIdOne));

            taskRunner.Initialize([metadata], [task]);

            taskRunner.ActiveTasks.Count.Should().Be(0);
            taskRunner.Metadata.Count.Should().Be(1);
            taskRunner.TaskInstance.Count.Should().Be(1);
        }

        [Test]
        public async Task StartAsync_TaskWithoutMetadata_ShouldThrowArgumentNullException()
        {
            var task = new TestBackgroundTask(Guid.Parse(TaskIdOne));
            Func<Task> act = async () => await taskRunner.StartAsync(task, null);

            await act.Should().ThrowAsync<ArgumentNullException>()
                .WithParameterName("metadata");

            taskRunner.ActiveTasks.Count.Should().Be(0);
            taskRunner.Metadata.Count.Should().Be(0);
            taskRunner.TaskInstance.Count.Should().Be(0);
        }

        [Test]
        public async Task StartAsync_MetadataWithoutTask_ShouldThrowArgumentNullException()
        {
            var metadata = TaskMetadataFactory.CreateOneTime(Guid.Parse(TaskIdOne));
            Func<Task> act = async () => await taskRunner.StartAsync(null, metadata);

            await act.Should().ThrowAsync<ArgumentNullException>()
                .WithParameterName("backgroundTask");

            taskRunner.ActiveTasks.Count.Should().Be(0);
            taskRunner.Metadata.Count.Should().Be(0);
            taskRunner.TaskInstance.Count.Should().Be(0);
        }

        [Test]
        public async Task StartAsync_TaskWithDifferentMetadata_ShouldNotBeAdded()
        {
            var task = new TestBackgroundTask(Guid.Parse(TaskIdOne));
            var metadata = TaskMetadataFactory.CreateOneTime(Guid.Parse(TaskIdTwo));

            await taskRunner.StartAsync(task, metadata);

            taskRunner.ActiveTasks.Count.Should().Be(0);
            taskRunner.Metadata.Count.Should().Be(1);
            taskRunner.TaskInstance.Count.Should().Be(0);
        }

        [Test]
        public async Task StartAsync_TaskWithMatchingMetadata_ShouldBeAddedAndRunning()
        {
            var task = new TestBackgroundTask(Guid.Parse(TaskIdOne));
            var metadata = TaskMetadataFactory.CreateDefault(Guid.Parse(TaskIdOne));

            await taskRunner.StartAsync(task, metadata);

            taskRunner.ActiveTasks.Count.Should().Be(1);
            taskRunner.Metadata.Count.Should().Be(1);
            taskRunner.TaskInstance.Count.Should().Be(1);
        }

        [Test]
        public async Task StartAsync_SameTaskStartedTwice_ShouldRunOnlyOnce()
        {
            var task = new TestBackgroundTask(Guid.Parse(TaskIdOne));
            var metadata = TaskMetadataFactory.CreateDefault(Guid.Parse(TaskIdOne));

            await taskRunner.StartAsync(task, metadata);
            await taskRunner.StartAsync(task, metadata);

            taskRunner.ActiveTasks.Count.Should().Be(1);
            taskRunner.Metadata.Count.Should().Be(1);
            taskRunner.TaskInstance.Count.Should().Be(1);
        }

        [Test]
        public async Task StartAsync_TaskWithoutMetadata_ShouldThrowException()
        {
            var task = new TestBackgroundTask(Guid.Parse(TaskIdOne));

            Func<Task> act = async () => await taskRunner.StartAsync(task);

            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Test]
        public async Task StartAsync_TaskWithAttribute_ShouldStart()
        {
            var task = new TestBackgroundTaskWithAttribute(Guid.Parse(TaskIdOne));

            await taskRunner.StartAsync(task);

            taskRunner.ActiveTasks.Count.Should().Be(1);
            taskRunner.Metadata.Count.Should().Be(1);
            taskRunner.TaskInstance.Count.Should().Be(1);
        }

        [Test]
        public async Task StartAsync_TaskWithMetadata_ShouldStart()
        {
            var task = new TestBackgroundTaskWithMetadata(nameof(TestBackgroundTaskWithMetadata), Guid.Parse(TaskIdOne));

            await taskRunner.StartAsync(task);

            taskRunner.ActiveTasks.Count.Should().Be(1);
            taskRunner.Metadata.Count.Should().Be(1);
            taskRunner.TaskInstance.Count.Should().Be(1);
        }

        [Test]
        public async Task StartTask_OneTime_ShouldOneTimeExecuted()
        {
            var task = new TestBackgroundTask(Guid.Parse(TaskIdOne));
            var metadata = TaskMetadataFactory.CreateOneTime(Guid.Parse(TaskIdOne));

            await taskRunner.StartAsync(task, metadata);

            await Task.Delay(1000);

            taskRunner.ActiveTasks.Count.Should().Be(0);
            taskRunner.EndTasks.Count.Should().Be(1);
            taskRunner.Metadata.Count.Should().Be(1);
            taskRunner.TaskInstance.Count.Should().Be(1);
            var data = taskRunner.EndTasks.Single();
            data.Status.Should().Be(PRTaskStatus.Complete);
            data.CompleteStatus.Should().Be(PRTaskCompletionResult.Success);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(null, 2)]
        [TestCase(2, 3)]
        public async Task StartTask_WithLimitExecute_ShouldLimitedExecuted(int? repeatSeconds, int limit)
        {
            var task = new TestBackgroundTask(Guid.Parse(TaskIdOne));
            var metadata = TaskMetadataFactory.CreateWithLimitedRepeats(repeatSeconds, limit, Guid.Parse(TaskIdOne));

            await taskRunner.StartAsync(task, metadata);
            await Task.Delay(1200 * (limit > 0 ? limit : 1) * (repeatSeconds.GetValueOrDefault() > 0 ? repeatSeconds.GetValueOrDefault() : 1));

            taskRunner.ActiveTasks.Count.Should().Be(0);
            taskRunner.EndTasks.Count.Should().Be(1);
            taskRunner.Metadata.Count.Should().Be(1);
            taskRunner.TaskInstance.Count.Should().Be(1);
            var data = taskRunner.EndTasks.Single();
            data.ExecutedCount.Should().Be(limit);
            data.Status.Should().Be(PRTaskStatus.Complete);
            data.CompleteStatus.Should().Be(PRTaskCompletionResult.Success);
        }

        [Test]
        public async Task StartTask_WithLimitZero_ShouldOneTimeExecuted()
        {
            var task = new TestBackgroundTask(Guid.Parse(TaskIdOne));
            var metadata = TaskMetadataFactory.CreateWithLimitedRepeats(0, 0, Guid.Parse(TaskIdOne));

            await taskRunner.StartAsync(task, metadata);
            await Task.Delay(1200);

            taskRunner.ActiveTasks.Count.Should().Be(0);
            taskRunner.EndTasks.Count.Should().Be(1);
            taskRunner.Metadata.Count.Should().Be(1);
            taskRunner.TaskInstance.Count.Should().Be(1);
            var data = taskRunner.EndTasks.Single();
            data.ExecutedCount.Should().Be(1);
            data.Status.Should().Be(PRTaskStatus.Complete);
            data.CompleteStatus.Should().Be(PRTaskCompletionResult.Success);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task StartTask_WithErrorsExecute_ShouldLimitedExecuted(int errorLimit)
        {
            var task = new TestBackgroundTaskWithException(Guid.Parse(TaskIdOne));
            var metadata = TaskMetadataFactory.CreateWithErrorLimit(null, errorLimit, Guid.Parse(TaskIdOne));

            await taskRunner.StartAsync(task, metadata);
            await Task.Delay(1200 * (errorLimit > 0 ? errorLimit : 1));

            taskRunner.ActiveTasks.Count.Should().Be(0);
            taskRunner.EndTasks.Count.Should().Be(1);
            taskRunner.Metadata.Count.Should().Be(1);
            taskRunner.TaskInstance.Count.Should().Be(1);
            var data = taskRunner.EndTasks.Single();
            data.ErrorCount.Should().Be(errorLimit);
            data.ExecutedCount.Should().Be(errorLimit);
            data.Status.Should().Be(PRTaskStatus.Complete);
            data.CompleteStatus.Should().Be(PRTaskCompletionResult.Failed);
        }

        [Test]
        public async Task TaskFromDI_ShouldBeResolvedAndStarted_WhenMetadataExists()
        {
            ServiceCollection services = new();
            services.AddTransient<IPRBackgroundTask, TestBackgroundTask>();
            using ServiceProvider provider = services.BuildServiceProvider();

            var metadata = TaskMetadataFactory.CreateDefault(Guid.Parse(TaskIdOne), "Test");

            taskRunner.Initialize([metadata], []);
            await taskRunner.StartAsync();

            taskRunner.ActiveTasks.Count.Should().Be(1);
            taskRunner.Metadata.Count.Should().Be(1);
            taskRunner.TaskInstance.Count.Should().Be(0);
        }
    }
}
