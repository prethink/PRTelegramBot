using PRTelegramBot.Core.Middlewares;
using PRTelegramBot.Core.UpdateHandlers;
using PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
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
        /// Фасад для обработки логики сообщений.
        /// </summary>
        public MessageFacade MessageFacade { get; private set; }

        /// <summary>
        /// Обработчик inline команд.
        /// </summary>
        public InlineUpdateHandler InlineUpdateHandler { get; private set; }

        /// <summary>
        /// Бот.
        /// </summary>
        private PRBotBase bot;

        #endregion

        #region IUpdateHandler

        public MiddlewareBase Middleware { get; }

        /// <summary>
        /// Обработчик обновлений.
        /// </summary>
        /// <param name="botClient">Клиент телеграм бота.</param>
        /// <param name="update">Обновление телеграм.</param>
        /// <param name="cancellationToken">Токен.</param>
        /// <returns></returns>
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (update == null)
                    return;

                //Связь update вместе ITelegramBotClient
                update.AddTelegramClient(bot);

                await Middleware.InvokeOnPreUpdateAsync(botClient, update, async () =>
                {
                    await UpdateAsync(update);
                });
            }   
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex, update);
            }
        }

        public async Task UpdateAsync(Update update)
        {
            if (bot.Events.UpdateEvents.HasEventOnPreUpdate())
            {
                var resultUpdate = await bot.Events.UpdateEvents.OnPreInvoke(new BotEventArgs(bot, update));

                if (resultUpdate == UpdateResult.Stop || resultUpdate == UpdateResult.Handled)
                    return;
            }

            if (bot.Options.WhiteListManager.Count > 0)
            {
                if (!(await bot.Options.WhiteListManager.HasUser(update.GetChatId())))
                {
                    bot.Events.OnAccessDeniedInvoke(new BotEventArgs(bot, update));
                    return;
                }
            }

            if (update.Type == UpdateType.CallbackQuery)
                await InlineUpdateHandler.Handle(update);

            if (update.Type == UpdateType.Message)
                await MessageFacade.Handle(update);

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

        /// <summary>
        /// Обработчик ошибок.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="cancellationToken">Токен.</param>
        public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            try
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };
                //TODO Logging exception
                //return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex);
            }
        }

        public void HotReload()
        {
            MessageFacade = new MessageFacade(this.bot);
            InlineUpdateHandler = new InlineUpdateHandler(this.bot);
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
            HotReload();
        }

        #endregion
    }
}
