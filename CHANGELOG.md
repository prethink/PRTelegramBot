# Changelog

**English** | [Русский](CHANGELOG.ru.md)


## August 23, 2026 - v1.0.0

### 🔄 Breaking changes

- Removed the unfinished `PRTelegramBot.Workflow` namespace: `IWorkflowNode`, `IWorkflowState`, `IWorkflowCondition`, `IWorkflowManulTask`, `TelegramStateManager` and the other types in it. They were empty stubs with no members and were used nowhere.
- Removed the `IInlineStorage` interface. It was never implemented or used.
- Misspelled parameter names have been corrected. This only affects callers that pass these arguments by name:
  - `StepTelegram.RegisterNextStep` and the `StepTelegram` constructors: `expiriedTime` -> `expiredTime`
  - `PRBotBuilder.SetInlineSerializer`: `serializator` -> `serializer`
  - `BackgroundTaskExtension.GetMetadata`: `metadates` -> `existingMetadata`
- `OptionMessage.thumbnail` renamed to `OptionMessage.Thumbnail`. It was the only public member that did not follow PascalCase.
- Optional parameters that accept `null` are now declared nullable (`OptionMessage? option = null` and similar). This is metadata only — existing code keeps compiling; projects with nullable checks enabled simply get an accurate picture.
- `UpdateExtension.TryGetBot` now declares its `out` parameter as `PRBotBase?`, because it is `null` when the bot is not found.
- `GetChatId`, `GetMessageId` and `GetUserId` now throw `InvalidOperationException` with a clear message instead of a `NullReferenceException` when the update carries no chat, message or sender. `TryGetChatId` still returns `false` in those cases.
- Namespaces that had been split in two are unified. Each of these lived in a single folder while its files declared two different namespaces, so building a menu could require two `using` directives for no reason:
  - `PRTelegramBot.InlineButtons` -> `PRTelegramBot.Models.InlineButtons`
  - `PRTelegramBot.Core.Factory` -> `PRTelegramBot.Core.Factories`
  - `PRTelegramBot.Models.TCommands` -> `PRTelegramBot.Models.CallbackCommands` (the folder was renamed to match)
- Removed the obsolete `PRTelegramBot.Helpers.Message` facade together with its 21 methods. It was marked obsolete in v0.9.0 and only forwarded to `MessageSender`, `MessageEditor`, `MessageDeleter`, `MessageCopier`, `MediaSender` and `MediaEditor`; use those directly.
- Removed `PRTelegramBot.Models.InlineButton`. It was used nowhere and its `GetContent` threw `NotImplementedException`.
- `PRLoggerEvents<T>` and `PRLoggerEventsFactory` are now `internal`. They remain the fallback that keeps event-based logging working when no `ILoggerFactory` is supplied, but they were never meant to be built by hand.
- `InlineCallbackWithConfirmation.DataCollection` is no longer public. Pending confirmations are looked up by the framework itself; the public field only allowed callers to corrupt that state.
- Two attributes that express the same idea were named differently and put the words in the opposite order to the Telegram.Bot types they filter on. They now read the same way and match `ChatType` and `MessageType`:
  - `RequiredTypeChatAttribute` -> `RequireChatTypeAttribute`, its `TypesChat` property -> `ChatTypes`
  - `RequireTypeMessageAttribute` -> `RequireMessageTypeAttribute`, its `TypeMessages` property -> `MessageTypes`
- `AccessAttribute`, `RequireChatTypeAttribute`, `RequireMessageTypeAttribute` and `WhiteListAnonymousAttribute` now declare `[AttributeUsage(AttributeTargets.Method)]`. Without it they could be placed on a class, a field or a parameter, where the framework never looks at them and nothing reports the mistake.
- `AccessAttribute` and `WhiteListAnonymousAttribute` are now `sealed`, like every other attribute in the library.

### 🚀 New functionality

