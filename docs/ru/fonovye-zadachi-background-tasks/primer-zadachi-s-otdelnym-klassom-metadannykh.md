# Пример задачи с отдельным классом метаданных

#### Класс реализующий [IPRBackgroundTask](../api/interfeisy/iprbackgroundtask.md) + класс [IPRBackgroundTaskMetadata](../api/interfeisy/iprbackgroundtaskmetadata.md)

В данном примере логика фоновой задачи и её метаданные вынесены в **разные классы**, что позволяет разделить ответственность и переиспользовать конфигурацию задачи независимо от её реализации.

Класс **`ExampleBackgroundTasksMetadata`** реализует интерфейс **`IPRBackgroundTaskMetadata`** и отвечает исключительно за описание параметров выполнения фоновой задачи:

* **`Id`** — уникальный идентификатор задачи, по которому происходит сопоставление с реализацией фоновой задачи;
* **`Name`** — имя задачи;
* **`InitialDelaySeconds`** — задержка перед первым запуском;
* **`RepeatSeconds`** — интервал повторного выполнения;
* **`BotIds`** — список идентификаторов ботов, для которых разрешено выполнение задачи (пустая коллекция означает выполнение для всех ботов);
* **`MaxErrorAttempts`** — максимально допустимое количество ошибок выполнения;
* **`MaxRepeatCount`** — максимальное количество повторений задачи;

Класс **`ExampleWithoutMetadataBackgroundTasks`** реализует интерфейс **`IPRBackgroundTask`** и содержит только бизнес-логику фоновой задачи:

* **`Initialize`** — инициализация задачи для конкретного бота;
* **`CanExecute`** — проверка возможности выполнения задачи;
* **`ExecuteAsync`** — основной код выполнения фоновой задачи.

Связь между логикой задачи и её метаданными осуществляется через совпадающее значение свойства **`Id`**.\
При инициализации **`PRBackgroundTaskRunner`** автоматически сопоставляет реализацию фоновой задачи с соответствующими метаданными.

```csharp
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



```

Такой подход удобен в случаях, когда:

* метаданные задачи формируются динамически (например, из конфигурации или базы данных);
* требуется переиспользовать одну и ту же реализацию задачи с разными параметрами;
* необходимо чётко разделить конфигурацию и бизнес-логику фоновых задач.
