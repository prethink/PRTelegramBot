﻿using PRTelegramBot.Core.UpdateHandlers;
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

                if (bot.Events.UpdateEvents.HasEventOnPreUpdate())
                {
                    var resultUpdate = await bot.Events.UpdateEvents.OnPreUpdateInvoke(new BotEventArgs(bot, update));

                    if (resultUpdate == UpdateResult.Stop || resultUpdate == UpdateResult.Handled)
                        return;
                }

                if (bot.Options.WhiteListUsers.Count > 0)
                {
                    if (!bot.Options.WhiteListUsers.Contains(update.GetChatId()))
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
                    bot.Events.UpdateEvents.OnUpdateChannelPostHandler(new BotEventArgs(bot, update));

                if (update.Type == UpdateType.ChatJoinRequest)
                    bot.Events.UpdateEvents.OnUpdateChatJoinRequestHandler(new BotEventArgs(bot, update));

                if (update.Type == UpdateType.ChatMember)
                    bot.Events.UpdateEvents.OnUpdateChatMemberHandler(new BotEventArgs(bot, update));

                if (update.Type == UpdateType.ChosenInlineResult)
                    bot.Events.UpdateEvents.OnUpdateChosenInlineResultHandler(new BotEventArgs(bot, update));

                if (update.Type == UpdateType.EditedChannelPost)
                    bot.Events.UpdateEvents.OnUpdateEditedChannelPostHandler(new BotEventArgs(bot, update));

                if (update.Type == UpdateType.EditedMessage)
                    bot.Events.UpdateEvents.OnUpdateEditedMessageHandler(new BotEventArgs(bot, update));

                if (update.Type == UpdateType.InlineQuery)
                    bot.Events.UpdateEvents.OnUpdateInlineQueryHandler(new BotEventArgs(bot, update));

                if (update.Type == UpdateType.MyChatMember)
                    bot.Events.UpdateEvents.OnUpdateMyChatMemberHandler(new BotEventArgs(bot, update));

                if (update.Type == UpdateType.Poll)
                    bot.Events.UpdateEvents.OnUpdatePollHandler(new BotEventArgs(bot, update));

                if (update.Type == UpdateType.PollAnswer)
                    bot.Events.UpdateEvents.OnUpdatePollAnswerHandler(new BotEventArgs(bot, update));

                if (update.Type == UpdateType.PreCheckoutQuery)
                    bot.Events.UpdateEvents.OnUpdatePreCheckoutQueryHandler(new BotEventArgs(bot, update));

                if (update.Type == UpdateType.ShippingQuery)
                    bot.Events.UpdateEvents.OnUpdateShippingQuerHandler(new BotEventArgs(bot, update));

                if (update.Type == UpdateType.Unknown)
                    bot.Events.UpdateEvents.OnUpdateUnknownHandler(new BotEventArgs(bot, update));

                bot.Events.UpdateEvents.OnPostUpdateInvoke(new BotEventArgs(bot, update));
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex, update);
            }
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
            HotReload();
        }

        #endregion
    }
}
