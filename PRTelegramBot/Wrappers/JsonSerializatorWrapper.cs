using PRTelegramBot.Interfaces;
using System.Text.Json;

namespace PRTelegramBot.Wrappers
{
    public class JsonSerializatorWrapper : IPRSerializator
    {
        public T Deserialize<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data);
        }

        public string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize<T>(data);
        }
    }
}
