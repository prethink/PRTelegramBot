using PRTelegramBot.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Creates a button that opens a WebApp.
    /// </summary>
    public sealed class InlineWebApp : InlineBase, IInlineContent
    {
        #region Fields and properties

        /// <summary>
        /// Data for the WebApp.
        /// </summary>
        public string WebAppUrl { get; set; }

        #endregion

        #region IInlineContent

        /// <inheritdoc />
        public object GetContent()
        {
            var webApp = new WebAppInfo(WebAppUrl);
            return webApp;
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithWebApp(ButtonName, GetContent() as WebAppInfo);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        /// <param name="webAppUrl">Link to the webApp.</param>
        public InlineWebApp(string buttonName, string webAppUrl)
            : base(buttonName)
        {
            WebAppUrl = webAppUrl;
        }

        #endregion
    }
}
