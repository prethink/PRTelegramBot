using PRTelegramBot.Core.CommandStores;
using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Core.UpdateHandlers;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
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
        private DateTime lastErrorPollingDate;

        /// <summary>
        /// Бот.
        /// </summary>
        private readonly PRBotBase bot;

        #endregion

        #region IPRUpdateHandler

        /// <inheritdoc />
        public MiddlewareBase Middleware { get; }

        /// <summary>
        /// Обработчик обновлений.
        /// </summary>
        /// <param name="botClient">Клиент telegram бота.</param>
        /// <param name="update">Обновление telegram.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        public Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update == null)
                return Task.CompletedTask;

            // Связь update вместе ITelegramBotClient.
            update.AddTelegramClient(bot);
            var context = new BotContext(bot, update, cancellationToken);
            _ = HandleUpdateInternalAsync(context);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Обработать update в отдельном потоке.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Task.</returns>
        /// <remarks>Требуется, чтобы 1 update не повесил обработку всего приложения.</remarks>
        private async Task HandleUpdateInternalAsync(BotContext context)
        {
            try
            {
                await Middleware.InvokeOnPreUpdateAsync(context, async () =>
                {
                    await UpdateAsync(context);
                });
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(new ErrorLogEventArgs(context, ex));
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
        /// <param name="botClient">Клиент telegram бота.</param>
        /// <param name="exception">Исключение.</param>
        /// <param name="source">Исходник ошибки</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            if (source == HandleErrorSource.PollingError &&  exception.Message.Contains("Exception during making request"))
            {
                if (DateTime.Now < lastErrorPollingDate)
                    return Task.CompletedTask;

                lastErrorPollingDate = DateTime.Now.AddMinutes(bot.Options.AntiSpamErrorMinute);
            }
            bot.Events.OnErrorLogInvoke(ErrorLogEventArgs.Create(bot, exception, cancellationToken));
            
            return Task.CompletedTask;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обработка обновлений.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        public async Task UpdateAsync(IBotContext context)
        {
            var whiteListManager = bot.Options.WhiteListManager;
            var update = context.Update;
            if (bot.Events.UpdateEvents.HasEventOnPreUpdate())
            {
                var resultUpdate = await bot.Events.UpdateEvents.OnPreInvoke(context.CreateBotEventArgs());

                if (resultUpdate is UpdateResult.Stop or UpdateResult.Handled)
                    return;
            }

            if (whiteListManager.Settings == WhiteListSettings.OnPreUpdate && whiteListManager.Count > 0)
            {
                var hasUserInWhiteList = await whiteListManager.HasUser(update.GetChatId());
                if (!hasUserInWhiteList)
                {
                    bot.Events.OnAccessDeniedInvoke(context.CreateBotEventArgs());
                    return;
                }
            }

            if (update.Type == UpdateType.CallbackQuery)
                _ = CallBackQueryDispatcher.Dispatch(context);

            if (update.Type == UpdateType.Message)
                _ = MessageDispatcher.Dispatch(context);

            if (update.Type == UpdateType.ChannelPost)
                bot.Events.UpdateEvents.OnChannelPostHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.ChatJoinRequest)
                bot.Events.UpdateEvents.OnChatJoinRequestHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.ChatMember)
                bot.Events.UpdateEvents.OnChatMemberHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.ChosenInlineResult)
                bot.Events.UpdateEvents.OnChosenInlineResultHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.ChatBoost)
                bot.Events.UpdateEvents.OnChatBoostHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.EditedChannelPost)
                bot.Events.UpdateEvents.OnEditedChannelPostHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.EditedMessage)
                bot.Events.UpdateEvents.OnEditedMessageHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.BusinessConnection)
                bot.Events.UpdateEvents.OnBusinessConnectionHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.EditedBusinessMessage)
                bot.Events.UpdateEvents.OnEditedBusinessHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.DeletedBusinessMessages)
                bot.Events.UpdateEvents.OnDeletedBusinessConnectionHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.MessageReaction)
                bot.Events.UpdateEvents.OnMessageReactionHandleHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.MessageReactionCount)
                bot.Events.UpdateEvents.OnMessageReactionCountHandleHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.InlineQuery)
                bot.Events.UpdateEvents.OnInlineQueryHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.MyChatMember)
                bot.Events.UpdateEvents.OnMyChatMemberHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.Poll)
                bot.Events.UpdateEvents.OnPollHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.PollAnswer)
                bot.Events.UpdateEvents.OnPollAnswerHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.PreCheckoutQuery)
                bot.Events.UpdateEvents.OnPreCheckoutQueryHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.RemovedChatBoost)
                bot.Events.UpdateEvents.OnRemovedChatBoostHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.ShippingQuery)
                bot.Events.UpdateEvents.OnShippingQueryHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.Unknown)
                bot.Events.UpdateEvents.OnUnknownHandler(context.CreateBotEventArgs());

            bot.Events.UpdateEvents.OnPostInvoke(context.CreateBotEventArgs());
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

            MessageDispatcher = new MessageUpdateDispatcher(bot);
            CallBackQueryDispatcher = new CallBackQueryUpdateDispatcher();
            HotReload();
        }

        #endregion
    }
}
