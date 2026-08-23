---
description: Background task runner.
---

# PRBackgroundTaskRunner

Background task runner.

Inherits `IPRBackgroundTaskRunner`, `IPRTaskRunnerSubscriber`.

## Fields

| Field | Description |
| --- | --- |
| `IReadOnlyDictionary<Guid, IRunningBackgroundTaskData> ActiveTasks => activeTasks` |  |
| `IReadOnlyCollection<IRunningBackgroundTaskData> EndTasks => completedTasks` |  |

## Methods

| Method | Description |
| --- | --- |
| `IReadOnlyCollection<IPRBackgroundTask> TaskInstance => registeredTaskInstances.ToList()` |  |
| `IReadOnlyCollection<IPRBackgroundTaskMetadata> Metadata => registeredTaskMetadata.ToList()` |  |
| `Task StartAsync()` |  |
| `Task StartAsync(IPRBackgroundTask task)` |  |
| `Task StartAsync(IPRBackgroundTask backgroundTask, IPRBackgroundTaskMetadata metadata)` |  |
| `async Task StopAsync()` |  |
| `async Task StopAsync(Guid taskId)` |  |
| `async Task StopAsync(IPRBackgroundTaskMetadata metadata)` |  |
| `void Initialize(IEnumerable<IPRBackgroundTaskMetadata> metadata, IEnumerable<IPRBackgroundTask> tasks)` |  |
| `void StopEvent(IEnumerable<long> botIds, Guid taskId)` |  |
| `void StopEvent(Guid taskId)` |  |
| `void Subscribe()` |  |
| `void Unsubscribe()` |  |
| `void Dispose()` |  |

## Constructors

| Constructor | Description |
| --- | --- |
| `PRBackgroundTaskRunner(PRBotBase bot)` | Constructor. |

