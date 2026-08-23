# Пример фоновой задачи с атрибутом метаданных

#### Класс реализующий [IPRBackgroundTask ](../api/interfeisy/iprbackgroundtask.md)+ [PRBackgroundTaskAttribute](../api/atributy/prbackgroundtaskattribute.md)

В данном примере фоновая задача использует атрибут **`PRBackgroundTaskAttribute`**, который реализует интерфейс **`IPRBackgroundTaskMetadata`**.\
Этот атрибут применяется непосредственно к классу фоновой задачи и служит для описания её метаданных.

С помощью **`PRBackgroundTaskAttribute`** задаются основные параметры выполнения задачи, такие как:

* идентификатор задачи;
* режим выполнения (однократный или периодический);
* интервал повторения;
* ограничения по количеству выполнений;
* лимиты ошибок и другие параметры, влияющие на жизненный цикл задачи.

Использование атрибута позволяет:

* декларативно описывать конфигурацию фоновой задачи;
* отделить логику выполнения задачи от её настроек;
* автоматически извлекать метаданные при инициализации задач без необходимости ручной регистрации.

Таким образом, фоновые задачи могут быть подключены и запущены на основе метаданных, заданных через атрибут, что упрощает расширение и сопровождение системы.

```csharp
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

```

