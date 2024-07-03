using PRTelegramBot.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Создает кнопку для обработки WebApp.
    /// </summary>
    public sealed class InlineWebApp : InlineBase, IInlineContent
    {
        #region Поля и свойства

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

        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithWebApp(ButtonName, GetContent() as WebAppInfo);
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="webAppUrl">Ссылка на webApp.</param>
        public InlineWebApp(string buttonName, string webAppUrl)
            : base(buttonName)
        {
            WebAppUrl = webAppUrl;
        }

        #endregion
    }
}
