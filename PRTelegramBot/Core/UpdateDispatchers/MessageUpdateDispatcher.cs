using PRTelegramBot.Core.CommandHandlers;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers
{
    /// <summary>
    /// Диспетчер для обработчик update типа message.
    /// </summary>
    internal sealed class MessageUpdateDispatcher
    {
        #region Поля и свойства

        /// <summary>
        /// Коллекция типов сообщений и событий для вызова.
        /// </summary>
        public Dictionary<MessageType, Action<BotEventArgs>> TypeMessage { get; private set; }

        /// <summary>
        /// Обработчик пошаговых команд.
        /// </summary>
        private NextStepCommandHandler nextStepHandler;

        /// <summary>
        /// Бот.
        /// </summary>
        private readonly PRBotBase bot;

        #endregion

        #region Методы

        /// <summary>
        /// Вызвать обработку update типа message.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Результат выполнения.</returns>
        public async Task<UpdateResult> Dispatch(IBotContext context)
        {
            var eventResult = EventHandler(context);
            if (eventResult == UpdateResult.Handled)
                return eventResult;
 
            if(context.Update.Message.Type.Equals(MessageType.Text))
                return await UpdateMessageCommands(context);

            
            return UpdateResult.NotFound;
        }

        /// <summary>
        /// Логика обработки сообщений.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<UpdateResult> UpdateMessageCommands(IBotContext context)
        {
            var result = UpdateResult.Continue;
            bot.Events.MessageEvents.OnTextHandleInvoke(context.CreateBotEventArgs());

            if (!nextStepHandler.IgnoreBasicCommand(context))
            {
                foreach (var handler in bot.Options.MessageHandlers)
                {
                    result = await handler.Handle(context, context.Update.Message);
                    if (!result.IsContinueHandle(context))
                        return result;
                }
            }

            result = await nextStepHandler.Handle(context);
            if (result == UpdateResult.Handled)
            {
                if (nextStepHandler.LastStepExecuted(context.Update))
                    nextStepHandler.ClearSteps(context.Update);
                return result;
            }

            bot.Events.OnMissingCommandInvoke(context.CreateBotEventArgs());

            return UpdateResult.NotFound;
        }

        /// <summary>
        /// Обработчик для разных событий.
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <returns>Результат выполнения.</returns>
        private UpdateResult EventHandler(IBotContext context)
        {
            foreach (var item in TypeMessage)
            {
                if ((int)item.Key == (int)context.Update!.Message!.Type)
                {
                    item.Value.Invoke(context.CreateBotEventArgs());
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
            TypeMessage.Add(MessageType.ChannelChatCreated, bot.Events.MessageEvents.OnChannelCreatedHandleInvoke);
            TypeMessage.Add(MessageType.LeftChatMember, bot.Events.MessageEvents.OnChatMemberLeftHandleInvoke);
            TypeMessage.Add(MessageType.NewChatMembers , bot.Events.MessageEvents.OnChatMembersAddedHandleInvoke);
            TypeMessage.Add(MessageType.NewChatPhoto, bot.Events.MessageEvents.OnChatPhotoChangedHandleInvoke);
            TypeMessage.Add(MessageType.DeleteChatPhoto, bot.Events.MessageEvents.OnChatPhotoDeletedHandleInvoke);
            TypeMessage.Add(MessageType.ChatShared, bot.Events.MessageEvents.OnChatSharedHandleInvoke);
            TypeMessage.Add(MessageType.NewChatTitle, bot.Events.MessageEvents.OnChatTitleChangedHandleInvoke);
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
            TypeMessage.Add(MessageType.GroupChatCreated, bot.Events.MessageEvents.OnGroupCreatedHandleInvoke);
            TypeMessage.Add(MessageType.Invoice, bot.Events.MessageEvents.OnInvoiceHandleInvoke);
            TypeMessage.Add(MessageType.Location, bot.Events.MessageEvents.OnLocationHandleInvoke);
            TypeMessage.Add(MessageType.MessageAutoDeleteTimerChanged, bot.Events.MessageEvents.OnMessageAutoDeleteTimerChangedHandleInvoke);
            TypeMessage.Add(MessageType.PinnedMessage, bot.Events.MessageEvents.OnMessagePinnedHandleInvoke);
            TypeMessage.Add(MessageType.MigrateToChatId, bot.Events.MessageEvents.OnMigratedFromGroupHandleInvoke);
            TypeMessage.Add(MessageType.MigrateFromChatId, bot.Events.MessageEvents.OnMigratedToSupergroupHandleInvoke);
            TypeMessage.Add(MessageType.Photo, bot.Events.MessageEvents.OnPhotoHandleInvoke);
            TypeMessage.Add(MessageType.Poll, bot.Events.MessageEvents.OnPollHandleInvoke);
            TypeMessage.Add(MessageType.ProximityAlertTriggered, bot.Events.MessageEvents.OnProximityAlertTriggeredHandleInvoke);
            TypeMessage.Add(MessageType.Sticker, bot.Events.MessageEvents.OnStickerHandleInvoke);
            TypeMessage.Add(MessageType.SuccessfulPayment, bot.Events.MessageEvents.OnSuccessfulPaymentHandleInvoke);
            TypeMessage.Add(MessageType.SupergroupChatCreated, bot.Events.MessageEvents.OnSupergroupCreatedHandleInvoke);
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
            TypeMessage.Add(MessageType.ConnectedWebsite, bot.Events.MessageEvents.OnWebsiteConnectedHandleInvoke);
            TypeMessage.Add(MessageType.WriteAccessAllowed, bot.Events.MessageEvents.OnWriteAccessAllowedInvoke);
            TypeMessage.Add(MessageType.Story, bot.Events.MessageEvents.OnStoryHandleInvoke);
            TypeMessage.Add(MessageType.GiveawayCreated, bot.Events.MessageEvents.OnGiveawayCreatedHandleInvoke);
            TypeMessage.Add(MessageType.PassportData, bot.Events.MessageEvents.OnPassportDataHandleInvoke);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public MessageUpdateDispatcher(PRBotBase bot)
        {
            this.bot = bot;
            nextStepHandler = new NextStepCommandHandler();

            UpdateEventLink();
        }

        #endregion
    }
}
