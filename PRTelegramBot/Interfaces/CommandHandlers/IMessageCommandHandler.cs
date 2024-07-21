using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс обработчика для message команд.
    /// </summary>
    public interface IMessageCommandHandler : ICommandHandlerBase<Message>
    {
    }
}
