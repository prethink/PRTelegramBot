---
description: Events raised for each kind of message the bot receives.
---

# Message events

`bot.Events.MessageEvents` raises one event per `MessageType`. Every one has the same shape:

```csharp
public event Func<BotEventArgs, Task>? OnPhotoHandle;
```

Subscribe to the ones you care about:

```csharp
bot.Events.MessageEvents.OnPhotoHandle += async e =>
{
    await MessageSender.Send(e.Context, "Nice photo");
};
```

There are 78 of them, grouped below. If Telegram adds a message type and the framework has not caught up, the update falls through to `OnUnknownHandle` — and the guard tests in the repository are designed to fail loudly when that happens, rather than letting it go unnoticed.

## Content

| Event | Raised for |
| --- | --- |
| `OnTextHandle` | a plain text message |
| `OnPhotoHandle` | a photo |
| `OnVideoHandle` | a video |
| `OnAudioHandle` | an audio file |
| `OnVoiceHandle` | a voice message |
| `OnVideoNoteHandle` | a round video note |
| `OnDocumentHandle` | a document |
| `OnStickerHandle` | a sticker |
| `OnAnimationHandle` | an animation or GIF |
| `OnStoryHandle` | a story |
| `OnLivePhotoHandle` | a live photo |
| `OnPaidMediaHandle` | paid media |
| `OnRichMessageHandle` | a rich message |
| `OnGameHandle` | a game |
| `OnDiceHandle` | a dice roll |

## Things the user shares

| Event | Raised for |
| --- | --- |
| `OnContactHandle` | a contact |
| `OnLocationHandle` | a location |
| `OnVenueHandle` | a venue |
| `OnPollHandle` | a poll |
| `OnPollOptionAddedHandle` | an option added to a poll |
| `OnPollOptionDeletedHandle` | an option removed from a poll |
| `OnWebAppsHandle` | data sent back from a WebApp |
| `OnChatSharedHandle` | a chat the user picked for the bot |
| `OnUserSharedHandle` | a user the user picked for the bot |
| `OnPassportDataHandle` | Telegram Passport data |

## Chat lifecycle

| Event | Raised for |
| --- | --- |
| `OnChatMembersAddedHandle` | someone joined |
| `OnChatMemberLeftHandle` | someone left |
| `OnChatTitleChangedHandle` | the title changed |
| `OnChatPhotoChangedHandle` | the photo changed |
| `OnChatPhotoDeletedHandle` | the photo was removed |
| `OnChatBackgroundSetHandle` | the background was set |
| `OnMessagePinnedHandle` | a message was pinned |
| `OnMessageAutoDeleteTimerChangedHandle` | the auto-delete timer changed |
| `OnGroupCreatedHandle` | a group was created |
| `OnSupergroupCreatedHandle` | a supergroup was created |
| `OnChannelCreatedHandle` | a channel was created |
| `OnMigratedToSupergroupHandle` | the group became a supergroup |
| `OnMigratedFromGroupHandle` | the supergroup came from a group |
| `OnChatOwnerChangedHandle` | ownership changed |
| `OnChatOwnerLeftHandle` | the owner left |
| `OnCommunityChatAddedHandle` | a community chat was added |
| `OnCommunityChatRemovedHandle` | a community chat was removed |
| `OnCommunityChatJoinedHandle` | a user joined the chat from a community |
| `OnManagedBotCreatedHandle` | a managed bot was created |

## Forum topics

| Event | Raised for |
| --- | --- |
| `OnForumTopicCreatedHandle` | a topic was created |
| `OnForumTopicEditedHandle` | a topic was edited |
| `OnForumTopicClosedHandle` | a topic was closed |
| `OnForumTopicReopenedHandle` | a topic was reopened |
| `OnGeneralForumTopicHiddenHandle` | the General topic was hidden |
| `OnGeneralForumTopicUnhiddenHandle` | the General topic was unhidden |

## Video chats

| Event | Raised for |
| --- | --- |
| `OnVideoChatStartedHandle` | a video chat started |
| `OnVideoChatEndedHandle` | it ended |
| `OnVideoChatScheduledHandle` | one was scheduled |
| `OnVideoChatParticipantsInvitedHandle` | participants were invited |

## Payments and gifts

| Event | Raised for |
| --- | --- |
| `OnInvoiceHandle` | an invoice |
| `OnSuccessfulPaymentHandle` | a payment went through |
| `OnRefundedPaymentHandle` | a payment was refunded |
| `OnGiftHandle` | a gift |
| `OnUniqueGiftHandle` | a unique gift |
| `OnGiftUpgradeSentHandle` | a gift upgrade was sent |
| `OnPaidMessagePriceChangedHandle` | the paid-message price changed |
| `OnDirectMessagePriceChangedHandle` | the direct-message price changed |

## Giveaways and boosts

| Event | Raised for |
| --- | --- |
| `OnGiveawayHandle` | a giveaway |
| `OnGiveawayCreatedHandle` | a giveaway was created |
| `OnGiveawayWinnersHandle` | winners were announced |
| `OnGiveawayCompletedHandle` | a giveaway finished |
| `OnBoostAddedHandle` | a boost was added |

## Checklists

| Event | Raised for |
| --- | --- |
| `OnChecklistHandle` | a checklist |
| `OnChecklistTasksDoneHandle` | tasks were completed |
| `OnChecklistTasksAddedHandle` | tasks were added |

## Suggested posts

| Event | Raised for |
| --- | --- |
| `OnSuggestedPostApprovedHandle` | a suggested post was approved |
| `OnSuggestedPostApprovalFailedHandle` | approval failed |
| `OnSuggestedPostDeclinedHandle` | it was declined |
| `OnSuggestedPostPaidHandle` | it was paid for |
| `OnSuggestedPostRefundedHandle` | it was refunded |

## Everything else

| Event | Raised for |
| --- | --- |
| `OnWebsiteConnectedHandle` | a website was connected |
| `OnWriteAccessAllowedHandle` | the user allowed the bot to write |
| `OnProximityAlertTriggeredHandle` | a proximity alert fired |
| `OnUnknownHandle` | a message type the framework does not know |
