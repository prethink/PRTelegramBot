using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface of the handler for callbackQuery commands.
    /// </summary>
    public interface ICallbackQueryCommandHandler : ICommandHandlerBase<CallbackQuery> { }
}
