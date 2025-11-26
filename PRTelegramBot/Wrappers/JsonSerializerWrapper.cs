using PRTelegramBot.Interfaces;
using System.Text.Json;

namespace PRTelegramBot.Wrappers
{
    /// <summary>
    /// Сериализатор данных Json.
    /// </summary>
    public class JsonSerializerWrapper : IPRSerializer
    {
        #region IPRSerializator

        /// <inheritdoc />
        public T Deserialize<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data);
        }

        /// <inheritdoc />
        public string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize<T>(data);
        }

        #endregion

    }
}
