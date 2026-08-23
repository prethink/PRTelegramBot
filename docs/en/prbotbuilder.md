---
description: The fluent builder that creates and configures a bot.
---

# PRBotBuilder — creating bots

`PRBotBuilder` builds a bot through a fluent chain. Everything a bot needs to know is set here, then `Build()` produces the instance.

```csharp
var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .Build();
```

Every method returns the builder, so calls chain in any order. `Build()` is what ends the chain.

## Creating and finishing

| Method | What it does |
| --- | --- |
| `PRBotBase Build()` | Builds the bot instance. |
| `void ClearOptions(string token)` | Resets the options, keeping the token. |
| `void ClearOptions(TelegramBotClient client)` | Resets the options, keeping the client. |
| `SetToken(string token)` | Sets the token. |
| `SetTelegramClient(TelegramBotClient client)` | Uses a Telegram client you built yourself. |
| `SetBotId(long botId)` | Sets the bot identifier — see [Parameters](command-handling/parameters.md). Defaults to `0`. |
| `UseFactory(PRBotFactoryBase factory)` | Chooses how the bot is created. `PRBotWebHookFactory` is what makes a webhook bot. |
| `SetInitializeAction(Action action)` | Runs your code once the bot has been initialised. |

## Users and access

| Method | What it does |
| --- | --- |
| `AddAdmin(params long[] telegramId)` | Adds one or more administrators. |
| `AddAdmins(List<long> telegramIds)` | Adds administrators from a list. |
| `SetAdminManager(IAdminManager adminManager)` | Replaces the administrator list with your own implementation. |
| `AddUserWhiteList(params long[] telegramId)` | Adds users to the white list. |
| `AddUsersWhiteList(List<long> telegramIds)` | Adds users from a list. |
| `SetWhiteListManager(IWhiteListManager whiteListManager)` | Replaces the white list with your own implementation. |
| `SetWhiteListSettings(WhiteListSettings settings)` | Narrows where the white list applies — see [User white list](user-white-list.md). |

## Commands and handling

| Method | What it does |
| --- | --- |
| `AddReplyDynamicCommand(string key, string value)` | Adds one dynamic reply command. |
| `AddReplyDynamicCommands(Dictionary<string, string> dynamicCommands)` | Adds dynamic reply commands from a dictionary — see [Dynamic reply commands](command-handling/reply-commands/dynamic-reply-commands.md). |
| `AddCommandChecker(InternalChecker checker)` | Adds a check that runs before a command executes. |
| `AddCommandChecker(List<InternalChecker> checkers)` | Adds several. |
| `AddMiddlewares(MiddlewareBase middleware)` | Adds one middleware — see [Middleware](middleware.md). |
| `AddMiddlewares(params MiddlewareBase[] middlewares)` | Adds several. |
| `SetUpdateHandler(IPRUpdateHandler updateHandler)` | Replaces the update handler entirely. |
| `SetRegisterCommand(IRegisterCommand registerCommand)` | Replaces how commands are discovered and registered. |
| `AddMessageCommandHandlers(params IMessageCommandHandler[] handlers)` | Adds handlers for `message` updates. |
| `AddCallbackQueryCommandHandlers(params ICallbackQueryCommandHandler[] handlers)` | Adds handlers for `callbackQuery` updates. |
| `AddInlineClassHandler(Enum @enum, Type type)` | Binds an inline command to a class instance rather than a method. |

Both `Add*CommandHandlers` methods also take a `List<T>`.

## Inline data

| Method | What it does |
| --- | --- |
| `SetInlineSerializer(IPRSerializer serializer)` | Chooses how inline button data is serialised. `ToonSerializerWrapper` is more compact than the JSON default. |
| `SetInlineMenuConverter(IInlineMenuConverter inlineMenuConverter)` | Chooses how `callback_data` is formed. `FileInlineConverter` removes the 64-byte limit — see [Creating an inline menu](command-handling/inline-commands/inline-menu.md). |

Both are also resolvable from DI; the builder wins. See [Component resolution priorities](dependency-injection/resolution-priorities.md).

## Receiving updates

| Method | What it does |
| --- | --- |
| `SetClearUpdatesOnStart(bool flag)` | Drops everything pending when the bot starts, so a restart does not replay a backlog. |
| `AddReceivingOptions(ReceiverOptions receiverOptions)` | Telegram.Bot receiver options — among other things, which update types to ask for. |

### Webhook

| Method | What it does |
| --- | --- |
| `SetUrlWebHook(string url)` | The public URL Telegram delivers to. Must match the route you registered. |
| `SetSecretTokenWebHook(string secretToken)` | The secret token. Generated automatically if you do not set one. |
| `SetIpAddressWebHook(string ipAddress)` | Fixes the IP address Telegram connects to. |
| `SetMaxConnectionsWebHook(int maxConnections)` | Caps simultaneous connections. |
| `SetCertificateWebHook(InputFileStream certificate)` | Supplies a self-signed certificate. |
| `SetDropPendingUpdates(bool flag)` | Drops pending updates when the webhook is set. |

See [Webhook](getting-started/webhook/) for the full setup.

## Infrastructure

| Method | What it does |
| --- | --- |
| `SetServiceProvider(IServiceProvider serviceProvider)` | Hands the bot a DI container — see [Dependency injection](dependency-injection/). |
| `SetLoggerFactory(ILoggerFactory loggerFactory)` | Sets the logger factory. Used when there is no DI container, or logging is wired by hand — see [Logging](logging.md). |
| `AddConfigPath(string key, string path)` | Registers a configuration file under a key. |
| `AddConfigPaths(Dictionary<string, string> configPaths)` | Registers several. |
| `SetAntiSpamErrorMinute(int minute)` | Rate-limits repeated error log entries, so one failing update cannot flood the log. |

## Background tasks

| Method | What it does |
| --- | --- |
| `AddBackgroundTask(IPRBackgroundTask backgroundTask)` | Adds a task. It must implement `IPRBackgroundTaskMetadata` or carry `[PRBackgroundTask]`, otherwise the framework does not know how often to run it. |
| `AddBackgroundTask(IPRBackgroundTask backgroundTask, IPRBackgroundTaskMetadata metadata)` | Adds a task with its metadata supplied separately. |
| `AddBackgroundTaskMetadata(IPRBackgroundTaskMetadata metadata)` | Registers metadata on its own, for tasks resolved from DI. |
