using PRTelegramBot.BackgroundTasks.Interfaces;

namespace PRTelegramBot.Tests.BackgroundTasksTests.Models
{
    internal sealed class TestBackgroundTaskMetadata : IPRBackgroundTaskMetadata
    {
        public HashSet<long> BotIds { get; init; } = new();

        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public int? InitialDelaySeconds { get; init; }

        public int? RepeatSeconds { get; init; }

        public int? MaxRepeatCount { get; init; }

        public int? MaxErrorAttempts { get; init; }
    }
}
