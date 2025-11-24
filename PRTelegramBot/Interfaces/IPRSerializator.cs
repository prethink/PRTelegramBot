namespace PRTelegramBot.Interfaces
{
    public interface IPRSerializator
    {
        T Deserialize<T>(string data);
        string Serialize<T>(T data);
    }
}
