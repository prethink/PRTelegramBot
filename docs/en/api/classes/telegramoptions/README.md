---
description: Telegram bot options.
---

# TelegramOptions

Telegram bot options.

## Properties

| Property | Description |
| --- | --- |
| `ITelegramBotClient? Client { get; set; }` | The Telegram client. |
| `string Token { get; set; } = null!` | Telegram bot token. |
| `bool ClearUpdatesOnStart { get; set; }` | Before startup, clears the updates that piled up while the bot was down. |
| `long BotId { get; set; }` | Unique identifier of the bot; it lets several bots run in a single application. |
| `Dictionary<string, string> ReplyDynamicCommands { get; set; } = new()` | Additional configuration files. |
| `Dictionary<string, string> ConfigPaths { get; set; } = new()` | Additional configuration files. |
| `CancellationTokenSource CancellationTokenSource { get; set; } = new()` | The cancellation token source. |
| `ReceiverOptions ReceiverOptions { get; set; } = new ReceiverOptions { AllowedUpdates = { } }` | Telegram bot settings. |
| `IServiceProvider? ServiceProvider { get; set; }` | Service provider. |
| `IPRUpdateHandler? UpdateHandler { get; set; }` | Telegram update handler. |
| `IAdminManager? AdminManager { get; set; }` | Administrator manager. |
| `IWhiteListManager? WhiteListManager { get; set; }` | White list manager. |
| `List<MiddlewareBase> Middlewares { get; set; } = []` | Middleware handlers that run before the update. |
| `List<InternalChecker> CommandCheckers { get; set; } = []` | Additional checks performed before commands are handled. |
| `int? Timeout { get; set; }` | Timeout for receiving updates in polling mode. |
| `List<ICallbackQueryCommandHandler> CallbackQueryHandlers { get; set; } = []` | Handlers for callbackQuery (inline) commands. |
| `List<IMessageCommandHandler> MessageHandlers { get; set; } = []` | Handlers for message. |
| `int AntiSpamErrorMinute { get; set; } = 1` | This parameter prevents error spam when the network drops. The default is 1 minute and can be changed. |
| `IPRSerializer? PRSerializer { get; set; }` | Serializer. |
| `IInlineMenuConverter? InlineConverter { get; set; }` | Converter for the inline menu. |
| `HashSet<long> AdminIds { get; set; } = new()` | Predefined administrator identifiers. |
| `HashSet<long> WhiteListIds { get; set; } = new()` | Predefined identifiers of the users on the white list. |
| `WhiteListSettings WhiteListSettings { get; set; } = WhiteListSettings.OnPreUpdate` | White list settings. |
| `Action? InitializeAction { get; set; }` | An additional action to run when the bot is initialized. |
| `HashSet<IPRBackgroundTaskMetadata> BackgroundTaskMetadata { get; set; } = new()` | Background task metadata. |
| `HashSet<IPRBackgroundTask> BackgroundTasks { get; set; } = new()` | Background task metadata. |
| `ILoggerFactory? LoggerFactory { get; set; }` | Logger factory. |

## Methods

| Method | Description |
| --- | --- |
| `readonly WebHookOptions WebHookOptions = new()` | Webhook options. |
| `readonly CommandOptions CommandOptions = new()` | Command options. |

