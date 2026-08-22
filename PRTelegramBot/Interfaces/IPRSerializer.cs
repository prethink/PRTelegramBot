namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Interface of the serializer wrapper.
    /// </summary>
    public interface IPRSerializer
    {
        /// <summary>
        /// Deserializes the string representation of an object into an instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type the data has to be converted into.</typeparam>
        /// <param name="data">A string containing the serialized data.</param>
        /// <returns>An object of type <typeparamref name="T"/>.</returns>
        T Deserialize<T>(string data);

        /// <summary>
        /// Serializes an object of type <typeparamref name="T"/> into a string.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="data">The object to serialize.</param>
        /// <returns>A string holding the serialized representation of the object.</returns>
        string Serialize<T>(T data);
    }
}
