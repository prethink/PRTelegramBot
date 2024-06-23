using PRTelegramBot.Converters;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Создает кнопку обработкой данных.
    /// </summary>
    /// <typeparam name="T">Тип данных.</typeparam>
    public class InlineCallback<T> : InlineCallback where T : TCommandBase
    {
        #region Поля и свойства

        /// <summary>
        /// Данные для обработки.
        /// </summary>
        [JsonPropertyName("d")]
        public new T D { get; set; }

        public new T Data => D;

        #endregion

        #region Методы

        /// <summary>
        /// Преобразовать данные в команду.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>InlineCallback или null.</returns>
        public new static InlineCallback<T> GetCommandByCallbackOrNull(string data)
        {
            try
            {
                return JsonSerializer.Deserialize<InlineCallback<T>>(data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="c">Заголовок команды.</param>
        /// <param name="d">Данные.</param>
        [JsonConstructor]
        public InlineCallback(string buttonName, Enum c, T d) : base(buttonName, c, d)
        {
            ButtonName = buttonName;
            C = c;
            D = d;
        }

        #endregion
    }

    /// <summary>
    /// Создает кнопку обработкой данных.
    /// </summary>
    public class InlineCallback : IInlineContent
    {
        #region Константы

        /// <summary>
        /// Максимальный допустимый размер данных для обработки.
        /// </summary>
        public const int MAX_SIZE_CALLBACK_DATA = 128;

        #endregion

        #region Поля и свойства

        /// <summary>
        /// Название кнопки.
        /// </summary>
        [JsonIgnore]
        public string ButtonName { get; set; }

        /// <summary>
        /// Тип команды.
        /// </summary>
        [JsonPropertyName("c")]
        [JsonConverter(typeof(HeaderConverter))]
        [JsonInclude]
        public Enum C { get; set; }

        /// <summary>
        /// Тип команды.
        /// </summary>
        [JsonIgnore]
        public Enum CommandType => C;

        /// <summary>
        /// Данные для обработки.
        /// </summary>
        [JsonPropertyName("d")]
        [JsonInclude]
        public TCommandBase D { get; set; }

        /// <summary>
        /// Данные для обработки.
        /// </summary>
        [JsonIgnore]
        public TCommandBase Data => D;

        #endregion

        #region Методы

        /// <summary>
        /// Преобразовать данные в команду.
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <returns>InlineCallback или null.</returns>
        public static InlineCallback GetCommandByCallbackOrNull(string data)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    //PropertyNameCaseInsensitive = true,
                    IncludeFields = true
                };
                return JsonSerializer.Deserialize<InlineCallback>(data, options);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region IInlineContent

        public string GetTextButton()
        {
            return ButtonName;
        }

        public object GetContent()
        {
            var result = JsonSerializer.Serialize(this);
            var byteSize = result.Length * sizeof(char);
            if (byteSize > MAX_SIZE_CALLBACK_DATA)
                throw new Exception($"Callback_data limit exceeded {byteSize} > {MAX_SIZE_CALLBACK_DATA}. Try reducing the amount of data in the command.");

            return result;
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="commandType">Заголовок команды.</param>
        /// <param name="d">Данные.</param>
        [JsonConstructor]
        public InlineCallback(Enum c, TCommandBase d)
        {
            ButtonName = "";
            C = c;
            D = d;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="commandType">Заголовок команды.</param>
        /// <param name="d">Данные.</param>
        public InlineCallback(string buttonName, Enum c, TCommandBase d)
        {
            ButtonName = buttonName;
            C = c;
            D = d;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="c">Заголовок команды.</param>
        public InlineCallback(string buttonName, Enum c)
        {
            ButtonName = buttonName;
            C = c;
            D = new TCommandBase(0);
        }

        #endregion
    }
}
