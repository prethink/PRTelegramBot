﻿using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Core.CommandStores;
using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Core.UpdateDispatchers;
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
    /// Handler.
    /// </summary>
    public sealed class Handler : IPRUpdateHandler
    {
        #region Fields and properties
        
        /// <summary>
        /// Store for callbackQuery commands.
        /// </summary>
        public CallbackQueryCommandStore CallbackQueryCommandsStore { get; private set; }

        /// <summary>
        /// Store for reply commands.
        /// </summary>
        public ReplyCommandStore ReplyCommandsStore { get; private set; }

        /// <summary>
        /// Store for dynamic reply commands.
        /// </summary>
        public ReplyDynamicCommandStore ReplyDynamicCommandsStore { get; private set; }

        /// <summary>
        /// Store for slash commands.
        /// </summary>
        public SlashCommandStore SlashCommandsStore { get; private set; }

        /// <summary>
        /// Dispatcher that handles message-type updates.
        /// </summary>
        internal MessageUpdateDispatcher MessageDispatcher { get; private set; }

        /// <summary>
        /// Dispatcher that handles callbackQuery-type updates.
        /// </summary>
        internal CallBackQueryUpdateDispatcher CallBackQueryDispatcher { get; private set; }

        /// <summary>
        /// Log spam limiter.
        /// </summary>
        private DateTime lastErrorPollingDate;

        /// <summary>
        /// Bot.
        /// </summary>
        private readonly PRBotBase bot;

        #endregion

        #region IPRUpdateHandler

        /// <summary>
        /// Update handler.
        /// </summary>
        /// <param name="botClient">Telegram bot client.</param>
        /// <param name="update">Telegram update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update == null)
                return Task.CompletedTask;

            // Pairs the update with its ITelegramBotClient.
            update.AddTelegramClient(bot);
            var context = new BotContext(bot, update, cancellationToken);
            _ = HandleUpdateInternalAsync(context);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Handles the update on a separate thread.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>Task.</returns>
        /// <remarks>Needed so that a single update cannot stall processing for the whole application.</remarks>
        private async Task HandleUpdateInternalAsync(BotContext context)
        {
            using (var scope = new BotDataScope(context, bot))
            {
                try
                {
                    var middlewares = new MiddlewareBuilder().Build(bot);
                    await middlewares.InvokeOnPreUpdateAsync(context, async () =>
                    {
                        await UpdateAsync(context);
                    });
                }
                catch (Exception ex)
                {
                    bot.GetLogger<Handler>().LogErrorInternal(ex);
                }
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
        /// API error handler.
        /// </summary>
        /// <param name="botClient">Telegram bot client.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="source">Source of the error</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            if (source == HandleErrorSource.PollingError &&  exception.Message.Contains("Exception during making request"))
            {
                if (DateTime.Now < lastErrorPollingDate)
                    return Task.CompletedTask;

                lastErrorPollingDate = DateTime.Now.AddMinutes(bot.Options.AntiSpamErrorMinute);
            }
            bot.GetLogger<Handler>().LogErrorInternal(exception);
            
            return Task.CompletedTask;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles updates.
        /// </summary>
        /// <param name="context">Bot context.</param>
        public async Task UpdateAsync(IBotContext context)
        {
            var whiteListManager = bot.GetWhiteListManager();
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
                await CallBackQueryDispatcher.Dispatch(context);

            if (update.Type == UpdateType.Message)
                await MessageDispatcher.Dispatch(context);

            if (update.Type == UpdateType.ChannelPost)
                await bot.Events.UpdateEvents.OnChannelPostHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.ChatJoinRequest)
                await bot.Events.UpdateEvents.OnChatJoinRequestHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.ChatMember)
                await bot.Events.UpdateEvents.OnChatMemberHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.ChosenInlineResult)
                await bot.Events.UpdateEvents.OnChosenInlineResultHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.ChatBoost)
                await bot.Events.UpdateEvents.OnChatBoostHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.EditedChannelPost)
                await bot.Events.UpdateEvents.OnEditedChannelPostHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.EditedMessage)
                await bot.Events.UpdateEvents.OnEditedMessageHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.BusinessConnection)
                await bot.Events.UpdateEvents.OnBusinessConnectionHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.BusinessMessage)
                await bot.Events.UpdateEvents.OnBusinessMessageHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.EditedBusinessMessage)
                await bot.Events.UpdateEvents.OnEditedBusinessHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.DeletedBusinessMessages)
                await bot.Events.UpdateEvents.OnDeletedBusinessConnectionHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.MessageReaction)
                await bot.Events.UpdateEvents.OnMessageReactionHandleHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.MessageReactionCount)
                await bot.Events.UpdateEvents.OnMessageReactionCountHandleHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.InlineQuery)
                await bot.Events.UpdateEvents.OnInlineQueryHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.MyChatMember)
                await bot.Events.UpdateEvents.OnMyChatMemberHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.Poll)
                await bot.Events.UpdateEvents.OnPollHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.PollAnswer)
                await bot.Events.UpdateEvents.OnPollAnswerHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.PreCheckoutQuery)
                await bot.Events.UpdateEvents.OnPreCheckoutQueryHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.RemovedChatBoost)
                await bot.Events.UpdateEvents.OnRemovedChatBoostHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.ShippingQuery)
                await bot.Events.UpdateEvents.OnShippingQueryHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.PurchasedPaidMedia)
                await bot.Events.UpdateEvents.OnPurchasedPaidMediaHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.Unknown)
                await bot.Events.UpdateEvents.OnUnknownHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.ManagedBot)
                await bot.Events.UpdateEvents.OnManagedBotHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.GuestMessage)
                await bot.Events.UpdateEvents.OnGuestMessageHandler(context.CreateBotEventArgs());

            if (update.Type == UpdateType.Subscription)
                await bot.Events.UpdateEvents.OnSubscriptionHandler(context.CreateBotEventArgs());

            await bot.Events.UpdateEvents.OnPostInvoke(context.CreateBotEventArgs());
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public Handler(PRBotBase bot)
        {
            this.bot = bot;

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
