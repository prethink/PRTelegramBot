# События для update

```csharp
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Core.Events
{
    /// <summary>
    /// События обновлений.
    /// </summary>
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
        
        /// <summary>
        /// Событие вызываемое при обработке update callbackquery
        /// </summary>
        public event Func<BotEventArgs, Task>? OnCallbackQueryHandle;

        /// <summary>
        /// Событие покупки платного медиа.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPurchasedPaidMediaHandle;

        /// <summary>
        /// Событие остановки пользователем генерации сообщения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnStoppedMessageGenerationHandle;

        /// <summary>
        /// Событие обновления управляемого бота.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnManagedBotHandle;

        /// <summary>
        /// Событие сообщения от гостя.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGuestMessageHandle;

        /// <summary>
        /// Событие подписки.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSubscriptionHandle;

        /// <summary>
        /// Событие сообщения в бизнес-аккаунте.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnBusinessMessageHandle;

        #endregion
    }
}

```
