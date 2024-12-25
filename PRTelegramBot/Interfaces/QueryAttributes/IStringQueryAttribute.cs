namespace PRTelegramBot.Interfaces
{
    internal interface IStringQueryAttribute : IBaseQueryAttribute
    {
        /// <summary>
        /// Тип сравнение строки.
        /// </summary>
        public StringComparison StringComparison { get; }
    }
}
