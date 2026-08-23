---
description: Background task metadata attribute.
---

# PRBackgroundTaskAttribute

Background task metadata attribute.

Inherits `Attribute`, `IPRBackgroundTaskMetadata`.

## Properties

| Property | Description |
| --- | --- |
| `HashSet<long> BotIds { get; } = new HashSet<long>()` |  |
| `Guid Id { get; private set; }` |  |
| `string Name { get; private set; }` |  |
| `int? InitialDelaySeconds { get; private set; }` |  |
| `int? RepeatSeconds { get; private set; }` |  |
| `int? MaxErrorAttempts { get; private set; }` |  |
| `int? MaxRepeatCount { get; private set; }` |  |

## Constructors

| Constructor | Description |
| --- | --- |
| `PRBackgroundTaskAttribute(string id, string name)` | Constructor. |
| `PRBackgroundTaskAttribute(string id, int initialDelaySeconds, string name)` | Constructor. |
| `PRBackgroundTaskAttribute(string id, string name, int repeatSeconds)` | Constructor. |
| `PRBackgroundTaskAttribute(string id, string name, int repeatSeconds, int maxRepeatCount)` | Constructor. |
| `PRBackgroundTaskAttribute(string id, int initialDelaySeconds, string name, int repeatSeconds)` | Constructor. |
| `PRBackgroundTaskAttribute(string id, int initialDelaySeconds, string name, int repeatSeconds, int maxRepeatCount)` | Constructor. |
| `PRBackgroundTaskAttribute(long[] botsIds, string id, string name, int? initialDelaySeconds, int? maxRepeatCount, int? repeatSeconds, int? maxErrorAttempts)` | Constructor. |

