using PRTelegramBot.Interfaces;
using ToonNetSerializer;

namespace PRTelegramBot.Wrappers
{
    /// <summary>
    /// Toon data serializer.
    /// </summary>
    public class ToonSerializerWrapper : IPRSerializer
    {
        #region Fields and properties

        /// <summary>
        /// Serialization options. 
        /// </summary>
        private readonly ToonDecodeOptions decodeOptions;

        /// <summary>
        /// Serialization options. 
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

        #region Constructors    

        /// <summary>
        /// Constructor.    
        /// </summary>
        public ToonSerializerWrapper()
            { }

        /// <summary>
        /// Constructor.    
        /// </summary>
        /// <param name="decodeOptions">Deserialization options.</param>
        public ToonSerializerWrapper(ToonDecodeOptions decodeOptions)
            : this(null, decodeOptions)
                { }

        /// <summary>
        /// Constructor.    
        /// </summary>
        /// <param name="serializeOptions">Serialization options.</param>
        public ToonSerializerWrapper(ToonOptions serializeOptions) 
            : this(serializeOptions, null) 
                { }

        /// <summary>
        /// Constructor.    
        /// </summary>
        /// <param name="serializeOptions">Serialization options.</param>
        /// <param name="decodeOptions">Deserialization options.</param>
        public ToonSerializerWrapper(ToonOptions serializeOptions, ToonDecodeOptions decodeOptions)
        {
            this.serializeOptions = serializeOptions;
            this.decodeOptions = decodeOptions;
        }

        #endregion
    }
}
