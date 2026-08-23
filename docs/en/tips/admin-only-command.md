---
description: Combining a custom attribute with a pre-execution check to gate commands.
---

# A command for administrators only

A worked example of [pre-execution checks](../command-handling/pre-execution-checks.md): marking methods with an attribute of your own, and having a checker enforce it.

The pattern generalises — swap the attribute and the condition and you have subscription-only commands, beta-tester commands, or anything else decided per method.

## 1. The attribute

Nothing but a marker. Methods carrying it are for administrators.

```csharp
namespace ConsoleExample.Attributes
{
    internal class AdminOnlyExampleAttribute : Attribute
    {
    }
}
```

## 2. The checker

The checker asks whether the method about to run carries that attribute, and if it does, whether the user is an administrator.

```csharp
namespace ConsoleExample.Checkers
{
    // Must implement IInternalCheck.
    internal class AdminExampleChecker : IInternalCheck
    {
        public async Task<InternalCheckResult> Check(IBotContext context, CommandHandler handler)
        {
            // The handler gives us the method that is about to run.
            var method = handler.Command.Method;

            // Does it carry our attribute?
            var adminAttribute = method.GetCustomAttribute<AdminOnlyExampleAttribute>();
            if (adminAttribute != null)
            {
                var userIsAdmin = await context.IsAdmin(context.Update.GetChatId());
                if (!userIsAdmin)
                    await MessageSender.Send(context, "You are not an admin!");

                // Passed lets the method run; anything else stops it.
                return userIsAdmin ? InternalCheckResult.Passed : InternalCheckResult.Custom;
            }

            // No attribute — not our business.
            return InternalCheckResult.Passed;
        }
    }
}
```

Two details worth noticing.

`handler.Command.Method` is what makes this work at all: the check can see *which* method is about to run and read its attributes. None of the other hooks can.

`InternalCheckResult.Custom` rather than a plain failure is deliberate. The checker has already told the user why, so `Custom` means "stopped, and handled" — it prevents a second, generic refusal on top of the specific one.

## 3. Registering it

```csharp
var adminChecker = new InternalChecker(
    new List<CommandType>() { CommandType.Reply, CommandType.NextStep, CommandType.Inline, CommandType.DynamicReply, CommandType.Slash },
    new AdminExampleChecker());

var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddCommandChecker(adminChecker)
                    .Build();
```

All five command kinds are listed on purpose. A check registered for `Reply` alone leaves the same command reachable through an inline button — gate every route or the gate is decorative.

## 4. The command

```csharp
/// <summary>
/// Runs for the bot with botId 0.
/// Runs when the user writes "Admins only".
/// An example of a custom checker together with a custom attribute.
/// </summary>
[AdminOnlyExample]
[ReplyMenuHandler("Admins only")]
public static async Task AdminOnlyExample(IBotContext context)
{
    bool isAdminUpdate = await context.IsAdmin();
    bool isAdminById = await context.IsAdmin(context.Update.GetChatId());

    await MessageSender.Send(context, $"You are a bot administrator: {isAdminById} {isAdminUpdate}");
}
```

<figure><img src="../.gitbook/assets/изображение (23).png" alt="The bot answering differently for an administrator and for an ordinary user"><figcaption>The same command, seen by an administrator and by everyone else</figcaption></figure>

The command body no longer decides anything about access — the attribute does. Adding `[AdminOnlyExample]` to another method is now the whole of the work.
