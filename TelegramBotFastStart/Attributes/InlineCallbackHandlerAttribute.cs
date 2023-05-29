using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Attributes
{
    /// <summary>
    /// Аттрибут для inline команд
    /// </summary>
    internal class InlineCallbackHandlerAttribute : Attribute
    {
        /// <summary>
        /// Коллекция inline команд
        /// </summary>
        public List<CallbackId> Commands { get; set; }

        public InlineCallbackHandlerAttribute(params CallbackId[] commands)
        {
            Commands = commands.ToList();
        }
    }
}
