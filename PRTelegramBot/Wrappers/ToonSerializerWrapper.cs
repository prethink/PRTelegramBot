using PRTelegramBot.Interfaces;
using ToonNetSerializer;

namespace PRTelegramBot.Wrappers
{
    /// <summary>
    /// Сериализатор данных Toon.
    /// </summary>
    public class ToonSerializerWrapper : IPRSerializer
    {
        #region Поля и свойства

        /// <summary>
        /// Опции сериализации. 
        /// </summary>
        private readonly ToonDecodeOptions decodeOptions;

        /// <summary>
        /// Опции сериализации. 
        /// </summary>
        private readonly ToonOptions serializeOptions;

        #endregion

        #region IPRSerializator

        /// <inheritdoc />
        public T Deserialize<T>(string data)
        {
            return ToonNet.Decode<T>(data, decodeOptions);
        }

        /// <inheritdoc />
        public string Serialize<T>(T data)
        {
            return serializeOptions == null 
                ? ToonNet.Encode(data)
                : ToonNet.Encode(data, serializeOptions);
        }

        #endregion

        #region Конструкторы    

        /// <summary>
        /// Конструктор.    
        /// </summary>
        public ToonSerializerWrapper()
            { }

        /// <summary>
        /// Конструктор.    
        /// </summary>
        /// <param name="decodeOptions">Параметры десериализации.</param>
        public ToonSerializerWrapper(ToonDecodeOptions decodeOptions)
            : this(null, decodeOptions)
                { }

        /// <summary>
        /// Конструктор.    
        /// </summary>
        /// <param name="serializeOptions">Параметры сериализации.</param>
        public ToonSerializerWrapper(ToonOptions serializeOptions) 
            : this(serializeOptions, null) 
                { }

        /// <summary>
        /// Конструктор.    
        /// </summary>
        /// <param name="serializeOptions">Параметры сериализации.</param>
        /// <param name="decodeOptions">Параметры десериализации.</param>
        public ToonSerializerWrapper(ToonOptions serializeOptions, ToonDecodeOptions decodeOptions)
        {
            this.serializeOptions = serializeOptions;
            this.decodeOptions = decodeOptions;
        }

        #endregion
    }
}