- `OptionMessage` gained the send parameters that Telegram.Bot already supported but the framework did not pass on: `BusinessConnectionId`, `MessageEffectId`, `AllowPaidBroadcast`, `DirectMessagesTopicId` and `SuggestedPostParameters`. They are forwarded by `MessageSender`, `MediaSender` (photos, photo groups, files and media by URL) and `MessageCopier`, each as far as the corresponding Bot API method accepts them.
- Added the message events that had been missed while Telegram.Bot was being updated: `OnGiftUpgradeSentHandle`, `OnChatOwnerChangedHandle`, `OnChatOwnerLeftHandle`, `OnManagedBotCreatedHandle`, `OnPollOptionAddedHandle`, `OnPollOptionDeletedHandle`, `OnLivePhotoHandle`, `OnRichMessageHandle`, `OnCommunityChatAddedHandle` and `OnCommunityChatRemovedHandle`. The message dispatcher now covers every `MessageType` except `Text`, which has always been routed through the command pipeline instead.
- Added the update events that had been missed for the same reason: `OnManagedBotHandle`, `OnGuestMessageHandle` and `OnSubscriptionHandle`.
- Added `OptionMessage.ShowCaptionAboveMedia`, forwarded by `MediaSender.SendPhoto`, `MessageCopier` and `MediaEditor.EditCaption`.
- Added `InlineCopyText`, a button that copies the given text to the clipboard. Telegram.Bot has supported it for a while; the framework had no wrapper for it.
- Added `ReplyKeyboardBuilder.AddRequestManagedBot`, a button that asks the user to create and share a bot managed by this one.
- Added guard tests that fail when Telegram.Bot is updated and the framework has not caught up: a missing `MessageType` or `UpdateType` handler, an event that is declared but never raised, a new button kind with no wrapper, or a new request parameter nobody has reviewed. All of the gaps listed above went unnoticed precisely because nothing was watching for them.

### 🧩 Common

- Telegram.Bot updated to 22.10.2.1
- The code comments and the example texts have been translated into English.
- Added English versions of README and CHANGELOG; the Russian versions live alongside them as `README.ru.md` and `CHANGELOG.ru.md`.
- The README was restructured for readers who arrive without knowing the project: it now opens with what the framework adds on top of Telegram.Bot, and has a Getting started section with prerequisites, installation and a hello world that compiles as written. The feature list is grouped by topic instead of being one flat list of forty bullets, and Versioning, Contributing and License sections were added, along with a badge for the supported Bot API version.
- The package description is now `A .NET framework for building Telegram bots on top of Telegram.Bot: attribute-based command routing, menus, middleware, DI and background tasks`. The previous one never named the platform, which is the first thing anyone looking at a search result needs to know. This is the text NuGet shows in search results, and the same wording opens the About section of both READMEs.
- Every public member is now documented: the XML documentation no longer has gaps, and the malformed doc comments have been repaired. IntelliSense is complete.
- `PageExtension.GetPaged` is no longer declared `async` — it did not await anything. The signature callers see is unchanged.
- The setters of `RunningBackgroundTask` and `SlashHandlerAttribute.SplitChar` changed from `protected` to `private`. Both classes are `sealed`, so these setters were never reachable from outside.
- `InlineUtils.GetInlineButton` no longer switches over concrete button types; it calls `GetInlineButton()` on the button itself. The built-in buttons convert exactly as before, but a type the switch did not list — one added later, or one defined outside the library — is now converted instead of failing with `NotImplementedException`, and a subclass that overrides the conversion is honoured. The method now also throws `ArgumentNullException` when it is given null. This affects everything that builds inline keyboards: `InlineKeyboardBuilder`, `MenuGenerator` and the calendar.

### 🐞 Bugs

