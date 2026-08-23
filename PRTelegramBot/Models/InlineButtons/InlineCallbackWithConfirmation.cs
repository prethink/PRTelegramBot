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
        /// How long a pending confirmation stays available before it is discarded.
        /// </summary>
        private static readonly TimeSpan lifetime = TimeSpan.FromHours(1);

        /// <summary>
        /// Pending confirmations, keyed by the identifier carried in the callback data.
        /// </summary>
        /// <remarks>
        /// A confirmation is registered when the button is built and is needed only until the
        /// user answers it. Entries that nobody ever answers are swept once they grow older
        /// than <see cref="lifetime"/>, so a long-running bot does not accumulate them forever.
        /// </remarks>
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, (InlineCallbackWithConfirmation Item, DateTime CreatedUtc)> pending = new();

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

        #region Methods

        /// <summary>
        /// Registers a pending confirmation and discards the ones that have expired.
        /// </summary>
        /// <param name="id">Identifier carried in the callback data.</param>
        /// <param name="confirmation">The confirmation to remember.</param>
        private static void Register(string id, InlineCallbackWithConfirmation confirmation)
        {
            var now = DateTime.UtcNow;
            pending[id] = (confirmation, now);

            foreach (var entry in pending)
            {
                if (now - entry.Value.CreatedUtc > lifetime)
                    pending.TryRemove(entry.Key, out _);
            }
        }

        /// <summary>
        /// Looks up a pending confirmation by the identifier from the callback data.
        /// </summary>
        /// <param name="id">Identifier carried in the callback data.</param>
        /// <param name="confirmation">The confirmation that was found.</param>
        /// <returns>True if the confirmation is still pending; False if it is unknown or expired.</returns>
        internal static bool TryGetPending(string id, out InlineCallbackWithConfirmation? confirmation)
        {
            if (pending.TryGetValue(id, out var entry))
            {
                confirmation = entry.Item;
                return true;
            }

            confirmation = null;
            return false;
        }

        /// <summary>
        /// Forgets a confirmation once it has been answered.
        /// </summary>
        /// <param name="id">Identifier carried in the callback data.</param>
        internal static void Complete(string id)
        {
            pending.TryRemove(id, out _);
        }

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
            Register(id, this);
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
