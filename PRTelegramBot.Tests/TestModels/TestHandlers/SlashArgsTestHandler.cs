using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;

namespace PRTelegramBot.Tests.TestModels.TestHandlers
{
    /// <summary>
    /// Records the arguments a slash command was given, so a test can see what survived parsing.
    /// </summary>
    internal static class SlashArgsTestHandler
    {
        /// <summary>
        /// A bot of its own, so this handler does not disturb the counts other tests assert on.
        /// </summary>
        public const long SlashArgsBotId = 9901;

        public static List<string>? LastArgs { get; private set; }

        public static void Reset() => LastArgs = null;

        [SlashHandler(SlashArgsBotId, '_', "/argtest")]
        public static Task Capture(IBotContext context)
        {
            LastArgs = context.GetSlashArgs();
            return Task.CompletedTask;
        }
    }
}
