namespace PRTelegramBot.Interfaces
{
    internal interface IStringQueryAttribute : IBaseQueryAttribute
    {
        public StringComparison StringComparison { get; }
    }
}
