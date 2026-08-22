using PRTelegramBot.Converters.Json;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.EventsArgs;
using System.Text.Json.Serialization;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Creates a button that carries data to be handled.
    /// </summary>
    /// <typeparam name="T">Data type.</typeparam>
    public class InlineCallback<T> : InlineCallback
        where T : TCommandBase
    {
        #region Fields and properties

        /// <summary>
        /// The data to process.
        /// </summary>
        [JsonPropertyName("d")]
        public new T Data { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Converts the data into a command.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <returns>InlineCallback, or null.</returns>
        public new static InlineCallback<T> GetCommandByCallbackOrNull(string data)
        {
            return CurrentScope.Bot.GetInlineConverter().GetCommandByCallbackOrNull<T>(data);
        }

        /// <summary>
        /// Converts the data into a command.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>InlineCallback, or null.</returns>
        public new static InlineCallback<T> GetCommandByCallbackOrNull(IBotContext context)
        {
            return GetCommandByCallbackOrNull(context?.Update?.CallbackQuery?.Data ?? string.Empty);
        }

        /// <summary>
        /// Converts the data into a command.
        /// </summary>
        /// <returns>InlineCallback, or null.</returns>
        public new InlineCallback<T> GetCommandByCallbackOrNull()
        {
            return GetCommandByCallbackOrNull(Context?.Update?.CallbackQuery?.Data ?? string.Empty);
        }

        /// <inheritdoc />
        public override object GetContent()
        {
            return CurrentScope.Bot.GetInlineConverter().GenerateCallbackData<T>(this);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        /// <param name="commandType">Command header.</param>
        /// <param name="data">Data.</param>
        public InlineCallback(string buttonName, Enum commandType, T data) : base(buttonName, commandType, data)
        {
            ButtonName = buttonName;
            CommandType = commandType;
            Data = data;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        /// <param name="commandType">Command header.</param>
        public InlineCallback(string buttonName, Enum commandType) : base(buttonName, commandType)
        {
            ButtonName = buttonName;
            CommandType = commandType;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Bot context.</param>
        public InlineCallback(IBotContext context) : base(context)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        public InlineCallback() { }

        #endregion
    }

    /// <summary>
    /// Creates a button that carries data to be handled.
    /// </summary>
    public class InlineCallback : InlineBase, IInlineContent, IDisposable
    {
        #region Fields and properties

        /// <summary>
        /// Command type.
        /// </summary>
        [JsonPropertyName("c")]
        [JsonConverter(typeof(HeaderConverter))]
        public Enum CommandType { get; set; }

        /// <summary>
        /// The data to process.
        /// </summary>
        [JsonPropertyName("d")]
        public TCommandBase Data { get; set; }

        /// <summary>
        /// Update.
        /// </summary>
        [JsonIgnore]
        public IBotContext Context { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Converts the data into a command.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <returns>InlineCallback, or null.</returns>
        public static InlineCallback GetCommandByCallbackOrNull(string data)
        {
            return CurrentScope.Bot.GetInlineConverter().GetCommandByCallbackOrNull(data);
        }

        /// <summary>
        /// Converts the data into a command.
        /// </summary>
        /// <param name="context">Bot context.</param>
        /// <returns>InlineCallback, or null.</returns>
        public static InlineCallback GetCommandByCallbackOrNull(IBotContext context)
        {
            return GetCommandByCallbackOrNull(context?.Update?.CallbackQuery?.Data ?? string.Empty);
        }

        /// <summary>
        /// Converts the data into a command.
        /// </summary>
        /// <returns>InlineCallback, or null.</returns>
        public InlineCallback GetCommandByCallbackOrNull()
        {
            return GetCommandByCallbackOrNull(Context?.Update?.CallbackQuery?.Data ?? string.Empty);
        }

        /// <summary>
        /// Action to perform on the last message.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteActionWithLastMessage()
        {
            if (Context is null || Data is null || Context.Update?.CallbackQuery is null)
                return;

            try
            {
                var lastMessage = Context.Update.CallbackQuery.Message;
                var actionWithLastMessage = Data.GetActionWithLastMessage();
                if (actionWithLastMessage == Enums.ActionWithLastMessage.Delete)
                    await Context.BotClient.DeleteMessage(Context.Update.GetChatIdClass(), lastMessage.MessageId);
            }
            catch (Exception ex)
            {
                Context.Current.GetLogger<InlineCallback>().LogErrorInternal(ex);
            }
        }

        /// <summary>
        /// Attempts to update the data.
        /// </summary>
        public void TryUpdateData()
        {
            var command = GetCommandByCallbackOrNull();
            if (command is not null)
            {
                Data = command.Data;
            }
        }

        #endregion

        #region IInlineContent

        /// <inheritdoc />
        public virtual object GetContent()
        {
            return CurrentScope.Bot.GetInlineConverter().GenerateCallbackData(this);
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithCallbackData(ButtonName, GetContent() as string);
        }

        #endregion

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            _ = ExecuteActionWithLastMessage();
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        /// <param name="commandType">Command header.</param>
        /// <param name="data">Data.</param>
        public InlineCallback(string buttonName, Enum commandType, TCommandBase data)
            : base(buttonName)
        {
            CommandType = commandType;
            Data = data;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        /// <param name="commandType">Command header.</param>
        public InlineCallback(string buttonName, Enum commandType)
            : base(buttonName)
        {
            CommandType = commandType;
            Data = new TCommandBase();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Bot context.</param>
        public InlineCallback(IBotContext context)
        {
            Context = context;
            TryUpdateData();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public InlineCallback() : base() { }

        #endregion
    }
}