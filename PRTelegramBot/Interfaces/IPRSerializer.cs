namespace PRTelegramBot.Interfaces
{
    /// <summary>
    /// Интерфейс обертки сериализатора.
    /// </summary>
    public interface IPRSerializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        T Deserialize<T>(string data);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        string Serialize<T>(T data);
    }
}