- Fixed a recursion problem when checking for an administrator through the context.
- Renamed `AutoEditMessageСycle` to `AutoEditMessageCycle`: the old name contained a Cyrillic "С".
- `UpdateExtension.GetUserId` returned the wrong identifier for a callbackQuery: it read `CallbackQuery.Message.From`, which is the bot that sent the message, instead of `CallbackQuery.From`, the user who pressed the button. Anything keyed by user — cache, steps, access checks — was receiving the bot id for every user.
- `UpdateExtension.GetUserId` threw a `NullReferenceException` for channel posts, whose `From` is always empty.
- Pending confirmations created by `InlineCallbackWithConfirmation` were kept forever: every button ever built was added to a static dictionary that nothing ever removed from, so a long-running bot leaked them along with the callbacks they held. They are now dropped once the confirmation is answered, and any that nobody answers are discarded an hour after they were created.
- That same dictionary was a plain `Dictionary` written to from concurrently handled updates, which can corrupt it. It is now a `ConcurrentDictionary`.
- `MessageUtils.SplitIntoChunks` looped forever when it was given a chunk size of zero or less: the offset never advanced, so the call hung and the result list grew until the process ran out of memory. It now throws `ArgumentOutOfRangeException` for a non-positive chunk size, and `ArgumentNullException` for null text. The framework itself always passes `PRConstants.MAX_MESSAGE_LENGTH`, so only direct callers of this public method were exposed.
- The `OnPaidMessagePriceChangedHandle` event was declared but never wired into the message dispatcher, so it never fired. It is connected now.
- Exceptions are no longer swallowed silently. They are now written to the log in `PREventBus` (a faulty subscriber no longer disappears without a trace), in `MessageAwaiter` when the waiting message cannot be deleted, and in `TryGetConfigValue` when reading the configuration fails.
- Event handlers are still invoked without an await, so that a slow subscriber cannot hold up other updates — but a failure inside one is now logged instead of being lost with the unobserved task.
- The `FastBotTemplate` console template held the process open with an empty `while(true) { }`, which spins a CPU core at 100% for as long as the bot runs. It now awaits `Task.Delay(Timeout.Infinite)`. In the same template `bot.StartAsync()` was fire-and-forget (`_ = ...`), so a failure while the bot was starting vanished without a trace; it is awaited now.
- The confirmation example in `ConsoleExample` wrapped a button whose header was `ExampleThree`, but the handler for that header reads `EntityTCommand<string>` while the button carried `EntityTCommand<long>`. Pressing "Yes" therefore threw a `JsonException` inside the converter, which logged it and returned null, so the handler quietly did nothing. The example now uses `ExampleTwo`, whose handler reads the type the button actually carries.
- `ValidateTelegramBotAttribute` in the ASP.NET webhook example accepted every request. A stray semicolon after the `if` turned the secret-token comparison into an empty statement, so `return true` ran unconditionally on the first bot in the list — any request carrying the header passed the filter, whatever the header held. `BotController` compares the token again before handling an update, so the shipped example was not itself exploitable, but the filter is what people copy into their own projects, and on its own it protected nothing. The same code was printed on the webhook page of the documentation.
- `FileInlineConverter(string path)` ignored the folder name it was given and always used a folder literally called `path`. It now uses the name passed in, and rejects an empty one. Anyone who used this constructor was writing to the wrong folder; after upgrading, the inline payloads move to the folder they asked for, so any confirmation still pending at the moment of the upgrade will not be found.
- `InlineConfirmation.ActionWithConfirmation` dereferenced the parsed command without checking it. When the callback data could not be parsed — for example when a handler reads a different `EntityTCommand<T>` than the button carries — the converter returned null and the handler threw a `NullReferenceException` into the log instead of telling the user anything. It now falls through to the same "something went wrong" message as an unknown confirmation.
- Removed a duplicated byte order mark from 32 source files. It was introduced by the tooling used for the comment translation; the files compiled, but each one began with a stray zero-width character.

## June 20, 2026 - v0.9.10

### 🧩 Common

