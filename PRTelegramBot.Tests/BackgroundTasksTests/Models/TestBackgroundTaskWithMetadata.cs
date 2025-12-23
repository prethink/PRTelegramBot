using PRTelegramBot.BackgroundTasks.Interfaces;

namespace PRTelegramBot.Tests.BackgroundTasksTests.Models
{
    internal class TestBackgroundTaskWithMetadata : TestBackgroundTask, IPRBackgroundTaskMetadata
    {
        public TestBackgroundTaskWithMetadata(string name, Guid? id = null) : base(id)
        {
            this.Name = name;
        }

        public TestBackgroundTaskWithMetadata( HashSet<long> botIds, string name, int? initialDelaySeconds, int? repeatSeconds, int? maxRepeatCount, int? maxErrorAttempts, Guid? id = null) 
            : base(id)
        {
            BotIds = botIds;
            Name = name;
            InitialDelaySeconds = initialDelaySeconds;
            RepeatSeconds = repeatSeconds;
            MaxRepeatCount = maxRepeatCount;
            MaxErrorAttempts = maxErrorAttempts;
        }

        public HashSet<long> BotIds { get; } = new();

        public string Name { get; protected set; }

        public int? InitialDelaySeconds { get; protected set; }

        public int? RepeatSeconds { get; protected set; }

        public int? MaxRepeatCount { get; protected set; }

        public int? MaxErrorAttempts { get; protected set; }


    }
}
