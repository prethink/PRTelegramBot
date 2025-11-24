using PRTelegramBot.Interfaces;
using ToonNetSerializer;

namespace PRTelegramBot.Wrappers
{
    public class ToonSerializatorWrapper : IPRSerializator
    {
        public T Deserialize<T>(string data)
        {
            return ToonNet.Decode<T>(data);
        }

        public string Serialize<T>(T data)
        {
            return ToonNet.Encode(data);
        }
    }
}
