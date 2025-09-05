using PRTelegramBot.Core.CommandStores;
using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Core.UpdateHandlers;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Обработчик.
    /// </summary>
    public sealed class Handler : IPRUpdateHandler
    {
        #region Поля и свойства
        
        /// <summary>
        /// Хранилище callbackQuery команд.
        /// </summary>
        public CallbackQueryCommandStore CallbackQueryCommandsStore { get; private set; }

        /// <summary>
        /// Хранилище reply команд.
        /// </summary>
        public ReplyCommandStore ReplyCommandsStore { get; private set; }

        /// <summary>
        /// Хранилище reply dynamic команд.
        /// </summary>
        public ReplyDynamicCommandStore ReplyDynamicCommandsStore { get; private set; }

        /// <summary>
        /// Хранилище slash команд.
        /// </summary>
        public SlashCommandStore SlashCommandsStore { get; private set; }

        /// <summary>
        /// Диспетчер для обработки update типа message.
        /// </summary>
        internal MessageUpdateDispatcher MessageDispatcher { get; private set; }

        /// <summary>
        /// Диспетчер для обработки update типа callbackQuery.
        /// </summary>
        internal CallBackQueryUpdateDispatcher CallBackQueryDispatcher { get; private set; }

        /// <summary>
        /// Ограничитель спама логов.
        /// </summary>
        private DateTime _lastErrorPollingDate;

        /// <summary>
        /// Бот.
        /// </summary>
        private readonly PRBotBase _bot;

        #endregion

        #region IPRUpdateHandler

        /// <inheritdoc />
        public MiddlewareBase Middleware { get; }

        /// <summary>
        /// Обработчик обновлений.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="update">Обновление телеграм.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (update == null)
                    return;

                // Связь update вместе ITelegramBotClient.
                update.AddTelegramClient(_bot);
                
                _ = Middleware.InvokeOnPreUpdateAsync(botClient, update, async () =>
                {
                    await UpdateAsync(update, cancellationToken);
                });
            }   
            catch (Exception ex)
            {
                _bot.Events.OnErrorLogInvoke(ex, update);
            }
        }

        /// <inheritdoc />
        public void HotReload()
        {
            CallbackQueryCommandsStore.ClearCommands();
            ReplyCommandsStore.ClearCommands();
            ReplyDynamicCommandsStore.ClearCommands();
            SlashCommandsStore.ClearCommands();

            CallbackQueryCommandsStore.RegisterCommand();
            ReplyCommandsStore.RegisterCommand();
            ReplyDynamicCommandsStore.RegisterCommand();
            SlashCommandsStore.RegisterCommand();
        }

        /// <summary>
        /// Обработчик ошибок API.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="exception">Исключение.</param>
        /// <param name="source">Исходник ошибки</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            if (source == HandleErrorSource.PollingError &&  exception.Message.Contains("Exception during making request"))
            {
                if (DateTime.Now < _lastErrorPollingDate)
                    return Task.CompletedTask;

                _lastErrorPollingDate = DateTime.Now.AddMinutes(_bot.Options.AntiSpamErrorMinute);
            }
            _bot.Events.OnErrorLogInvoke(exception);
            
            return Task.CompletedTask;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обработка обновлений.
        /// </summary>
        /// <param name="update">Update.</param>
        public async Task UpdateAsync(Update update, CancellationToken cancellationToken)  // TODO: разобраться с CancellationToken.
        {
            var whiteListManager = _bot.Options.WhiteListManager;

            if (_bot.Events.UpdateEvents.HasEventOnPreUpdate())
            {
                var resultUpdate = await _bot.Events.UpdateEvents.OnPreInvoke(new BotEventArgs(_bot, update));

                if (resultUpdate is UpdateResult.Stop or UpdateResult.Handled)
                    return;
            }

            if (whiteListManager.Settings == WhiteListSettings.OnPreUpdate && whiteListManager.Count > 0)
            {
                var hasUserInWhiteList = await whiteListManager.HasUser(update.GetChatId());
                if (!hasUserInWhiteList)
                {
                    _bot.Events.OnAccessDeniedInvoke(new BotEventArgs(_bot, update));
                    return;
                }
            }

            if (update.Type == UpdateType.CallbackQuery)
                _ = CallBackQueryDispatcher.Dispatch(update);

            if (update.Type == UpdateType.Message)
                _ = MessageDispatcher.Dispatch(update);

            if (update.Type == UpdateType.ChannelPost)
                _bot.Events.UpdateEvents.OnChannelPostHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.ChatJoinRequest)
                _bot.Events.UpdateEvents.OnChatJoinRequestHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.ChatMember)
                _bot.Events.UpdateEvents.OnChatMemberHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.ChosenInlineResult)
                _bot.Events.UpdateEvents.OnChosenInlineResultHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.ChatBoost)
                _bot.Events.UpdateEvents.OnChatBoostHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.EditedChannelPost)
                _bot.Events.UpdateEvents.OnEditedChannelPostHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.EditedMessage)
                _bot.Events.UpdateEvents.OnEditedMessageHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.BusinessConnection)
                _bot.Events.UpdateEvents.OnBusinessConnectionHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.EditedBusinessMessage)
                _bot.Events.UpdateEvents.OnEditedBusinessHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.DeletedBusinessMessages)
                _bot.Events.UpdateEvents.OnDeletedBusinessConnectionHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.MessageReaction)
                _bot.Events.UpdateEvents.OnMessageReactionHandleHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.MessageReactionCount)
                _bot.Events.UpdateEvents.OnMessageReactionCountHandleHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.InlineQuery)
                _bot.Events.UpdateEvents.OnInlineQueryHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.MyChatMember)
                _bot.Events.UpdateEvents.OnMyChatMemberHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.Poll)
                _bot.Events.UpdateEvents.OnPollHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.PollAnswer)
                _bot.Events.UpdateEvents.OnPollAnswerHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.PreCheckoutQuery)
                _bot.Events.UpdateEvents.OnPreCheckoutQueryHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.RemovedChatBoost)
                _bot.Events.UpdateEvents.OnRemovedChatBoostHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.ShippingQuery)
                _bot.Events.UpdateEvents.OnShippingQueryHandler(new BotEventArgs(_bot, update));

            if (update.Type == UpdateType.Unknown)
                _bot.Events.UpdateEvents.OnUnknownHandler(new BotEventArgs(_bot, update));

            _bot.Events.UpdateEvents.OnPostInvoke(new BotEventArgs(_bot, update));
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public Handler(PRBotBase bot)
        {
            _bot = bot;
            Middleware = new MiddlewareBuilder().Build(bot.Options.Middlewares);

            CallbackQueryCommandsStore = new CallbackQueryCommandStore(bot);
            ReplyCommandsStore = new ReplyCommandStore(bot);
            ReplyDynamicCommandsStore = new ReplyDynamicCommandStore(bot);
            SlashCommandsStore = new SlashCommandStore(bot);

            MessageDispatcher = new MessageUpdateDispatcher(bot);
            CallBackQueryDispatcher = new CallBackQueryUpdateDispatcher(bot);
            HotReload();
        }

        #endregion
    }
}
