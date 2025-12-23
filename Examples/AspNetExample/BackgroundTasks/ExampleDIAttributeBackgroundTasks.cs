using AspNetExample.Models;
using PRTelegramBot.BackgroundTasks;
using PRTelegramBot.BackgroundTasks.Interfaces;
using PRTelegramBot.Core;
using System.Diagnostics;

namespace AspNetExample.BackgroundTasks
{
    [PRBackgroundTask(Constants.EXAMPLE_TASK_DI_ATTRIBUTES_ID, "Test Data base", 1)]
    public class ExampleDIAttributeBackgroundTasks : IPRBackgroundTask
    {
        private readonly AppDbContext db;
        private PRBotBase bot;

        public Task<bool> CanExecute()
        {
            return Task.FromResult(true);
        }

        public Guid Id => Constants.EXAMPLE_DI_TASK_GUID;

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var users = db.Users.ToList();
            Debug.WriteLine($"Users is {users.Count}");
        }

        public Task Initialize(PRBotBase bot)
        {
            this.bot = bot;
            return Task.CompletedTask;
        }

        public ExampleDIAttributeBackgroundTasks(AppDbContext db)
        {
            this.db = db;   
        }
    }
}