- Microsoft.Extensions packages updated to version 9.0.17:
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.Binder
- Microsoft.Extensions.Configuration.Json
- Microsoft.Extensions.Hosting.Abstractions
- Microsoft.Extensions.Logging.Abstractions
- Telegram.Bot updated to 22.10.1

## April 26, 2026 - v0.9.9

### 🧩 Common
- Telegram.Bot: updated to 22.9.6.1

### 📝 Background Tasks
- Fixed a problem with starting background tasks.

## March 2, 2026 - v0.9.8

### 🧩 Common
- Telegram.Bot: updated to 22.9.5

## February 10, 2026 - v0.9.7

### 🧩 Common
- Telegram.Bot: updated to 22.9.0

## February 2, 2026 - v0.9.6

### 🧩 Common
- Fixed bugs in FileInlineConverter. The inline button builder is now assembled correctly.

## January 3, 2026 - v0.9.5

### 🧩 Common
- A SetServiceProvider method has been added to PRBotBase.
- If an ILogger is registered in DI instead of an ILoggerFactory, the bot will try to use it for logging.

#### Logger source priority:
1. The logger factory set on the builder
2. ILogger from DI
3. ILoggerFactory from DI
4. The built-in logger factory (fallback)

## January 3, 2026 - v0.9.4

### 🧩 Common
- Telegram.Bot: updated to 22.8.1

## January 2, 2026 - v0.9.3

### 🧩 Common
- Telegram.Bot: updated to 22.8.0

## December 28, 2025 - v0.9.2

### 🧩 Common
- The `Microsoft.Extensions.Configuration` library updated to version 9.0.11
- The `Microsoft.Extensions.Configuration.Json` library updated to version 9.0.11
- The `Microsoft.Extensions.Configuration.Json` library updated to version 9.0.11
- The `Microsoft.Extensions.Hosting.Abstractions` library updated to version 9.0.11
- Added the `Microsoft.Extensions.Logging.Abstractions` library, version 9.0.11

### 🧾 Logger
- Added support for `ILogger` and `ILoggerFactory`.
- Through `PRBotBuilder` you can supply your own `ILoggerFactory`, which will be used to create the `ILogger`.
- Resolving an `ILoggerFactory` from the DI container is supported as well.
- If neither is provided, the built-in (default) logger factory is used, which keeps backward compatibility with the old mechanism.
#### Logger source priority:
1. The logger factory set on the builder
2. ILoggerFactory from DI
3. The built-in logger factory (fallback)


## December 23, 2025 - v0.9.1

### 🔄 Breaking changes
- `MiddlewareBase` has become an abstract class.
- A new `ExecutionOrder` property has been added to `MiddlewareBase`. It determines the execution order.

### Common
- An `ALL_BOTS_ID = -1` constant has been added to PRConstants. This identifier is used when a command should apply to every bot. It is not limited to commands.
- Minor refactoring
- Added a background task module. Background tasks support DI
- Added DI support to `MiddlewareBase`.
- Added event bus support.


## December 13, 2025 - v0.9.0

### 🔄 Breaking changes
- `PRBotBuilder` moved from `PRTelegramBot.Core` to `PRTelegramBot.Builders`
- The `Message.NotifyFromCallBack` method moved to `IBotContext`

### 🧱 Builders
- Added a reply button builder — `ReplyKeyboardBuilder`
- Added an inline button builder — `InlineKeyboardBuilder`

### ♻️ Refactoring
- Refactored the `Message` class
  The class was split into separate components:
  - `MessageSender`
  - `MessageEditor`
  - `MessageDeleter`
  - `MessageNotification`
  - `MessageCopier`
  - `MediaEditor`
  - `MediaSender`

### 📋 Inline menus / conversion
- Added the `IInlineMenuConverter` interface for converting inline menu data.
- The bot builder now accepts your own menu conversion implementation: `.SetInlineMenuConverter(IInlineMenuConverter inlineMenuConverter)`
- Added the `FileInlineConverter` class, an `IInlineMenuConverter` implementation that converts inline menu data using the file system to work around the `callback_data` size limit.

