using PRTelegramBot.Models.EventsArgs;

namespace ConsoleExample.Examples.Events
{
    internal static class ExampleLogEvents
    {
        public static Task OnLogError(ErrorLogEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now}: {e.Exception}");
            Console.ResetColor();
            return Task.CompletedTask;
        }

        public static Task OnLogCommon(CommonLogEventArgs e)
        {
            Console.ForegroundColor = e.Color;
            Console.WriteLine($"{DateTime.Now}: {e.Message}");
            Console.ResetColor();
            return Task.CompletedTask;
        }
    }
}
