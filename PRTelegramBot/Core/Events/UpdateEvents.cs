using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Core.Events
{
    public class UpdateEvents
    {
        #region События

        /// <summary>
        /// Событие вызывается после обработки update типа Message и CallbackQuery.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPost;

        /// <summary>
        /// Событие вызывается до обработки update, может быть прекращено выполнение.
        /// </summary>
        public event Func<BotEventArgs, Task<UpdateResult>>? OnPre;

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
        /// Событие, вызываемое при установлении бизнес-соединения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnBusinessConnectionHandle;

        /// <summary>
        /// Событие, вызываемое при редактировании бизнес-сообщения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnEditedBusinessMessageHandle;

        /// <summary>
        /// Событие, вызываемое при удалении бизнес-сообщений.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnDeletedBusinessMessagesHandle;

        /// <summary>
        /// Событие, вызываемое при реакции на сообщение.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMessageReactionHandle;

        /// <summary>
        /// Событие, вызываемое при изменении количества реакций на сообщение.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMessageReactionCountHandle;

        /// <summary>
        /// Событие, вызываемое при увеличении активности в чате.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatBoostHandle;

        /// <summary>
        /// Событие, вызываемое при отмене увеличения активности в чате.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnRemovedChatBoostHandle;

        #endregion

        #region Методы

        internal async Task<UpdateResult> OnPreInvoke(BotEventArgs e)
        {
            if (HasEventOnPreUpdate())
                return await OnPre.Invoke(e);

            return UpdateResult.Continue;
        }

        internal bool HasEventOnPreUpdate()
        {
            return OnPre != null;
        }

        internal void OnPostInvoke(BotEventArgs e)
        {
            OnPost?.Invoke(e);
        }

        internal void OnChannelPostHandler(BotEventArgs e)
        {
            OnChannelPostHandle?.Invoke(e);
        }

        internal void OnChatJoinRequestHandler(BotEventArgs e)
        {
            OnChatJoinRequestHandle?.Invoke(e);
        }

        internal void OnChatMemberHandler(BotEventArgs e)
        {
            OnChatMemberHandle?.Invoke(e);
        }

        internal void OnChosenInlineResultHandler(BotEventArgs e)
        {
            OnChosenInlineResultHandle?.Invoke(e);
        }

        internal void OnEditedChannelPostHandler(BotEventArgs e)
        {
            OnEditedChannelPostHandle?.Invoke(e);
        }

        internal void OnEditedMessageHandler(BotEventArgs e)
        {
            OnEditedMessageHandle?.Invoke(e);
        }

        internal void OnInlineQueryHandler(BotEventArgs e)
        {
            OnInlineQueryHandle?.Invoke(e);
        }

        internal void OnMyChatMemberHandler(BotEventArgs e)
        {
            OnMyChatMemberHandle?.Invoke(e);
        }

        internal void OnPollHandler(BotEventArgs e)
        {
            OnPollHandle?.Invoke(e);
        }

        internal void OnPollAnswerHandler(BotEventArgs e)
        {
            OnPollAnswerHandle?.Invoke(e);
        }

        internal void OnPreCheckoutQueryHandler(BotEventArgs e)
        {
            OnPreCheckoutQueryHandle?.Invoke(e);
        }

        internal void OnShippingQueryHandler(BotEventArgs e)
        {
            OnShippingQueryHandle?.Invoke(e);
        }

        internal void OnUnknownHandler(BotEventArgs e)
        {
            OnUnknownHandle?.Invoke(e);
        }

        internal void OnBusinessConnectionHandler(BotEventArgs e)
        {
            OnBusinessConnectionHandle?.Invoke(e);
        }

        internal void OnEditedBusinessHandler(BotEventArgs e)
        {
            OnEditedBusinessMessageHandle?.Invoke(e);
        }

        internal void OnDeletedBusinessConnectionHandler(BotEventArgs e)
        {
            OnDeletedBusinessMessagesHandle?.Invoke(e);
        }

        internal void OnMessageReactionHandleHandler(BotEventArgs e)
        {
            OnMessageReactionHandle?.Invoke(e);
        }

        internal void OnMessageReactionCountHandleHandler(BotEventArgs e)
        {
            OnMessageReactionCountHandle?.Invoke(e);
        }

        internal void OnChatBoostHandler(BotEventArgs e)
        {
            OnChatBoostHandle?.Invoke(e);
        }

        internal void OnRemovedChatBoostHandler(BotEventArgs e)
        {
            OnRemovedChatBoostHandle?.Invoke(e);
        }

        #endregion
    }
}
