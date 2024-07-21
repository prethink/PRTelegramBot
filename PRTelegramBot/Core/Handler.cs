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
        /// Диспетчер для обработки update типа message.
        /// </summary>
        internal MessageUpdateDispatcher MessageDispatcher { get; private set; }

        /// <summary>
        /// Диспетчер для обработки update типа callbackQuery.
        /// </summary>
        internal CallBackQueryUpdateDispatcher CallBackQueryDispatcher { get; private set; }

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
        /// Бот.
        /// </summary>
        private PRBotBase bot;

        #endregion

        #region IPRUpdateHandler

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

                //Связь update вместе ITelegramBotClient
                update.AddTelegramClient(bot);

                _ =  Middleware.InvokeOnPreUpdateAsync(botClient, update, async () =>
                {
                    await UpdateAsync(update);
                });
            }   
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex, update);
            }
        }

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
        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            var bot = botClient.GetBotDataOrNull();
            bot.Events.OnErrorLogInvoke(exception);
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обработка обновлений.
        /// </summary>
        /// <param name="update">Update.</param>
        public async Task UpdateAsync(Update update)
        {
            var whiteListManager = bot.Options.WhiteListManager;

            if (bot.Events.UpdateEvents.HasEventOnPreUpdate())
            {
                var resultUpdate = await bot.Events.UpdateEvents.OnPreInvoke(new BotEventArgs(bot, update));

                if (resultUpdate == UpdateResult.Stop || resultUpdate == UpdateResult.Handled)
                    return;
            }

            if (whiteListManager.Settings == WhiteListSettings.OnPreUpdate && whiteListManager.Count > 0)
            {
                var hasUserInWhiteList = await whiteListManager.HasUser(update.GetChatId());
                if (!hasUserInWhiteList)
                {
                    bot.Events.OnAccessDeniedInvoke(new BotEventArgs(bot, update));
                    return;
                }
            }

            if (update.Type == UpdateType.CallbackQuery)
                _ = CallBackQueryDispatcher.Dispatch(update);

            if (update.Type == UpdateType.Message)
                _ = MessageDispatcher.Dispatch(update);

            if (update.Type == UpdateType.ChannelPost)
                bot.Events.UpdateEvents.OnChannelPostHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.ChatJoinRequest)
                bot.Events.UpdateEvents.OnChatJoinRequestHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.ChatMember)
                bot.Events.UpdateEvents.OnChatMemberHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.ChosenInlineResult)
                bot.Events.UpdateEvents.OnChosenInlineResultHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.ChatBoost)
                bot.Events.UpdateEvents.OnChatBoostHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.EditedChannelPost)
                bot.Events.UpdateEvents.OnEditedChannelPostHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.EditedMessage)
                bot.Events.UpdateEvents.OnEditedMessageHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.BusinessConnection)
                bot.Events.UpdateEvents.OnBusinessConnectionHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.EditedBusinessMessage)
                bot.Events.UpdateEvents.OnEditedBusinessHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.DeletedBusinessMessages)
                bot.Events.UpdateEvents.OnDeletedBusinessConnectionHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.MessageReaction)
                bot.Events.UpdateEvents.OnMessageReactionHandleHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.MessageReactionCount)
                bot.Events.UpdateEvents.OnMessageReactionCountHandleHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.InlineQuery)
                bot.Events.UpdateEvents.OnInlineQueryHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.MyChatMember)
                bot.Events.UpdateEvents.OnMyChatMemberHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.Poll)
                bot.Events.UpdateEvents.OnPollHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.PollAnswer)
                bot.Events.UpdateEvents.OnPollAnswerHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.PreCheckoutQuery)
                bot.Events.UpdateEvents.OnPreCheckoutQueryHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.RemovedChatBoost)
                bot.Events.UpdateEvents.OnRemovedChatBoostHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.ShippingQuery)
                bot.Events.UpdateEvents.OnShippingQueryHandler(new BotEventArgs(bot, update));

            if (update.Type == UpdateType.Unknown)
                bot.Events.UpdateEvents.OnUnknownHandler(new BotEventArgs(bot, update));

            bot.Events.UpdateEvents.OnPostInvoke(new BotEventArgs(bot, update));
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public Handler(PRBotBase bot)
        {
            this.bot = bot;
            Middleware = new MiddlewareBuilder().Build(bot.Options.Middlewares);

            CallbackQueryCommandsStore = new CallbackQueryCommandStore(bot);
            ReplyCommandsStore = new ReplyCommandStore(bot);
            ReplyDynamicCommandsStore = new ReplyDynamicCommandStore(bot);
            SlashCommandsStore = new SlashCommandStore(bot);

            MessageDispatcher = new MessageUpdateDispatcher(this.bot);
            CallBackQueryDispatcher = new CallBackQueryUpdateDispatcher(this.bot);
            HotReload();
        }

        #endregion
    }
}
