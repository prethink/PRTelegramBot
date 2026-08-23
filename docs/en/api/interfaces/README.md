---
description: The interfaces you implement to replace or extend a part of the framework.
---

# Interfaces

The interfaces you implement to replace or extend a part of the framework.

| | |
| --- | --- |
| [IAdminManager](iadminmanager.md) | Interface of the administrator manager. |
| [IBotCollection](ibotcollection.md) | Interface for working with the bot collection. Lets you look bots up by various criteria and manage the collection. |
| [IBotConfigProvider](ibotconfigprovider.md) | Interface of the bot configuration provider. |
| [IBotContext](ibotcontext.md) | Interface of the bot context. |
| [ICallbackQueryCommandHandler](icallbackquerycommandhandler.md) | Interface of the handler for callbackQuery commands. |
| [IExecuteStep](iexecutestep.md) | Interface for step-by-step command execution. |
| [IInlineContent](iinlinecontent.md) | Common interface for inline buttons. |
| [IInlineMenuConverter](iinlinemenuconverter.md) | Interface of the InlineCallback converter. |
| [IInternalCheck](iinternalcheck.md) | Interface for checking commands before they run. |
| [IMessageCommandHandler](imessagecommandhandler.md) | Interface of the handler for message commands. |
| [IPRBackgroundTask](iprbackgroundtask.md) | Interface of a background task. |
| [IPRBackgroundTaskMetadata](iprbackgroundtaskmetadata.md) | Interface for background task metadata. Holds the information needed to schedule the task and control how it runs without describing its business logic. |
| [IPRBackgroundTaskRunner](iprbackgroundtaskrunner.md) | Interface of the background task runner. Responsible for starting, stopping and managing the lifetime of background tasks. |
| [IPRGlobalSubscriber](iprglobalsubscriber.md) | Interface of a global subscriber. Used by the EventBus system. |
| [IPRSerializer](iprserializer.md) | Interface of the serializer wrapper. |
| [IPRSettings](iprsettings.md) | Global settings. |
| [IPRTaskRunnerSubscriber](iprtaskrunnersubscriber.md) | Subscriber of the background task runner. |
| [IPRUpdateHandler](iprupdatehandler.md) | Telegram update handler. |
| [IRegisterCommands](iregistercommands.md) | Interface of the command registrar. |
| [IRunningBackgroundTaskData](irunningbackgroundtaskdata.md) | Interface for the data of a running task. |
| [ITelegramCache](itelegramcache.md) | Cache for the data. |
| [IUserManager](iusermanager.md) | Interface of the user management manager. |
| [IWhiteListManager](iwhitelistmanager.md) | Interface of the user white list manager. |
