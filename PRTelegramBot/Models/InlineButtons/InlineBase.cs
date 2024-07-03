using System.Text.Json.Serialization;

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

        public virtual string GetTextButton()
        {
            return ButtonName;
        }

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
