namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс провайдера конфигурации бота.
    /// </summary>
    public interface IBotConfigProvider
    {
        /// <summary>
        /// Установить путь до конфигурационного файла.
        /// </summary>
        /// <param name="configPath">Путь до файла.</param>
        public void SetConfigPath(string configPath);

        /// <summary>
        /// Получить параметры из конфигурационного файла.
        /// </summary>
        /// <typeparam name="TOptions">Класс параметров.</typeparam>
        /// <returns>Параметры.</returns>
        public TOptions GetOptions<TOptions>() where TOptions : class;

        /// <summary>
        /// Получить значение из параметра.
        /// </summary>
        /// <typeparam name="TReturn">Тип возращаемого значения.</typeparam>
        /// <param name="optionName">Название параметра.</param>
        /// <returns>Значение параметра.</returns>
        public TReturn GetValue<TReturn>(string optionName);

        /// <summary>
        /// Получить словарь ключ-значение из конфигурационного файла.
        /// </summary>
        /// <returns>Словарь ключ-значение.</returns>
        public Dictionary<string, string> GetKeysAndValues();

        /// <summary>
        /// Получить коллекцию ключ-значение из параметров конфигурационного файла.
        /// </summary>
        /// <typeparam name="TOptions">Класс параметров.</typeparam>
        /// <returns>Словарь ключ-значение.</returns>
        public Dictionary<string, string> GetKeysAndValuesByOptions<TOptions>() where TOptions : class;
    }
}
