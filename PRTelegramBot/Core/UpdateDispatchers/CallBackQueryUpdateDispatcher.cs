using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Core.UpdateHandlers
{
    /// <summary>
    /// Update dispatcher for the callbackQuery type.
    /// </summary>
    internal sealed class CallBackQueryUpdateDispatcher
    {
        #region Methods

        /// <summary>
        /// Sends the update to be handled.
        /// </summary>
        /// <param name="context">Bot context.</param>
        public async Task<UpdateResult> Dispatch(IBotContext context)
        {
            try
            {
                context.Current.Events.UpdateEvents
                    .OnCallbackQueryHandler(context.CreateBotEventArgs())
                    .FireAndForget(context, typeof(CallBackQueryUpdateDispatcher));
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

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public CallBackQueryUpdateDispatcher() { }

        #endregion
    }
}
