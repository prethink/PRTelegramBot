using PRTelegramBot.Interfaces;
using Telegram.Bot.Types;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Создает кнопку встроенной клавиатуры с HTTPS URL для автоматической авторизации пользователя. Может использоваться как замена для <a href="https://core.telegram.org/widgets/login">Виджета входа в Telegram</a>
    /// </summary>
    public class InlineLoginUrl : InlineBase, IInlineContent
    {
        #region Поля и свойства

        public LoginUrl LoginUrl { get; set; }

        #endregion

        #region IInlineContent

        public object GetContent()
        {
            return LoginUrl;
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="loginUrl">HTTPS URL для автоматической авторизации пользователя. Может использоваться как замена для <a href="https://core.telegram.org/widgets/login">Виджета входа в Telegram</a>.</param>
        public InlineLoginUrl(string buttonName, LoginUrl loginUrl)
            : base(buttonName)
        {
            LoginUrl = loginUrl;
        }

        #endregion
    }
}
