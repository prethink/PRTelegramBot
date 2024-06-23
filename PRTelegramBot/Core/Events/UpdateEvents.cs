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
        public event Func<BotEventArgs, Task>? OnPostUpdate;

        /// <summary>
        /// Событие вызывается до обработки update, может быть прекращено выполнение.
        /// </summary>
        public event Func<BotEventArgs, Task<UpdateResult>>? OnPreUpdate;

        /// <summary>
        /// Событие обновления поста в канале. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdateChannelPostHandle;

        /// <summary>
        /// Событие обработки запроса на присоединение к чату. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdateChatJoinRequestHandle;

        /// <summary>
        /// Событие обновления участника чата.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdateChatMemberHandle;

        /// <summary>
        /// Событие выбора inline результата. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdateChosenInlineResultHandle;

        /// <summary>
        /// Событие обновления отредактированного поста в канале. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdateEditedChannelPostHandle;

        /// <summary>
        /// Событие обновления отредактированного сообщения. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdateEditedMessageHandle;

        /// <summary>
        /// Событие обработки inline запроса. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdateInlineQueryHandle;

        /// <summary>
        /// Событие обновления моего участника чата.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdateMyChatMemberHandle;

        /// <summary>
        /// Событие обновления голосования. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdatePollHandle;

        /// <summary>
        /// Событие обновления ответа на голосование.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdatePollAnswerHandle;

        /// <summary>
        /// Событие обработки предзаказа.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdatePreCheckoutQueryHandle;

        /// <summary>
        /// Событие обработки запроса на доставку.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdateShippingQuerHandle;

        /// <summary>
        /// Событие обновления неизвестного типа.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUpdateUnknownHandle;

        #endregion

        #region Методы

        internal async Task<UpdateResult> OnPreUpdateInvoke(BotEventArgs e)
        {
            if (HasEventOnPreUpdate())
                return await OnPreUpdate.Invoke(e);

            return UpdateResult.Continue;
        }

        internal bool HasEventOnPreUpdate()
        {
            return OnPreUpdate != null;
        }

        internal void OnPostUpdateInvoke(BotEventArgs e)
        {
            OnPostUpdate?.Invoke(e);
        }

        internal void OnUpdateChannelPostHandler(BotEventArgs e)
        {
            OnUpdateChannelPostHandle?.Invoke(e);
        }

        internal void OnUpdateChatJoinRequestHandler(BotEventArgs e)
        {
            OnUpdateChatJoinRequestHandle?.Invoke(e);
        }

        internal void OnUpdateChatMemberHandler(BotEventArgs e)
        {
            OnUpdateChatMemberHandle?.Invoke(e);
        }

        internal void OnUpdateChosenInlineResultHandler(BotEventArgs e)
        {
            OnUpdateChosenInlineResultHandle?.Invoke(e);
        }

        internal void OnUpdateEditedChannelPostHandler(BotEventArgs e)
        {
            OnUpdateEditedChannelPostHandle?.Invoke(e);
        }

        internal void OnUpdateEditedMessageHandler(BotEventArgs e)
        {
            OnUpdateEditedMessageHandle?.Invoke(e);
        }

        internal void OnUpdateInlineQueryHandler(BotEventArgs e)
        {
            OnUpdateInlineQueryHandle?.Invoke(e);
        }

        internal void OnUpdateMyChatMemberHandler(BotEventArgs e)
        {
            OnUpdateMyChatMemberHandle?.Invoke(e);
        }

        internal void OnUpdatePollHandler(BotEventArgs e)
        {
            OnUpdatePollHandle?.Invoke(e);
        }

        internal void OnUpdatePollAnswerHandler(BotEventArgs e)
        {
            OnUpdatePollAnswerHandle?.Invoke(e);
        }

        internal void OnUpdatePreCheckoutQueryHandler(BotEventArgs e)
        {
            OnUpdatePreCheckoutQueryHandle?.Invoke(e);
        }

        internal void OnUpdateShippingQuerHandler(BotEventArgs e)
        {
            OnUpdateShippingQuerHandle?.Invoke(e);
        }

        internal void OnUpdateUnknownHandler(BotEventArgs e)
        {
            OnUpdateUnknownHandle?.Invoke(e);
        }

        #endregion
    }
}
