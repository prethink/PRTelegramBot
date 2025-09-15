using PRTelegramBot.Models.EventsArgs;

namespace FastBotTemplateConsole.Events
{
    internal class LogEvents
    {
        public static Task OnLogError(ErrorLogEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string errorMsg = $"{DateTime.Now}: {e.Exception.ToString()}";
            Console.WriteLine(errorMsg);
            Console.ResetColor();
            return Task.CompletedTask;
        }

        public static Task OnLogCommon(CommonLogEventArgs e)
        {
            Console.ForegroundColor = e.Color;
            string formatMsg = $"{DateTime.Now}: {e.Message}";
            Console.WriteLine(formatMsg);
            Console.ResetColor();
            return Task.CompletedTask;
        }
    }
}
