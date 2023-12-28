using PRTelegramBot.Interface;

namespace PRTelegramBot.InlineButtons
{
    /// <summary>
    /// Создает кнопку с ссылкой
    /// </summary>
    public class InlineURL : IInlineContent
    {
        /// <summary>
        /// Название кнопки
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// Ссылка
        /// </summary>
        public string URL { get; set; }

        public InlineURL(string buttonName, string url)
        {
            ButtonName = buttonName;
            URL = url;
        }

        public object GetContent()
        {
            return URL;
        }

        public string GetTextButton()
        {
            return ButtonName;
        }
    }
}
