---
description: Declaring who administers the bot, and checking it in a command.
---

# Bot administrators

The library keeps a list of administrator user ids. You can fill it when the bot is created:

```csharp
var telegram = new PRBotBuilder("")
                    .AddAdmin(1111111)
                    .AddAdmin(33333, 5555, 6666, 777)
                    .AddAdmins(new List<long>() { 222222, 33333, 44444, 55555 })
                    .Build();
```

## Checking inside a command

Use the `IsAdmin` extension. It is asynchronous, because a custom manager may need to go to a database.

```csharp
[ReplyMenuHandler("Admin menu")]
public static async Task AdminMenu(IBotContext context)
{
    // The user this update came from.
    if (await context.IsAdmin())
    {
        // The user is an administrator.
    }

    // Or an explicit id.
    if (await context.IsAdmin(context.Update.GetChatId()))
    {
        // ...
    }
}
```

## Using your own administrator list

The list lives behind the `AdminManager` property on [`TelegramOptions`](https://prethink.gitbook.io/prtelegrambot/api/klassy/telegramoptions). The built-in [`AdminListManager`](https://prethink.gitbook.io/prtelegrambot/api/klassy/adminlistmanager) implements [`IAdminManager`](https://prethink.gitbook.io/prtelegrambot/api/interfeisy/iadminmanager), which means you can substitute your own — one backed by a database, for instance, so administrators can be changed without a redeploy.

Register your implementation in **DI**, or pass it with `SetAdminManager` when building the bot. Which one wins is described in [Component resolution priorities](dependency-injection/resolution-priorities.md).

## Related

For finer-grained rules than "administrator or not", see [Restricted access to commands](restricted-access.md), where each command declares the privileges it needs.
