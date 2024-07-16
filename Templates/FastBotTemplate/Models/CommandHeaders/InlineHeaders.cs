using PRTelegramBot.Attributes;
using System.ComponentModel;

namespace FastBotTemplateConsole.Models.CommandHeaders
{
    [InlineCommand]
    internal enum InlineHeaders
    {
        [Description(nameof(TestAddCommand))]
        TestAddCommand = 700,
        [Description(nameof(TestAddCommandTwo))]
        TestAddCommandTwo,
    }
}
