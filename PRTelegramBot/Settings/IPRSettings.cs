using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Providers
{
    /// <summary>
    /// Глобальные настройки.
    /// </summary>
    public interface IPRSettings
    {
        /// <summary>
        /// Глобальный сериализатор.    
        /// </summary>
        public IPRSerializer Serializator { get; }

        /// <summary>
        /// Установить глобальный сериализатор.
        /// </summary>
        /// <param name="serializator">Сериализатор.</param>
        void SetSerializator(IPRSerializer serializator);
    }
}
