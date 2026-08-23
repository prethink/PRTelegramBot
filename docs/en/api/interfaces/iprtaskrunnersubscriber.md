---
description: Subscriber of the background task runner.
---

# IPRTaskRunnerSubscriber

Subscriber of the background task runner.

Inherits `IPRGlobalSubscriber`.

## Methods

| Method | Description |
| --- | --- |
| `void StopEvent(IEnumerable<long> botIds, Guid taskId)` | Event raised when a background task stops. |
| `void StopEvent(Guid taskId)` | Event raised when a background task stops. |

