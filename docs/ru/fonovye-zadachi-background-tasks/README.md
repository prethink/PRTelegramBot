# Фоновые задачи (Background tasks)

Начиная с версии 0.9.1 фреймворк стал поддерживать запуск фоновых задач для бота.

**Фоновая задача** — это независимый процесс, выполняющийся параллельно с основным циклом работы бота. Она не привязана к конкретному входящему событию (сообщению, callback, update) и предназначена для выполнения периодических или длительных операций

За запуск фоновых задач отвечает класс [PRBackgroundTaskRunner](../api/klassy/prbackgroundtaskrunner.md). Внутри каждого бота стоит свой раннер для фоновых задач.

Доступ к ранеру через сам бот

```csharp
bot.BackgroundTaskRunner
```

Фоновая задача состоит из двух компонентов.

* Интерфейс [IPRBackgroundTask](../api/interfeisy/iprbackgroundtask.md) - отвечает за выполнения задачи.
* Интерфейс [IPRBackgroundTaskMetadata](../api/interfeisy/iprbackgroundtaskmetadata.md) - отвечает за метаданные задачи.

> **Важно ID задачи и метаданных должны быть одинаковы**

## Фоновая задача

Фоновая задача описывается через интерфейс `IPRBackgroundTask`.\
Она содержит **бизнес-логику**, которая выполняется по расписанию и работает совместно с метаданными (`IPRBackgroundTaskMetadata`).

#### Доступные методы

* **Id** (`Guid`) — **обязательный**
  * Уникальный идентификатор задачи.
  * Используется для сопоставления задачи и её метаданных, управления выполнением и логирования.
* **CanExecute()** (`Task<bool>`) — **обязательный**
  * Метод проверяет, может ли задача быть выполнена в текущий момент.
  * Вызывается перед каждой попыткой запуска задачи.
  * Возврат `false` означает, что задача будет пропущена до следующего планового запуска.
* **ExecuteAsync(CancellationToken cancellationToken)** — **обязательный**
  * Основной метод, выполняющий бизнес-логику задачи.
  * `cancellationToken` используется для безопасной отмены выполнения задачи.
* **Initialize(PRBotBase bot)** — _необязательный_
  * Метод инициализации задачи, вызывается фреймворком перед первым запуском.
  * Позволяет сохранить экземпляр бота, чтобы получать доступ к его сервисам и контексту.
  * Если реализация не требует дополнительных действий при инициализации, можно возвращать `Task.CompletedTask`.

***

#### Примечания

* Каждая задача должна иметь уникальный `Id`, чтобы правильно сопоставляться с метаданными.
* Метод `CanExecute` позволяет динамически контролировать, запускать задачу или пропускать выполнение.
* Метод `Initialize` обычно используется для внедрения зависимостей и получения контекста бота.
* `ExecuteAsync` выполняется фреймворком в соответствии с настройками из метаданных (`IPRBackgroundTaskMetadata`).

## Метаданные фоновой задачи

Метаданные фоновой задачи описываются через интерфейс `IPRBackgroundTaskMetadata`.\
Они используются для планирования, запуска и контроля выполнения фоновых задач и **не содержат бизнес-логику**.

#### Доступные параметры метаданных

* **Id** (`Guid`) — **обязательный**
  * Уникальный идентификатор фоновой задачи.
  * Используется для сопоставления задачи и её метаданных, а также для управления выполнением (остановка, перезапуск и т.д.).
* **Name** (`string`) — **обязательный**
  * Уникальное имя фоновой задачи.
  * Применяется для логирования, диагностики и отладки.
* **BotIds** (`HashSet<long>`) — _необязательный_
  * Идентификаторы ботов, для которых предназначена задача.
  * Используется для разграничения задач при работе через DI.
  * Если коллекция пустая или содержит значение `PRConstants.ALL_BOTS_ID`, задача применяется ко всем ботам.
* **InitialDelaySeconds** (`int?`) — _необязательный_
  * Задержка перед первым запуском задачи (в секундах).
  * `null`, `0` или отрицательное значение — задача запускается немедленно после старта бота.
* **RepeatSeconds** (`int?`) — _необязательный_
  * Интервал повторного выполнения задачи (в секундах).
  * Минимально допустимый интервал повторения — **1 секунда**.
* **MaxRepeatCount** (`int?`) — _необязательный_
  * Максимальное количество запусков задачи (включая успешные и неуспешные).
  * `null` или `-1` — неограниченное количество запусков.
* **MaxErrorAttempts** (`int?`) — _необязательный_
  * Максимальное количество попыток выполнения задачи при ошибках.
  * Учитывается первый запуск.
  * `null` или `-1` — без ограничения.
  * Значение `1` означает однократное выполнение без повторных попыток при ошибке.

***

#### Примечания

* Метаданные могут быть заданы:
  * через атрибут `PRBackgroundTaskAttribute`,
  * через отдельную реализацию `IPRBackgroundTaskMetadata`,
  * либо непосредственно в коде при инициализации задач.
* При отсутствии метаданных задача не будет запущена, если они не определены иным способом.

## Добавление фоновых задач в бота

Фоновые задачи в `PRBot` можно добавлять двумя способами: через **DI (Dependency Injection)** или через **билдер**.

### Через DI