### 🧱 Builders

### 🧭 Execution context
- Added BotContextScope, which gives access to the current bot instance and context while an update is being handled.
Now you can obtain them anywhere in your code, as long as that code was invoked by a Telegram update:
`var currentContext = CurrentScope.Context;
var currentBot = CurrentScope.Bot;
var services = CurrentScope.Services (IServiceProvider);`

### 📡 Events
- Added events for `updateType`:
  - `PurchasedPaidMedia`
  - `BusinessMessage`
- Added events for `messageType`:
  - `PaidMedia`
  - `RefundedPayment`
  - `Gift`
  - `UniqueGift`
  - `PaidMessagePriceChanged`
  - `Checklist`
  - `ChecklistTasksDone`
  - `ChecklistTasksAdded`
  - `DirectMessagePriceChanged`
  - `SuggestedPostApproved`
  - `SuggestedPostApprovalFailed`
  - `SuggestedPostDeclined`
  - `SuggestedPostPaid`
  - `SuggestedPostRefunded`

### 🏗 Bot initialization
- The builder now lets you supply a bot initialization Action: `SetInitializeAction(Action action)`. This Action is invoked when the bot starts, after all managers have been initialized.

### 👮 Managers and interfaces
- `AdminManager` now implements the `IAdminManager` interface.
- An Initialize() method has been added to the IUserManager, IWhiteListManager and IAdminManager interfaces.

### 💉 DI integration
- The IInlineMenuConverter, IPRSerializer, IAdminManager and IWhiteListManager interfaces are meant to work with DI.
If you use a DI container, register them there and the bots will pick them up themselves in AdminManager and WhiteListManager.
The bot resolves these interfaces in the following order of priority:
1. Set on the builder via SetAdminManager, SetWhiteListManager, SetInlineMenuConverter, SetPRSerializer
2. From DI
3. Local / default classes.

## December 8, 2025 - V0.8.6
- Telegram.Bot: updated to 22.7.6

## December 4, 2025 - V0.8.5
- The SlashHandlerAttribute attribute now lets you specify the separator character for arguments. Example: [SlashHandler('_', "/get")]
- While a slash command runs, you can now get the list of arguments from the context.
var args = context.GetSlashArgs();
var args = context.GetSlashArgs<int>();
var args = context.GetSlashArgs<bool>();
- /start with a deeplink can now be used in your own slash methods, rather than only through events as before.

## November 29, 2025 - V0.8.4
- The builder now lets you choose which serializer to use for inline buttons (SetInlineSerializer): JsonSerializerWrapper or ToonSerializerWrapper. ToonSerializerWrapper uses fewer bytes in callback_data.
- Serialization options can be set when the serializer instance is created.
- Added the PRSettingsProvider class holding the project's global settings.
- Added the ToonNet library.
- Added Microsoft.Extensions.Hosting.Abstractions so the bot can be used as an IHostedService.

## November 9, 2025 - V0.8.3
- Telegram.Bot: updated to 22.7.5

## October 31, 2025 - V0.8.2
- Telegram.Bot: updated to 22.7.4

## October 27, 2025 - V0.8.1
- Telegram.Bot: updated to 22.7.3
- Refactored the GetFullNameFromChat method

## September 15, 2025 - V0.8
- Code refactoring. Thanks to @Harlok13 for the help as well.
- Added IBotContext, which holds: every bot instance in the system, the current bot instance, Update, BotClient, CurrentUpdateType and CancelationToken.
- The signature of methods and commands changed from ...ITelegramBotClient botClient, Update update... to IBotContext context
- Added extension methods for IBotContext mirroring those for update: Cache, Steps and others.
- CacheExtension.
-- Added a GetOrCreate method.
-- Fixed the CreateCacheData method. It now always creates a new cache when called.
- Fixed the example bots.
- Added a new GetUserId() extension method for getting the user identifier
- The documentation will be updated later, after the merge into master.


