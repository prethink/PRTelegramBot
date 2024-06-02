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
        public override async Task<ResultUpdate> Handle(Update update)
        {
            var eventResult = EventHandler(update);
            if (eventResult == ResultUpdate.Handled)
                return eventResult;

            return await UpdateMessageCommands(update);
        }

        /// <summary>
        /// Логика обработки сообщений.
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <returns>Результат выполнения.</returns>
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
                if (nextStepHandler.LastStepExecuted(update))
                    nextStepHandler.ClearSteps(update);
                return result;
            }

            if (result == ResultUpdate.Continue)
                bot.Events.OnMissingCommandInvoke(new BotEventArgs(bot, update));

            return ResultUpdate.Handled;
        }

        /// <summary>
        /// Обработчик для разных событий.
        /// </summary>
        /// <param name="update">Обновление.</param>
        /// <returns>Результат выполнения.</returns>
        private ResultUpdate EventHandler(Update update)
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

        /// <summary>
        /// Обновление ссылок для событий и сообщений.
        /// </summary>
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
