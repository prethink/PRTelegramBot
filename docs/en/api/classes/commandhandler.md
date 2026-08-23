---
description: Common command handler.
---

# CommandHandler

Common command handler.

## Properties

| Property | Description |
| --- | --- |
| `CommandComparison CommandComparison { get;}` | Command comparison. |
| `MethodInfo Method { get; private set; }` | Information about the method. |

## Methods

| Method | Description |
| --- | --- |
| `async Task ExecuteCommand(IBotContext context)` | Executes the command. |

## Constructors

| Constructor | Description |
| --- | --- |
| `CommandHandler(MethodInfo method)` | Constructor. |
| `CommandHandler(MethodInfo method, CommandComparison commandComparison)` | Constructor. |
| `CommandHandler(MethodInfo method, PRBotBase bot)` | Constructor. |
| `CommandHandler(Func<IBotContext, Task> command)` | Constructor. |
| `CommandHandler(Func<IBotContext, Task> command, PRBotBase bot)` | Constructor. |
| `CommandHandler(Func<IBotContext, Task> command, CommandComparison commandComparison)` | Constructor. |
| `CommandHandler(Func<IBotContext, Task> command, PRBotBase bot, CommandComparison commandComparison)` | Constructor. |
| `CommandHandler(MethodInfo method, PRBotBase bot, CommandComparison commandComparison)` | Constructor. |

