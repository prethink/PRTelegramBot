using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Models.InlineButtons
{
    /// <summary>
    /// Создает кнопку встроенной клавиатуры <a href="https://core.telegram.org/bots/api#payments">Кнопка оплаты</a>. Подстроки “⭐” и “XTR” в тексте кнопки будут заменены на иконку звезды Telegram.
    /// Этот тип кнопки должен всегда быть первой кнопкой в первой строке и может использоваться только в сообщениях invoice.
    /// </summary>
    public class InlinePay : InlineBase, IInlineContent
    {
        #region IInlineContent

        public object GetContent()
        {
            return "";
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        public InlinePay(string buttonName)
            : base(buttonName) { }

        #endregion
    }
}
