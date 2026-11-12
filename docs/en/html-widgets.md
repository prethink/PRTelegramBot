# HTML widgets

Telegram Bot API 10.4 renders HTML widgets inside messages. In production, bots can send widgets to private chats, and Telegram protects those messages from forwarding. Use Telegram's test server to try groups, inline mode and forwards. [Widget availability](https://core.telegram.org/bots/widgets#availability).

## Send a widget and answer a request

Register the events before `bot.StartAsync()` and put the command class after the startup code. The `/widget` command sends a widget with a button that sends `ping` to the bot.

```csharp
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using Telegram.Bot;

bot.Events.UpdateEvents.OnWidgetQueryHandle += e =>
{
    var query = e.Context.Update.WidgetQuery!;
    return e.Context.BotClient.AnswerWidgetQuery(query.Id,
        data: query.Data == "ping" ? "pong" : "Unknown request",
        cancellationToken: e.Context.CancellationToken);
};

bot.Events.UpdateEvents.OnSharedWidgetQueryHandle += e =>
{
    var query = e.Context.Update.SharedWidgetQuery!;
    return e.Context.BotClient.AnswerWidgetQuery(query.Id,
        data: query.Data == "ping" ? "pong" : "Unknown request",
        cancellationToken: e.Context.CancellationToken);
};
```

```csharp
public static class WidgetCommands
{
    [SlashHandler("/widget")]
    public static Task ShowWidget(IBotContext context)
    {
        const string html = @"
            <button id='ping'>Ping</button>
            <p id='status'></p>
            <script>
              const status = document.getElementById('status');
              document.getElementById('ping').addEventListener('click', async () => {
                try {
                  const answer = await TelegramWidget.request('ping');
                  status.textContent = answer.result ?? '';
                } catch (error) {
                  status.textContent = error.code ?? 'Request failed';
                }
              });
            </script>
            ";

        return context.BotClient.SendWidget(context.GetChatId(), html,
            allowQueries: true, cancellationToken: context.CancellationToken);
    }
}
```

## Events and data

| Event | Payload |
| --- | --- |
| `bot.Events.MessageEvents.OnWidgetHandle` | An incoming message with a widget at `e.Context.Update.Message?.Widget`. |
| `bot.Events.UpdateEvents.OnWidgetQueryHandle` | A query from a widget in a message sent by or via the bot. An inline message has an `InlineMessageId`. |
| `bot.Events.UpdateEvents.OnSharedWidgetQueryHandle` | A query from a widget in a forwarded message. The update carries the `WidgetId` and the sender. |

If polling or webhook configuration supplies an explicit `AllowedUpdates` list, add `UpdateType.WidgetQuery` and `UpdateType.SharedWidgetQuery`. Both query types expose the sender through `e.Context.GetUserId()`. Use `TryGetChatId` to check for a chat ID. A chat allowlist in `OnPreUpdate` mode requires the update to carry a chat ID.

Answer each query within 15 seconds. `AnswerWidgetQuery` returns the result to the widget that made the request. To change stored data, use `BotClient.EditMessageWidgetData` for a message or `BotClient.EditSharedWidgetData` for shared state. To create a reusable shared widget, pass `isShared: true` to `SendWidget` and use the returned `Message.Widget.Id` with `SendSharedWidget`. [Telegram widget guide](https://core.telegram.org/bots/widgets).
