using PRTelegramBot.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Creates an inline keyboard button with an HTTPS URL used to automatically authorize the user. Can be used as a replacement for the <a href="https://core.telegram.org/widgets/login">Telegram Login Widget</a>
    /// </summary>
    public class InlineLoginUrl : InlineBase, IInlineContent
    {
        #region Fields and properties

        /// <summary>
        /// Login URL the button opens.
        /// </summary>
        public LoginUrl LoginUrl { get; set; }

        #endregion

        #region IInlineContent

        /// <inheritdoc />
        public object GetContent()
        {
            return LoginUrl;
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithLoginUrl(ButtonName, LoginUrl);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        /// <param name="loginUrl">An HTTPS URL used to automatically authorize the user. Can be used as a replacement for the <a href="https://core.telegram.org/widgets/login">Telegram Login Widget</a>.</param>
        public InlineLoginUrl(string buttonName, LoginUrl loginUrl)
            : base(buttonName)
        {
            LoginUrl = loginUrl;
        }

        #endregion
    }
}
