namespace PRTelegramBot.Interfaces
{
    internal interface IStringQueryAttribute : IBaseQueryAttribute
    {
        /// <summary>
        /// The string comparison type.
        /// </summary>
        public StringComparison StringComparison { get; }
    }
}