### Migration:
#### MiddlewareBase:
- InvokeOnPreUpdateAsync(ITelegramBotClient context.BotClient, context.Update update, Func<Task> next) -> InvokeOnPreUpdateAsync(IBotContext context, Func<Task> next)
- InvokeOnPostUpdateAsync(ITelegramBotClient context.BotClient, context.Update update, Func<Task> next) -> InvokeOnPostUpdateAsync(IBotContext context)

#### IExecuteStep and its implementations:
ExecuteStep(ITelegramBotClient context.BotClient, context.Update update) -> ExecuteStep(IBotContext context)

#### PRBotBuilder
- SetIpAddresWebHook(string ipAddress) -> SetIpAddressWebHook(string ipAddress)
- AddRecevingOptions(ReceiverOptions recevierOptions) -> AddReceivingOptions(ReceiverOptions receiverOptions)

#### PRBotWebHook
- GetWebHookInfo(CancellationToken cancellationToken = default) -> GetWebHookInfoAsync(CancellationToken cancellationToken = default)

#### PRBotBase
- Start -> StartAsync
- Stop -> StopAsync

Methods in your code have to be changed from the (ITelegramBotClient context.BotClient, context.Update update) signature to (IBotContext context), and other places where the old arguments were passed or read need fixing too.
Examples:
update -> context.Update
botClient -> context.BotClient


## September 4, 2025 - V0.7.12
- Fixed the callback_data size check. Author: @Harlok13
- Telegram.Bot: updated to 22.7.2

## August 31, 2025 - V0.7.11
- More work on the DI Scope for nextStep.

## August 29, 2025 - V0.7.10
- Fixed the DI Scope.
- Fixed a problem running steps when a step is time-limited.
- Added a CanExecute method to IExecuteStep.

## August 27, 2025 - V0.7.9.6
- Added a RemoveCacheData method to CacheExtension for removing a cache key via update.

## August 20, 2025 - V0.7.9.5
- Telegram.Bot: updated to 22.6.2

## July 13, 2025 - V0.7.9.4
- Telegram.Bot: updated to 22.6.0

## May 5, 2025 - V0.7.9.3
- Added a OneTimeKeyboard parameter to the reply menu generator
- Fixes to the privilege flag checks

## February 18, 2025 - V0.7.9
- Telegram.Bot: updated to 22.4.3
- Refactored the methods in Messages so they match telegram.bot

## February 13, 2025 - V0.7.8
- Telegram.Bot: updated to 22.4.0

## January 4, 2025 - V0.7.7
- Telegram.Bot: updated to 22.3.0

## December 25, 2024 - V0.7.6
- update: Added an inline handler for class instances. It lets you bind a command type to a specific class type that implements the ICallbackQueryCommandHandler interface. Examples added for the console application and for ASP.NET DI.
- refactoring: RegisterCommand moved from Options to CommandOptions.
- refactoring: The SplitIntoChunks method moved from the Message class to MessageUtils.

## December 5, 2024 - V0.7.5
- Telegram.Bot: updated to 22.2.0

## November 19, 2024 - V0.7.4
- Telegram.Bot: updated to 22.1.0

## November 10, 2024 - V0.7.3
- Telegram.Bot: updated to 22.0.2

## August 1, 2024 - V0.7.2
- Telegram.Bot: updated to 21.8.0

## July 27, 2024 - V0.7.1
- fix: Added a setting that limits error log spam when the network drops. TelegramOptions.AntiSpamErrorMinute, 1 minute by default.

