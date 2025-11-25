using System.Text.Json.Serialization;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Models.InlineButtons
{
    public abstract class InlineBase
    {
        #region Поля и свойства

        /// <summary>
        /// Название кнопки.
        /// </summary>
        [JsonIgnore]
        public string ButtonName { get; set; }


        #endregion

        #region Методы

        /// <summary>
        /// Получить текст кнопки.
        /// </summary>
        /// <returns>Текст кнопки.</returns>
        public virtual string GetTextButton()
        {
            return ButtonName;
        }

        /// <summary>
        /// Получить Inline кнопку.
        /// </summary>
        /// <returns>Inline кнопка.</returns>
        public abstract InlineKeyboardButton GetInlineButton();

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="buttonName">Название кнопки.</param>
        public InlineBase(string buttonName)
        {
            ButtonName = buttonName;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public InlineBase() { }

        #endregion
    }
}
