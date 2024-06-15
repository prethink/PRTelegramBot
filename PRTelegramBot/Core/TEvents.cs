using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// События для бота.
    /// </summary>
    public sealed class TEvents
    {
        #region Поля и свойства

        /// <summary>
        /// Бот для событий.
        /// </summary>
        public PRBotBase Bot { get; private set; }

        #endregion

        #region События

        /// <summary>
        /// Событие когда пользователь написал start с аргументом.
        /// </summary>
        public event Func<StartEventArgs, Task>? OnUserStartWithArgs;

        /// <summary>
        /// Событие когда нужно проверить привилегии перед выполнением команды.
        /// </summary>
        public event Func<PrivilegeEventArgs, Task>? OnCheckPrivilege;

        /// <summary>
        /// Событие когда указан не верный тип сообщения.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWrongTypeMessage;

        /// <summary>
        /// Событие когда указан не верный тип чат.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWrongTypeChat;

        /// <summary>
        /// Событие когда не найдена команда.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMissingCommand;

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
        /// Событие когда отказано в доступе.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnAccessDenied;

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
        /// Событие вызывается до обработки update, может быть прекращено выполнение.
        /// </summary>
        public event Func<BotEventArgs, Task<UpdateResult>>? OnPreUpdate;

        /// <summary>
        /// Событие вызывается после обработки update типа Message и CallbackQuery.
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostMessageUpdate;

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
        /// Событие ошибки.
        /// </summary>
        public event Func<ErrorLogEventArgs, Task>? OnErrorLog;

        /// <summary>
        /// Событие общих логов.
        /// </summary>
        public event Func<CommonLogEventArgs, Task>? OnCommonLog;

        #endregion

        #region Методы

        internal void OnUserStartWithArgsInvoke(StartEventArgs e)
        {
            OnUserStartWithArgs?.Invoke(e);
        }

        internal void OnMissingCommandInvoke(BotEventArgs e)
        {
            OnMissingCommand?.Invoke(e);
        }

        internal void OnAccessDeniedInvoke(BotEventArgs e)
        {
            OnAccessDenied?.Invoke(e);
        }

        internal void OnCheckPrivilegeInvoke(PrivilegeEventArgs e)
        {
            OnCheckPrivilege?.Invoke(e);
        }

        internal void OnWrongTypeMessageInvoke(BotEventArgs e)
        {
            OnWrongTypeMessage?.Invoke(e);
        }
        internal void OnWrongTypeChatInvoke(BotEventArgs e)
        {
            OnWrongTypeChat?.Invoke(e);
        }

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

        internal void OnPostMessageUpdateInvoke(BotEventArgs e)
        {
            OnPostMessageUpdate?.Invoke(e);
        }

        internal void OnContactHandleInvoke(BotEventArgs e)
        {
            OnContactHandle?.Invoke(e);    
        }

        internal void OnAudioHandleInvoke(BotEventArgs e)
        {
            OnAudioHandle?.Invoke(e);
        }

        internal void OnLocationHandleInvoke(BotEventArgs e)
        {
            OnLocationHandle?.Invoke(e);
        }

        internal void OnDiceHandleInvoke(BotEventArgs e)
        {
            OnDiceHandle?.Invoke(e);
        }

        internal void OnDocumentHandleInvoke(BotEventArgs e)
        {
            OnDocumentHandle?.Invoke(e);
        }

        internal void OnWebAppsHandleInvoke(BotEventArgs e)
        {
            OnWebAppsHandle?.Invoke(e);
        }

        internal void OnPollHandleInvoke(BotEventArgs e)
        {
            OnPollHandle?.Invoke(e);
        }

        internal void OnGameHandleInvoke(BotEventArgs e)
        {
            OnGameHandle?.Invoke(e);
        }

        internal void OnVideoHandleInvoke(BotEventArgs e)
        {
            OnVideoHandle?.Invoke(e);
        }

        internal void OnPhotoHandleInvoke(BotEventArgs e)
        {
            OnPhotoHandle?.Invoke(e);
        }

        internal void OnStickerHandleInvoke(BotEventArgs e)
        {
            OnStickerHandle?.Invoke(e);
        }

        internal void OnVoiceHandleInvoke(BotEventArgs e)
        {
            OnVoiceHandle?.Invoke(e);
        }

        internal void OnVenueHandleInvoke(BotEventArgs e)
        {
            OnVenueHandle?.Invoke(e);
        }

        internal void OnUnknownHandleInvoke(BotEventArgs e)
        {
            OnUnknownHandle?.Invoke(e);
        }

        internal void OnVideoNoteHandleInvoke(BotEventArgs e)
        {
            OnVideoNoteHandle?.Invoke(e);
        }

        internal void OnAnimationHandleInvoke(BotEventArgs e)
        {
            OnAnimationHandle?.Invoke(e);
        }

        internal void OnChannelCreatedHandleInvoke(BotEventArgs e)
        {
            OnChannelCreatedHandle?.Invoke(e);
        }

        internal void OnChatMemberLeftHandleInvoke(BotEventArgs e)
        {
            OnChatMemberLeftHandle?.Invoke(e);
        }

        internal void OnChatMembersAddedHandleInvoke(BotEventArgs e)
        {
            OnChatMembersAddedHandle?.Invoke(e);
        }

        internal void OnChatPhotoChangedHandleInvoke(BotEventArgs e)
        {
            OnChatPhotoChangedHandle?.Invoke(e);
        }

        internal void OnChatPhotoDeletedHandleInvoke(BotEventArgs e)
        {
            OnChatPhotoDeletedHandle?.Invoke(e);
        }

        internal void OnChatSharedHandleInvoke(BotEventArgs e)
        {
            OnChatSharedHandle?.Invoke(e);
        }

        internal void OnChatTitleChangedHandleInvoke(BotEventArgs e)
        {
            OnChatTitleChangedHandle?.Invoke(e);
        }

        internal void OnForumTopicClosedHandleInvoke(BotEventArgs e)
        {
            OnForumTopicClosedHandle?.Invoke(e);
        }

        internal void OnForumTopicCreatedHandleInvoke(BotEventArgs e)
        {
            OnForumTopicCreatedHandle?.Invoke(e);
        }

        internal void OnForumTopicEditedHandleInvoke(BotEventArgs e)
        {
            OnForumTopicEditedHandle?.Invoke(e);
        }

        internal void OnForumTopicReopenedHandleInvoke(BotEventArgs e)
        {
            OnForumTopicReopenedHandle?.Invoke(e);
        }

        internal void OnGeneralForumTopicHiddenHandleInvoke(BotEventArgs e)
        {
            OnGeneralForumTopicHiddenHandle?.Invoke(e);
        }

        internal void OnGeneralForumTopicUnhiddenHandleInvoke(BotEventArgs e)
        {
            OnGeneralForumTopicUnhiddenHandle?.Invoke(e);
        }

        internal void OnGroupCreatedHandleInvoke(BotEventArgs e)
        {
            OnGroupCreatedHandle?.Invoke(e);
        }

        internal void OnInvoiceHandleInvoke(BotEventArgs e)
        {
            OnInvoiceHandle?.Invoke(e);
        }

        internal void OnMessageAutoDeleteTimerChangedHandleInvoke(BotEventArgs e)
        {
            OnMessageAutoDeleteTimerChangedHandle?.Invoke(e);
        }

        internal void OnMessagePinnedHandleInvoke(BotEventArgs e)
        {
            OnMessagePinnedHandle?.Invoke(e);
        }

        internal void OnMigratedFromGroupHandleInvoke(BotEventArgs e)
        {
            OnMigratedFromGroupHandle?.Invoke(e);
        }

        internal void OnMigratedToSupergroupHandleInvoke(BotEventArgs e)
        {
            OnMigratedToSupergroupHandle?.Invoke(e);
        }

        internal void OnSuccessfulPaymentHandleInvoke(BotEventArgs e)
        {
            OnSuccessfulPaymentHandle?.Invoke(e);
        }

        internal void OnSupergroupCreatedHandleInvoke(BotEventArgs e)
        {
            OnSupergroupCreatedHandle?.Invoke(e);
        }

        internal void OnUserSharedHandleInvoke(BotEventArgs e)
        {
            OnUserSharedHandle?.Invoke(e);
        }

        internal void OnVideoChatEndedHandleInvoke(BotEventArgs e)
        {
            OnVideoChatEndedHandle?.Invoke(e);
        }

        internal void OnVideoChatParticipantsInvitedHandleInvoke(BotEventArgs e)
        {
            OnVideoChatParticipantsInvitedHandle?.Invoke(e);
        }

        internal void OnVideoChatScheduledHandleInvoke(BotEventArgs e)
        {
            OnVideoChatScheduledHandle?.Invoke(e);
        }

        internal void OnVideoChatStartedHandleInvoke(BotEventArgs e)
        {
            OnVideoChatStartedHandle?.Invoke(e);
        }

        internal void OnWebsiteConnectedHandleInvoke(BotEventArgs e)
        {
            OnWebsiteConnectedHandle?.Invoke(e);
        }

        internal void OnWriteAccessAllowedHandleInvoke(BotEventArgs e)
        {
            OnWriteAccessAllowedHandle?.Invoke(e);
        }

        internal void OnProximityAlertTriggeredHandleHandleInvoke(BotEventArgs e)
        {
            OnProximityAlertTriggeredHandle?.Invoke(e);
        }

        internal void OnErrorLogInvoke(ErrorLogEventArgsCreator e)
        {
            OnErrorLog?.Invoke(new ErrorLogEventArgs(Bot, e));
        }

        public void OnErrorLogInvoke(Exception exception, Update update)
        {
            OnErrorLogInvoke(new ErrorLogEventArgsCreator(exception, update));
        }

        public void OnErrorLogInvoke(Exception exception)
        {
            OnErrorLogInvoke(new ErrorLogEventArgsCreator(exception));
        }

        public void OnCommonLogInvoke(CommonLogEventArgsCreator e)
        {
            OnCommonLog?.Invoke(new CommonLogEventArgs(Bot, e));
        }

        public void OnCommonLogInvoke(string message, string type, Update update)
        {
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, update));
        }

        public void OnCommonLogInvoke(string message, string type, ConsoleColor color)
        {
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, color));
        }

        public void OnCommonLogInvoke(string message, string type, ConsoleColor color, Update update)
        {
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type, color, update));
        }

        public void OnErrorOnCommonLogInvokeInvoke(string message, string type)
        {
            OnCommonLogInvoke(new CommonLogEventArgsCreator(message, type));
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public TEvents(PRBotBase bot)
        {
            this.Bot = bot;
        }

        #endregion
    }
}
