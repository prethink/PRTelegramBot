using PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using System.Reflection.Metadata;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core.UpdateHandlers
{
    public sealed class MessageFacade : UpdateHandler
    {
        #region Поля и свойства

        public ReplyMessageUpdateHandler ReplyHandler { get; private set; }

        public ReplyDynamicMessageUpdateHandler ReplyDynamicHandler { get; private set; }
        public SlashMessageUpdateHandler SlashHandler { get; private set; }

        private NextStepUpdateHandler nextStepHandler;
        public Dictionary<MessageType, Action<BotEventArgs>> TypeMessage { get; private set; }

        #endregion

        public MessageFacade(PRBot bot, IServiceProvider serviceProvider) : base(bot)
        {
            ReplyHandler        = new ReplyMessageUpdateHandler(bot, serviceProvider);
            ReplyDynamicHandler = new ReplyDynamicMessageUpdateHandler(bot, serviceProvider);
            SlashHandler        = new SlashMessageUpdateHandler(bot, serviceProvider);
            nextStepHandler     = new NextStepUpdateHandler(bot);

            UpdateEventLink();
        }

        public override UpdateType TypeUpdate => UpdateType.Message;

        public override async Task<ResultUpdate> Handle(Update update)
        {
            var eventResult = await EventHandler(update);
            if (eventResult == ResultUpdate.Handled)
                return eventResult;

            return await UpdateMessageCommands(update); 
        }

        private async Task<ResultUpdate> UpdateMessageCommands(Update update)
        {
            var result = ResultUpdate.Continue;

            if (!nextStepHandler.IgnoreBasicCommand(update))
            {
                result = await SlashHandler.Handle(update);
                if (result == ResultUpdate.Handled)
                    return result;

                result = await ReplyHandler.Handle(update);
                if (result == ResultUpdate.Handled)
                    return result;

                result = await ReplyDynamicHandler.Handle(update);
                if (result == ResultUpdate.Handled)
                    return result;
            }

            result = await nextStepHandler.Handle(update);
            if (result == ResultUpdate.Handled)
            {
                if(nextStepHandler.LastStepExecuted(update))
                    nextStepHandler.ClearSteps(update);
                return result;
            }

            if (result == ResultUpdate.Continue)
                bot.Events.OnMissingCommandInvoke(new BotEventArgs(bot, update));

            return ResultUpdate.Handled;
        }

        private async Task<ResultUpdate> EventHandler(Update update)
        {
            foreach (var item in TypeMessage)
            {
                if (item.Key == update!.Message!.Type)
                {
                    item.Value.Invoke(new BotEventArgs(bot, update));
                    return ResultUpdate.Handled;
                }
            }
            return ResultUpdate.Continue;
        }

        private void UpdateEventLink()
        {
            TypeMessage = new();
            TypeMessage.Add(MessageType.Contact, bot.Events.OnContactHandleInvoke);
            TypeMessage.Add(MessageType.Location, bot.Events.OnLocationHandleInvoke);
            TypeMessage.Add(MessageType.WebAppData, bot.Events.OnWebAppsHandleInvoke);
            TypeMessage.Add(MessageType.Poll, bot.Events.OnPollHandleInvoke);
            TypeMessage.Add(MessageType.Document, bot.Events.OnDocumentHandleInvoke);
            TypeMessage.Add(MessageType.Audio, bot.Events.OnAudioHandleInvoke);
            TypeMessage.Add(MessageType.Video, bot.Events.OnVideoHandleInvoke);
            TypeMessage.Add(MessageType.Photo, bot.Events.OnPhotoHandleInvoke);
            TypeMessage.Add(MessageType.Sticker, bot.Events.OnStickerHandleInvoke);
            TypeMessage.Add(MessageType.Voice, bot.Events.OnVoiceHandleInvoke);
            TypeMessage.Add(MessageType.Unknown, bot.Events.OnUnknownHandleInvoke);
            TypeMessage.Add(MessageType.Venue, bot.Events.OnVenueHandleInvoke);
            TypeMessage.Add(MessageType.Game, bot.Events.OnGameHandleInvoke);
            TypeMessage.Add(MessageType.VideoNote, bot.Events.OnVideoNoteHandleInvoke);
            TypeMessage.Add(MessageType.Dice, bot.Events.OnDiceHandleInvoke);
        }
    }
}
