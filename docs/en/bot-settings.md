# Bot settings and callback queries

PRTelegramBot 1.1.4 uses Telegram.Bot 22.10.5 (Bot API 10.5). Use the bot client to read the bot’s capabilities and change its settings.

```csharp
using PRTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

static async Task ConfigureAsync(IBotContext context, CancellationToken token)
{
    BotFullInfo bot = await context.BotClient.GetMe(token);
    Console.WriteLine(bot.Username);

    await context.BotClient.SetMySettings(
        callbackQueryMode: SetMySettingsCallbackQueryMode.Auto,
        cancellationToken: token);
}
```

`SetMySettings` also configures inline queries and feedback, group privacy, business connections, managed bots, guest and join request queries, private chat topics, and bot-to-bot communication. [Telegram method reference](https://core.telegram.org/bots/api#setmysettings).

## Which message does a button refer to?

For buttons on messages sent by the bot, the callback mode controls which identifier Telegram delivers.

| Mode | Callback target |
| --- | --- |
| `Default` | The chat message, possibly an inaccessible message. |
| `Auto` | The chat message when accessible, otherwise an inline message ID. A recently deleted message may still arrive as an inaccessible message. |
| `Inline` | An inline message ID, even for a message the bot sent to a chat. |

Buttons on messages sent via inline mode already use an inline message ID. In a callback handler, acknowledge the click with `AnswerCallbackQuery`. To edit text, call `MessageEditor.Edit(context, text)`. It returns the edited chat message or null after a successful inline edit. To replace the keyboard, use `MessageEditor.EditInline(context, keyboard)`. Pass null to remove the keyboard. Sending a new chat message or deleting the original requires a chat ID. A chat allowlist rejects callbacks without a chat ID when the allowlist applies.

## Migrating existing code

- `GetMe` returns `BotFullInfo`. Use that type (or `var`) to read capability flags such as `CanJoinGroups`, active usernames or inline query settings. In typed mocks, use `IRequest<BotFullInfo>`.
- Both incoming and outgoing expandable rich quotations now contain an ordered `Blocks` collection. To keep an existing text quotation, wrap its text in a paragraph. Import `Telegram.Bot.Types` for this example.

```csharp
var quote = new InputRichBlockExpandableBlockQuotation
{
    Blocks = new InputRichBlock[]
    {
        new InputRichBlockParagraph { Text = "Quoted text" }
    }
};
```

The existing `OnWriteAccessAllowedHandle` event also delivers the optional `Message.WriteAccessAllowed.Inviter` field when a bot owner invites a user to a private chat with the bot.
