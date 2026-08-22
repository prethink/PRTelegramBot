using System.Text.Json.Serialization;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Base class for inline buttons.
    /// </summary>
    public abstract class InlineBase
    {
        #region Fields and properties

        /// <summary>
        /// Button name.
        /// </summary>
        [JsonIgnore]
        public string ButtonName { get; set; }


        #endregion

        #region Methods

        /// <summary>
        /// Gets the button text.
        /// </summary>
        /// <returns>Button text.</returns>
        public virtual string GetButtonName()
        {
            return ButtonName;
        }


        /// <summary>
        /// Sets a new value for the button.
        /// </summary>
        /// <returns>Button name.</returns>
        public virtual string SetButtonName(string name)
        {
            ButtonName = name;
            return ButtonName;
        }

        /// <summary>
        /// Gets the inline button.
        /// </summary>
        /// <returns>Inline button.</returns>
        public abstract InlineKeyboardButton GetInlineButton();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        public InlineBase(string buttonName)
        {
            ButtonName = buttonName;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public InlineBase() { }

        #endregion
    }
}
