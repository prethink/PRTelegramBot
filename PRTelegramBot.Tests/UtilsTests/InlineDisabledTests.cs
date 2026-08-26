using FluentAssertions;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Tests.UtilsTests
{
    /// <summary>
    /// Bot API 10.3 lets a bot show an inline button that is visible but inert.
    /// </summary>
    public class InlineDisabledTests
    {
        [Test]
        public void ADisabledButtonKeepsItsLabelAndCarriesNoAction()
        {
            var button = new InlineDisabled("Coming soon").GetInlineButton();

            button.Text.Should().Be("Coming soon");
            button.Disabled.Should().NotBeNull("the button has to be marked disabled for Telegram to grey it out");
            button.CallbackData.Should().BeNull();
            button.Url.Should().BeNull();
        }

        [Test]
        public void TheLabelIsAllADisabledButtonCarries()
        {
            new InlineDisabled("Coming soon").GetContent().Should().Be("Coming soon");
        }

        [Test]
        public void InlineUtilsConvertsDisabledButtons()
        {
            var button = InlineUtils.GetInlineButton(new InlineDisabled("Locked"));

            button.Text.Should().Be("Locked");
            button.Disabled.Should().NotBeNull();
        }

        [Test]
        public void DisabledButtonsSitAlongsideLiveOnesInAMenu()
        {
            var keyboard = new InlineKeyboardBuilder()
                .AddButton(new InlineURL("Docs", "https://example.com"))
                .AddButton(new InlineDisabled("Locked"))
                .Build();

            var buttons = keyboard.InlineKeyboard.SelectMany(row => row).ToList();

            buttons.Should().HaveCount(2);
            buttons[0].Disabled.Should().BeNull();
            buttons[1].Disabled.Should().NotBeNull();
            buttons.Select(x => x.Text).Should().Equal("Docs", "Locked");
        }

        [Test]
        public void RenamingADisabledButtonChangesWhatTelegramShows()
        {
            var button = new InlineDisabled("Locked");
            button.SetButtonName("Unavailable on your plan");

            button.GetInlineButton().Text.Should().Be("Unavailable on your plan");
        }
    }
}
