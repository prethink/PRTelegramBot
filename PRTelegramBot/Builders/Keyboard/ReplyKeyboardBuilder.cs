using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Builders.Keyboard
{
    /// <summary>
    /// Builder for conveniently constructing a ReplyKeyboardMarkup.
    /// Lets you set keyboard options and add buttons and rows dynamically.
    /// </summary>
    public class ReplyKeyboardBuilder : KeyboardBuilderBase<KeyboardButton, ReplyKeyboardMarkup, ReplyKeyboardBuilder>
    {
        #region Fields and properties 

        /// <summary>
        /// Asks Telegram clients to always show
        /// the custom keyboard, even when the regular system keyboard is hidden.
        /// Default: false.  
        /// If false, the custom keyboard can be hidden and reopened with the keyboard icon.
        /// </summary>
        private bool isPersistent;

        /// <summary>
        /// Asks clients to automatically adjust the height
        /// of the keyboard for an optimal layout (for example, to reduce the height when there are only
        /// two rows of buttons).  
        /// Default: false.  
        /// If false, the keyboard is always shown with the same height as the standard one.
        /// </summary>
        private bool resizeKeyboard;

        /// <summary>
        /// Asks clients to hide the keyboard right after
        /// the user presses a button.  
        /// The keyboard stays available, but Telegram automatically switches back to the regular keyboard,
        /// and the user can reopen the custom keyboard with the button in the input field.
        /// Default: false.
        /// </summary>
        private bool oneTimeKeyboard;

        /// <summary>
        /// The placeholder text shown in the input field while the keyboard
        /// is active. May contain from 1 to 64 characters.
        /// </summary>
        private string? inputFieldPlaceholder;

        /// <summary>
        /// Use it when the keyboard should be shown only
        /// specific users.  
        /// The keyboard will be visible to:
        /// 1) Users mentioned in the message text (@username).  
        /// 2) If the message is a reply, the sender of the original message in the same chat/thread.
        /// </summary>
        private bool selective;

        /// <summary>
        /// Name of the main menu button. If not specified, there is no button.
        /// </summary>
        private string? mainMenuButton;

        /// <summary>
        /// Position of the main menu button, if there is one.
        /// </summary>
        private MainMenuButtonPosition mainMenuButtonPosition;

        #endregion

        #region Methods

        /// <summary>
        /// Sets the persistent keyboard flag.
        /// </summary>
        public ReplyKeyboardBuilder SetPersistent(bool value = true)
        {
            this.isPersistent = value;
            return this;
        }

        /// <summary>
        /// Sets the keyboard resize flag.
        /// </summary>
        public ReplyKeyboardBuilder SetResizeKeyboard(bool value = true)
        {
            this.resizeKeyboard = value;
            return this;
        }

        /// <summary>
        /// Sets the one-time keyboard flag.
        /// </summary>
        public ReplyKeyboardBuilder SetOneTimeKeyboard(bool value = true)
        {
            this.oneTimeKeyboard = value;
            return this;
        }

        /// <summary>
        /// Sets the placeholder text in the input field.
        /// </summary>
        public ReplyKeyboardBuilder SetInputFieldPlaceholder(string placeholder)
        {
            this.inputFieldPlaceholder = placeholder;
            return this;
        }

        /// <summary>
        /// Shows the keyboard only to specific users.
        /// </summary>
        public ReplyKeyboardBuilder SetSelective(bool value = true)
        {
            this.selective = value;
            return this;
        }

        /// <summary>
        /// Sets the name of the main menu button and the position
        /// it is added at (the top or the bottom of the keyboard).
        /// If no name is given, the button is not added.
        /// </summary>
        /// <param name="buttonName">Text of the main menu button.</param>
        /// <param name="mainMenuButtonPosition">Position of the button on the keyboard (Bottom by default).</param>
        /// <returns>The current builder instance.</returns>
        public ReplyKeyboardBuilder SetMainMenuButton(string buttonName, MainMenuButtonPosition mainMenuButtonPosition = MainMenuButtonPosition.Bottom)
        {
            this.mainMenuButton = buttonName;
            this.mainMenuButtonPosition = mainMenuButtonPosition;
            return this;
        }

        /// <summary>
        /// Adds a regular button with the specified text.
        /// You can specify whether the button should be added on a new row.
        /// </summary>
        /// <param name="buttonName">Button text.</param>
        /// <param name="newRow">If true, the button is added on a new row.</param>
        /// <returns>The current builder instance.</returns>
        public ReplyKeyboardBuilder AddButton(string buttonName, bool newRow = false)
        {
            this.AddButton(new KeyboardButton(buttonName), newRow);
            return this;
        }

        /// <summary>
        /// Adds a button that opens a WebApp by its link.
        /// </summary>
        /// <param name="buttonName">Button text.</param>
        /// <param name="url">URL WebApp.</param>
        /// <param name="newRow">If true, the button is added on a new row.</param>
        /// <returns>The current builder instance.</returns>
        public ReplyKeyboardBuilder AddButtonWebApp(string buttonName, string url, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithWebApp(buttonName, new WebAppInfo() { Url = url }), newRow);
            return this;
        }

        /// <summary>
        /// Adds a button that requests the user's contact.
        /// When pressed, Telegram sends the user's contact.
        /// </summary>
        /// <param name="buttonName">Button text.</param>
        /// <param name="newRow">If true, the button is added on a new row.</param>
        /// <returns>The current builder instance.</returns>
        public ReplyKeyboardBuilder AddRequestContact(string buttonName, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestContact(buttonName), newRow);
            return this;
        }

        /// <summary>
        /// Adds a button that requests the user's location.
        /// When pressed, Telegram sends the user's current location.
        /// </summary>
        /// <param name="buttonName">Button text.</param>
        /// <param name="newRow">If true, the button is added on a new row.</param>
        /// <returns>The current builder instance.</returns>
        public ReplyKeyboardBuilder AddRequestLocation(string buttonName, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestLocation(buttonName), newRow);
            return this;
        }

        /// <summary>
        /// Adds a button that requests a chat selection.
        /// Lets the user pick a chat according to the request parameters.
        /// </summary>
        /// <param name="buttonName">Button text.</param>
        /// <param name="requestChat">The chat request parameters object.</param>
        /// <param name="newRow">If true, the button is added on a new row.</param>
        /// <returns>The current builder instance.</returns>
        public ReplyKeyboardBuilder AddRequestChat(string buttonName, KeyboardButtonRequestChat requestChat, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestChat(buttonName, requestChat), newRow);
            return this;
        }

        /// <summary>
        /// Adds a button that requests a chat selection, with the request parameters specified manually.
        /// </summary>
        /// <param name="buttonName">Button text.</param>
        /// <param name="requestId">Request ID.</param>
        /// <param name="chatIsChannel">True to pick channels only; false for groups/chats only.</param>
        /// <param name="newRow">If true, the button is added on a new row.</param>
        /// <returns>The current builder instance.</returns>
        public ReplyKeyboardBuilder AddRequestChat(string buttonName, int requestId, bool chatIsChannel, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestChat(buttonName, requestId, chatIsChannel), newRow);
            return this;
        }

        /// <summary>
        /// Adds a button that requests a user selection.
        /// </summary>
        /// <param name="buttonName">Button text.</param>
        /// <param name="requestUsers">User request parameters.</param>
        /// <param name="newRow">If true, the button is added on a new row.</param>
        /// <returns>The current builder instance.</returns>
        /// <returns></returns>
        public ReplyKeyboardBuilder AddRequestUsers(string buttonName, KeyboardButtonRequestUsers requestUsers, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestUsers(buttonName, requestUsers), newRow);
            return this;
        }

        /// <summary>
        /// Adds a button that asks the user to create and share a bot managed by this one.
        /// </summary>
        /// <remarks>
        /// Available only to bots that enabled management of other bots in the @BotFather
        /// Mini App, and only in private chats.
        /// </remarks>
        /// <param name="buttonName">Button text.</param>
        /// <param name="requestManagedBot">Managed bot request parameters.</param>
        /// <param name="newRow">If true, the button is added on a new row.</param>
        /// <returns>The current builder instance.</returns>
        public ReplyKeyboardBuilder AddRequestManagedBot(string buttonName, KeyboardButtonRequestManagedBot requestManagedBot, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestManagedBot(buttonName, requestManagedBot), newRow);
            return this;
        }

        /// <summary>
        /// Adds a button that requests a user selection,
        /// with the request parameters specified manually.
        /// </summary>
        /// <param name="buttonName">Button text.</param>
        /// <param name="requestId">Request ID.</param>
        /// <param name="maxQuantity">Maximum number of users that can be selected.</param>
        /// <param name="newRow">If true, the button is added on a new row.</param>
        /// <returns>The current builder instance.</returns>
        public ReplyKeyboardBuilder AddRequestUsers(string buttonName, int requestId, int? maxQuantity = null, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestUsers(buttonName, requestId, maxQuantity), newRow);
            return this;
        }

        /// <summary>
        /// Adds a button that requests a poll to be created.
        /// When pressed, Telegram prompts the user to create a poll of the specified type.
        /// </summary>
        /// <param name="buttonName">Button text.</param>
        /// <param name="pollType">Poll type (regular or quiz).</param>
        /// <param name="newRow">If true, the button is added on a new row.</param>
        /// <returns>The current builder instance.</returns>
        public ReplyKeyboardBuilder AddRequestPoll(string buttonName, KeyboardButtonPollType pollType, bool newRow = false)
        {
            this.AddButton(KeyboardButton.WithRequestPoll(buttonName, pollType), newRow);
            return this;
        }

        /// <summary>
        /// Adds the specified number of "empty" buttons — decorative elements
        /// used to align the layout or fill up free space.
        /// </summary>
        /// <param name="count">How many empty buttons to add.</param>
        /// <param name="newRow">If true, every button is added on a new row.</param>
        /// <returns>The current builder instance.</returns>
        public ReplyKeyboardBuilder AddEmptyButton(int count = 1, bool newRow = false)
        {
            for (int i = 0; i < count; i++)
            {
                this.AddButton(new KeyboardButton(KEY_EMPTY_BUTTON_NAME), newRow);
                newRow = false;
            }

            return this;
        }

        #endregion

        #region Base class

        /// <inheritdoc/>
        protected override void ReplaceEmptyButtons()
        {
            foreach (var row in buttons)
            {
                foreach (var button in row)
                {
                    if(button.Text.Equals(KEY_EMPTY_BUTTON_NAME, StringComparison.OrdinalIgnoreCase))
                        button.Text = emptyButtonName;
                }
            }
        }

        /// <inheritdoc/>
        public override ReplyKeyboardMarkup Build()
        {
            this.ReplaceEmptyButtons();

            var resultButtons = buttons.ToList();
            buttons.Clear();

            if (!string.IsNullOrEmpty(mainMenuButton) && mainMenuButtonPosition == MainMenuButtonPosition.Top)
            {
                this.AddButton(mainMenuButton);
                this.AddRow();
            }

            buttons.AddRange(resultButtons);

            if (!string.IsNullOrEmpty(mainMenuButton) && mainMenuButtonPosition == MainMenuButtonPosition.Bottom)
            {
                this.AddRow();
                this.AddButton(mainMenuButton);
            }

            buttons.RemoveAll(x => x == null || x.Count == 0);

            ReplyKeyboardMarkup replyKeyboardMarkup = new(buttons);
            replyKeyboardMarkup.IsPersistent = isPersistent;
            replyKeyboardMarkup.ResizeKeyboard = resizeKeyboard;
            replyKeyboardMarkup.OneTimeKeyboard = oneTimeKeyboard;
            replyKeyboardMarkup.InputFieldPlaceholder = inputFieldPlaceholder;
            replyKeyboardMarkup.Selective = selective;

            return replyKeyboardMarkup;
        }

        #endregion
    }

    /// <summary>
    /// Enum for the position of the main menu button.
    /// </summary>
    public enum MainMenuButtonPosition
    {
        /// <summary>
        /// Top.
        /// </summary>
        Top,
        /// <summary>
        /// Bottom.
        /// </summary>
        Bottom
    }
}
