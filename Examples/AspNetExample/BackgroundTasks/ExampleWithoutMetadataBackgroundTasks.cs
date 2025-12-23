using AspNetExample.Models;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;
using System.Diagnostics;

namespace AspNetExample.BackgroundTasks
{
    public class ExampleWithoutMetadataBackgroundTasks : IPRBackgroundTask
    {
        public Guid Id => Constants.EXAMPLE_TASK_WITHOUT_METADATA;

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Execute {nameof(ExampleWithoutMetadataBackgroundTasks)}");
            return Task.CompletedTask;
        }

        public Task<bool> CanExecute()
        {
            return Task.FromResult(true);
        }


        public Task Initialize(PRBotBase bot)
        {
            return Task.CompletedTask;
        }
    }
}
