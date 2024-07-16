using PRTelegramBot.Models.EventsArgs;

namespace FastBotTemplateConsole.Events
{
    internal class LogEvents
    {
        public static async Task OnLogError(ErrorLogEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string errorMsg = $"{DateTime.Now}: {e.Exception.ToString()}";
            Console.WriteLine(errorMsg);
            Console.ResetColor();
        }

        public static async Task OnLogCommon(CommonLogEventArgs e)
        {
            Console.ForegroundColor = e.Color;
            string formatMsg = $"{DateTime.Now}: {e.Message}";
            Console.WriteLine(formatMsg);
            Console.ResetColor();
        }
    }
}
