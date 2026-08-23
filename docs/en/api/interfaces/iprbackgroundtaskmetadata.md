---
description: Interface for background task metadata. Holds the information needed to schedule the task and control how it runs without describing its business logic.
---

# IPRBackgroundTaskMetadata

Interface for background task metadata. Holds the information needed to schedule the task and control how it runs without describing its business logic.

## Properties

| Property | Description |
| --- | --- |
| `HashSet<long> BotIds { get; }` | Identifiers of the bots the background task is intended for. An optional parameter. Used to separate background tasks per bot when working through DI. An empty collection, or the presence of `PRConstants.ALL_BOTS_ID`, means the task applies to every bot. |
| `Guid Id { get; }` | Unique identifier of the background task. Used to match the metadata with the task implementation. |
| `string Name { get; }` | Unique name of the background task. Used for logging, diagnostics and identifying the task. |
| `int? InitialDelaySeconds { get; }` | Delay in seconds before the background task runs for the first time. A value of null, or a value less than or equal to 0, means the task starts immediately. |
| `int? RepeatSeconds { get; }` | Interval in seconds at which the background task repeats. The minimum repeat interval is always 1 second. |
| `int? MaxRepeatCount { get; }` | Maximum number of runs of the background task (including both successful and failed attempts). A value of null or -1 means an unlimited number of runs. |
| `int? MaxErrorAttempts { get; }` | Maximum number of attempts to run the background task when errors occur (including the first run). A value of null or -1 means no limit. A value of 1 means a single run with no retries on error. |

