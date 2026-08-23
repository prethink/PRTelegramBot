---
description: Handles a TCommand in page form.
---

# PageTCommand

Handles a TCommand in page form.

Inherits `TCommandBase`.

## Properties

| Property | Description |
| --- | --- |
| `int Page { get; set; }` | Page number. |
| `int Header { get; set; }` | Command header. |

## Constructors

| Constructor | Description |
| --- | --- |
| `PageTCommand(int page, Enum enumValueInt)` | Constructor. |
| `PageTCommand(int page, Enum enumValueInt, int lastCommand)` | Constructor. |
| `PageTCommand(int page, Enum enumValueInt, ActionWithLastMessage action)` | Constructor. |
| `PageTCommand(int page, Enum enumValueInt, int lastCommand, ActionWithLastMessage action)` | Constructor. |
| `PageTCommand() { }` | Constructor. |

