using PRTelegramBot.Interfaces;
using ToonNetSerializer;

namespace PRTelegramBot.Wrappers
{
    /// <summary>
    /// Сериализатор данных Toon.
    /// </summary>
    public class ToonSerializerWrapper : IPRSerializer
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
