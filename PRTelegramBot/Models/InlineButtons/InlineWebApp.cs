using PRTelegramBot.Interfaces;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Создает кнопку для обработки WebApp.
    /// </summary>
    public sealed class InlineWebApp : IInlineContent
    {
        #region Поля и свойства

        /// <summary>
        /// Название кнопки.
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// Данные для WebApp.
        /// </summary>
        public string WebAppUrl { get; set; }

        #endregion

        #region IInlineContent

        public object GetContent()
        {
            var webApp = new WebAppInfo(WebAppUrl);
            return webApp;
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
        /// <param name="webAppUrl">Ссылка на webApp.</param>
        public InlineWebApp(string buttonName, string webAppUrl)
        {
            ButtonName = buttonName;
            WebAppUrl = webAppUrl;
        }

        #endregion
    }
}
