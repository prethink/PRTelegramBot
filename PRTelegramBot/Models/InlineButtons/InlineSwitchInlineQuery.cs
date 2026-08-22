using PRTelegramBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Creates an inline keyboard button. Pressing it prompts the user to select one of their chats, open that chat and insert the bot's username and the specified inline query into the input field. May be empty, in which case only the bot's username is inserted. Not supported for messages sent on behalf of a Telegram Business account.
    /// </summary>
    public class InlineSwitchInlineQuery : InlineBase, IInlineContent
    {
        #region Fields and properties

        /// <summary>
        /// If set, pressing the button prompts the user to select one of their chats, open that chat and insert the bot's username and the specified inline query into the input field. May be empty, in which case only the bot's username is inserted. Not supported for messages sent on behalf of a Telegram Business account.
        /// </summary>
        public string SwitchInlineQuery { get; set; }

        #endregion

        #region IInlineContent

        /// <inheritdoc />
        public object GetContent()
        {
            return SwitchInlineQuery;
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithSwitchInlineQuery(ButtonName, SwitchInlineQuery);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        /// <param name="switchInlineQuery">If set, pressing the button prompts the user to select one of their chats, open that chat and insert the bot's username and the specified inline query into the input field. May be empty, in which case only the bot's username is inserted. Not supported for messages sent on behalf of a Telegram Business account.</param>
        public InlineSwitchInlineQuery(string buttonName, string switchInlineQuery)
            : base(buttonName)
        {
            SwitchInlineQuery = switchInlineQuery;
        }

        #endregion
    }
}
