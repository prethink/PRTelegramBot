using PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers
{
    /// <summary>
    /// Фасад для правильной обработки reply, slash, step команд.
    /// </summary>
    public sealed class MessageFacade : UpdateHandler
    {
        #region Поля и свойства

        /// <summary>
        /// Обработчик reply команд.
        /// </summary>
        public ReplyMessageUpdateHandler ReplyHandler { get; private set; }

        /// <summary>
        /// Обработчик динамических reply команд.
        /// </summary>
        public ReplyDynamicMessageUpdateHandler ReplyDynamicHandler { get; private set; }

        /// <summary>
        /// Обработчик slash команд.
        /// </summary>
        public SlashMessageUpdateHandler SlashHandler { get; private set; }

        /// <summary>
        /// Коллекция типов сообщений и событий для вызова.
        /// </summary>
        public Dictionary<MessageType, Action<BotEventArgs>> TypeMessage { get; private set; }

        public override UpdateType TypeUpdate => UpdateType.Message;

        /// <summary>
        /// Обработчик пошаговых команд.
        /// </summary>
        private NextStepUpdateHandler nextStepHandler;

        #endregion

        #region Методы

        /// <summary>
        /// Обработка обновления.
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <returns>Результат выполнения.</returns>
        public override async Task<UpdateResult> Handle(Update update)
        {
            var eventResult = EventHandler(update);
            if (eventResult == UpdateResult.Handled)
                return eventResult;
 
            if(update.Message.Type.Equals(MessageType.Text))
                return await UpdateMessageCommands(update);
            
            return UpdateResult.NotFound;
        }

        /// <summary>
        /// Логика обработки сообщений.
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<UpdateResult> UpdateMessageCommands(Update update)
        {
            var result = UpdateResult.Continue;

            if (!nextStepHandler.IgnoreBasicCommand(update))
            {
                result = await SlashHandler.Handle(update);
                if (result == UpdateResult.Handled)
                    return result;

                result = await ReplyHandler.Handle(update);
                if (result == UpdateResult.Handled)
                    return result;

                result = await ReplyDynamicHandler.Handle(update);
                if (result == UpdateResult.Handled)
                    return result;
            }

            result = await nextStepHandler.Handle(update);
            if (result == UpdateResult.Handled)
            {
                if (nextStepHandler.LastStepExecuted(update))
                    nextStepHandler.ClearSteps(update);
                return result;
            }

            bot.Events.OnMissingCommandInvoke(new BotEventArgs(bot, update));
            return UpdateResult.NotFound;
        }

        /// <summary>
        /// Обработчик для разных событий.
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <returns>Результат выполнения.</returns>
        private UpdateResult EventHandler(Update update)
        {
            foreach (var item in TypeMessage)
            {
                if (item.Key == update!.Message!.Type)
                {
                    item.Value.Invoke(new BotEventArgs(bot, update));
                    return UpdateResult.Handled;
                }
            }
            return UpdateResult.Continue;
        }

        /// <summary>
        /// Обновление ссылок для событий и сообщений.
        /// </summary>
        private void UpdateEventLink()
        {
            TypeMessage = new();
            TypeMessage.Add(MessageType.Animation, bot.Events.OnAnimationHandleInvoke);
            TypeMessage.Add(MessageType.Audio, bot.Events.OnAudioHandleInvoke);
            TypeMessage.Add(MessageType.ChannelCreated, bot.Events.OnChannelCreatedHandleInvoke);
            TypeMessage.Add(MessageType.ChatMemberLeft, bot.Events.OnChatMemberLeftHandleInvoke);
            TypeMessage.Add(MessageType.ChatMembersAdded, bot.Events.OnChatMembersAddedHandleInvoke);
            TypeMessage.Add(MessageType.ChatPhotoChanged, bot.Events.OnChatPhotoChangedHandleInvoke);
            TypeMessage.Add(MessageType.ChatPhotoDeleted, bot.Events.OnChatPhotoDeletedHandleInvoke);
            TypeMessage.Add(MessageType.ChatShared, bot.Events.OnChatSharedHandleInvoke);
            TypeMessage.Add(MessageType.ChatTitleChanged, bot.Events.OnChatTitleChangedHandleInvoke);
            TypeMessage.Add(MessageType.Contact, bot.Events.OnContactHandleInvoke);
            TypeMessage.Add(MessageType.Dice, bot.Events.OnDiceHandleInvoke);
            TypeMessage.Add(MessageType.Document, bot.Events.OnDocumentHandleInvoke);
            TypeMessage.Add(MessageType.ForumTopicClosed, bot.Events.OnForumTopicClosedHandleInvoke);
            TypeMessage.Add(MessageType.ForumTopicCreated, bot.Events.OnForumTopicCreatedHandleInvoke);
            TypeMessage.Add(MessageType.ForumTopicEdited, bot.Events.OnForumTopicEditedHandleInvoke);
            TypeMessage.Add(MessageType.ForumTopicReopened, bot.Events.OnForumTopicReopenedHandleInvoke);
            TypeMessage.Add(MessageType.Game, bot.Events.OnGameHandleInvoke);
            TypeMessage.Add(MessageType.GeneralForumTopicHidden, bot.Events.OnGeneralForumTopicHiddenHandleInvoke);
            TypeMessage.Add(MessageType.GeneralForumTopicUnhidden, bot.Events.OnGeneralForumTopicUnhiddenHandleInvoke);
            TypeMessage.Add(MessageType.GroupCreated, bot.Events.OnGroupCreatedHandleInvoke);
            TypeMessage.Add(MessageType.Invoice, bot.Events.OnInvoiceHandleInvoke);
            TypeMessage.Add(MessageType.Location, bot.Events.OnLocationHandleInvoke);
            TypeMessage.Add(MessageType.MessageAutoDeleteTimerChanged, bot.Events.OnMessageAutoDeleteTimerChangedHandleInvoke);
            TypeMessage.Add(MessageType.MessagePinned, bot.Events.OnMessagePinnedHandleInvoke);
            TypeMessage.Add(MessageType.MigratedFromGroup, bot.Events.OnMigratedFromGroupHandleInvoke);
            TypeMessage.Add(MessageType.MigratedToSupergroup, bot.Events.OnMigratedToSupergroupHandleInvoke);
            TypeMessage.Add(MessageType.Photo, bot.Events.OnPhotoHandleInvoke);
            TypeMessage.Add(MessageType.Poll, bot.Events.OnPollHandleInvoke);
            TypeMessage.Add(MessageType.ProximityAlertTriggered, bot.Events.OnProximityAlertTriggeredHandleHandleInvoke);
            TypeMessage.Add(MessageType.Sticker, bot.Events.OnStickerHandleInvoke);
            TypeMessage.Add(MessageType.SuccessfulPayment, bot.Events.OnSuccessfulPaymentHandleInvoke);
            TypeMessage.Add(MessageType.SupergroupCreated, bot.Events.OnSupergroupCreatedHandleInvoke);
            TypeMessage.Add(MessageType.UserShared, bot.Events.OnUserSharedHandleInvoke);
            TypeMessage.Add(MessageType.Unknown, bot.Events.OnUnknownHandleInvoke);
            TypeMessage.Add(MessageType.VideoChatEnded, bot.Events.OnVideoChatEndedHandleInvoke);
            TypeMessage.Add(MessageType.VideoChatParticipantsInvited, bot.Events.OnVideoChatParticipantsInvitedHandleInvoke);
            TypeMessage.Add(MessageType.VideoChatScheduled, bot.Events.OnVideoChatScheduledHandleInvoke);
            TypeMessage.Add(MessageType.VideoChatStarted, bot.Events.OnVideoChatStartedHandleInvoke);
            TypeMessage.Add(MessageType.Video, bot.Events.OnVideoHandleInvoke);
            TypeMessage.Add(MessageType.Voice, bot.Events.OnVoiceHandleInvoke);
            TypeMessage.Add(MessageType.Venue, bot.Events.OnVenueHandleInvoke);
            TypeMessage.Add(MessageType.VideoNote, bot.Events.OnVideoNoteHandleInvoke);
            TypeMessage.Add(MessageType.WebAppData, bot.Events.OnWebAppsHandleInvoke);
            TypeMessage.Add(MessageType.WebsiteConnected, bot.Events.OnWebsiteConnectedHandleInvoke);
            TypeMessage.Add(MessageType.WriteAccessAllowed, bot.Events.OnWriteAccessAllowedHandleInvoke);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        /// <param name="serviceProvider">Сервис провайдер.</param>
        public MessageFacade(PRBot bot, IServiceProvider serviceProvider) : base(bot)
        {
            ReplyHandler = new ReplyMessageUpdateHandler(bot, serviceProvider);
            ReplyDynamicHandler = new ReplyDynamicMessageUpdateHandler(bot, serviceProvider);
            SlashHandler = new SlashMessageUpdateHandler(bot, serviceProvider);
            nextStepHandler = new NextStepUpdateHandler(bot);

            UpdateEventLink();
        }

        #endregion
    }
}
