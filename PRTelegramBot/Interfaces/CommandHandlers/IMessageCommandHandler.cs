using Telegram.Bot.Types;

namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface of the handler for message commands.
    /// </summary>
    public interface IMessageCommandHandler : ICommandHandlerBase<Message>
    {
    }
}
