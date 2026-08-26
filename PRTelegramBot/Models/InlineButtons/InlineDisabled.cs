using PRTelegramBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Creates a button that is shown but does nothing when it is pressed.
    /// </summary>
    /// <remarks>
    /// Useful for a menu that has to keep its shape while an action is unavailable:
    /// a step the user has not reached yet, an option their plan does not include,
    /// or a button that is busy while a long operation runs.
    /// Telegram draws the button greyed out and ignores taps on it, so no callback arrives.
    /// </remarks>
    public sealed class InlineDisabled : InlineBase, IInlineContent
    {
        #region IInlineContent

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <returns>
        /// The button label. A disabled button carries no payload, so its label is all there is.
        /// </returns>
        public object GetContent()
        {
            return ButtonName;
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithDisabled(ButtonName);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        public InlineDisabled(string buttonName)
            : base(buttonName)
        {
        }

        #endregion
    }
}
