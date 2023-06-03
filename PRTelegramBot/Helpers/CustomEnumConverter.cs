using Newtonsoft.Json;



namespace PRTelegramBot.Helpers
{
    public class HeaderConverter : JsonConverter<Enum>
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override Enum ReadJson(JsonReader reader, Type objectType, Enum existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // Обработка десериализации JSON в тип Enum
            if (reader.TokenType == JsonToken.Integer)
            {
                int numericValue = Convert.ToInt32(reader.Value);
                return EnumHeaders.Instance.Get(numericValue);
            }
            else
            {
                throw new JsonSerializationException($"Unable to deserialize Enum. Unexpected token type: {reader.TokenType}");
            }
        }

        public override void WriteJson(JsonWriter writer, Enum value, JsonSerializer serializer)
        {
            // Обработка сериализации типа Enum в JSON
            if (value != null)
            {
                writer.WriteValue(EnumHeaders.Instance.Get(value));
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}
