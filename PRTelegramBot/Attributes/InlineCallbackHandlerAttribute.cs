using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Аттрибут для inline команд
    /// </summary>
    public class InlineCallbackHandlerAttribute : Attribute
    {
        /// <summary>
        /// Коллекция inline команд
        /// </summary>
        public List<Header> Commands { get; set; }

        public InlineCallbackHandlerAttribute(params Header[] commands)
        {
            Commands = commands.ToList();
        }
    }
}
