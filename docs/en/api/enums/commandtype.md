---
description: Command types.
---

# CommandType

Command types.

## Values

| Value | Description |
| --- | --- |
| `None` | No command type. |
| `Reply` | A reply command declared in code. |
| `ReplyDynamic` | A reply command whose trigger text comes from a configuration file. |
| `Slash` | A slash command, for example /start. |
| `NextStep` | A step of a step-by-step command sequence. |
| `Inline` | An inline command triggered by a callbackQuery. |
| `Message` | A command handled through the message pipeline. |
| `Custom` | A command type defined by the application. |

