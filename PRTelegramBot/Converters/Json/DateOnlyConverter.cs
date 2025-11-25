using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Конвертер даты в json.
/// </summary>
public class DateOnlyConverter : JsonConverter<DateTime>
{
    #region Константы

    /// <summary>
    /// Формат даты.
    /// </summary>
    private const string DATE_FORMAT = "yyyy-MM-dd";

    #endregion

    #region Базовый класс

    /// <inheritdoc />
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString()).Date;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DATE_FORMAT));
    }

    #endregion
}
