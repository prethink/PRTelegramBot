using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.InlineButtons;

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

        public object GetContent()
        {
            return URL;
        }

        public string GetTextButton()
        {
            return ButtonName;
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
