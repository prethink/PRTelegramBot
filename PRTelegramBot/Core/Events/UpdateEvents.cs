using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Core.Events
{
    /// <summary>
    /// События обновлений.
    /// </summary>
    public sealed class UpdateEvents
    {
        #region События

        /// <summary>
        /// Событие вызывается после обработки update типа Message и CallbackQuery.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostUpdate;

        /// <summary>
        /// Событие вызывается до обработки update, может быть прекращено выполнение.
        /// </summary>
        public event Func<BotEventArgs, Task<UpdateResult>>? OnPreUpdate;

        /// <summary>
        /// Событие обновления поста в канале. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChannelPostHandle;

        /// <summary>
        /// Событие обработки запроса на присоединение к чату. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatJoinRequestHandle;

        /// <summary>
        /// Событие обновления участника чата.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatMemberHandle;

        /// <summary>
        /// Событие выбора inline результата. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChosenInlineResultHandle;

        /// <summary>
        /// Событие обновления отредактированного поста в канале. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnEditedChannelPostHandle;

        /// <summary>
        /// Событие обновления отредактированного сообщения. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnEditedMessageHandle;

        /// <summary>
        /// Событие обработки inline запроса. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnInlineQueryHandle;

        /// <summary>
        /// Событие обновления моего участника чата.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMyChatMemberHandle;

        /// <summary>
        /// Событие обновления голосования. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPollHandle;

        /// <summary>
        /// Событие обновления ответа на голосование.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPollAnswerHandle;

        /// <summary>
        /// Событие обработки предзаказа.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPreCheckoutQueryHandle;

        /// <summary>
        /// Событие обработки запроса на доставку.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnShippingQueryHandle;

        /// <summary>
        /// Событие вызываемое при покупке платного медиа.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPurchasedPaidMediaHandle;

        /// <summary>
        /// Событие обновления неизвестного типа.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUnknownHandle;

        /// <summary>
        /// Событие вызываемое при установлении бизнес-соединения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnBusinessConnectionHandle;

        /// <summary>
        /// Событие вызываемое при редактировании бизнес-сообщения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnEditedBusinessMessageHandle;

        /// <summary>
        /// Событие вызываемое при бизнес-сообщение.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnBusinessMessageHandle;

        /// <summary>
        /// Событие вызываемое при удалении бизнес-сообщений.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnDeletedBusinessMessagesHandle;

        /// <summary>
        /// Событие вызываемое при реакции на сообщение.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMessageReactionHandle;

        /// <summary>
        /// Событие вызываемое при изменении количества реакций на сообщение.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMessageReactionCountHandle;

        /// <summary>
        /// Событие вызываемое при увеличении активности в чате.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatBoostHandle;

        /// <summary>
        /// Событие вызываемое при отмене увеличения активности в чате.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnRemovedChatBoostHandle;

        /// <summary>
        /// Событие вызываемое при обработке update callbackquery
        /// </summary>
        public event Func<BotEventArgs, Task>? OnCallbackQueryHandle;

        #endregion

        #region Методы

        /// <summary>
        /// Вызвать событие <see cref="OnPreUpdate"/> и получить результат продолжения/остановки обработки.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        internal async Task<UpdateResult> OnPreInvoke(BotEventArgs e)
        {
            if (HasEventOnPreUpdate())
                return await OnPreUpdate.Invoke(e);

            return UpdateResult.Continue;
        }

        /// <summary>
        /// Проверить наличие подписчиков на <see cref="OnPreUpdate"/>.
        /// </summary>
        internal bool HasEventOnPreUpdate() => OnPreUpdate is not null;

        /// <summary>Вызвать событие <see cref="OnPostUpdate"/>.</summary>
        internal Task OnPostInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPostUpdate, e);

        /// <summary>Вызвать событие <see cref="OnChannelPostHandle"/>.</summary>
        internal Task OnChannelPostHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChannelPostHandle, e);

        /// <summary>Вызвать событие <see cref="OnChatJoinRequestHandle"/>.</summary>
        internal Task OnChatJoinRequestHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatJoinRequestHandle, e);

        /// <summary>Вызвать событие <see cref="OnChatMemberHandle"/>.</summary>
        internal Task OnChatMemberHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatMemberHandle, e);

        /// <summary>Вызвать событие <see cref="OnChosenInlineResultHandle"/>.</summary>
        internal Task OnChosenInlineResultHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChosenInlineResultHandle, e);

        /// <summary>Вызвать событие <see cref="OnEditedChannelPostHandle"/>.</summary>
        internal Task OnEditedChannelPostHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnEditedChannelPostHandle, e);

        /// <summary>Вызвать событие <see cref="OnEditedMessageHandle"/>.</summary>
        internal Task OnEditedMessageHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnEditedMessageHandle, e);

        /// <summary>Вызвать событие <see cref="OnInlineQueryHandle"/>.</summary>
        internal Task OnInlineQueryHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnInlineQueryHandle, e);

        /// <summary>Вызвать событие <see cref="OnMyChatMemberHandle"/>.</summary>
        internal Task OnMyChatMemberHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMyChatMemberHandle, e);

        /// <summary>Вызвать событие <see cref="OnPollHandle"/>.</summary>
        internal Task OnPollHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPollHandle, e);

        /// <summary>Вызвать событие <see cref="OnPollAnswerHandle"/>.</summary>
        internal Task OnPollAnswerHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPollAnswerHandle, e);

        /// <summary>Вызвать событие <see cref="OnPreCheckoutQueryHandle"/>.</summary>
        internal Task OnPreCheckoutQueryHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPreCheckoutQueryHandle, e);

        /// <summary>Вызвать событие <see cref="OnShippingQueryHandle"/>.</summary>
        internal Task OnShippingQueryHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnShippingQueryHandle, e);

        /// <summary>Вызвать событие <see cref="OnPurchasedPaidMediaHandle"/>.</summary>
        internal Task OnPurchasedPaidMediaHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPurchasedPaidMediaHandle, e);

        /// <summary>Вызвать событие <see cref="OnUnknownHandle"/>.</summary>
        internal Task OnUnknownHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnUnknownHandle, e);

        /// <summary>Вызвать событие <see cref="OnBusinessConnectionHandle"/>.</summary>
        internal Task OnBusinessConnectionHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnBusinessConnectionHandle, e);

        /// <summary>Вызвать событие <see cref="OnEditedBusinessMessageHandle"/>.</summary>
        internal Task OnEditedBusinessHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnEditedBusinessMessageHandle, e);

        /// <summary>Вызвать событие <see cref="OnBusinessMessageHandle"/>.</summary>
        internal Task OnBusinessMessageHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnBusinessMessageHandle, e);

        /// <summary>Вызвать событие <see cref="OnDeletedBusinessMessagesHandle"/>.</summary>
        internal Task OnDeletedBusinessConnectionHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnDeletedBusinessMessagesHandle, e);

        /// <summary>Вызвать событие <see cref="OnMessageReactionHandle"/>.</summary>
        internal Task OnMessageReactionHandleHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMessageReactionHandle, e);

        /// <summary>Вызвать событие <see cref="OnMessageReactionCountHandle"/>.</summary>
        internal Task OnMessageReactionCountHandleHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMessageReactionCountHandle, e);

        /// <summary>Вызвать событие <see cref="OnChatBoostHandle"/>.</summary>
        internal Task OnChatBoostHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatBoostHandle, e);

        /// <summary>Вызвать событие <see cref="OnRemovedChatBoostHandle"/>.</summary>
        internal Task OnRemovedChatBoostHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnRemovedChatBoostHandle, e);

        /// <summary>Вызвать событие <see cref="OnCallbackQueryHandle"/>.</summary>
        internal Task OnCallbackQueryHandler(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnCallbackQueryHandle, e);

        #endregion
    }
}
