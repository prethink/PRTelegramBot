---
description: Interface for the data of a running task.
---

# IRunningBackgroundTaskData

Interface for the data of a running task.

## Properties

| Property | Description |
| --- | --- |
| `Task Task { get; }` | Task. |
| `IPRBackgroundTaskMetadata Metadata { get; }` | Metadata. |
| `IReadOnlyList<Exception> Errors { get; }` | Errors |
| `int ErrorCount { get; }` | Number of errors. |
| `int ExecutedCount { get; }` | Number of runs |
| `DateTime? StartDate { get; }` | Date and time the task started. |
| `DateTime? EndDate { get; }` | Date and time the task finished. |
| `PRTaskStatus Status { get; }` | Task status. |
| `PRTaskCompletionResult CompleteStatus { get; }` | The task's completion status. |
| `CancellationTokenSource CancellationTokenSource { get; }` | The cancellation token source. |

## Methods

| Method | Description |
| --- | --- |
| `void IncrementExecutionCount()` | Increments the task's run counter. |
| `void AddError(Exception ex)` | Records an error. |
| `void SetStatus(PRTaskStatus status)` | Sets the task status. |
| `void SetCompleteStatus(PRTaskCompletionResult status)` | Sets the task's completion status. |
| `void StartTask()` | Starts the task. |
| `void EndTask()` | Finishes the task. |

