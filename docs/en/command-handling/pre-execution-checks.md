---
description: Running your own check before a command executes, and stopping it if the check fails.
---

# Pre-execution checks

Every command kind can be given checks of its own. A check runs before the command, and can stop it.

The example below adds a checker for [Reply](../api/enums/commandtype.md) commands only:

```csharp
// A checker used only for reply commands.
var checkerReplyCommand = new InternalChecker(CommandType.Reply, new ReplyExampleChecker());

// Register it when creating the bot.
var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddCommandChecker(checkerReplyCommand)
                    .Build();
```

`CommandType` is what scopes the check: pass `CommandType.Slash` and it applies to slash commands, `CommandType.Inline` to inline ones, and so on. A check that should apply everywhere is registered once per kind.

## Writing a checker

A checker implements [`IInternalCheck`](../api/interfaces/iinternalcheck.md) and returns a verdict:

```csharp
namespace ConsoleExample.Checkers
{
    internal class ReplyExampleChecker : IInternalCheck
    {
        public async Task<InternalCheckResult> Check(IBotContext context, CommandHandler handler)
        {
            // Check whatever needs checking before a reply command runs.
            // Passed lets the command run; any other result stops it.
            return InternalCheckResult.Passed;
        }
    }
}
```

Only `InternalCheckResult.Passed` allows the command through. Every other value stops it — including `Custom`, which is the one to return when your check has already answered the user itself.

The `handler` argument is what makes this more than a blanket filter: it describes the command about to run, so a check can look at the method's attributes and decide per command rather than per kind. That is how "administrators only" is built without touching the framework.

## How this differs from the neighbouring hooks

| Mechanism | Runs | Can stop the command | Knows which command |
| --- | --- | --- | --- |
| [Middleware](../middleware.md) | around the whole update | yes | no |
| `OnPreUpdate` [event](../events/update-events.md) | before the update | yes | no |
| **Pre-execution check** | before the command | **yes** | **yes** |
| [Command events](../events/command-events.md) | around the command | no | yes |

The check is the only place that both knows what is about to run and is allowed to prevent it.

## Example

* [A command for administrators only](../tips/admin-only-command.md)
