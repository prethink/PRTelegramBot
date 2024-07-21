using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс обработчика для callbackQuery команд.
    /// </summary>
    public interface ICallbackQueryCommandHandler : ICommandHandlerBase<CallbackQuery>
    {

    }
}
