using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types;
using ErrorLogEventArgs = PRTelegramBot.Models.EventsArgs.ErrorLogEventArgs;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// События для бота.
    /// </summary>
    public sealed class TEvents
    {
        #region Поля и свойства

        public PRBot Bot { get; private set; }

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
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatPhotoChangedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatPhotoDeletedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatSharedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnChatTitleChangedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnForumTopicClosedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnForumTopicCreatedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnForumTopicEditedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnForumTopicReopenedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGeneralForumTopicHiddenHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGeneralForumTopicUnhiddenHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGroupCreatedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnInvoiceHandle;
        
        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMessageAutoDeleteTimerChangedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMessagePinnedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMigratedFromGroupHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMigratedToSupergroupHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnProximityAlertTriggeredHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSuccessfulPaymentHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnSupergroupCreatedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUserSharedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoChatEndedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoChatParticipantsInvitedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoChatScheduledHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoChatStartedHandle;

        /// <summary>
        /// Событие
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWebsiteConnectedHandle;

        /// <summary>
        /// Событие
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

        public void OnUserStartWithArgsInvoke(StartEventArgs e)
        {
            OnUserStartWithArgs?.Invoke(e);
        }        
        
        public void OnMissingCommandInvoke(BotEventArgs e)
        {
            OnMissingCommand?.Invoke(e);
        }

        public void OnAccessDeniedInvoke(BotEventArgs e)
        {
            OnAccessDenied?.Invoke(e);
        }

        public void OnCheckPrivilegeInvoke(PrivilegeEventArgs e)
        {
            OnCheckPrivilege?.Invoke(e);
        }

        public void OnWrongTypeMessageInvoke(BotEventArgs e)
        {
            OnWrongTypeMessage?.Invoke(e);
        }
        public void OnWrongTypeChatInvoke(BotEventArgs e)
        {
            OnWrongTypeChat?.Invoke(e);
        }

        public async Task<UpdateResult> OnPreUpdateInvoke(BotEventArgs e)
        {
            if (HasEventOnPreUpdate())
                return await OnPreUpdate.Invoke(e);

            return UpdateResult.Continue;
        }

        public bool HasEventOnPreUpdate()
        {
            return OnPreUpdate != null;
        }
        
        public void OnPostMessageUpdateInvoke(BotEventArgs e)
        {
            OnPostMessageUpdate?.Invoke(e);
        }

        public void OnContactHandleInvoke(BotEventArgs e)
        {
            OnContactHandle?.Invoke(e);    
        }

        public void OnAudioHandleInvoke(BotEventArgs e)
        {
            OnAudioHandle?.Invoke(e);
        }

        public void OnLocationHandleInvoke(BotEventArgs e)
        {
            OnLocationHandle?.Invoke(e);
        }

        public void OnDiceHandleInvoke(BotEventArgs e)
        {
            OnDiceHandle?.Invoke(e);
        }

        public void OnDocumentHandleInvoke(BotEventArgs e)
        {
            OnDocumentHandle?.Invoke(e);
        }

        public void OnWebAppsHandleInvoke(BotEventArgs e)
        {
            OnWebAppsHandle?.Invoke(e);
        }

        public void OnPollHandleInvoke(BotEventArgs e)
        {
            OnPollHandle?.Invoke(e);
        }

        public void OnGameHandleInvoke(BotEventArgs e)
        {
            OnGameHandle?.Invoke(e);
        }        
        
        public void OnVideoHandleInvoke(BotEventArgs e)
        {
            OnVideoHandle?.Invoke(e);
        }        
        
        public void OnPhotoHandleInvoke(BotEventArgs e)
        {
            OnPhotoHandle?.Invoke(e);
        }        
        
        public void OnStickerHandleInvoke(BotEventArgs e)
        {
            OnStickerHandle?.Invoke(e);
        }        
        
        public void OnVoiceHandleInvoke(BotEventArgs e)
        {
            OnVoiceHandle?.Invoke(e);
        }        
        
        public void OnVenueHandleInvoke(BotEventArgs e)
        {
            OnVenueHandle?.Invoke(e);
        }        
        
        public void OnUnknownHandleInvoke(BotEventArgs e)
        {
            OnUnknownHandle?.Invoke(e);
        }        
        
        public void OnVideoNoteHandleInvoke(BotEventArgs e)
        {
            OnVideoNoteHandle?.Invoke(e);
        }

        public void OnAnimationHandleInvoke(BotEventArgs e)
        {
            OnAnimationHandle?.Invoke(e);
        }

        public void OnChannelCreatedHandleInvoke(BotEventArgs e)
        {
            OnChannelCreatedHandle?.Invoke(e);
        }

        public void OnChatMemberLeftHandleInvoke(BotEventArgs e)
        {
            OnChatMemberLeftHandle?.Invoke(e);
        }

        public void OnChatMembersAddedHandleInvoke(BotEventArgs e)
        {
            OnChatMembersAddedHandle?.Invoke(e);
        }

        public void OnChatPhotoChangedHandleInvoke(BotEventArgs e)
        {
            OnChatPhotoChangedHandle?.Invoke(e);
        }

        public void OnChatPhotoDeletedHandleInvoke(BotEventArgs e)
        {
            OnChatPhotoDeletedHandle?.Invoke(e);
        }

        public void OnChatSharedHandleInvoke(BotEventArgs e)
        {
            OnChatSharedHandle?.Invoke(e);
        }

        public void OnChatTitleChangedHandleInvoke(BotEventArgs e)
        {
            OnChatTitleChangedHandle?.Invoke(e);
        }

        public void OnForumTopicClosedHandleInvoke(BotEventArgs e)
        {
            OnForumTopicClosedHandle?.Invoke(e);
        }

        public void OnForumTopicCreatedHandleInvoke(BotEventArgs e)
        {
            OnForumTopicCreatedHandle?.Invoke(e);
        }

        public void OnForumTopicEditedHandleInvoke(BotEventArgs e)
        {
            OnForumTopicEditedHandle?.Invoke(e);
        }

        public void OnForumTopicReopenedHandleInvoke(BotEventArgs e)
        {
            OnForumTopicReopenedHandle?.Invoke(e);
        }

        public void OnGeneralForumTopicHiddenHandleInvoke(BotEventArgs e)
        {
            OnGeneralForumTopicHiddenHandle?.Invoke(e);
        }

        public void OnGeneralForumTopicUnhiddenHandleInvoke(BotEventArgs e)
        {
            OnGeneralForumTopicUnhiddenHandle?.Invoke(e);
        }

        public void OnGroupCreatedHandleInvoke(BotEventArgs e)
        {
            OnGroupCreatedHandle?.Invoke(e);
        }

        public void OnInvoiceHandleInvoke(BotEventArgs e)
        {
            OnInvoiceHandle?.Invoke(e);
        }

        public void OnMessageAutoDeleteTimerChangedHandleInvoke(BotEventArgs e)
        {
            OnMessageAutoDeleteTimerChangedHandle?.Invoke(e);
        }

        public void OnMessagePinnedHandleInvoke(BotEventArgs e)
        {
            OnMessagePinnedHandle?.Invoke(e);
        }

        public void OnMigratedFromGroupHandleInvoke(BotEventArgs e)
        {
            OnMigratedFromGroupHandle?.Invoke(e);
        }

        public void OnMigratedToSupergroupHandleInvoke(BotEventArgs e)
        {
            OnMigratedToSupergroupHandle?.Invoke(e);
        }

        public void OnSuccessfulPaymentHandleInvoke(BotEventArgs e)
        {
            OnSuccessfulPaymentHandle?.Invoke(e);
        }

        public void OnSupergroupCreatedHandleInvoke(BotEventArgs e)
        {
            OnSupergroupCreatedHandle?.Invoke(e);
        }

        public void OnUserSharedHandleInvoke(BotEventArgs e)
        {
            OnUserSharedHandle?.Invoke(e);
        }

        public void OnVideoChatEndedHandleInvoke(BotEventArgs e)
        {
            OnVideoChatEndedHandle?.Invoke(e);
        }

        public void OnVideoChatParticipantsInvitedHandleInvoke(BotEventArgs e)
        {
            OnVideoChatParticipantsInvitedHandle?.Invoke(e);
        }

        public void OnVideoChatScheduledHandleInvoke(BotEventArgs e)
        {
            OnVideoChatScheduledHandle?.Invoke(e);
        }

        public void OnVideoChatStartedHandleInvoke(BotEventArgs e)
        {
            OnVideoChatStartedHandle?.Invoke(e);
        }

        public void OnWebsiteConnectedHandleInvoke(BotEventArgs e)
        {
            OnWebsiteConnectedHandle?.Invoke(e);
        }

        public void OnWriteAccessAllowedHandleInvoke(BotEventArgs e)
        {
            OnWriteAccessAllowedHandle?.Invoke(e);
        }
        
        public void OnProximityAlertTriggeredHandleHandleInvoke(BotEventArgs e)
        {
            OnProximityAlertTriggeredHandle?.Invoke(e);
        }

        public void OnErrorLogInvoke(ErrorLogEventArgsCreator e)
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
        public TEvents(PRBot bot)
        {
            this.Bot = bot;
        }

        #endregion
    }
}
