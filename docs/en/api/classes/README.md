---
description: The classes that make up the framework's public surface.
---

# Classes

The classes that make up the framework's public surface.

| | |
| --- | --- |
| [AdminListManager](adminlistmanager.md) | Administrator manager. |
| [BotContext](botcontext.md) | Bot context. |
| [Bot factories](bot-factories.md) | A factory decides what kind of bot `PRBotBuilder.Build()` produces, chiefly how it receives updates. |
| [CommandHandler](commandhandler.md) | Common command handler. |
| [CurrentScope](currentscope.md) | Provides access to the current state of the context and the bot. Read-only. The stack is managed by BotDataScope. |
| [FileInlineConverter](fileinlineconverter.md) | Converter that stores inline menus in files. Stores the temporary data as files named "{bot id}-{user id}-{command id}". The files are saved in the "InlineCallbacks" folder inside the application directory. A different folder name can be given when the instance is created. |
| [InlineCallbackWithConfirmation](inlinecallbackwithconfirmation.md) | Lets an inlineCallBack be executed with a confirmation. |
| [InlineCopyText](inlinecopytext.md) | Creates a button that copies the given text to the clipboard when it is pressed. |
| [InlineDisabled](inlinedisabled.md) | Creates a button that is shown but does nothing when it is pressed. |
| [InlineKeyboardBuilder](inlinekeyboardbuilder.md) | Builder for conveniently constructing an InlineKeyboardMarkup. Lets you set keyboard options and add buttons and rows dynamically. |
| [JsonSerializerWrapper](jsonserializerwrapper.md) | Json data serializer. |
| [KeyboardBuilderBase](keyboardbuilderbase.md) | Base class for building keyboards. |
| [MediaEditor](mediaeditor.md) | Edits media that has already been sent, and its caption. |
| [MediaSender](mediasender.md) | Sends media: photos, photo groups, files and media by URL. |
| [MessageBuilder](messagebuilder.md) | Message builder with support for named tokens and positional arguments. Lets you compose strings in the style of `string.Format(string, object?[])`, but extended with tokens such as {QA}, {Dev} and so on. |
| [MessageCopier](messagecopier.md) | Copies messages between chats. |
| [MessageDeleter](messagedeleter.md) | Deletes messages. |
| [MessageEditor](messageeditor.md) | Edits messages that have already been sent. |
| [MessageNotification](messagenotification.md) | Shows notifications and alerts in response to a callbackQuery. |
| [MessageSender](messagesender.md) | Sends messages to Telegram. |
| [MiddlewareBase](middlewarebase.md) | Base middleware handler. |
| [OptionMessage](optionmessage.md) | Helper class that holds the settings used to send messages in Telegram. |
| [PRBackgroundTaskRunner](prbackgroundtaskrunner.md) | Background task runner. |
| [PREventBus](preventbus.md) | Event bus. |
| [PRSettingsProvider](prsettingsprovider.md) | Provider of the global settings. |
| [ReplyKeyboardBuilder](replykeyboardbuilder.md) | Builder for conveniently constructing a ReplyKeyboardMarkup. Lets you set keyboard options and add buttons and rows dynamically. |
| [TelegramInlineConverter](telegraminlineconverter.md) | Inherits `IInlineMenuConverter`. |
| [ToonSerializerWrapper](toonserializerwrapper.md) | Toon data serializer. |
| [WhiteListManager](whitelistmanager.md) | White list manager. |
| [Callback commands](callback-commands/) | |
| [TelegramOptions](telegramoptions/) | |
