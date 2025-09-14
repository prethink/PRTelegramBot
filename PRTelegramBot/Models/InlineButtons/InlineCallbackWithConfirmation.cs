using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Позволяет выполнить inlineCallBack с подтверждением.
    /// </summary>
    public class InlineCallbackWithConfirmation : InlineCallback<EntityTCommand<string>>, IInlineContent
    {
        #region Поля и свойства

        /// <summary>
        /// Коллекция InlineCallbackWithConfirmation для поиска и обработки данных.
        /// </summary>
        [JsonIgnore]
        public static Dictionary<string, InlineCallbackWithConfirmation> DataCollection = new();

        /// <summary>
        /// Название кнопки да.
        /// </summary>
        [JsonIgnore]
        public string YesButton = "Да";

        /// <summary>
        /// Название кнопки нет.
        /// </summary>
        [JsonIgnore]
        public string NoButton = "Нет";

        /// <summary>
        /// Текст сообщения подтверждения.
        /// </summary>
        [JsonIgnore]
        public string BaseMessage = "Подтвердите действие";

        /// <summary>
        /// Обработка при нажатие на да.
        /// </summary>
        [JsonIgnore]
        public InlineCallback YesCallback { get; set; }

        /// <summary>
        /// Обработка при нажатие на нет.
        /// </summary>
        [JsonIgnore]
        public InlineCallback NoCallback { get; set; }

        #endregion

        #region IInlineContent

        /// <inheritdoc />
        public override object GetContent()
        {
            return base.GetContent();
        }

        #endregion

        #region Базовый класс

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return base.GetInlineButton();
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback кнопка.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack)
            : this(inlineCallBack, ActionWithLastMessage.Nothing, PRTelegramBotCommand.CallbackWithConfirmation) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback кнопка.</param>
        /// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage)
            : this(inlineCallBack, actionWithLastMessage, PRTelegramBotCommand.CallbackWithConfirmation) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback кнопка.</param>
        /// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
        /// <param name="callbackWithConfirmation">Заголовок для обработки подтверждения.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation)
            : base(inlineCallBack.ButtonName, callbackWithConfirmation)
        {
            string guidString = Guid.NewGuid().ToString();
            var id = guidString.Replace("-", string.Empty).Remove(0, guidString.Length / 2);
            YesCallback = inlineCallBack;
            Data = new EntityTCommand<string>(id, actionWithLastMessage);
            DataCollection.Add(id, this);
            NoCallback = new InlineCallback<EntityTCommand<string>>(NoButton, PRTelegramBotCommand.CallbackWithConfirmationResultNo, Data);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback кнопка.</param>
        /// <param name="noCallBack">Callback при нажатие на кнопку нет.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, InlineCallback noCallBack)
            : this(inlineCallBack, ActionWithLastMessage.Nothing, PRTelegramBotCommand.CallbackWithConfirmation)
        {
            NoCallback = noCallBack;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback кнопка.</param>
        /// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
        /// <param name="noCallBack">Callback при нажатие на кнопку нет.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, InlineCallback noCallBack)
            : this(inlineCallBack, actionWithLastMessage, PRTelegramBotCommand.CallbackWithConfirmation)
        {
            NoCallback = noCallBack;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback кнопка.</param>
        /// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
        /// <param name="callbackWithConfirmation">Заголовок для обработки подтверждения.</param>
        /// <param name="noCallBack">Callback при нажатие на кнопку нет.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation, InlineCallback noCallBack)
            : this(inlineCallBack, actionWithLastMessage, callbackWithConfirmation)
        {
            NoCallback = noCallBack;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback кнопка.</param>
        /// <param name="yesButton">Названия кнопки действия да.</param>
        /// <param name="noButton">Название кнопки действия нет.</param>
        /// <param name="messageText">Текст сообщения.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, string yesButton, string noButton, string messageText)
            : this(inlineCallBack, ActionWithLastMessage.Nothing, PRTelegramBotCommand.CallbackWithConfirmation, yesButton, noButton, messageText) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback кнопка.</param>
        /// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
        /// <param name="yesButton">Названия кнопки действия да.</param>
        /// <param name="noButton">Название кнопки действия нет.</param>
        /// <param name="messageText">Текст сообщения.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, string yesButton, string noButton, string messageText)
            : this(inlineCallBack, actionWithLastMessage, PRTelegramBotCommand.CallbackWithConfirmation, yesButton, noButton, messageText) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback кнопка.</param>
        /// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
        /// <param name="callbackWithConfirmation">Заголовок для обработки подтверждения.</param>
        /// <param name="yesButton">Названия кнопки действия да.</param>
        /// <param name="noButton">Название кнопки действия нет.</param>
        /// <param name="messageText">Текст сообщения.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation, string yesButton, string noButton, string messageText)
            : this(inlineCallBack, actionWithLastMessage, callbackWithConfirmation)
        {
            YesButton = yesButton;
            NoButton = noButton;
            BaseMessage = messageText;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback кнопка.</param>
        /// <param name="yesButton">Названия кнопки действия да.</param>
        /// <param name="messageText">Текст сообщения.</param>
        /// <param name="noCallBack">Callback при нажатие на кнопку нет.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, string yesButton, string messageText, InlineCallback noCallBack)
            : this(inlineCallBack, ActionWithLastMessage.Nothing, PRTelegramBotCommand.CallbackWithConfirmation, yesButton, messageText, noCallBack) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback кнопка.</param>
        /// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
        /// <param name="yesButton">Названия кнопки действия да.</param>
        /// <param name="messageText">Текст сообщения.</param>
        /// <param name="noCallBack">Callback при нажатие на кнопку нет.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, string yesButton, string messageText, InlineCallback noCallBack)
            : this(inlineCallBack, actionWithLastMessage, PRTelegramBotCommand.CallbackWithConfirmation, yesButton, messageText, noCallBack) { }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback кнопка.</param>
        /// <param name="actionWithLastMessage">Действие с последним сообщение.</param>
        /// <param name="callbackWithConfirmation">Заголовок для обработки подтверждения.</param>
        /// <param name="yesButton">Названия кнопки действия да.</param>
        /// <param name="messageText">Текст сообщения.</param>
        /// <param name="noCallBack">Callback при нажатие на кнопку нет.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation, string yesButton, string messageText, InlineCallback noCallBack)
            : this(inlineCallBack, actionWithLastMessage, callbackWithConfirmation, yesButton, noCallBack.ButtonName, messageText)
        {
            NoCallback = noCallBack;
        }

        #endregion
    }
}
