---
description: Interface of the command registrar.
---

# IRegisterCommands

Interface of the command registrar.

## Methods

| Method | Description |
| --- | --- |
| `bool AddSlashCommand(string command, Func<IBotContext, Task> method)` | Registers a slash command |
| `bool AddReplyCommand(string command, Func<IBotContext, Task> method)` | Registers a reply command |
| `bool AddInlineCommand(Enum command, Func<IBotContext, Task> method)` | Registers an inline command |
| `bool RemoveReplyCommand(string command)` | Removes a reply command |
| `bool RemoveSlashCommand(string command)` | Removes a slash command |
| `bool RemoveInlineCommand(Enum command)` | Removes an inline command |
| `void Init(PRBotBase bot)` | Initialization. |

