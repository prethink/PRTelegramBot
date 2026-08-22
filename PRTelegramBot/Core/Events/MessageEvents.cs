using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Core.Events
{
    /// <summary>
    /// Events for message-type updates.
    /// </summary>
    public sealed class MessageEvents
    {
        #region Events

        /// <summary>
        /// Event raised when contact data is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnContactHandle;

        /// <summary>
        /// Event raised when polls are handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPollHandle;

        /// <summary>
        /// Event raised when a location is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnLocationHandle;

        /// <summary>
        /// Event raised when WebApps are handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWebAppsHandle;

        /// <summary>
        /// Event raised when a message with a document is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnDocumentHandle;

        /// <summary>
        /// Event raised when a message with audio is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnAudioHandle;

        /// <summary>
        /// Event raised when a message with a video is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoHandle;

        /// <summary>
        /// Event raised when a message with a photo is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPhotoHandle;

        /// <summary>
        /// Event raised when a message with a sticker is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnStickerHandle;

        /// <summary>
        /// Event raised when a message with a voice message is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVoiceHandle;

        /// <summary>
        /// Event raised when a message of an unknown type is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUnknownHandle;

        /// <summary>
        /// Event raised when a message with a venue is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVenueHandle;

        /// <summary>
        /// Event raised when a message with a game is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGameHandle;

        /// <summary>
        /// Event raised when a message with a video note is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoNoteHandle;

        /// <summary>
        /// Event raised when a message with a dice is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnDiceHandle;

        /// <summary>
        /// Event raised for an animation in the chat.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnAnimationHandle;

        /// <summary>
        /// Event raised when a channel is created.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChannelCreatedHandle;

        /// <summary>
        /// Event raised when a user leaves the channel.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatMemberLeftHandle;

        /// <summary>
        /// Event raised when a user joins the channel.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatMembersAddedHandle;

        /// <summary>
        /// Event raised when the chat photo changes.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatPhotoChangedHandle;

        /// <summary>
        /// Event raised when the chat photo is deleted.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatPhotoDeletedHandle;

        /// <summary>
        /// Event raised when a chat is shared.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatSharedHandle;

        /// <summary>
        /// Event raised when the chat title changes.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatTitleChangedHandle;

        /// <summary>
        /// Event raised when a forum topic is closed.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnForumTopicClosedHandle;

        /// <summary>
        /// Event raised when a forum topic is created.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnForumTopicCreatedHandle;

        /// <summary>
        /// Event raised when a forum topic is edited.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnForumTopicEditedHandle;

        /// <summary>
        /// Event raised when a forum topic is reopened.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnForumTopicReopenedHandle;

        /// <summary>
        /// Event raised when the general forum topic is hidden.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGeneralForumTopicHiddenHandle;

        /// <summary>
        /// Event raised when the general forum topic is unhidden.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGeneralForumTopicUnhiddenHandle;

        /// <summary>
        /// Event raised when a group is created.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGroupCreatedHandle;

        /// <summary>
        /// Event raised when an invoice is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnInvoiceHandle;

        /// <summary>
        /// Event raised when the message auto-delete timer changes.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMessageAutoDeleteTimerChangedHandle;

        /// <summary>
        /// Event raised when a message is pinned.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMessagePinnedHandle;

        /// <summary>
        /// Event raised on migration from a group
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMigratedFromGroupHandle;

        /// <summary>
        /// Event raised on migration to a supergroup.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMigratedToSupergroupHandle;

        /// <summary>
        /// Event raised when a proximity alert is triggered.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnProximityAlertTriggeredHandle;

        /// <summary>
        /// Event raised on a successful payment.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuccessfulPaymentHandle;

        /// <summary>
        /// Event raised when a supergroup is created.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSupergroupCreatedHandle;

        /// <summary>
        /// Event raised when users are shared.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUserSharedHandle;

        /// <summary>
        /// Event raised when a video chat ends.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoChatEndedHandle;

        /// <summary>
        /// Event raised when participants are invited to a video chat.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoChatParticipantsInvitedHandle;

        /// <summary>
        /// Event raised when a video chat is scheduled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoChatScheduledHandle;

        /// <summary>
        /// Event raised when a video chat starts.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoChatStartedHandle;

        /// <summary>
        /// Event raised when a website is connected.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWebsiteConnectedHandle;

        /// <summary>
        /// Event raised when write access is allowed.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWriteAccessAllowedHandle;

        /// <summary>
        /// Event raised when a giveaway is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGiveawayHandle;

        /// <summary>
        /// Event raised when the giveaway winners are announced.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGiveawayWinnersHandle;

        /// <summary>
        /// Event raised when a giveaway is completed. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGiveawayCompletedHandle;

        /// <summary>
        /// Event raised when a boost is added. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnBoostAddedHandle;

        /// <summary>
        /// Event raised when the chat background is set. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatBackgroundSetHandle;

        /// <summary>
        /// Event raised when a giveaway is created. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGiveawayCreatedHandle;

        /// <summary>
        /// Event raised when a text message is received. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnTextHandle;

        /// <summary>
        /// Event raised when a message in "Story" form is received. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnStoryHandle;

        /// <summary>
        /// Event raised when passport data is received. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPassportDataHandle;

        /// <summary>
        /// Event raised when paid media is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPaidMediaHandle;

        /// <summary>
        /// Event raised when a payment refund is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnRefundedPaymentHandle;

        /// <summary>
        /// Event raised when a gift is received.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGiftHandle;

        /// <summary>
        /// Event raised when a unique gift is received.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUniqueGiftHandle;

        /// <summary>
        /// Event raised when the price of a paid message changes.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPaidMessagePriceChangedHandle;

        /// <summary>
        /// Event raised when a checklist is received.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChecklistHandle;

        /// <summary>
        /// Event raised when checklist tasks are completed.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChecklistTasksDoneHandle;

        /// <summary>
        /// Event raised when tasks are added to a checklist.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChecklistTasksAddedHandle;

        /// <summary>
        /// Event raised when the direct message price changes.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnDirectMessagePriceChangedHandle;

        /// <summary>
        /// Event raised when a suggested post is approved.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostApprovedHandle;

        /// <summary>
        /// Event raised when approving a suggested post fails.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostApprovalFailedHandle;

        /// <summary>
        /// Event raised when a suggested post is declined.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostDeclinedHandle;

        /// <summary>
        /// Event raised when a suggested post is paid for successfully.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostPaidHandle;

        /// <summary>
        /// Event raised when a suggested post payment is refunded.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostRefundedHandle;

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="OnContactHandle"/> event.
        /// </summary>
        internal Task OnContactHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnContactHandle, e);

        /// <summary>
        /// Raises the <see cref="OnAudioHandle"/> event.
        /// </summary>
        internal Task OnAudioHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnAudioHandle, e);

        /// <summary>
        /// Raises the <see cref="OnLocationHandle"/> event.
        /// </summary>
        internal Task OnLocationHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnLocationHandle, e);

        /// <summary>
        /// Raises the <see cref="OnDiceHandle"/> event.
        /// </summary>
        internal Task OnDiceHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnDiceHandle, e);

        /// <summary>
        /// Raises the <see cref="OnDocumentHandle"/> event.
        /// </summary>
        internal Task OnDocumentHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnDocumentHandle, e);

        /// <summary>
        /// Raises the <see cref="OnWebAppsHandle"/> event.
        /// </summary>
        internal Task OnWebAppsHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnWebAppsHandle, e);

        /// <summary>
        /// Raises the <see cref="OnPollHandle"/> event.
        /// </summary>
        internal Task OnPollHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPollHandle, e);

        /// <summary>
        /// Raises the <see cref="OnGameHandle"/> event.
        /// </summary>
        internal Task OnGameHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGameHandle, e);

        /// <summary>
        /// Raises the <see cref="OnVideoHandle"/> event.
        /// </summary>
        internal Task OnVideoHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVideoHandle, e);

        /// <summary>
        /// Raises the <see cref="OnPhotoHandle"/> event.
        /// </summary>
        internal Task OnPhotoHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPhotoHandle, e);

        /// <summary>
        /// Raises the <see cref="OnStickerHandle"/> event.
        /// </summary>
        internal Task OnStickerHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnStickerHandle, e);

        /// <summary>
        /// Raises the <see cref="OnVoiceHandle"/> event.
        /// </summary>
        internal Task OnVoiceHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVoiceHandle, e);

        /// <summary>
        /// Raises the <see cref="OnVenueHandle"/> event.
        /// </summary>
        internal Task OnVenueHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVenueHandle, e);

        /// <summary>
        /// Raises the <see cref="OnUnknownHandle"/> event.
        /// </summary>
        internal Task OnUnknownHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnUnknownHandle, e);

        /// <summary>
        /// Raises the <see cref="OnVideoNoteHandle"/> event.
        /// </summary>
        internal Task OnVideoNoteHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVideoNoteHandle, e);

        /// <summary>
        /// Raises the <see cref="OnAnimationHandle"/> event.
        /// </summary>
        internal Task OnAnimationHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnAnimationHandle, e);

        /// <summary>
        /// Raises the <see cref="OnChannelCreatedHandle"/> event.
        /// </summary>
        internal Task OnChannelCreatedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChannelCreatedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnChatMemberLeftHandle"/> event.
        /// </summary>
        internal Task OnChatMemberLeftHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatMemberLeftHandle, e);

        /// <summary>
        /// Raises the <see cref="OnChatMembersAddedHandle"/> event.
        /// </summary>
        internal Task OnChatMembersAddedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatMembersAddedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnChatPhotoChangedHandle"/> event.
        /// </summary>
        internal Task OnChatPhotoChangedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatPhotoChangedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnChatPhotoDeletedHandle"/> event.
        /// </summary>
        internal Task OnChatPhotoDeletedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatPhotoDeletedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnChatSharedHandle"/> event.
        /// </summary>
        internal Task OnChatSharedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatSharedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnChatTitleChangedHandle"/> event.
        /// </summary>
        internal Task OnChatTitleChangedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatTitleChangedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnForumTopicClosedHandle"/> event.
        /// </summary>
        internal Task OnForumTopicClosedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnForumTopicClosedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnForumTopicCreatedHandle"/> event.
        /// </summary>
        internal Task OnForumTopicCreatedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnForumTopicCreatedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnForumTopicEditedHandle"/> event.
        /// </summary>
        internal Task OnForumTopicEditedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnForumTopicEditedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnForumTopicReopenedHandle"/> event.
        /// </summary>
        internal Task OnForumTopicReopenedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnForumTopicReopenedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnGeneralForumTopicHiddenHandle"/> event.
        /// </summary>
        internal Task OnGeneralForumTopicHiddenHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGeneralForumTopicHiddenHandle, e);

        /// <summary>
        /// Raises the <see cref="OnGeneralForumTopicUnhiddenHandle"/> event.
        /// </summary>
        internal Task OnGeneralForumTopicUnhiddenHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGeneralForumTopicUnhiddenHandle, e);

        /// <summary>
        /// Raises the <see cref="OnGroupCreatedHandle"/> event.
        /// </summary>
        internal Task OnGroupCreatedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGroupCreatedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnInvoiceHandle"/> event.
        /// </summary>
        internal Task OnInvoiceHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnInvoiceHandle, e);

        /// <summary>
        /// Raises the <see cref="OnMessageAutoDeleteTimerChangedHandle"/> event.
        /// </summary>
        internal Task OnMessageAutoDeleteTimerChangedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMessageAutoDeleteTimerChangedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnMessagePinnedHandle"/> event.
        /// </summary>
        internal Task OnMessagePinnedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMessagePinnedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnMigratedFromGroupHandle"/> event.
        /// </summary>
        internal Task OnMigratedFromGroupHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMigratedFromGroupHandle, e);

        /// <summary>
        /// Raises the <see cref="OnMigratedToSupergroupHandle"/> event.
        /// </summary>
        internal Task OnMigratedToSupergroupHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMigratedToSupergroupHandle, e);

        /// <summary>
        /// Raises the <see cref="OnSuccessfulPaymentHandle"/> event.
        /// </summary>
        internal Task OnSuccessfulPaymentHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSuccessfulPaymentHandle, e);

        /// <summary>
        /// Raises the <see cref="OnSupergroupCreatedHandle"/> event.
        /// </summary>
        internal Task OnSupergroupCreatedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSupergroupCreatedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnUserSharedHandle"/> event.
        /// </summary>
        internal Task OnUserSharedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnUserSharedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnVideoChatEndedHandle"/> event.
        /// </summary>
        internal Task OnVideoChatEndedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVideoChatEndedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnVideoChatParticipantsInvitedHandle"/> event.
        /// </summary>
        internal Task OnVideoChatParticipantsInvitedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVideoChatParticipantsInvitedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnVideoChatScheduledHandle"/> event.
        /// </summary>
        internal Task OnVideoChatScheduledHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVideoChatScheduledHandle, e);

        /// <summary>
        /// Raises the <see cref="OnVideoChatStartedHandle"/> event.
        /// </summary>
        internal Task OnVideoChatStartedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVideoChatStartedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnWebsiteConnectedHandle"/> event.
        /// </summary>
        internal Task OnWebsiteConnectedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnWebsiteConnectedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnWriteAccessAllowedHandle"/> event.
        /// </summary>
        internal Task OnWriteAccessAllowedInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnWriteAccessAllowedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnProximityAlertTriggeredHandle"/> event.
        /// </summary>
        internal Task OnProximityAlertTriggeredHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnProximityAlertTriggeredHandle, e);

        /// <summary>
        /// Raises the <see cref="OnGiveawayHandle"/> event.
        /// </summary>
        internal Task OnGiveawayHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGiveawayHandle, e);

        /// <summary>
        /// Raises the <see cref="OnGiveawayWinnersHandle"/> event.
        /// </summary>
        internal Task OnGiveawayWinnersHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGiveawayWinnersHandle, e);

        /// <summary>
        /// Raises the <see cref="OnGiveawayCompletedHandle"/> event.
        /// </summary>
        internal Task OnGiveawayCompletedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGiveawayCompletedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnBoostAddedHandle"/> event.
        /// </summary>
        internal Task OnBoostAddedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnBoostAddedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnChatBackgroundSetHandle"/> event.
        /// </summary>
        internal Task OnChatBackgroundSetHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatBackgroundSetHandle, e);

        /// <summary>
        /// Raises the <see cref="OnTextHandle"/> event.
        /// </summary>
        internal Task OnTextHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnTextHandle, e);

        /// <summary>
        /// Raises the <see cref="OnStoryHandle"/> event.
        /// </summary>
        internal Task OnStoryHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnStoryHandle, e);

        /// <summary>
        /// Raises the <see cref="OnPassportDataHandle"/> event.
        /// </summary>
        internal Task OnPassportDataHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPassportDataHandle, e);

        /// <summary>
        /// Raises the <see cref="OnGiveawayCreatedHandle"/> event.
        /// </summary>
        internal Task OnGiveawayCreatedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGiveawayCreatedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnPaidMediaHandle"/> event.
        /// </summary>
        internal Task OnPaidMediaHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPaidMediaHandle, e);

        /// <summary>
        /// Raises the <see cref="OnRefundedPaymentHandle"/> event.
        /// </summary>
        internal Task OnRefundedPaymentHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnRefundedPaymentHandle, e);

        /// <summary>
        /// Raises the <see cref="OnGiftHandle"/> event.
        /// </summary>
        internal Task OnGiftHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGiftHandle, e);

        /// <summary>
        /// Raises the <see cref="OnUniqueGiftHandle"/> event.
        /// </summary>
        internal Task OnUniqueGiftHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnUniqueGiftHandle, e);

        /// <summary>
        /// Raises the <see cref="OnPaidMessagePriceChangedHandle"/> event.
        /// </summary>
        internal Task OnPaidMessagePriceChangedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPaidMessagePriceChangedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnChecklistHandle"/> event.
        /// </summary>
        internal Task OnChecklistHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChecklistHandle, e);

        /// <summary>
        /// Raises the <see cref="OnChecklistTasksDoneHandle"/> event.
        /// </summary>
        internal Task OnChecklistTasksDoneHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChecklistTasksDoneHandle, e);

        /// <summary>
        /// Raises the <see cref="OnChecklistTasksAddedHandle"/> event.
        /// </summary>
        internal Task OnChecklistTasksAddedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChecklistTasksAddedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnDirectMessagePriceChangedHandle"/> event.
        /// </summary>
        internal Task OnDirectMessagePriceChangedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnDirectMessagePriceChangedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnSuggestedPostApprovedHandle"/> event.
        /// </summary>
        internal Task OnSuggestedPostApprovedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSuggestedPostApprovedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnSuggestedPostApprovalFailedHandle"/> event.
        /// </summary>
        internal Task OnSuggestedPostApprovalFailedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSuggestedPostApprovalFailedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnSuggestedPostDeclinedHandle"/> event.
        /// </summary>
        internal Task OnSuggestedPostDeclinedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSuggestedPostDeclinedHandle, e);

        /// <summary>
        /// Raises the <see cref="OnSuggestedPostPaidHandle"/> event.
        /// </summary>
        internal Task OnSuggestedPostPaidHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSuggestedPostPaidHandle, e);

        /// <summary>
        /// Raises the <see cref="OnSuggestedPostRefundedHandle"/> event.
        /// </summary>
        internal Task OnSuggestedPostRefundedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSuggestedPostRefundedHandle, e);

        #endregion
    }
}
