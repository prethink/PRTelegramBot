---
description: Work that runs on a schedule, independently of any incoming update.
---

# Background tasks

Available since version 0.9.1.

A **background task** runs alongside the bot's main loop. It is not tied to an incoming message, callback or update — it exists for periodic or long-running work: a nightly digest, a queue drained every ten seconds, a cache refreshed each hour.

Each bot has its own runner, [`PRBackgroundTaskRunner`](../api/classes/prbackgroundtaskrunner.md), reachable from the bot:

```csharp
bot.BackgroundTaskRunner
```

A task is two pieces:

* [`IPRBackgroundTask`](../api/interfaces/iprbackgroundtask.md) — what the task does.
* [`IPRBackgroundTaskMetadata`](../api/interfaces/iprbackgroundtaskmetadata.md) — when and how often it does it.

{% hint style="warning" %}
The `Id` on the task and the `Id` on its metadata **must be the same value**. That is what pairs them up. A mismatch leaves the task with no schedule, and it never runs.
{% endhint %}

## The task

`IPRBackgroundTask` holds the business logic. The framework runs it according to the metadata.

| Member | Required | What it is for |
| --- | --- | --- |
| `Id` (`Guid`) | **yes** | Unique identifier. Pairs the task with its metadata, and identifies it for control and logging. |
| `CanExecute()` (`Task<bool>`) | **yes** | Asked before every attempt. Returning `false` skips this run and waits for the next scheduled one. |
| `ExecuteAsync(CancellationToken)` | **yes** | The work itself. Honour the token so the task can be stopped cleanly. |
| `Initialize(PRBotBase bot)` | no | Called once before the first run. The usual place to keep the bot instance so the task can reach its services and context. Return `Task.CompletedTask` if there is nothing to do. |

`CanExecute` is what makes a schedule conditional: a task can run every minute but do nothing outside working hours, without the schedule knowing anything about working hours.

## The metadata

`IPRBackgroundTaskMetadata` carries scheduling only — no business logic.

| Parameter | Required | Meaning |
| --- | --- | --- |
| `Id` (`Guid`) | **yes** | Must match the task's `Id`. |
| `Name` (`string`) | **yes** | A unique name, used in logs and diagnostics. |
| `BotIds` (`HashSet<long>`) | no | Which bots the task belongs to, when tasks are registered through DI. Empty, or containing `PRConstants.ALL_BOTS_ID`, means every bot. |
| `InitialDelaySeconds` (`int?`) | no | Delay before the first run. `null`, `0` or negative means start immediately. |
| `RepeatSeconds` (`int?`) | no | Interval between runs. The minimum is **1 second**. |
| `MaxRepeatCount` (`int?`) | no | How many times to run at most, successes and failures alike. `null` or `-1` means without limit. |
| `MaxErrorAttempts` (`int?`) | no | How many failures to tolerate, counting the first run. `null` or `-1` means without limit; `1` means one attempt and no retry. |

Metadata can be supplied three ways: the `PRBackgroundTaskAttribute`, a separate `IPRBackgroundTaskMetadata` implementation, or directly in code. **A task with no metadata is never started** — nothing complains, it simply does not run.

## Registering tasks

### Through DI

```csharp
builder.Services.AddTransient<IPRBackgroundTask, ExampleDIAttributeBackgroundTasks>();
builder.Services.AddTransient<IPRBackgroundTask, ExampleWithMetadataBackgroundTasks>();
builder.Services.AddTransient<IPRBackgroundTask, ExampleWithoutMetadataBackgroundTasks>();

var serviceProvider = app.Services.GetService<IServiceProvider>();

var prBotInstance = new PRBotBuilder("token")
    .SetClearUpdatesOnStart(true)
    .SetServiceProvider(serviceProvider)                            // hand over the container
    .AddInlineClassHandler(ClassTHeader.DefaultTestClass, typeof(BotInlineHandlerWithDependency))
    .AddBackgroundTaskMetadata(new ExampleBackgroundTasksMetadata()) // metadata for a DI-resolved task
    .Build();
```

Every registered service implementing `IPRBackgroundTask` is picked up automatically. This is the route to take when a task needs a database, a logger or any other service.

### Through the builder

```csharp
// 1. A task that carries its own metadata.
//    It must implement IPRBackgroundTaskMetadata or carry [PRBackgroundTask].
var bot = new PRBotBuilder("token")
    .AddBackgroundTask(new AttributeBackgroundTask())
    .Build();

// 2. A task with its metadata supplied separately.
var botWithMetadata = new PRBotBuilder("token")
    .AddBackgroundTask(new ExampleBackgroundTask(), new ExampleBackgroundTasksMetadata())
    .Build();

// 3. Metadata only — for a task that DI will resolve.
var botWithDI = new PRBotBuilder("token")
    .AddBackgroundTaskMetadata(new ExampleBackgroundTasksMetadata())
    .Build();
```

## Watching the tasks

### Running

`ActiveTasks` is a dictionary keyed by the task's `Guid`. Each value is an `IRunningBackgroundTaskData` carrying the metadata, the `PRTaskStatus`, `ExecutedCount`, `ErrorCount` and the `CancellationTokenSource`.

```csharp
var activeTasks = bot.BackgroundTaskRunner.ActiveTasks;

foreach (var kvp in activeTasks)
{
    var taskData = kvp.Value;
    Console.WriteLine($"Task: {taskData.Metadata.Name}, status: {taskData.Status}, errors: {taskData.ErrorCount}");
}
```

### Finished

Anything that has finished — succeeded, failed or been cancelled — moves to `EndTasks`, where `CompleteStatus` is `Success`, `Failed` or `Canceled`, alongside `StartDate`, `EndDate` and the run and error counts.

```csharp
var completedTasks = bot.BackgroundTaskRunner.EndTasks;

foreach (var taskData in completedTasks)
{
    Console.WriteLine($"Task: {taskData.Metadata.Name}, result: {taskData.CompleteStatus}, runs: {taskData.ExecutedCount}, errors: {taskData.ErrorCount}");
}
```

Together these make a `/status` command for administrators about ten lines of work.

## Stopping

```csharp
// One task.
await bot.BackgroundTaskRunner.StopAsync(taskId);

// All of them.
await bot.BackgroundTaskRunner.StopAsync();
```

## Examples

* [A task with the metadata attribute](metadata-attribute.md)
* [A task implementing the metadata interface](metadata-interface.md)
* [A task with a separate metadata class](separate-metadata-class.md)
