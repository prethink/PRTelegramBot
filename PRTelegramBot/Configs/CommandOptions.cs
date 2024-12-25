using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Configs
{
    /// <summary>
    /// Параметры команд.
    /// </summary>
    public class CommandOptions
    {
        /// <summary>
        /// Обработчика inline для экзепляров класса.
        /// </summary>
        public Dictionary<Enum, Type> InlineClassHandlers { get; set; } = new();

        /// <summary>
        /// Регистратор команд.
        /// </summary>
        public IRegisterCommand RegisterCommand { get; set; }
    }
}
