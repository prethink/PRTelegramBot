using PRTelegramBot.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Creates an inline keyboard button. Pressing it prompts the user to select one of their chats of the specified type, open that chat and insert the bot's username and the specified inline query into the input field. Not supported for messages sent on behalf of a Telegram Business account.
    /// </summary>
    public class InlineSwitchInlineQueryChosenChat : InlineBase, IInlineContent
    {
        #region Fields and properties

        /// <summary>
        /// If set, pressing the button prompts the user to select one of their chats of the specified type, open that chat and insert the bot's username and the specified inline query into the input field. Not supported for messages sent on behalf of a Telegram Business account.
        /// </summary>
        public SwitchInlineQueryChosenChat SwitchInlineQueryChosenChat { get; set; }

        #endregion

        #region IInlineContent

        /// <inheritdoc />
        public object GetContent()
        {
            return SwitchInlineQueryChosenChat;
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithSwitchInlineQueryChosenChat(ButtonName, SwitchInlineQueryChosenChat);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        /// <param name="switchInlineQueryChosenChat">If set, pressing the button prompts the user to select one of their chats of the specified type, open that chat and insert the bot's username and the specified inline query into the input field. Not supported for messages sent on behalf of a Telegram Business account.</param>
        public InlineSwitchInlineQueryChosenChat(string buttonName, SwitchInlineQueryChosenChat switchInlineQueryChosenChat)
            : base(buttonName)
        {
            SwitchInlineQueryChosenChat = switchInlineQueryChosenChat;
        }

        #endregion
    }
}
