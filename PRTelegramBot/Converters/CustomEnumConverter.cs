using PRTelegramBot.Utils;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PRTelegramBot.Converters
{
    /// <summary>
    /// Конвертер enum в json.
    /// </summary>
    public sealed class HeaderConverter : JsonConverter<Enum>
    {
        #region Базовый класс

        public override Enum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Обработка десериализации JSON в тип Enum
            if (reader.TokenType == JsonTokenType.Number)
            {
                int numericValue = Convert.ToInt32(reader.GetInt32());
                return EnumHeaders.Instance.Get(numericValue);
            }
            else
            {
                throw new JsonException($"Unable to deserialize Enum. Unexpected token type: {reader.TokenType}");
            }
        }

        public override void Write(Utf8JsonWriter writer, Enum value, JsonSerializerOptions options)
        {
            // Обработка сериализации типа Enum в JSON
            if (value != null)
                writer.WriteNumberValue(EnumHeaders.Instance.Get(value));
            else
                writer.WriteNullValue();
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }

        #endregion
    }
}
