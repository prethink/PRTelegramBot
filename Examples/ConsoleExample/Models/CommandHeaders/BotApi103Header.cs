using PRTelegramBot.Attributes;
using System.ComponentModel;

namespace ConsoleExample.Models.CommandHeaders
{
    [InlineCommand]
    public enum BotApi103Header
    {
        [Description("Unlocked step")]
        UnlockedStep = 800,
        [Description("Answer privately")]
        AnswerPrivately,
        [Description("Replace the menu")]
        ReplaceTheMenu,
        [Description("Private report")]
        RichPrivately,
    }
}
