using AspNetExample.Models;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;

namespace AspNetExample.BackgroundTasks
{
    public class ExampleBackgroundTasksMetadata : IPRBackgroundTaskMetadata
    {
        public HashSet<long> BotIds { get; } = new HashSet<long>();
        public Guid Id => Constants.EXAMPLE_TASK_WITHOUT_METADATA;

        public string Name => nameof(ExampleBackgroundTasksMetadata);

        public int? InitialDelaySeconds => 1;

        public int? RepeatSeconds => 1;

        public int? MaxErrorAttempts => PRConstants.INFINITY;

        public int? MaxRepeatCount => -1;
    }
}
