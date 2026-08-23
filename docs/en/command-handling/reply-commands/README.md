---
description: Reacting to the plain text a user sends.
---

# Reply commands

The user writes something, the bot answers. This is the simplest kind of command, and the one most bots are built from.

See [Parameters](../parameters.md) for what `botId`, `CommandComparison` and `StringComparison` mean.

## The attribute

Reply commands are declared with **`ReplyMenuHandler`**:

```csharp
/// <param name="botId">Bot identifier.</param>
/// <param name="botIds">Bot identifiers.</param>
/// <param name="commandComparison">How to compare the command.</param>
/// <param name="stringComparison">How to compare the string.</param>
/// <param name="commands">Commands.</param>
public ReplyMenuHandlerAttribute(params string[] commands)
public ReplyMenuHandlerAttribute(long botId, params string[] commands)
public ReplyMenuHandlerAttribute(long[] botIds, params string[] commands)
public ReplyMenuHandlerAttribute(CommandComparison commandComparison, params string[] commands)
public ReplyMenuHandlerAttribute(long botId, CommandComparison commandComparison, params string[] commands)
public ReplyMenuHandlerAttribute(long[] botIds, CommandComparison commandComparison, params string[] commands)
public ReplyMenuHandlerAttribute(StringComparison stringComparison, params string[] commands)
public ReplyMenuHandlerAttribute(long botId, StringComparison stringComparison, params string[] commands)
public ReplyMenuHandlerAttribute(long[] botIds, StringComparison stringComparison, params string[] commands)
public ReplyMenuHandlerAttribute(CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
public ReplyMenuHandlerAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
public ReplyMenuHandlerAttribute(long[] botIds, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
```

Used without a `botId`, the attribute binds to the bot whose id is `0`.

`CommandComparison` decides whether the message must match the command exactly or merely contain it. For reply commands the default is `Equals`.

`botId` matters once a project runs more than one bot. It is the value set here:

```csharp
var telegram = new PRBotBuilder("").SetBotId(0).Build();
```

## Examples

```csharp
public class Commands
{
    /// <summary>
    /// Runs for the bot with botId 0.
    /// Runs when the message text contains "Command contains text".
    /// Case is ignored during the check.
    /// </summary>
    [ReplyMenuHandler(CommandComparison.Contains, StringComparison.OrdinalIgnoreCase, "Command contains text")]
    public static async Task ReplyExampleOne(IBotContext context)
    {
        await MessageSender.Send(context, nameof(ReplyExampleOne));
    }

    /// <summary>
    /// Runs for the bot with botId 0.
    /// Runs when the message text equals "Exact command match", ignoring case.
    /// </summary>
    [ReplyMenuHandler("Exact command match")]
    public static async Task ReplyExampleTwo(IBotContext context)
    {
        await MessageSender.Send(context, nameof(ReplyExampleTwo));
    }

    /// <summary>
    /// Runs for the bot with botId 0.
    /// Write "Example 1" or "Example 2" in the chat.
    /// One method serving several commands.
    /// </summary>
    [ReplyMenuHandler("Example 1", "Example 2")]
    public static async Task ExampleReplyMany(IBotContext context)
    {
        await MessageSender.Send(context, nameof(ExampleReplyMany));
    }

    /// <summary>
    /// Runs for the bot with botId 1 only.
    /// </summary>
    [ReplyMenuHandler(1, "Example command for bot id 1")]
    public static async Task ExampleReplyBotIdOne(IBotContext context)
    {
        await MessageSender.Send(context, nameof(ExampleReplyBotIdOne));
    }

    /// <summary>
    /// Runs for every bot, whatever its botId.
    /// </summary>
    [ReplyMenuHandler(-1, "Command for all bots")]
    public static async Task ReplyExampleAllBots(IBotContext context)
    {
        await MessageSender.Send(context, nameof(ReplyExampleAllBots));
    }
}
```

## Next

* [Creating a reply menu](reply-menu.md) — turning these commands into buttons the user can tap.
* [Dynamic reply commands from a JSON file](dynamic-reply-commands.md) — commands whose text lives in configuration rather than in code.
