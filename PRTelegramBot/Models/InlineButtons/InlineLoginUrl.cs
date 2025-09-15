using PRTelegramBot.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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

        /// <inheritdoc />
        public object GetContent()
        {
            return LoginUrl;
        }

        /// <inheritdoc />
        public override InlineKeyboardButton GetInlineButton()
        {
            return InlineKeyboardButton.WithLoginUrl(ButtonName, LoginUrl);
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
