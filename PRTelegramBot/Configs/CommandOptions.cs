using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Command options.
    /// </summary>
    public class CommandOptions
    {
        /// <summary>
        /// Inline handlers for class instances.
        /// </summary>
        public Dictionary<Enum, Type> InlineClassHandlers { get; set; } = new();

        /// <summary>
        /// Command registrar.
        /// </summary>
        public IRegisterCommand RegisterCommand { get; set; }
    }
}
