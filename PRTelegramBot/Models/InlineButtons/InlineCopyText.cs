using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.InlineButtons
{
    /// <summary>
    /// Creates a button that copies the given text to the clipboard when it is pressed.
    /// </summary>
    public sealed class InlineCopyText : InlineBase, IInlineContent
    {
        #region Fields and properties

        /// <summary>
        /// Text that is copied to the clipboard.
        /// </summary>
        public string CopyText { get; set; }

        #endregion

        #region IInlineContent

        /// <inheritdoc />
        public object GetContent()
        {
            return CopyText;
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithCopyText(ButtonName, new CopyTextButton { Text = CopyText });
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        /// <param name="copyText">Text that is copied to the clipboard.</param>
        public InlineCopyText(string buttonName, string copyText)
            : base(buttonName)
        {
            CopyText = copyText;
        }

        #endregion
    }
}
