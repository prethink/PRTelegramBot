---
description: Helper class that holds the settings used to send messages in Telegram.
---

# OptionMessage

Helper class that holds the settings used to send messages in Telegram.

## Properties

| Property | Description |
| --- | --- |
| `ReplyKeyboardMarkup MenuReplyKeyboardMarkup { get; set; }` | Adds a reply menu. |
| `InlineKeyboardMarkup MenuInlineKeyboardMarkup { get; set; }` | Adds an inline menu. |
| `ParseMode ParseMode { get; set; } = ParseMode.Html` | Parse mode. |
| `bool ClearMenu { get; set; }` | Clears the menu. |
| `string Message { get; set; }` | Message text. |
| `int? MessageId { get; set; }` | Message identifier. |
| `int? MessageThreadId { get; set; }` | Topic / channel identifier. |
| `bool ProtectedContent { get; set; }` | Indicates that the message content is protected. |
| `CancellationToken CancellationToken { get; set; }` | Cancellation token. |
| `IEnumerable<MessageEntity>? Entities { get; set; }` | Message entities. |
| `bool DisableWebPagePreview { get; set; }` | Disables web page previews. |
| `bool DisableNotification { get; set; }` | Disables notifications. |
| `bool DisableContentTypeDetection { get; set; }` | Disables content type detection. |
| `int? ReplyToMessageId { get; set; }` | Identifier of the message to reply to. |
| `int? ReplyToEphemeralMessageId { get; set; }` | Identifier of the incoming ephemeral message to reply to. A reply to an ephemeral message must itself be ephemeral, and Telegram accepts it only within 15 seconds of the original. |
| `bool AllowSendingWithoutReply { get; set; }` | Allows sending without a reply. |
| `string? Caption { get; set; }` | Message caption. |
| `InputFile? Thumbnail { get; set; }` | Message thumbnail. |
| `bool HasSpoiler { get; set; }` | Indicates that the message contains a spoiler. |
| `string? BusinessConnectionId { get; set; }` | Unique identifier of the business connection the message is sent on behalf of. |
| `string? MessageEffectId { get; set; }` | Unique identifier of the message effect to add to the message. Private chats only. |
| `bool AllowPaidBroadcast { get; set; }` | Allows up to 1000 messages per second, ignoring the broadcasting limits, for a fee in Telegram Stars that is withdrawn from the bot's balance. |
| `long? DirectMessagesTopicId { get; set; }` | Identifier of the direct messages topic the message is sent to. Required when the message goes to a direct messages chat. |
| `SuggestedPostParameters? SuggestedPostParameters { get; set; }` | Parameters of the suggested post to send. Direct messages chats only. |
| `bool ShowCaptionAboveMedia { get; set; }` | Shows the caption above the media instead of below it. Applies to photos, copied messages and caption edits. |
| `EphemeralMessageParameters? EphemeralMessageParameters { get; set; }` | Parameters of the ephemeral message to send. An ephemeral message is shown to a single user as an overlay over the chat and is never stored in the chat history. A plain `long` converts to it implicitly. |

## Methods

| Method | Description |
| --- | --- |
| `bool HasMessage => !string.IsNullOrWhiteSpace(Message)` | Checks that the message is present. |

