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
        public IPRSerializator Serializator { get; }

        /// <summary>
        /// Установить глобальный сериализатор.
        /// </summary>
        /// <param name="serializator">Сеализатор.</param>
        void SetSerializator(IPRSerializator serializator);
    }
}
