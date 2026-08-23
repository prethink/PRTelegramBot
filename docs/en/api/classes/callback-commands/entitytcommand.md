---
description: Command that carries the entity identifier.
---

# EntityTCommand

Command that carries the entity identifier.

Inherits `TCommandBase`.

## Properties

| Property | Description |
| --- | --- |
| `T EntityId { get; set; }` | Entity identifier |

## Constructors

| Constructor | Description |
| --- | --- |
| `EntityTCommand(T entityId)` | Constructor. |
| `EntityTCommand(T entityId, int lastCommand)` | Constructor. |
| `EntityTCommand(T entityId, ActionWithLastMessage action)` | Constructor. |
| `EntityTCommand(T entityId, int lastCommand, ActionWithLastMessage action)` | Constructor. |
| `EntityTCommand() { }` | Constructor. |

