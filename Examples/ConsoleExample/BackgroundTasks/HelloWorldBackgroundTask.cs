using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.Core;

namespace ConsoleExample.BackgroundTask
{
    internal class HelloWorldBackgroundTask : PRBackgroundTaskBase
    {
        public override Guid Id => new Guid("d4b5f8e2-3c6a-4f1e-9f7e-2a5b6c8d9e0f");

        public override string Name => "HelloWorldBackgroundTask";

        public override int? InitialDelaySeconds => 5;

        public override int? RepeatSeconds => 3;

        public override int? MaxErrorAttempts => PRConstants.INFINITY;

        public override int? MaxRepeatCount => -1;

        public override Task<bool> CanExecute()
        {
            return Task.FromResult(true);
        }

        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("HelloWorldBackgroundTask work");
            await Task.Delay(2000);
        }
    }
}
