using PRTelegramBot.Interfaces;
using ToonNetSerializer;

namespace PRTelegramBot.Wrappers
{
    /// <summary>
    /// Сеализатор данных Toon.
    /// </summary>
    public class ToonSerializatorWrapper : IPRSerializator
    {
        #region IPRSerializator

        /// <inheritdoc />
        public T Deserialize<T>(string data)
        {
            return ToonNet.Decode<T>(data);
        }

        /// <inheritdoc />
        public string Serialize<T>(T data)
        {
            return ToonNet.Encode(data);
        }

        #endregion
    }
}