Если вы используете контейнер зависимостей (например, `Microsoft.Extensions.DependencyInjection`), можно зарегистрировать задачи как сервисы:

```csharp
// Регистрация фоновых задач через DI
// Регистрация фоновых задач через DI
builder.Services.AddTransient<IPRBackgroundTask, ExampleDIAttributeBackgroundTasks>();
builder.Services.AddTransient<IPRBackgroundTask, ExampleWithMetadataBackgroundTasks>();
builder.Services.AddTransient<IPRBackgroundTask, ExampleWithoutMetadataBackgroundTasks>();

var serviceProvider = app.Services.GetService<IServiceProvider>();

var prBotInstance = new PRBotBuilder("token")
    .SetClearUpdatesOnStart(true)
    .SetServiceProvider(serviceProvider) // подключаем DI
    .AddInlineClassHandler(ClassTHeader.DefaultTestClass, typeof(BotInlineHandlerWithDependency))
    .AddBackgroundTaskMetadata(new ExampleBackgroundTasksMetadata()) // добавляем метаданные задачи
    .Build();

```

**Пояснения:**

* Все зарегистрированные сервисы, реализующие `IPRBackgroundTask`, будут автоматически подхвачены ботом.
* `AddBackgroundTaskMetadata` добавляет метаданные, если задача не содержит встроенные атрибуты.
* Задачи через DI позволяют использовать все преимущества контейнера зависимостей (например, логирование, сервисы, базы данных).

### Через билдер

Если вы хотите добавить конкретный экземпляр задачи вручную, можно использовать методы `AddBackgroundTask`:

```csharp
// 1. Добавление задачи без отдельного объекта метаданных.
// ВАЖНО: backgroundTask должен реализовывать IPRBackgroundTaskMetadata
// или использовать атрибут PRBackgroundTaskAttribute.
var bot = new PRBotBuilder("token")
    .AddBackgroundTask(new AttributeBackgroundTask())
    .Build();

// 2. Добавление задачи с явными метаданными
var botWithMetadata = new PRBotBuilder("token")
    .AddBackgroundTask(new ExampleBackgroundTask(), new ExampleBackgroundTasksMetadata())
    .Build();

// 3. Добавление только метаданных (например, для DI-зависимых задач)
var botWithDI = new PRBotBuilder("token")
    .AddBackgroundTaskMetadata(new ExampleBackgroundTasksMetadata())
    .Build();
```

**Пояснения:**

* Метод `AddBackgroundTask(IPRBackgroundTask)` требует, чтобы задача либо имела встроенные метаданные (`IPRBackgroundTaskMetadata`), либо была помечена атрибутом `PRBackgroundTaskAttribute`.
* Метод `AddBackgroundTask(IPRBackgroundTask, IPRBackgroundTaskMetadata)` позволяет отдельно указать метаданные для задачи.
* Метод `AddBackgroundTaskMetadata(IPRBackgroundTaskMetadata)` полезен для DI-зависимых задач: сами задачи регистрируются через DI, а метаданные добавляются через билдер.
* Все задачи, зарегистрированные через билдер, будут подхвачены `PRBackgroundTaskRunner` и выполнены, указанному в метаданных.

## Управление текущими и завершенными задачами

Класс `PRBackgroundTaskRunner` хранит состояние всех фоновых задач бота и предоставляет доступ к информации о **текущих** (активных) и **завершенных** задачах.

**1. Текущие задачи**

* Все запущенные задачи хранятся в словаре `ActiveTasks`.
* Ключ — `Guid` задачи, значение — объект `IRunningBackgroundTaskData`, содержащий:
  * Метаданные задачи (`IPRBackgroundTaskMetadata`)
  * Статус выполнения (`PRTaskStatus`)
  * Количество выполнений (`ExecutedCount`)
  * Количество ошибок (`ErrorCount`)
  * Токен отмены (`CancellationTokenSource`)
* **Пример получения всех активных задач:**

```csharp
var activeTasks = bot.BackgroundTaskRunner.ActiveTasks;

foreach (var kvp in activeTasks)
{
    var taskData = kvp.Value;
    Console.WriteLine($"Задача: {taskData.Metadata.Name}, Статус: {taskData.Status}, Ошибки: {taskData.ErrorCount}");
}
```

**Завершенные задачи**

* Все задачи, которые завершились (успешно, с ошибкой или отменой), добавляются в коллекцию `EndTasks`.
* `EndTasks` содержит объекты `IRunningBackgroundTaskData`, где можно узнать:
  * Статус завершения (`CompleteStatus`) — `Success`, `Failed` или `Canceled`
  * Дата и время начала и окончания (`StartDate`, `EndDate`)
  * Общее количество выполнений и ошибок

```csharp
var completedTasks = bot.BackgroundTaskRunner.EndTasks;

foreach (var taskData in completedTasks)
{
    Console.WriteLine($"Задача: {taskData.Metadata.Name}, Результат: {taskData.CompleteStatus}, Выполнено: {taskData.ExecutedCount}, Ошибки: {taskData.ErrorCount}");
}

```

3. Примеры использования

Остановка конкретной задачи::

```csharp
await bot.BackgroundTaskRunner.StopAsync(taskId);
```

Остановка всех задач:

```csharp
await bot.BackgroundTaskRunner.StopAsync();
```
