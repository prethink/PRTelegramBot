# События для типа update message

```csharp
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Core.Events
{
    /// <summary>
    /// События для обновления типа сообщения.
    /// </summary>
    public class MessageEvents
    {
        #region События

        /// <summary>
        /// Событие Обработки контактных данных.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnContactHandle;

        /// <summary>
        /// Событие обработки голосований.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPollHandle;

        /// <summary>
        /// Событие обработки локации.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnLocationHandle;

        /// <summary>
        /// Событие обработки WebApps.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWebAppsHandle;

        /// <summary>
        /// Событие обработки сообщением с документом.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnDocumentHandle;

        /// <summary>
        /// Событие обработки сообщением с аудио.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnAudioHandle;

        /// <summary>
        /// Событие обработки сообщением с видео.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoHandle;

        /// <summary>
        /// Событие обработки сообщением с фото.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPhotoHandle;

        /// <summary>
        /// Событие обработки сообщением с стикером.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnStickerHandle;

        /// <summary>
        /// Событие обработки сообщением с голосовым сообщением.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVoiceHandle;

        /// <summary>
        /// Событие обработки сообщением с неизвестный типом сообщения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUnknownHandle;

        /// <summary>
        /// Событие обработки сообщением с местом.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVenueHandle;

        /// <summary>
        /// Событие обработки сообщением с игрой.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGameHandle;

        /// <summary>
        /// Событие обработки сообщением с видеозаметкой.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoNoteHandle;

        /// <summary>
        /// Событие обработки сообщением с игральной кости.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnDiceHandle;

        /// <summary>
        /// Событие анимации в чате.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnAnimationHandle;

        /// <summary>
        /// Событие создание канала.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChannelCreatedHandle;

        /// <summary>
        /// Событие выхода пользователя из канала.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatMemberLeftHandle;

        /// <summary>
        /// Событие входа пользователя в канала.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatMembersAddedHandle;

        /// <summary>
        /// Событие изменения фото чата.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatPhotoChangedHandle;

        /// <summary>
        /// Событие удаления фото чата.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatPhotoDeletedHandle;

        /// <summary>
        /// Событие общего доступа к чату.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatSharedHandle;

        /// <summary>
        /// Событие изменения названия чата.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatTitleChangedHandle;

        /// <summary>
        /// Событие закрытия темы форума.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnForumTopicClosedHandle;

        /// <summary>
        /// Событие создания темы форума.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnForumTopicCreatedHandle;

        /// <summary>
        /// Событие редактирования темы форума.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnForumTopicEditedHandle;

        /// <summary>
        /// Событие повторного открытия темы форума.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnForumTopicReopenedHandle;

        /// <summary>
        /// Событие скрытия общей темы форума.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGeneralForumTopicHiddenHandle;

        /// <summary>
        /// Событие отмены скрытия общей темы форума.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGeneralForumTopicUnhiddenHandle;

        /// <summary>
        /// Событие создания группы.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGroupCreatedHandle;

        /// <summary>
        /// Событие обработки счета.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnInvoiceHandle;

        /// <summary>
        /// Событие изменения таймера автоудаления сообщений.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMessageAutoDeleteTimerChangedHandle;

        /// <summary>
        /// Событие закрепления сообщения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMessagePinnedHandle;

        /// <summary>
        /// Событие миграции из группы
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMigratedFromGroupHandle;

        /// <summary>
        /// Событие миграции в супергруппу.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMigratedToSupergroupHandle;

        /// <summary>
        /// Событие срабатывания оповещения о приближении.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnProximityAlertTriggeredHandle;

        /// <summary>
        /// Событие успешного платежа.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuccessfulPaymentHandle;

        /// <summary>
        /// Событие создания супергруппы.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSupergroupCreatedHandle;

        /// <summary>
        /// Событие общего доступа пользователя.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUserSharedHandle;

        /// <summary>
        /// Событие завершения видеочата.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoChatEndedHandle;

        /// <summary>
        /// Событие приглашения участников в видеочат.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoChatParticipantsInvitedHandle;

        /// <summary>
        /// Событие планирования видеочата.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoChatScheduledHandle;

        /// <summary>
        /// Событие начала видеочата.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoChatStartedHandle;

        /// <summary>
        /// Событие подключения веб-сайта.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWebsiteConnectedHandle;

        /// <summary>
        /// Событие разрешения записи.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWriteAccessAllowedHandle;

        /// <summary>
        /// Событие, которое происходит при обработке розыгрыша.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGiveawayHandle;

        /// <summary>
        /// Событие, которое происходит при объявлении победителей розыгрыша.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGiveawayWinnersHandle;

        /// <summary>
        /// Событие, которое происходит при завершении розыгрыша. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGiveawayCompletedHandle;

        /// <summary>
        /// Событие, которое происходит при добавлении буста. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnBoostAddedHandle;

        /// <summary>
        /// Событие, которое происходит при установке фона чата. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatBackgroundSetHandle;

        /// <summary>
        /// Событие, которое происходит при создании розыгрыша. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGiveawayCreatedHandle;

        /// <summary>
        /// Событие, которое происходит при получении текстового сообщения. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnTextHandle;

        /// <summary>
        /// Событие, которое происходит при получении сообщения в формате "Story". 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnStoryHandle;

        /// <summary>
        /// Событие, которое происходит при получении данных паспорта. 
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPassportDataHandle;

        /// <summary>
        /// Событие, которое происходит при получении платного медиа.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPaidMediaHandle;

        /// <summary>
        /// Событие, которое происходит при возврате платежа.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnRefundedPaymentHandle;

        /// <summary>
        /// Событие, которое происходит при получении подарка.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGiftHandle;

        /// <summary>
        /// Событие, которое происходит при получении уникального подарка.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUniqueGiftHandle;

        /// <summary>
        /// Событие, которое происходит при отправке улучшения подарка.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGiftUpgradeSentHandle;

        /// <summary>
        /// Событие, которое происходит при изменении цены платного сообщения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPaidMessagePriceChangedHandle;

        /// <summary>
        /// Событие, которое происходит при изменении цены прямого сообщения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnDirectMessagePriceChangedHandle;

        /// <summary>
        /// Событие, которое происходит при получении чеклиста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChecklistHandle;

        /// <summary>
        /// Событие, которое происходит при выполнении задач чеклиста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChecklistTasksDoneHandle;

        /// <summary>
        /// Событие, которое происходит при добавлении задач в чеклист.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChecklistTasksAddedHandle;

        /// <summary>
        /// Событие, которое происходит при одобрении предложенного поста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostApprovedHandle;

        /// <summary>
        /// Событие, которое происходит при неудачном одобрении предложенного поста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostApprovalFailedHandle;

        /// <summary>
        /// Событие, которое происходит при отклонении предложенного поста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostDeclinedHandle;

        /// <summary>
        /// Событие, которое происходит при оплате предложенного поста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostPaidHandle;

        /// <summary>
        /// Событие, которое происходит при возврате оплаты предложенного поста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostRefundedHandle;

        /// <summary>
        /// Событие, которое происходит при смене владельца чата.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatOwnerChangedHandle;

        /// <summary>
        /// Событие, которое происходит, когда владелец покинул чат.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatOwnerLeftHandle;

        /// <summary>
        /// Событие, которое происходит при добавлении чата сообщества.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnCommunityChatAddedHandle;

        /// <summary>
        /// Событие, которое происходит при удалении чата сообщества.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnCommunityChatRemovedHandle;

        /// <summary>
        /// Событие, которое происходит, когда пользователь заходит в чат из сообщества.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnCommunityChatJoinedHandle;

        /// <summary>
        /// Событие, которое происходит при создании управляемого бота.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnManagedBotCreatedHandle;

        /// <summary>
        /// Событие, которое происходит при добавлении варианта в опрос.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPollOptionAddedHandle;

        /// <summary>
        /// Событие, которое происходит при удалении варианта из опроса.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPollOptionDeletedHandle;

        /// <summary>
        /// Событие, которое происходит при получении live-фото.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnLivePhotoHandle;

        /// <summary>
        /// Событие, которое происходит при получении rich-сообщения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnRichMessageHandle;

        #endregion
    }
}

```
