using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;

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
        internal void OnPostInvoke(BotEventArgs e) => OnPostUpdate?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnChannelPostHandle"/>.</summary>
        internal void OnChannelPostHandler(BotEventArgs e) => OnChannelPostHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnChatJoinRequestHandle"/>.</summary>
        internal void OnChatJoinRequestHandler(BotEventArgs e) => OnChatJoinRequestHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnChatMemberHandle"/>.</summary>
        internal void OnChatMemberHandler(BotEventArgs e) => OnChatMemberHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnChosenInlineResultHandle"/>.</summary>
        internal void OnChosenInlineResultHandler(BotEventArgs e) => OnChosenInlineResultHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnEditedChannelPostHandle"/>.</summary>
        internal void OnEditedChannelPostHandler(BotEventArgs e) => OnEditedChannelPostHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnEditedMessageHandle"/>.</summary>
        internal void OnEditedMessageHandler(BotEventArgs e) => OnEditedMessageHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnInlineQueryHandle"/>.</summary>
        internal void OnInlineQueryHandler(BotEventArgs e) => OnInlineQueryHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnMyChatMemberHandle"/>.</summary>
        internal void OnMyChatMemberHandler(BotEventArgs e) => OnMyChatMemberHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnPollHandle"/>.</summary>
        internal void OnPollHandler(BotEventArgs e) => OnPollHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnPollAnswerHandle"/>.</summary>
        internal void OnPollAnswerHandler(BotEventArgs e) => OnPollAnswerHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnPreCheckoutQueryHandle"/>.</summary>
        internal void OnPreCheckoutQueryHandler(BotEventArgs e) => OnPreCheckoutQueryHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnShippingQueryHandle"/>.</summary>
        internal void OnShippingQueryHandler(BotEventArgs e) => OnShippingQueryHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnUnknownHandle"/>.</summary>
        internal void OnUnknownHandler(BotEventArgs e) => OnUnknownHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnBusinessConnectionHandle"/>.</summary>
        internal void OnBusinessConnectionHandler(BotEventArgs e) => OnBusinessConnectionHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnEditedBusinessMessageHandle"/>.</summary>
        internal void OnEditedBusinessHandler(BotEventArgs e) => OnEditedBusinessMessageHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnDeletedBusinessMessagesHandle"/>.</summary>
        internal void OnDeletedBusinessConnectionHandler(BotEventArgs e) => OnDeletedBusinessMessagesHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnMessageReactionHandle"/>.</summary>
        internal void OnMessageReactionHandleHandler(BotEventArgs e) => OnMessageReactionHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnMessageReactionCountHandle"/>.</summary>
        internal void OnMessageReactionCountHandleHandler(BotEventArgs e) => OnMessageReactionCountHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnChatBoostHandle"/>.</summary>
        internal void OnChatBoostHandler(BotEventArgs e) => OnChatBoostHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnRemovedChatBoostHandle"/>.</summary>
        internal void OnRemovedChatBoostHandler(BotEventArgs e) => OnRemovedChatBoostHandle?.Invoke(e);

        /// <summary>Вызвать событие <see cref="OnCallbackQueryHandle"/>.</summary>
        internal void OnCallbackQueryHandler(BotEventArgs e) => OnCallbackQueryHandle?.Invoke(e);

        #endregion
    }
}
