using PRTelegramBot.Converters;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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

        /// <summary>
        /// Преобразовать данные в команду.
        /// </summary>
        /// <returns>InlineCallback или null.</returns>
        public InlineCallback<T> GetCommandByCallbackOrNull()
        {
            return GetCommandByCallbackOrNull(Update.CallbackQuery.Data);
        }

        public override object GetContent()
        {
            var result = JsonSerializer.Serialize<InlineCallback<T>>(this);
            ThrowExceptionIfBytesMore128(result);
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
        public InlineCallback(string buttonName, Enum commandType, T data) : base(buttonName, commandType, data)
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
        public InlineCallback(string buttonName, Enum commandType) : base(buttonName, commandType)
        {
            ButtonName = buttonName;
            CommandType = commandType;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botClient">Bot client.</param>
        /// <param name="update">Update.</param>
        public InlineCallback(ITelegramBotClient botClient, Update update)
            : base(botClient,update) {}

        /// <summary>
        /// Конструктор.
        /// </summary>
        public InlineCallback() { }

        #endregion
    }

    /// <summary>
    /// Создает кнопку обработкой данных.
    /// </summary>
    public class InlineCallback : InlineBase, IInlineContent, IDisposable
    {
        #region Константы

        /// <summary>
        /// Максимальный допустимый размер данных для обработки.
        /// </summary>
        public const int MAX_SIZE_CALLBACK_DATA = 128;

        #endregion

        #region Поля и свойства

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

        /// <summary>
        /// Update.
        /// </summary>
        [JsonIgnore]
        public Update Update { get; private set; }

        /// <summary>
        /// Update.
        /// </summary>
        [JsonIgnore]
        public ITelegramBotClient BotClient { get; private set; }

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

        /// <summary>
        /// Преобразовать данные в команду.
        /// </summary>
        /// <returns>InlineCallback или null.</returns>
        public InlineCallback GetCommandByCallbackOrNull()
        {
            return GetCommandByCallbackOrNull(Update.CallbackQuery.Data);
        }

        /// <summary>
        /// Выбросить исключение если результат больше чем 128 байт.
        /// </summary>
        /// <param name="result">Результат.</param>
        /// <exception cref="Exception">Исключение.</exception>
        protected void ThrowExceptionIfBytesMore128(string result)
        {
            var byteSize = result.Length * sizeof(char);
            if (byteSize > MAX_SIZE_CALLBACK_DATA)
                throw new Exception($"Callback_data limit exceeded {byteSize} > {MAX_SIZE_CALLBACK_DATA}. Try reducing the amount of data in the command.");
        }

        /// <summary>
        /// Действие с последним сообщением.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteActionWithLastMessage()
        {
            if (Update?.CallbackQuery == null || BotClient == null || Data == null)
                return;

            try
            {
                var lastMessage = Update.CallbackQuery.Message;
                var actionWithLastMessage = Data.GetActionWithLastMessage();
                if (actionWithLastMessage == Enums.ActionWithLastMessage.Delete)
                    await BotClient.DeleteMessageAsync(Update.GetChatIdClass(), lastMessage.MessageId);
            }
            catch (Exception ex)
            {
                BotClient.GetBotDataOrNull().Events.OnErrorLogInvoke(ex);
            }
        }

        /// <summary>
        /// Попытка обновить данные.
        /// </summary>
        public void TryUpdateData()
        {
            var command = GetCommandByCallbackOrNull();
            if(command != null)
            {
                Data = command.Data;
            }
        }

        #endregion

        #region IInlineContent

        public virtual object GetContent()
        {
            var result = JsonSerializer.Serialize(this);
            ThrowExceptionIfBytesMore128(result);   
            return result;
        }

        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithCallbackData(ButtonName, GetContent() as string);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _ = ExecuteActionWithLastMessage();
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="commandType">Заголовок команды.</param>
        /// <param name="data">Данные.</param>
        public InlineCallback(string buttonName, Enum commandType, TCommandBase data)
            : base(buttonName)
        {
            CommandType = commandType;
            Data = data;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="commandType">Заголовок команды.</param>
        public InlineCallback(string buttonName, Enum commandType) 
            : base(buttonName)
        {
            CommandType = commandType;
            Data = new TCommandBase();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="botClient">Bot client.</param>
        /// <param name="update">Update.</param>
        public InlineCallback(ITelegramBotClient botClient, Update update)
        {
            Update = update;
            BotClient = botClient;
            TryUpdateData();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public InlineCallback() : base() { }

        #endregion
    }
}