using PRTelegramBot.Interfaces;
using System.Text.Json;

namespace PRTelegramBot.Wrappers
{
    /// <summary>
    /// Сеализатор данных Json.
    /// </summary>
    public class JsonSerializatorWrapper : IPRSerializator
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
