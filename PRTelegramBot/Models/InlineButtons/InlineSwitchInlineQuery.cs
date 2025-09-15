using PRTelegramBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Создает кнопку встроенной клавиатуры. При нажатии на кнопку пользователю будет предложено выбрать один из своих чатов, открыть этот чат и вставить имя пользователя бота и указанной inline-запрос в поле ввода. Может быть пустым, в этом случае будет вставлено только имя пользователя бота. Не поддерживается для сообщений, отправленных от имени аккаунта Telegram Business.
    /// </summary>
    public class InlineSwitchInlineQuery : InlineBase, IInlineContent
    {
        #region Поля и свойства

        /// <summary>
        /// Если установлено, при нажатии на кнопку пользователю будет предложено выбрать один из своих чатов, открыть этот чат и вставить имя пользователя бота и указанной инлайн-запрос в поле ввода. Может быть пустым, в этом случае будет вставлено только имя пользователя бота. Не поддерживается для сообщений, отправленных от имени аккаунта Telegram Business.
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

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        /// <param name="switchInlineQuery">Если установлено, при нажатии на кнопку пользователю будет предложено выбрать один из своих чатов, открыть этот чат и вставить имя пользователя бота и указанной инлайн-запрос в поле ввода. Может быть пустым, в этом случае будет вставлено только имя пользователя бота. Не поддерживается для сообщений, отправленных от имени аккаунта Telegram Business.</param>
        public InlineSwitchInlineQuery(string buttonName, string switchInlineQuery)
            : base(buttonName)
        {
            SwitchInlineQuery = switchInlineQuery;
        }

        #endregion
    }
}
