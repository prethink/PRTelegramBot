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
        public new T Data { get; set; }

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
        /// <param name="commandType">Заголовок команды.</param>
        /// <param name="data">Данные.</param>
        [JsonConstructor]
        public InlineCallback(string buttonName, Enum commandType, T data) : base(buttonName, commandType, data)
        {
            ButtonName = buttonName;
            CommandType = commandType;
            Data = data;
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
        public Enum CommandType { get; set; }

        /// <summary>
        /// Данные для обработки.
        /// </summary>
        [JsonPropertyName("d")]
        public TCommandBase Data { get; set; }

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
                return JsonSerializer.Deserialize<InlineCallback>(data);
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
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="commandType">Заголовок команды.</param>
        /// <param name="data">Данные.</param>
        [JsonConstructor]
        public InlineCallback(string buttonName, Enum commandType, TCommandBase data)
        {
            ButtonName = buttonName;
            CommandType = commandType;
            Data = data;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="commandType">Заголовок команды.</param>
        public InlineCallback(string buttonName, Enum commandType)
        {
            ButtonName = buttonName;
            CommandType = commandType;
            Data = new TCommandBase();
        }

        #endregion
    }
}
