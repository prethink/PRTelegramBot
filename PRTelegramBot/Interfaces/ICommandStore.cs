namespace PRTelegramBot.Interfaces
{
    internal interface ICommandStore<T>
    {
        IEnumerable<T> Commands { get; }
    }
}
