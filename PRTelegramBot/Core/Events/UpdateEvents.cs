using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Core.Events
{
    /// <summary>
    /// Update events.
    /// </summary>
    public sealed class UpdateEvents
    {
        #region Events

        /// <summary>
        /// Raised after a Message or CallbackQuery update has been handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostUpdate;

        /// <summary>
        /// Raised before the update is handled; processing can be stopped from it.
        /// </summary>
        public event Func<BotEventArgs, Task<UpdateResult>>? OnPreUpdate;

        /// <summary>
        /// Event raised for a channel post update. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChannelPostHandle;

        /// <summary>
        /// Event raised when a chat join request is handled. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatJoinRequestHandle;

        /// <summary>
        /// Event raised when a chat member is updated.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatMemberHandle;

        /// <summary>
        /// Event raised when an inline result is chosen. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChosenInlineResultHandle;

        /// <summary>
        /// Event raised for an edited channel post update. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnEditedChannelPostHandle;

        /// <summary>
        /// Event raised for an edited message update. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnEditedMessageHandle;

        /// <summary>
        /// Event raised when an inline query is handled. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnInlineQueryHandle;

        /// <summary>
        /// Event raised when my chat member is updated.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMyChatMemberHandle;

        /// <summary>
        /// Event raised when a poll is updated. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPollHandle;

        /// <summary>
        /// Event raised when a poll answer is updated.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPollAnswerHandle;

        /// <summary>
        /// Event raised when a pre-checkout query is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPreCheckoutQueryHandle;

        /// <summary>
        /// Event raised when a shipping query is handled.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnShippingQueryHandle;

        /// <summary>
        /// Event raised when paid media is purchased.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPurchasedPaidMediaHandle;

        /// <summary>
        /// Event raised when a user asks the bot to stop generating a message.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnStoppedMessageGenerationHandle;

        /// <summary>
        /// Event raised for an update of an unknown type.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUnknownHandle;

        /// <summary>
        /// Event raised for an update about a managed bot.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnManagedBotHandle;

        /// <summary>
        /// Event raised for a guest message update.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGuestMessageHandle;

        /// <summary>
        /// Event raised for a subscription update.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSubscriptionHandle;

        /// <summary>
        /// Event raised when a business connection is established.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnBusinessConnectionHandle;

        /// <summary>
        /// Event raised when a business message is edited.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnEditedBusinessMessageHandle;

        /// <summary>
        /// Event raised for a business message.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnBusinessMessageHandle;

        /// <summary>
        /// Event raised when business messages are deleted.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnDeletedBusinessMessagesHandle;

        /// <summary>
        /// Event raised on a message reaction.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMessageReactionHandle;

        /// <summary>
        /// Event raised when the reaction count of a message changes.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMessageReactionCountHandle;

        /// <summary>
        /// Event raised when a chat boost is added.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatBoostHandle;

        /// <summary>
        /// Event raised when a chat boost is removed.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnRemovedChatBoostHandle;

        /// <summary>
        /// Event raised when a callbackQuery update is handled
        /// </summary>
        public event Func<BotEventArgs, Task>? OnCallbackQueryHandle;

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="OnPreUpdate"/> event and returns whether processing should continue or stop.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        internal async Task<UpdateResult> OnPreInvoke(BotEventArgs e)
        {
            if (HasEventOnPreUpdate())
                return await OnPreUpdate.Invoke(e);

            return UpdateResult.Continue;
        }

        /// <summary>
        /// Checks whether <see cref="OnPreUpdate"/> has any subscribers.
        /// </summary>
        internal bool HasEventOnPreUpdate() => OnPreUpdate is not null;

        /// <summary>Raises the <see cref="OnPostUpdate"/> event.</summary>
        internal Task OnPostInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPostUpdate, e);

        /// <summary>Raises the <see cref="OnChannelPostHandle"/> event.</summary>
        internal Task OnChannelPostHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChannelPostHandle, e);

        /// <summary>Raises the <see cref="OnChatJoinRequestHandle"/> event.</summary>
        internal Task OnChatJoinRequestHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatJoinRequestHandle, e);

        /// <summary>Raises the <see cref="OnChatMemberHandle"/> event.</summary>
        internal Task OnChatMemberHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatMemberHandle, e);

        /// <summary>Raises the <see cref="OnChosenInlineResultHandle"/> event.</summary>
        internal Task OnChosenInlineResultHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChosenInlineResultHandle, e);

        /// <summary>Raises the <see cref="OnEditedChannelPostHandle"/> event.</summary>
        internal Task OnEditedChannelPostHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnEditedChannelPostHandle, e);

        /// <summary>Raises the <see cref="OnEditedMessageHandle"/> event.</summary>
        internal Task OnEditedMessageHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnEditedMessageHandle, e);

        /// <summary>Raises the <see cref="OnInlineQueryHandle"/> event.</summary>
        internal Task OnInlineQueryHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnInlineQueryHandle, e);

        /// <summary>Raises the <see cref="OnMyChatMemberHandle"/> event.</summary>
        internal Task OnMyChatMemberHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMyChatMemberHandle, e);

        /// <summary>Raises the <see cref="OnPollHandle"/> event.</summary>
        internal Task OnPollHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPollHandle, e);

        /// <summary>Raises the <see cref="OnPollAnswerHandle"/> event.</summary>
        internal Task OnPollAnswerHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPollAnswerHandle, e);

        /// <summary>Raises the <see cref="OnPreCheckoutQueryHandle"/> event.</summary>
        internal Task OnPreCheckoutQueryHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPreCheckoutQueryHandle, e);

        /// <summary>Raises the <see cref="OnShippingQueryHandle"/> event.</summary>
        internal Task OnShippingQueryHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnShippingQueryHandle, e);

        /// <summary>Raises the <see cref="OnPurchasedPaidMediaHandle"/> event.</summary>
        internal Task OnPurchasedPaidMediaHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPurchasedPaidMediaHandle, e);

        /// <summary>Raises the <see cref="OnStoppedMessageGenerationHandle"/> event.</summary>
        internal Task OnStoppedMessageGenerationHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnStoppedMessageGenerationHandle, e);

        /// <summary>Raises the <see cref="OnUnknownHandle"/> event.</summary>
        internal Task OnUnknownHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnUnknownHandle, e);

        /// <summary>Raises the <see cref="OnManagedBotHandle"/> event.</summary>
        internal Task OnManagedBotHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnManagedBotHandle, e);

        /// <summary>Raises the <see cref="OnGuestMessageHandle"/> event.</summary>
        internal Task OnGuestMessageHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGuestMessageHandle, e);

        /// <summary>Raises the <see cref="OnSubscriptionHandle"/> event.</summary>
        internal Task OnSubscriptionHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSubscriptionHandle, e);

        /// <summary>Raises the <see cref="OnBusinessConnectionHandle"/> event.</summary>
        internal Task OnBusinessConnectionHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnBusinessConnectionHandle, e);

        /// <summary>Raises the <see cref="OnEditedBusinessMessageHandle"/> event.</summary>
        internal Task OnEditedBusinessHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnEditedBusinessMessageHandle, e);

        /// <summary>Raises the <see cref="OnBusinessMessageHandle"/> event.</summary>
        internal Task OnBusinessMessageHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnBusinessMessageHandle, e);

        /// <summary>Raises the <see cref="OnDeletedBusinessMessagesHandle"/> event.</summary>
        internal Task OnDeletedBusinessConnectionHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnDeletedBusinessMessagesHandle, e);

        /// <summary>Raises the <see cref="OnMessageReactionHandle"/> event.</summary>
        internal Task OnMessageReactionHandleHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMessageReactionHandle, e);

        /// <summary>Raises the <see cref="OnMessageReactionCountHandle"/> event.</summary>
        internal Task OnMessageReactionCountHandleHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMessageReactionCountHandle, e);

        /// <summary>Raises the <see cref="OnChatBoostHandle"/> event.</summary>
        internal Task OnChatBoostHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatBoostHandle, e);

        /// <summary>Raises the <see cref="OnRemovedChatBoostHandle"/> event.</summary>
        internal Task OnRemovedChatBoostHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnRemovedChatBoostHandle, e);

        /// <summary>Raises the <see cref="OnCallbackQueryHandle"/> event.</summary>
        internal Task OnCallbackQueryHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnCallbackQueryHandle, e);

        #endregion
    }
}
