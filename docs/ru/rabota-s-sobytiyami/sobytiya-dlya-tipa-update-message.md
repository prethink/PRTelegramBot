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

        #endregion
    }
}

```
