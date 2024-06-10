using PRTelegramBot.Core.UpdateHandlers;
using PRTelegramBot.Core.UpdateHandlers.CommandsUpdateHandlers;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Core
{
    /// <summary>
    /// Обработчик.
    /// </summary>
    public sealed class Handler : IUpdateHandler
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
                //Связь update вместе ITelegramBotClient
                update.AddTelegramClient(bot);

                if (bot.Events.HasEventOnPreUpdate())
                {
                    var resultUpdate = await bot.Events.OnPreUpdateInvoke(new BotEventArgs(bot, update));

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
                {
                    await InlineUpdateHandler.Handle(update);
                    return;
                }

                if (update.Type == UpdateType.Message)
                {
                    await MessageFacade.Handle(update);
                    return;
                }

                bot.Events.OnPostMessageUpdateInvoke(new BotEventArgs(bot, update));
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

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public Handler(PRBotBase bot)
        {
            this.bot = bot;
            MessageFacade = new MessageFacade(this.bot);
            InlineUpdateHandler = new InlineUpdateHandler(this.bot);
        }

        #endregion
    }
}
