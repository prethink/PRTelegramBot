using PRTelegramBot.Interfaces;
using System.Text.Json;

namespace PRTelegramBot.Wrappers
{
    /// <summary>
    /// Json data serializer.
    /// </summary>
    public class JsonSerializerWrapper : IPRSerializer
    {
        #region Fields and properties

        /// <summary>
        /// Serialization options. 
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

        #region Constructors    

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">Serialization options.</param>
        public JsonSerializerWrapper(JsonSerializerOptions options = null)
        {
            this.options = options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public JsonSerializerWrapper()
        {
            
        }

        #endregion
    }
}
