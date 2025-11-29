using PRTelegramBot.Interfaces;
using System.Text.Json;

namespace PRTelegramBot.Wrappers
{
    /// <summary>
    /// Сериализатор данных Json.
    /// </summary>
    public class JsonSerializerWrapper : IPRSerializer
    {
        #region Поля и свойства

        /// <summary>
        /// Опции сериализации. 
        /// </summary>
        private readonly JsonSerializerOptions options;

        #endregion

        #region IPRSerializator

        /// <inheritdoc />
        public T Deserialize<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data, options);
        }

        /// <inheritdoc />
        public string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize<T>(data, options);
        }

        #endregion

        #region Конструкторы    

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="options">Параметры сериализации.</param>
        public JsonSerializerWrapper(JsonSerializerOptions options = null)
        {
            this.options = options;
        }

        #endregion
    }
}
