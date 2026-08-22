using PRTelegramBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Creates an inline keyboard button. Pressing it inserts the bot's username and the specified inline query into the input field of the current chat. May be empty, in which case only the bot's username is inserted.<br/><br/>This offers a quick way for the user to open your bot in inline mode in the same chat — good for picking something out of several options. Not supported in channels and for messages sent on behalf of a Telegram Business account.
    /// </summary>
    public class InlineSwitchInlineQueryCurrentChat : InlineBase, IInlineContent
    {
        #region Fields and properties

        /// <summary>
        /// If set, pressing the button inserts the bot's username and the specified inline query into the input field of the current chat. May be empty, in which case only the bot's username is inserted.<br/><br/>This offers a quick way for the user to open your bot in inline mode in the same chat — good for picking something out of several options. Not supported in channels and for messages sent on behalf of a Telegram Business account.
        /// </summary>
        public string SwitchInlineQueryCurrentChat { get; set; }

        #endregion

        #region IInlineContent

        /// <inheritdoc />
        public object GetContent()
        {
            return SwitchInlineQueryCurrentChat;
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithSwitchInlineQueryCurrentChat(ButtonName, SwitchInlineQueryCurrentChat);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        /// <param name="switchInlineQueryCurrentChat">If set, pressing the button inserts the bot's username and the specified inline query into the input field of the current chat. May be empty, in which case only the bot's username is inserted.<br/><br/>This offers a quick way for the user to open your bot in inline mode in the same chat — good for picking something out of several options. Not supported in channels and for messages sent on behalf of a Telegram Business account.</param>
        public InlineSwitchInlineQueryCurrentChat(string buttonName, string switchInlineQueryCurrentChat)
            : base(buttonName)
        {
            SwitchInlineQueryCurrentChat = switchInlineQueryCurrentChat;
        }

        #endregion
    }
}
