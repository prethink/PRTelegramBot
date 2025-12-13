namespace PRTelegramBot.Interfaces
{
    public interface IInlineStorage
    {
        void Save<T>(string userKey, T data);
        T Load<T>(string userKey);
        bool TryLoad<T>(string userKey, out T data);
        void Delete(string userKey);
    }
}
