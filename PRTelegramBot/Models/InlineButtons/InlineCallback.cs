using Newtonsoft.Json;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Utils.Converters;

namespace PRTelegramBot.Models.InlineButtons
{

    /// <summary>
    /// Создает кнопку обработкой данных
    /// </summary>
    /// <typeparam name="T">Данные которые должны быть переданы</typeparam>
    public class InlineCallback<T> : InlineCallback where T : TCommandBase
    {
        /// <summary>
        /// Данные для обработки
        /// </summary>
        [JsonProperty("d")]
        public new T Data { get; set; }

        [JsonConstructor]
        public InlineCallback(string buttonName, Enum commandType, T data) : base(buttonName, commandType, data)
        {
            ButtonName = buttonName;
            CommandType = commandType;
            Data = data;
        }

        /// <summary>
        /// Преобразует в команду из callback данных
        /// </summary>
        /// <param name="data">Данные</param>
        /// <returns>Команда или null</returns>
        public new static InlineCallback<T> GetCommandByCallbackOrNull(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<InlineCallback<T>>(data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Создает кнопку обработкой данных
    /// </summary>
    public class InlineCallback : IInlineContent
    {
        /// <summary>
        /// Максимальный допустимый размер данных для обработки
        /// </summary>
        public const int MAX_SIZE_CALLBACK_DATA = 128;

        /// <summary>
        /// Название кнопки
        /// </summary>
        [JsonIgnore]
        public string ButtonName { get; set; }

        /// <summary>
        /// Тип команды
        /// </summary>
        [JsonProperty("c")]
        [JsonConverter(typeof(HeaderConverter))]
        public Enum CommandType { get; set; }

        /// <summary>
        /// Данные для обработки
        /// </summary>
        [JsonProperty("d")]
        public TCommandBase Data { get; set; }

        /// <summary>
        /// Создание нового объекта
        /// </summary>
        /// <param name="buttonName">Название кнопки</param>
        /// <param name="commandType">Заголовок команды</param>
        /// <param name="data">Данные</param>
        [JsonConstructor]
        public InlineCallback(string buttonName, Enum commandType, TCommandBase data)
        {
            ButtonName = buttonName;
            CommandType = commandType;
            Data = data;
        }

        /// <summary>
        /// Создание нового объекта
        /// </summary>
        /// <param name="buttonName">Название кнопки</param>
        /// <param name="commandType">Заголовок команды</param>
        public InlineCallback(string buttonName, Enum commandType)
        {
            ButtonName = buttonName;
            CommandType = commandType;
            Data = new TCommandBase();
        }

        public static InlineCallback GetCommandByCallbackOrNull(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<InlineCallback>(data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GetTextButton()
        {
            return ButtonName;
        }

        public object GetContent()
        {
            var result = JsonConvert.SerializeObject(this);
            var byteSize = result.Length * sizeof(char);
            if (byteSize > MAX_SIZE_CALLBACK_DATA)
                throw new Exception($"Callback_data limit exceeded {byteSize} > {MAX_SIZE_CALLBACK_DATA}. Try reducing the amount of data in the command.");

            return result;
        }
    }
}
