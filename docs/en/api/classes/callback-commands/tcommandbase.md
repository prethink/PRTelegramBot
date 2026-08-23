---
description: Base command.
---

# TCommandBase

Base command.

## Properties

| Property | Description |
| --- | --- |
| `int HeaderCallbackCommand { get; set; }` | Callback command header. |
| `int ActionWithLastMessage { get; set; }` | Action to perform on the previous message. |

## Methods

| Method | Description |
| --- | --- |
| `TEnum GetLastCommandEnum<TEnum>() where TEnum : Enum` | gets the command as the required enum type. |
| `ActionWithLastMessage GetActionWithLastMessage()` | Action to perform on the last message. |

## Constructors

| Constructor | Description |
| --- | --- |
| `TCommandBase()` | Constructor. |
| `TCommandBase(int command)` | Constructor. |
| `TCommandBase(int command, ActionWithLastMessage action)` | Constructor. |
| `TCommandBase(ActionWithLastMessage action)` | Constructor. |

