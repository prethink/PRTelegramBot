using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// События для бота.
    /// </summary>
    public sealed class TEvents
    {
        #region События

        /// <summary>
        /// Событие когда пользователь написал start с аргументом
        /// </summary>
        public event Func<StartEventArgs, Task>? OnUserStartWithArgs;

        /// <summary>
        /// Событие когда нужно проверить привилегии перед выполнением команды
        /// </summary>
        public event Func<PrivilegeEventArgs, Task>? OnCheckPrivilege;

        /// <summary>
        /// Событие когда указан не верный тип сообщения
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWrongTypeMessage;

        /// <summary>
        /// Событие когда указан не верный тип чат
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWrongTypeChat;

        /// <summary>
        /// Событие когда не найдена команда
        /// </summary>
        public event Func<BotEventArgs, Task>? OnMissingCommand;

        /// <summary>
        /// Событие Обработки контактных данных
        /// </summary>
        public event Func<BotEventArgs, Task>? OnContactHandle;

        /// <summary>
        /// Событие обработки голосований
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPollHandle;

        /// <summary>
        /// Событие обработки локации
        /// </summary>
        public event Func<BotEventArgs, Task>? OnLocationHandle;
        /// <summary>
        /// Событие обработки WebApps
        /// </summary>
        public event Func<BotEventArgs, Task>? OnWebAppsHandle;

        /// <summary>
        /// Событие когда отказано в доступе
        /// </summary>
        public event Func<BotEventArgs, Task>? OnAccessDenied;

        /// <summary>
        /// Событие обработки сообщением с документом
        /// </summary>
        public event Func<BotEventArgs, Task>? OnDocumentHandle;

        /// <summary>
        /// Событие обработки сообщением с аудио
        /// </summary>
        public event Func<BotEventArgs, Task>? OnAudioHandle;

        /// <summary>
        /// Событие обработки сообщением с видео
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoHandle;

        /// <summary>
        /// Событие обработки сообщением с фото
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPhotoHandle;

        /// <summary>
        /// Событие обработки сообщением с стикером
        /// </summary>
        public event Func<BotEventArgs, Task>? OnStickerHandle;

        /// <summary>
        /// Событие обработки сообщением с голосовым сообщением
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVoiceHandle;

        /// <summary>
        /// Событие обработки сообщением с неизвестный типом сообщения
        /// </summary>
        public event Func<BotEventArgs, Task>? OnUnknownHandle;

        /// <summary>
        /// Событие обработки сообщением с местом
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVenueHandle;

        /// <summary>
        /// Событие обработки сообщением с игрой
        /// </summary>
        public event Func<BotEventArgs, Task>? OnGameHandle;

        /// <summary>
        /// Событие обработки сообщением с видеозаметкой
        /// </summary>
        public event Func<BotEventArgs, Task>? OnVideoNoteHandle;

        /// <summary>
        /// Событие обработки сообщением с игральной кости
        /// </summary>
        public event Func<BotEventArgs, Task>? OnDiceHandle;

        /// <summary>
        /// Событие вызывается до обработки update, может быть прекращено выполнение 
        /// </summary>
        public event Func<BotEventArgs, Task<ResultUpdate>>? OnPreUpdate;

        /// <summary>
        /// Событие вызывается после обработки update типа Message и CallbackQuery
        /// </summary>
        public event Func<BotEventArgs, Task>? OnPostMessageUpdate;

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

        public async Task<ResultUpdate> OnPreUpdateInvoke(BotEventArgs e)
        {
            if (HasEventOnPreUpdate())
                return await OnPreUpdate.Invoke(e);

            return ResultUpdate.Continue;
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

        #endregion
    }
}
