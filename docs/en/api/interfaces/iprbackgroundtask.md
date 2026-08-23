---
description: Interface of a background task.
---

# IPRBackgroundTask

Interface of a background task.

## Properties

| Property | Description |
| --- | --- |
| `Guid Id { get; }` | Task identifier. |

## Methods

| Method | Description |
| --- | --- |
| `Task<bool> CanExecute()` | Checks whether the background task can run right now. The framework calls this method before every execution attempt. Returning false means execution is skipped and the check is repeated at the next scheduled run. |
| `Task ExecuteAsync(CancellationToken cancellationToken)` | Starts running the background task. |
| `Task Initialize(PRBotBase bot)` | Sets the bot instance so its context and services can be accessed. The framework calls this method when the background task is initialized. |

