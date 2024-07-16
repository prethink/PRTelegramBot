using PRTelegramBot.Models.EventsArgs;

namespace ConsoleExample.Examples.Events
{
    internal static class ExampleLogEvents
    {
        public static async Task OnLogError(ErrorLogEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now}: {e.Exception}");
            Console.ResetColor();
        }

        public static async Task OnLogCommon(CommonLogEventArgs e)
        {
            Console.ForegroundColor = e.Color;
            Console.WriteLine($"{DateTime.Now}: {e.Message}");
            Console.ResetColor();
        }
    }
}
