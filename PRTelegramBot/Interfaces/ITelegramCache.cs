namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Кэш для данных.
    /// </summary>
    public interface ITelegramCache
    {
        /// <summary>
        /// Очистка данных
        /// </summary>
        /// <returns>True - данные очищены, False - нет.</returns>
        public bool ClearData();
    }
}