## July 21, 2024 - V0.7
- update: The project now positions itself as a framework.
- Telegram.Bot: updated to 21.7.1
- feature: Added the ability to hook into the handling of message and callbackQuery updates. This lets you implement and register your own handlers for text and inline commands.
- feature: Command attributes can now carry several bot identifiers. Previously only one specific bot, or all of them, could be specified.
- refactoring: WebhookTelegramOptions renamed to WebHookOptions; it is now part of the TelegramOptions class rather than a descendant of it.
- refactoring: Added a new CommandEvents event type. All command-related events were moved there.
- refactoring: Simplified working with the calendar.

## July 18, 2024 - V0.6.6
- Telegram.Bot: updated to 21.7

## July 14, 2024 - V0.6.5
- Telegram.Bot: updated to 21.6.2

## July 7, 2024 - V0.6.4
- feature: InlineCallback now implements the IDisposable interface. If the data carries ActionWithLastMessage delete, the message is deleted automatically.
- feature: Added an extension class for the Message type with the AutoDeleteMessage, AutoEdit and AutoEditCycle methods.
- fix: XML comments were not shown in the NuGet package

## July 6, 2024 - V0.6.3
- feature: Added new inline buttons: InlinePay, InlineCallbackGame, InlineSwitchInlineQuery, InlineSwitchInlineQueryChosenChat, InlineSwitchInlineQueryCurrentChat, InlineLoginUrl.
- feature: Added the InlineCallbackWithConfirmation wrapper for InlineCallBack buttons. It shows a confirmation message before the action runs.
- feature: Added an ActionWithLastMessage property to TCommandBase and its descendants; it specifies what to do with the last message — nothing, delete, or edit.
- feature: Added a new OnErrorCommand event, raised when an error occurs while a command runs
- feature: Added a GetChatIdClass method to UpdateExtension that returns the ChatId as a class
- fix: The missingCommand event was raised when an error occurred during handling.

## July 1, 2024 - V0.6.2
- update: The telegram.bot core updated from 21.2.0 to 21.4.0.
- feature: Added a CommandHandler argument to the IInternalCheck interface
- feature: Added new events for message-type updates: OnPreReplyCommandHandle, OnPostReplyCommandHandle, OnPreDynamicReplyCommandHandle, OnPostDynamicReplyCommandHandle,
    OnPreSlashCommandHandle, OnPostSlashCommandHandle, OnPreInlineCommandHandle, OnPostInlineCommandHandle, OnPreNextStepCommandHandle, OnPostNextStepCommandHandle
- feature: Added the IsUserChatId and TryGetChatId methods to UpdateExtension
- feature: Added the MessageAwaiter class, which posts a placeholder message before the data is processed and deletes it automatically afterwards
- feature: Removed the awaits for reply, slash, inline and dynamicreply commands so they do not hold up the handling of other updates
- feature: Added polling mode. There are now three modes: classic (telegram.bot functionality), polling and webhook.

## June 30, 2024 - V0.6.1
- update: The telegram.bot core updated from 19 to 21.2.0.
- update: Because of the update, newtonsoft json was removed
- update: Added new message events: Giveaway, GiveawayWinners, GiveawayCompleted, BoostAdded, ChatBackgroundSet
- feature: Added the IUserManager interface and the AdminManager and WhiteListManager classes. The Admins and WhiteListUsers properties were removed from TelegramOptions.
- feature: Added a middleware system that runs before and after an update is handled
- feature: Added the WhiteListAnonymous attribute; when it is present on a handler method, the method runs for every user, even those not on the white list
- feature: Added settings that control how the white list works in WhiteListManager
- feature: The ability to add your own checks before specific reply, dynamicreply, nextstep, inline and slash commands run.
- refactoring: Bots are now created only through PRBotBuilder.
- refactoring: TEvents events related to messages moved to the MessageEvents class
- refactoring: TEvents events related to updates moved to the UpdateEvents class
- refactoring: In the builder, the long parameter of AddAdmin and AddWhiteListUser was replaced with params long[]

