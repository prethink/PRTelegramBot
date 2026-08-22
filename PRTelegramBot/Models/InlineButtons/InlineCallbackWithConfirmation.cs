using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Lets an inlineCallBack be executed with a confirmation.
    /// </summary>
    public class InlineCallbackWithConfirmation : InlineCallback<EntityTCommand<string>>, IInlineContent
    {
        #region Fields and properties

        /// <summary>
        /// Collection of InlineCallbackWithConfirmation used to look up and handle the data.
        /// </summary>
        [JsonIgnore]
        public static Dictionary<string, InlineCallbackWithConfirmation> DataCollection = new();

        /// <summary>
        /// Name of the "yes" button.
        /// </summary>
        [JsonIgnore]
        public string YesButton = "Yes";

        /// <summary>
        /// Name of the "no" button.
        /// </summary>
        [JsonIgnore]
        public string NoButton = "No";

        /// <summary>
        /// Text of the confirmation message.
        /// </summary>
        [JsonIgnore]
        public string BaseMessage = "Confirm the action";

        /// <summary>
        /// Handler invoked when "yes" is pressed.
        /// </summary>
        [JsonIgnore]
        public InlineCallback YesCallback { get; set; }

        /// <summary>
        /// Handler invoked when "no" is pressed.
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

        #region Base class

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return base.GetInlineButton();
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback button.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack)
            : this(inlineCallBack, ActionWithLastMessage.Nothing, PRTelegramBotCommand.CallbackWithConfirmation) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback button.</param>
        /// <param name="actionWithLastMessage">Action to perform on the last message.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage)
            : this(inlineCallBack, actionWithLastMessage, PRTelegramBotCommand.CallbackWithConfirmation) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback button.</param>
        /// <param name="actionWithLastMessage">Action to perform on the last message.</param>
        /// <param name="callbackWithConfirmation">Header used to handle the confirmation.</param>
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
        /// Constructor.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback button.</param>
        /// <param name="noCallBack">Callback invoked when the "no" button is pressed.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, InlineCallback noCallBack)
            : this(inlineCallBack, ActionWithLastMessage.Nothing, PRTelegramBotCommand.CallbackWithConfirmation)
        {
            NoCallback = noCallBack;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback button.</param>
        /// <param name="actionWithLastMessage">Action to perform on the last message.</param>
        /// <param name="noCallBack">Callback invoked when the "no" button is pressed.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, InlineCallback noCallBack)
            : this(inlineCallBack, actionWithLastMessage, PRTelegramBotCommand.CallbackWithConfirmation)
        {
            NoCallback = noCallBack;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback button.</param>
        /// <param name="actionWithLastMessage">Action to perform on the last message.</param>
        /// <param name="callbackWithConfirmation">Header used to handle the confirmation.</param>
        /// <param name="noCallBack">Callback invoked when the "no" button is pressed.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation, InlineCallback noCallBack)
            : this(inlineCallBack, actionWithLastMessage, callbackWithConfirmation)
        {
            NoCallback = noCallBack;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback button.</param>
        /// <param name="yesButton">Name of the "yes" action button.</param>
        /// <param name="noButton">Name of the "no" action button.</param>
        /// <param name="messageText">Message text.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, string yesButton, string noButton, string messageText)
            : this(inlineCallBack, ActionWithLastMessage.Nothing, PRTelegramBotCommand.CallbackWithConfirmation, yesButton, noButton, messageText) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback button.</param>
        /// <param name="actionWithLastMessage">Action to perform on the last message.</param>
        /// <param name="yesButton">Name of the "yes" action button.</param>
        /// <param name="noButton">Name of the "no" action button.</param>
        /// <param name="messageText">Message text.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, string yesButton, string noButton, string messageText)
            : this(inlineCallBack, actionWithLastMessage, PRTelegramBotCommand.CallbackWithConfirmation, yesButton, noButton, messageText) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback button.</param>
        /// <param name="actionWithLastMessage">Action to perform on the last message.</param>
        /// <param name="callbackWithConfirmation">Header used to handle the confirmation.</param>
        /// <param name="yesButton">Name of the "yes" action button.</param>
        /// <param name="noButton">Name of the "no" action button.</param>
        /// <param name="messageText">Message text.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation, string yesButton, string noButton, string messageText)
            : this(inlineCallBack, actionWithLastMessage, callbackWithConfirmation)
        {
            YesButton = yesButton;
            NoButton = noButton;
            BaseMessage = messageText;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback button.</param>
        /// <param name="yesButton">Name of the "yes" action button.</param>
        /// <param name="messageText">Message text.</param>
        /// <param name="noCallBack">Callback invoked when the "no" button is pressed.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, string yesButton, string messageText, InlineCallback noCallBack)
            : this(inlineCallBack, ActionWithLastMessage.Nothing, PRTelegramBotCommand.CallbackWithConfirmation, yesButton, messageText, noCallBack) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback button.</param>
        /// <param name="actionWithLastMessage">Action to perform on the last message.</param>
        /// <param name="yesButton">Name of the "yes" action button.</param>
        /// <param name="messageText">Message text.</param>
        /// <param name="noCallBack">Callback invoked when the "no" button is pressed.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, string yesButton, string messageText, InlineCallback noCallBack)
            : this(inlineCallBack, actionWithLastMessage, PRTelegramBotCommand.CallbackWithConfirmation, yesButton, messageText, noCallBack) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inlineCallBack">InlineCallback button.</param>
        /// <param name="actionWithLastMessage">Action to perform on the last message.</param>
        /// <param name="callbackWithConfirmation">Header used to handle the confirmation.</param>
        /// <param name="yesButton">Name of the "yes" action button.</param>
        /// <param name="messageText">Message text.</param>
        /// <param name="noCallBack">Callback invoked when the "no" button is pressed.</param>
        public InlineCallbackWithConfirmation(InlineCallback inlineCallBack, ActionWithLastMessage actionWithLastMessage, Enum callbackWithConfirmation, string yesButton, string messageText, InlineCallback noCallBack)
            : this(inlineCallBack, actionWithLastMessage, callbackWithConfirmation, yesButton, noCallBack.ButtonName, messageText)
        {
            NoCallback = noCallBack;
        }

        #endregion
    }
}
