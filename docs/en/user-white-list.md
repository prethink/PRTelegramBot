---
description: Restricting the bot to a known set of users.
---

# User white list

If the white list has anyone in it, **only those users can use the bot**. An empty list means the bot is open to everyone.

Fill it when the bot is created:

```csharp
var telegram = new PRBotBuilder("")
                    .AddUserWhiteList(1111)
                    .AddUserWhiteList(2222, 3333, 4444, 555)
                    .AddUsersWhiteList(new List<long>() { 5555, 6666, 77777 })
                    .Build();
```

## Using your own white list

The list lives behind the `WhiteListManager` property on [`TelegramOptions`](api/classes/telegramoptions/README.md). The built-in [`WhiteListManager`](api/classes/whitelistmanager.md) implements [`IWhiteListManager`](api/interfaces/iwhitelistmanager.md), so you can substitute your own — one reading from a database, for example.

Register it in **DI**, or pass it with `SetWhiteListManager` when building the bot. See [Component resolution priorities](dependency-injection/resolution-priorities.md) for which one takes effect.

<figure><img src=".gitbook/assets/изображение (34).png" alt="The IWhiteListManager interface and the WhiteListManager class that implements it"><figcaption>The interface to implement, and the built-in implementation to replace</figcaption></figure>

## Leaving some commands open to everyone

The white list can be made selective: most of the bot stays closed, but a few commands remain available to anyone. That takes two things.

**One** — set the white list mode when building the bot:

```csharp
var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
                    .AddConfigPath(ExampleConstants.MESSAGES_FILE_KEY, ".\\Configs\\messages.json")
                    .AddAdmin(1111111)
                    .AddUserWhiteList(552135213512)
                    .SetWhiteListSettings(WhiteListSettings.OnlyCommands)
                    .SetClearUpdatesOnStart(true)
                    .AddReplyDynamicCommands(dynamicCommands)
                    .AddMiddlewares(new OneMiddleware(), new TwoMiddleware(), new ThreeMiddleware())
                    .Build();
```

`WhiteListSettings.OnlyCommands` narrows the check so it applies to reply, slash and inline commands.

**Two** — mark the commands that should skip the check with `[WhiteListAnonymous]`:

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Services.Messages;

namespace ConsoleExample.Examples
{
    internal class ExampleWhiteList
    {
        /// <summary>
        /// Runs for the bot with botId 0.
        /// With the white list enabled and populated, only listed users reach this.
        /// </summary>
        [ReplyMenuHandler("OnlyWhiteList")]
        public static async Task OnlyWhiteList(IBotContext context)
        {
            await MessageSender.Send(context, nameof(OnlyWhiteList));
        }

        /// <summary>
        /// Reachable by anyone, listed or not.
        /// </summary>
        [WhiteListAnonymous]
        [ReplyMenuHandler("Anonymous")]
        public static async Task Anonymous(IBotContext context)
        {
            await MessageSender.Send(context, nameof(Anonymous));
        }
    }
}
```

A typical use is leaving `/start` and a "request access" command open, so a stranger can at least find out how to be added, while everything else stays closed.
