using PRTelegramBot.Extensions;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot.Types;

namespace PRTelegramBot.Core.UpdateHandlers
{
    /// <summary>
    /// Диспатчер update для типа callbackQuery.
    /// </summary>
    internal sealed class CallBackQueryUpdateDispatcher
    {
        #region Поля и свойства

        /// <summary>
        /// Бот.
        /// </summary>
        private readonly PRBotBase bot;

        #endregion

        #region Методы

        /// <summary>
        /// Отправить update на обработку.
        /// </summary>
        /// <param name="update">Update.</param>
        public async Task<UpdateResult> Dispatch(Update update)
        {
            try
            {
                bot.Events.UpdateEvents.OnCallbackQueryHandler(new BotEventArgs(bot, update));
                var result = UpdateResult.Continue;
                foreach (var handler in bot.Options.CallbackQueryHandlers)
                {
                    result = await handler.Handle(bot, update, update.CallbackQuery);
                    if (!result.IsContinueHandle(bot, update))
                        return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                bot.Events.OnErrorLogInvoke(ex, update);
                return UpdateResult.Error;
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bot">Бот.</param>
        public CallBackQueryUpdateDispatcher(PRBotBase bot)
        {
            this.bot = bot;
        }

        #endregion
    }
}
