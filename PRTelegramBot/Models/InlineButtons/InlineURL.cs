﻿using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Creates a button with a link.
    /// </summary>
    public sealed class InlineURL : InlineBase, IInlineContent
    {
        #region Fields and properties

        /// <summary>
        /// Link.
        /// </summary>
        public string URL { get; set; }

        #endregion

        #region IInlineContent

        /// <inheritdoc />
        public object GetContent()
        {
            return URL;
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithUrl(ButtonName, URL);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="buttonName">Button name.</param>
        /// <param name="url">Link.</param>
        public InlineURL(string buttonName, string url)
            : base(buttonName)
        {
            URL = url;
        }
        
        #endregion
    }
}
