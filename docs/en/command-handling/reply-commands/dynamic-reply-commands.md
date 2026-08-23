---
description: Reply commands whose text lives in a JSON file instead of in the code.
---

# Dynamic reply commands from a JSON file

An ordinary `[ReplyMenuHandler("Menu")]` bakes the button's caption into the assembly: renaming it means recompiling and redeploying.

Dynamic commands move the text into a JSON file. The handler binds to a **key**, the file supplies the **value**, and renaming a button becomes editing a file.

The attribute is **`ReplyMenuDynamicHandler`**:

```csharp
/// <param name="botId">Bot identifier.</param>
/// <param name="botIds">Bot identifiers.</param>
/// <param name="commandComparison">How to compare the command.</param>
/// <param name="stringComparison">How to compare the string.</param>
/// <param name="commands">Commands.</param>
public ReplyMenuDynamicHandlerAttribute(params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long botId, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long[] botIds, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(CommandComparison commandComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long botId, CommandComparison commandComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long[] botIds, CommandComparison commandComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(StringComparison stringComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long botId, StringComparison stringComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long[] botIds, StringComparison stringComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long[] botIds, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
```

The parameters mean the same as for [reply commands](README.md); see [Parameters](../parameters.md).

## 1. The file

`commands.json`, in plain key–value form:

```json
{
  "DYNAMIC_COMMAND_EXAMPLE": "Dynamic command",
  "MAIN_MENU": "Main menu",
  "MENU": "Menu",
  "RP_START": "Start"
}
```

## 2. Load it into the bot

```csharp
// A JSON provider for the dynamic commands.
var botJsonProvider = new BotConfigJsonProvider(".\\Configs\\commands.json");

// Read them as key:value pairs.
var dynamicCommands = botJsonProvider.GetKeysAndValues();

// Hand them to the builder.
var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .AddReplyDynamicCommands(dynamicCommands)
                    .Build();
```

## 3. Bind the handler to a key

Note what goes inside the attribute: the **key**, not the caption.

```csharp
/// <summary>
/// Runs for the bot with botId 0.
/// Runs when the user writes whatever "DYNAMIC_COMMAND_EXAMPLE" maps to in commands.json.
/// </summary>
[ReplyMenuDynamicHandler(nameof(ExampleConstants.DYNAMIC_COMMAND_EXAMPLE))]
public static async Task ExampleReplyDynamicCommand(IBotContext context)
{
    await MessageSender.Send(context, nameof(ExampleReplyDynamicCommand));
}
```

Write "Dynamic command" to the bot and the handler runs.

<figure><img src="../../.gitbook/assets/изображение (28).png" alt="The bot answering the caption that the JSON file currently maps the key to"><figcaption>The handler answers whatever the file currently says</figcaption></figure>

## Renaming without a rebuild

Edit the file:

```json
{
  "DYNAMIC_COMMAND_EXAMPLE": "Test command",
  "MAIN_MENU": "Main menu",
  "MENU": "Menu",
  "RP_START": "Start"
}
```

<figure><img src="../../.gitbook/assets/изображение (29).png" alt="The same handler now answering the new caption after only the JSON file changed"><figcaption>Same handler, new caption — only the file changed</figcaption></figure>

Now the same handler answers "Test command". Nothing was recompiled.

{% hint style="info" %}
This is also how a bot gets translated. Ship one `commands.json` per language, load the right one at startup, and every caption changes while the handlers stay untouched.
{% endhint %}

The file is read when the bot is built, so a change takes effect on the next restart rather than instantly. If you need commands to appear and disappear while the bot is running, see [Adding and removing commands at runtime](../../dynamic-command-management.md).
