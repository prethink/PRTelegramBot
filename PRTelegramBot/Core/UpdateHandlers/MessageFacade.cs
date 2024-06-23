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
                if ((int)item.Key == (int)update!.Message!.Type)
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
            TypeMessage.Add(MessageType.Animation, bot.Events.MessageEvents.OnAnimationHandleInvoke);
            TypeMessage.Add(MessageType.Audio, bot.Events.MessageEvents.OnAudioHandleInvoke);
            TypeMessage.Add(MessageType.ChannelCreated, bot.Events.MessageEvents.OnChannelCreatedHandleInvoke);
            TypeMessage.Add(MessageType.ChatMemberLeft, bot.Events.MessageEvents.OnChatMemberLeftHandleInvoke);
            TypeMessage.Add(MessageType.ChatMembersAdded, bot.Events.MessageEvents.OnChatMembersAddedHandleInvoke);
            TypeMessage.Add(MessageType.ChatPhotoChanged, bot.Events.MessageEvents.OnChatPhotoChangedHandleInvoke);
            TypeMessage.Add(MessageType.ChatPhotoDeleted, bot.Events.MessageEvents.OnChatPhotoDeletedHandleInvoke);
            TypeMessage.Add(MessageType.ChatShared, bot.Events.MessageEvents.OnChatSharedHandleInvoke);
            TypeMessage.Add(MessageType.ChatTitleChanged, bot.Events.MessageEvents.OnChatTitleChangedHandleInvoke);
            TypeMessage.Add(MessageType.ChatBackgroundSet, bot.Events.MessageEvents.OnChatBackgroundSetHandleInvoke);
            TypeMessage.Add(MessageType.Contact, bot.Events.MessageEvents.OnContactHandleInvoke);
            TypeMessage.Add(MessageType.Dice, bot.Events.MessageEvents.OnDiceHandleInvoke);
            TypeMessage.Add(MessageType.Document, bot.Events.MessageEvents.OnDocumentHandleInvoke);
            TypeMessage.Add(MessageType.ForumTopicClosed, bot.Events.MessageEvents.OnForumTopicClosedHandleInvoke);
            TypeMessage.Add(MessageType.ForumTopicCreated, bot.Events.MessageEvents.OnForumTopicCreatedHandleInvoke);
            TypeMessage.Add(MessageType.ForumTopicEdited, bot.Events.MessageEvents.OnForumTopicEditedHandleInvoke);
            TypeMessage.Add(MessageType.ForumTopicReopened, bot.Events.MessageEvents.OnForumTopicReopenedHandleInvoke);
            TypeMessage.Add(MessageType.Game, bot.Events.MessageEvents.OnGameHandleInvoke);
            TypeMessage.Add(MessageType.Giveaway, bot.Events.MessageEvents.OnGiveawayHandleInvoke);
            TypeMessage.Add(MessageType.GiveawayWinners, bot.Events.MessageEvents.OnGiveawayWinnersHandleInvoke);
            TypeMessage.Add(MessageType.GiveawayCompleted, bot.Events.MessageEvents.OnGiveawayCompletedHandleInvoke);
            TypeMessage.Add(MessageType.BoostAdded, bot.Events.MessageEvents.OnBoostAddedHandleInvoke);
            TypeMessage.Add(MessageType.GeneralForumTopicHidden, bot.Events.MessageEvents.OnGeneralForumTopicHiddenHandleInvoke);
            TypeMessage.Add(MessageType.GeneralForumTopicUnhidden, bot.Events.MessageEvents.OnGeneralForumTopicUnhiddenHandleInvoke);
            TypeMessage.Add(MessageType.GroupCreated, bot.Events.MessageEvents.OnGroupCreatedHandleInvoke);
            TypeMessage.Add(MessageType.Invoice, bot.Events.MessageEvents.OnInvoiceHandleInvoke);
            TypeMessage.Add(MessageType.Location, bot.Events.MessageEvents.OnLocationHandleInvoke);
            TypeMessage.Add(MessageType.MessageAutoDeleteTimerChanged, bot.Events.MessageEvents.OnMessageAutoDeleteTimerChangedHandleInvoke);
            TypeMessage.Add(MessageType.MessagePinned, bot.Events.MessageEvents.OnMessagePinnedHandleInvoke);
            TypeMessage.Add(MessageType.MigratedFromGroup, bot.Events.MessageEvents.OnMigratedFromGroupHandleInvoke);
            TypeMessage.Add(MessageType.MigratedToSupergroup, bot.Events.MessageEvents.OnMigratedToSupergroupHandleInvoke);
            TypeMessage.Add(MessageType.Photo, bot.Events.MessageEvents.OnPhotoHandleInvoke);
            TypeMessage.Add(MessageType.Poll, bot.Events.MessageEvents.OnPollHandleInvoke);
            TypeMessage.Add(MessageType.ProximityAlertTriggered, bot.Events.MessageEvents.OnProximityAlertTriggeredHandleInvoke);
            TypeMessage.Add(MessageType.Sticker, bot.Events.MessageEvents.OnStickerHandleInvoke);
            TypeMessage.Add(MessageType.SuccessfulPayment, bot.Events.MessageEvents.OnSuccessfulPaymentHandleInvoke);
            TypeMessage.Add(MessageType.SupergroupCreated, bot.Events.MessageEvents.OnSupergroupCreatedHandleInvoke);
            TypeMessage.Add(MessageType.UsersShared, bot.Events.MessageEvents.OnUserSharedHandleInvoke);
            TypeMessage.Add(MessageType.Unknown, bot.Events.MessageEvents.OnUnknownHandleInvoke);
            TypeMessage.Add(MessageType.VideoChatEnded, bot.Events.MessageEvents.OnVideoChatEndedHandleInvoke);
            TypeMessage.Add(MessageType.VideoChatParticipantsInvited, bot.Events.MessageEvents.OnVideoChatParticipantsInvitedHandleInvoke);
            TypeMessage.Add(MessageType.VideoChatScheduled, bot.Events.MessageEvents.OnVideoChatScheduledHandleInvoke);
            TypeMessage.Add(MessageType.VideoChatStarted, bot.Events.MessageEvents.OnVideoChatStartedHandleInvoke);
            TypeMessage.Add(MessageType.Video, bot.Events.MessageEvents.OnVideoHandleInvoke);
            TypeMessage.Add(MessageType.Voice, bot.Events.MessageEvents.OnVoiceHandleInvoke);
            TypeMessage.Add(MessageType.Venue, bot.Events.MessageEvents.OnVenueHandleInvoke);
            TypeMessage.Add(MessageType.VideoNote, bot.Events.MessageEvents.OnVideoNoteHandleInvoke);
            TypeMessage.Add(MessageType.WebAppData, bot.Events.MessageEvents.OnWebAppsHandleInvoke);
            TypeMessage.Add(MessageType.WebsiteConnected, bot.Events.MessageEvents.OnWebsiteConnectedHandleInvoke);
            TypeMessage.Add(MessageType.WriteAccessAllowed, bot.Events.MessageEvents.OnWriteAccessAllowedInvoke);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public MessageFacade(PRBotBase bot)
            : base(bot)
        {
            ReplyHandler = new ReplyMessageUpdateHandler(bot);
            ReplyDynamicHandler = new ReplyDynamicMessageUpdateHandler(bot);
            SlashHandler = new SlashMessageUpdateHandler(bot);
            nextStepHandler = new NextStepUpdateHandler(bot);

            UpdateEventLink();
        }

        #endregion
    }
}
