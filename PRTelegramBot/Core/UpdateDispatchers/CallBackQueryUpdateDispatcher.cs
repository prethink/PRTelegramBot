using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Core.UpdateHandlers
{
    /// <summary>
    /// Диспатчер update для типа callbackQuery.
    /// </summary>
    internal sealed class CallBackQueryUpdateDispatcher
    {
        #region Методы

        /// <summary>
        /// Отправить update на обработку.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        public async Task<UpdateResult> Dispatch(IBotContext context)
        {
            try
            {
                context.Current.Events.UpdateEvents.OnCallbackQueryHandler(context.CreateBotEventArgs());
                var result = UpdateResult.Continue;
                foreach (var handler in context.Current.Options.CallbackQueryHandlers)
                {
                    result = await handler.Handle(context, context.Update.CallbackQuery);
                    if (!result.IsContinueHandle(context))
                        return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                context.Current.GetLogger<CallBackQueryUpdateDispatcher>().LogErrorInternal(ex);
                return UpdateResult.Error;
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public CallBackQueryUpdateDispatcher() { }

        #endregion
    }
}
