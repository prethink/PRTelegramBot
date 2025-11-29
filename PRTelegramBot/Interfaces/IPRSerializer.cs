namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс обертки сериализатора.
    /// </summary>
    public interface IPRSerializer
    {
        /// <summary>
        /// Десериализует строковое представление объекта в экземпляр типа <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта, в который нужно преобразовать данные.</typeparam>
        /// <param name="data">Строка, содержащая сериализованные данные.</param>
        /// <returns>Объект типа <typeparamref name="T"/>.</returns>
        T Deserialize<T>(string data);

        /// <summary>
        /// Сериализует объект типа <typeparamref name="T"/> в строку.
        /// </summary>
        /// <typeparam name="T">Тип объекта, который нужно сериализовать.</typeparam>
        /// <param name="data">Объект для сериализации.</param>
        /// <returns>Строка с сериализованным представлением объекта.</returns>
        string Serialize<T>(T data);
    }
}
