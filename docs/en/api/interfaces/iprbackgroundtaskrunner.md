---
description: Interface of the background task runner. Responsible for starting, stopping and managing the lifetime of background tasks.
---

# IPRBackgroundTaskRunner

Interface of the background task runner. Responsible for starting, stopping and managing the lifetime of background tasks.

## Properties

| Property | Description |
| --- | --- |
| `IReadOnlyDictionary<Guid, IRunningBackgroundTaskData> ActiveTasks { get; }` | The current list of running tasks. Holds the metadata key and a reference to the running Task. |
| `IReadOnlyCollection<IRunningBackgroundTaskData> EndTasks { get; }` | Finished tasks. |
| `IReadOnlyCollection<IPRBackgroundTask> TaskInstance { get; }` | Task instances. |
| `IReadOnlyCollection<IPRBackgroundTaskMetadata> Metadata { get; }` | Task metadata. |

## Methods

| Method | Description |
| --- | --- |
| `void Initialize(IEnumerable<IPRBackgroundTaskMetadata> metadata, IEnumerable<IPRBackgroundTask> tasks)` | Initializes the background tasks. |
| `Task StartAsync()` | Starts the background tasks. |
| `Task StartAsync(IPRBackgroundTask backgroundTask)` | Starts the background task. IMPORTANT. Before calling this method, make sure the metadata is either already loaded into the runner or carried by the task itself. For example through the `PRBackgroundTaskAttribute` attribute, or by implementing the `IPRBackgroundTaskMetadata` interface |
| `Task StartAsync(IPRBackgroundTask backgroundTask, IPRBackgroundTaskMetadata metadata)` | Starts the background task. |
| `Task StopAsync()` | Stops all running background tasks. |
| `Task StopAsync(Guid taskId)` | Stops the specified background task. |
| `Task StopAsync(IPRBackgroundTaskMetadata metadata)` | Stops the specified background task. |

