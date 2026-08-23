# Пример фоновой задачи с реализацией интерфейса метаданных

#### Класс реализующий [IPRBackgroundTask](../api/interfeisy/iprbackgroundtask.md) и [IPRBackgroundTaskMetadata](../api/interfeisy/iprbackgroundtaskmetadata.md)

В данном примере фоновая задача реализует сразу два интерфейса — **`IPRBackgroundTask`** и **`IPRBackgroundTaskMetadata`**, объединяя в одном классе как логику выполнения задачи, так и её метаданные.

Интерфейс **`IPRBackgroundTask`** отвечает за поведение задачи и определяет:

* метод **`Initialize`** — инициализацию задачи с привязкой к экземпляру бота;
* метод **`CanExecute`** — проверку возможности выполнения задачи;
* метод **`ExecuteAsync`** — основную логику выполнения фоновой задачи.

Интерфейс **`IPRBackgroundTaskMetadata`** используется для описания параметров выполнения задачи и содержит:

* **`Id`** — уникальный идентификатор фоновой задачи;
* **`Name`** — имя задачи;
* **`InitialDelaySeconds`** — задержку перед первым запуском;
* **`RepeatSeconds`** — интервал повторного выполнения;
* **`BotIds`** — список идентификаторов ботов, для которых задача разрешена (пустой список означает выполнение для всех ботов);
* **`MaxErrorAttempts`** — максимальное количество допустимых ошибок выполнения;
* **`MaxRepeatCount`** — максимальное количество повторений задачи.

В данном примере задача:

* запускается с задержкой в 1 секунду;
* выполняется каждую секунду без ограничения по количеству повторов;
* не имеет ограничения по количеству ошибок выполнения;
* доступна для всех ботов.

```csharp
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

```

Такой подход позволяет отказаться от использования атрибутов и задавать метаданные напрямую в классе фоновой задачи, что может быть удобно для динамических или конфигурируемых сценариев выполнения.
