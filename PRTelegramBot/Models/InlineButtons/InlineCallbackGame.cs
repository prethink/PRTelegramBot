using PRTelegramBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Creates an inline keyboard button describing the game that starts when the user presses the button.<br/><br/><b>NOTE:</b> This kind of button <b>must</b> always be the first button in the first row.
    /// </summary>
    public class InlineCallbackGame : InlineBase, IInlineContent
    {
        #region IInlineContent

        /// <inheritdoc />
        public object GetContent()
        {
            return string.Empty;
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithCallbackGame(ButtonName);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        public InlineCallbackGame(string buttonName)
            : base(buttonName) { }

        #endregion
    }
}
