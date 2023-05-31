using Telegram.Bot.Types;
using PRTelegramBot.Models.Interface;

namespace PRTelegramBot.Models
{
    /// <summary>
    /// Создает кнопку для обработки WebApp
    /// </summary>
    public class InlineWebApp : IInlineContent
    {
        /// <summary>
        /// Название кнопки
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// Данные для WebApp
        /// </summary>
        public string WebAppUrl { get; set; }

        public InlineWebApp(string buttonName, string webAppUrl)
        {
            ButtonName = buttonName;
            WebAppUrl = webAppUrl;
        }

        public object GetContent()
        {
            var webApp = new WebAppInfo();
            webApp.Url = WebAppUrl;
            return webApp;
        }

        public string GetTextButton()
        {
            return ButtonName;
        }
    }
}