## June 22, 2024 - V0.6
- update: The Microsoft.Extensions.Configuration.Binder library updated to version 8
- update: The Microsoft.Extensions.Configuration.Json library updated to version 8
- test: Unit tests
- feature: Added the ability to supply your own update handler when creating a bot
- feature: Added the ability to supply your own command registrar when creating a bot
- feature: Added webhook support
- feature: Added the BotHandler attribute, which marks a class as working with dependency injection
- feature: AccessUtil for working with access flags and masks
- feature: Added the PRBotBuilder class, which lets you create a bot through a fluent builder
- feature: Added the BotCollection class, which holds every bot instance
- feature: Command attributes can now take the bot identifier -1; such methods are available from every bot
- feature: Added properties from Telegram.Bot.Net to OptionMessage
- feature: Added the ability to specify comparison options in commands
- feature: Added events for all the other update types
- feature: Added the ability to ignore regular (priority) commands during step-by-step command execution
- feature: Added properties to the IExecuteStep interface for ignoring the basic commands and for marking the last step
- feature: You can set your own client when creating a bot. This lets you use your own local servers instead of Telegram's
- refactoring: StepService renamed to StepExtension
- refactoring: Descriptions renamed to DescriptionExtension
- refactoring: Cache renamed to CacheExtension
- refactoring: PageHelper renamed to PageExtension
- refactoring: THeader renamed to PRTelegramBotCommand
- refactoring: Added a configPath parameter to TelegramOptions
- refactoring: TelegramConfig replaced with TelegramOptions
- refactoring: Refactored ServiceProviderExtension
- refactoring: Removed the TextConfig class
- refactoring: Removed the BaseEventTelegram enum
- refactoring: Replaced Enum with string in logging
- refactoring: Reworked the events, added separate classes for the arguments.
- refactoring: All events moved to the Events property: bot.Events
- refactoring: Refactored Router, split it into several classes
- refactoring: Refactoring
- fix: Fixed problems with the /start command
- fix: The cache and the steps are now tied to a specific bot and user
- fix: Added all events for messages

## January 2, 2024 - V0.5.5
- feature: Added the ability to supply your own enum in common logs
- feature: Added InlineCommandNotFoundException
- feature: Added GroupUtils, which holds the IsGroupMember, IsGroupAdmin and IsGroupCreator methods
- feature: StepCommand is replaced by an abstraction, the IExecuteStep interface
- refactoring: Refactored Router
- refactoring: DI types are now created with a Transient lifetime rather than Singleton
- refactoring: The Step class renamed to StepService
- refactoring: Step.RegisterNextStep renamed to RegisterStepHandler
- fix: IsSlashCommand now checks the first character for /

## December 24, 2023 - V0.5.4
- refactoring: ReflectionUtils moved to the PRTelegramBot.Utils namespace
- refactoring: ReflectionHelper renamed to ReflectionUtils
- refactoring: Calendar moved to the PRTelegramBot.Utils namespace
- refactoring: MenuGenerator moved to the PRTelegramBot.Utils namespace
- refactoring: Generator moved to the PRTelegramBot.Utils namespace
- feature: botClient can now raise plain and error log calls.
- feature: The ability to add and remove reply and slash commands through a PRBot instance
- feature: botClient.GetBotAdminIds() returns the bot's administrators
- feature: Added dynamic registration of inline commands
- fix: The SendPhoto method did not send messages when optionmessage was not empty
- fix: Enum writes the correct values from int

## December 18, 2023 - V0.5.3
- delete: Removed the TelegramBotHandler attribute
- fix: Fixed the lookup and creation of classes for Telegram bot handlers

## December 17, 2023 - V0.5.2
- fix: AddBotHandlers returns IServiceProvaider

## December 17, 2023 - V0.5.1
- fix: Changed the project url to https://prtelegrambot.gitbook.io/prtelegrambot/obrabotka-komand/obrabotka-inline-komand

## December 17, 2023 - V0.5
- feature: Added dynamic registration of reply and slash commands
- feature: Added dependency injection support and an ASP.NET example
