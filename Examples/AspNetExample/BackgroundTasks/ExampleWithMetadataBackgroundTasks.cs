using AspNetExample.Models;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;
using System.Diagnostics;

namespace AspNetExample.BackgroundTasks
{
    public class ExampleWithMetadataBackgroundTasks : IPRBackgroundTask, IPRBackgroundTaskMetadata
    {
        public Guid Id => Constants.EXAMPLE_TASK_WITH_METADATA;

        public string Name => nameof(ExampleWithMetadataBackgroundTasks);

        public int? InitialDelaySeconds => 1;

        public int? RepeatSeconds => 1;

        public HashSet<long> BotIds => new HashSet<long>();

        public int? MaxErrorAttempts => PRConstants.INFINITY;

        public int? MaxRepeatCount => -1;

        public Task<bool> CanExecute()
        {
            return Task.FromResult(true);
        }

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Execute {nameof(ExampleWithMetadataBackgroundTasks)}");
            return Task.CompletedTask;
        }

        public Task Initialize(PRBotBase bot)
        {
            return Task.CompletedTask;
        }
    }
}
