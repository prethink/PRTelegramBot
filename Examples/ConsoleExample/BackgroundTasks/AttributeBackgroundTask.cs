using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;

namespace ConsoleExample.BackgroundTask
{
    [PRBackgroundTask("59d6b27d-b0ca-4a4a-9bbf-09ebac94f131", "AttributeBackgroundTask", 0, 0)]
    internal class AttributeBackgroundTask : IPRBackgroundTask
    {
        public Guid Id => Guid.Parse("59d6b27d-b0ca-4a4a-9bbf-09ebac94f131");

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("AttributeBackgroundTask work");
            await Task.Delay(5000);
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
