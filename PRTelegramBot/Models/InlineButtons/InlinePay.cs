using PRTelegramBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Creates an inline keyboard <a href="https://core.telegram.org/bots/api#payments">Pay button</a>. The substrings “⭐” and “XTR” in the button text are replaced with the Telegram Star icon.
    /// This kind of button must always be the first button in the first row, and can only be used in invoice messages.
    /// </summary>
    public class InlinePay : InlineBase, IInlineContent
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
            return InlineKeyboardButton.WithPay(ButtonName);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        public InlinePay(string buttonName)
            : base(buttonName) { }

        #endregion
    }
}
