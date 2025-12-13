using PRTelegramBot.Models.EventsArgs;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Core.Events
{
    /// <summary>
    /// События для обновления типа сообщения.
    /// </summary>
    public sealed class MessageEvents
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
        /// Событие обработки платного медиа.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPaidMediaHandle;

        /// <summary>
        /// Событие обработки возврата платежа.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnRefundedPaymentHandle;

        /// <summary>
        /// Событие получения подарка.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGiftHandle;

        /// <summary>
        /// Событие получения уникального подарка.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUniqueGiftHandle;

        /// <summary>
        /// Событие изменения стоимости платного сообщения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPaidMessagePriceChangedHandle;

        /// <summary>
        /// Событие получения чеклиста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChecklistHandle;

        /// <summary>
        /// Событие выполнения задач чеклиста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChecklistTasksDoneHandle;

        /// <summary>
        /// Событие добавления задач в чеклист.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChecklistTasksAddedHandle;

        /// <summary>
        /// Событие изменения цены прямого сообщения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnDirectMessagePriceChangedHandle;

        /// <summary>
        /// Событие подтверждения предложенного поста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostApprovedHandle;

        /// <summary>
        /// Событие ошибки подтверждения предложенного поста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostApprovalFailedHandle;

        /// <summary>
        /// Событие отклонения предложенного поста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostDeclinedHandle;

        /// <summary>
        /// Событие успешной оплаты предложенного поста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostPaidHandle;

        /// <summary>
        /// Событие возврата оплаты предложенного поста.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuggestedPostRefundedHandle;

        #endregion

        #region Методы

        /// <summary>
        /// Вызвать событие <see cref="OnContactHandle"/>.
        /// </summary>
        internal Task OnContactHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnContactHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnAudioHandle"/>.
        /// </summary>
        internal Task OnAudioHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnAudioHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnLocationHandle"/>.
        /// </summary>
        internal Task OnLocationHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnLocationHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnDiceHandle"/>.
        /// </summary>
        internal Task OnDiceHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnDiceHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnDocumentHandle"/>.
        /// </summary>
        internal Task OnDocumentHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnDocumentHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnWebAppsHandle"/>.
        /// </summary>
        internal Task OnWebAppsHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnWebAppsHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnPollHandle"/>.
        /// </summary>
        internal Task OnPollHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPollHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnGameHandle"/>.
        /// </summary>
        internal Task OnGameHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGameHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnVideoHandle"/>.
        /// </summary>
        internal Task OnVideoHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVideoHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnPhotoHandle"/>.
        /// </summary>
        internal Task OnPhotoHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPhotoHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnStickerHandle"/>.
        /// </summary>
        internal Task OnStickerHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnStickerHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnVoiceHandle"/>.
        /// </summary>
        internal Task OnVoiceHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVoiceHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnVenueHandle"/>.
        /// </summary>
        internal Task OnVenueHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVenueHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnUnknownHandle"/>.
        /// </summary>
        internal Task OnUnknownHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnUnknownHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnVideoNoteHandle"/>.
        /// </summary>
        internal Task OnVideoNoteHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVideoNoteHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnAnimationHandle"/>.
        /// </summary>
        internal Task OnAnimationHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnAnimationHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnChannelCreatedHandle"/>.
        /// </summary>
        internal Task OnChannelCreatedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChannelCreatedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnChatMemberLeftHandle"/>.
        /// </summary>
        internal Task OnChatMemberLeftHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatMemberLeftHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnChatMembersAddedHandle"/>.
        /// </summary>
        internal Task OnChatMembersAddedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatMembersAddedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnChatPhotoChangedHandle"/>.
        /// </summary>
        internal Task OnChatPhotoChangedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatPhotoChangedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnChatPhotoDeletedHandle"/>.
        /// </summary>
        internal Task OnChatPhotoDeletedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatPhotoDeletedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnChatSharedHandle"/>.
        /// </summary>
        internal Task OnChatSharedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatSharedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnChatTitleChangedHandle"/>.
        /// </summary>
        internal Task OnChatTitleChangedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatTitleChangedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnForumTopicClosedHandle"/>.
        /// </summary>
        internal Task OnForumTopicClosedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnForumTopicClosedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnForumTopicCreatedHandle"/>.
        /// </summary>
        internal Task OnForumTopicCreatedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnForumTopicCreatedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnForumTopicEditedHandle"/>.
        /// </summary>
        internal Task OnForumTopicEditedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnForumTopicEditedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnForumTopicReopenedHandle"/>.
        /// </summary>
        internal Task OnForumTopicReopenedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnForumTopicReopenedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnGeneralForumTopicHiddenHandle"/>.
        /// </summary>
        internal Task OnGeneralForumTopicHiddenHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGeneralForumTopicHiddenHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnGeneralForumTopicUnhiddenHandle"/>.
        /// </summary>
        internal Task OnGeneralForumTopicUnhiddenHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGeneralForumTopicUnhiddenHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnGroupCreatedHandle"/>.
        /// </summary>
        internal Task OnGroupCreatedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGroupCreatedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnInvoiceHandle"/>.
        /// </summary>
        internal Task OnInvoiceHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnInvoiceHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnMessageAutoDeleteTimerChangedHandle"/>.
        /// </summary>
        internal Task OnMessageAutoDeleteTimerChangedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMessageAutoDeleteTimerChangedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnMessagePinnedHandle"/>.
        /// </summary>
        internal Task OnMessagePinnedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMessagePinnedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnMigratedFromGroupHandle"/>.
        /// </summary>
        internal Task OnMigratedFromGroupHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMigratedFromGroupHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnMigratedToSupergroupHandle"/>.
        /// </summary>
        internal Task OnMigratedToSupergroupHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnMigratedToSupergroupHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnSuccessfulPaymentHandle"/>.
        /// </summary>
        internal Task OnSuccessfulPaymentHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSuccessfulPaymentHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnSupergroupCreatedHandle"/>.
        /// </summary>
        internal Task OnSupergroupCreatedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSupergroupCreatedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnUserSharedHandle"/>.
        /// </summary>
        internal Task OnUserSharedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnUserSharedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnVideoChatEndedHandle"/>.
        /// </summary>
        internal Task OnVideoChatEndedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVideoChatEndedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnVideoChatParticipantsInvitedHandle"/>.
        /// </summary>
        internal Task OnVideoChatParticipantsInvitedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVideoChatParticipantsInvitedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnVideoChatScheduledHandle"/>.
        /// </summary>
        internal Task OnVideoChatScheduledHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVideoChatScheduledHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnVideoChatStartedHandle"/>.
        /// </summary>
        internal Task OnVideoChatStartedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnVideoChatStartedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnWebsiteConnectedHandle"/>.
        /// </summary>
        internal Task OnWebsiteConnectedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnWebsiteConnectedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnWriteAccessAllowedHandle"/>.
        /// </summary>
        internal Task OnWriteAccessAllowedInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnWriteAccessAllowedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnProximityAlertTriggeredHandle"/>.
        /// </summary>
        internal Task OnProximityAlertTriggeredHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnProximityAlertTriggeredHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnGiveawayHandle"/>.
        /// </summary>
        internal Task OnGiveawayHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGiveawayHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnGiveawayWinnersHandle"/>.
        /// </summary>
        internal Task OnGiveawayWinnersHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGiveawayWinnersHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnGiveawayCompletedHandle"/>.
        /// </summary>
        internal Task OnGiveawayCompletedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGiveawayCompletedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnBoostAddedHandle"/>.
        /// </summary>
        internal Task OnBoostAddedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnBoostAddedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnChatBackgroundSetHandle"/>.
        /// </summary>
        internal Task OnChatBackgroundSetHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChatBackgroundSetHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnTextHandle"/>.
        /// </summary>
        internal Task OnTextHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnTextHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnStoryHandle"/>.
        /// </summary>
        internal Task OnStoryHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnStoryHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnPassportDataHandle"/>.
        /// </summary>
        internal Task OnPassportDataHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPassportDataHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnGiveawayCreatedHandle"/>.
        /// </summary>
        internal Task OnGiveawayCreatedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGiveawayCreatedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnPaidMediaHandle"/>.
        /// </summary>
        internal Task OnPaidMediaHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPaidMediaHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnRefundedPaymentHandle"/>.
        /// </summary>
        internal Task OnRefundedPaymentHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnRefundedPaymentHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnGiftHandle"/>.
        /// </summary>
        internal Task OnGiftHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnGiftHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnUniqueGiftHandle"/>.
        /// </summary>
        internal Task OnUniqueGiftHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnUniqueGiftHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnPaidMessagePriceChangedHandle"/>.
        /// </summary>
        internal Task OnPaidMessagePriceChangedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnPaidMessagePriceChangedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnChecklistHandle"/>.
        /// </summary>
        internal Task OnChecklistHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChecklistHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnChecklistTasksDoneHandle"/>.
        /// </summary>
        internal Task OnChecklistTasksDoneHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChecklistTasksDoneHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnChecklistTasksAddedHandle"/>.
        /// </summary>
        internal Task OnChecklistTasksAddedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnChecklistTasksAddedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnDirectMessagePriceChangedHandle"/>.
        /// </summary>
        internal Task OnDirectMessagePriceChangedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnDirectMessagePriceChangedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnSuggestedPostApprovedHandle"/>.
        /// </summary>
        internal Task OnSuggestedPostApprovedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSuggestedPostApprovedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnSuggestedPostApprovalFailedHandle"/>.
        /// </summary>
        internal Task OnSuggestedPostApprovalFailedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSuggestedPostApprovalFailedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnSuggestedPostDeclinedHandle"/>.
        /// </summary>
        internal Task OnSuggestedPostDeclinedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSuggestedPostDeclinedHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnSuggestedPostPaidHandle"/>.
        /// </summary>
        internal Task OnSuggestedPostPaidHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSuggestedPostPaidHandle, e);

        /// <summary>
        /// Вызвать событие <see cref="OnSuggestedPostRefundedHandle"/>.
        /// </summary>
        internal Task OnSuggestedPostRefundedHandleInvoke(BotEventArgs e) => EventsUtils.InvokeAllAsync(OnSuggestedPostRefundedHandle, e);

        #endregion
    }
}
