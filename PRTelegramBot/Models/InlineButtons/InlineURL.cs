using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.InlineButtons
{
    /// <summary>
    /// Создает кнопку с ссылкой.
    /// </summary>
    public sealed class InlineURL : InlineBase, IInlineContent
    {
        #region Поля и свойства

        /// <summary>
        /// Ссылка.
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

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="url">Ссылка.</param>
        public InlineURL(string buttonName, string url)
            : base(buttonName)
        {
            URL = url;
        }
        
        #endregion
    }
}
