using FluentAssertions;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Tests.UtilsTests
{
    public class InlineUtilsTests
    {
        /// <summary>
        /// A button kind the library knows nothing about. It stands in for any button
        /// added later: if the conversion ever goes back to switching over concrete types,
        /// this one stops working and the test below fails.
        /// </summary>
        private sealed class CustomButton : InlineBase, IInlineContent
        {
            public CustomButton(string buttonName) : base(buttonName) { }

            public object GetContent() => ButtonName;

            public override InlineKeyboardButton GetInlineButton()
            {
                return InlineKeyboardButton.WithCallbackData(ButtonName, "custom-payload");
            }
        }

        [Test]
        public void GetInlineButtonDispatchesThroughTheButtonItself()
        {
            var button = InlineUtils.GetInlineButton(new CustomButton("custom"));

            button.Text.Should().Be("custom");
            button.CallbackData.Should().Be("custom-payload");
        }

        [Test]
        public void GetInlineButtonConvertsCopyTextButtons()
        {
            var button = InlineUtils.GetInlineButton(new InlineCopyText("Copy", "text to copy"));

            button.Text.Should().Be("Copy");
            button.CopyText!.Text.Should().Be("text to copy");
        }

        [Test]
        public void BuilderConvertsEveryButtonKindItIsGiven()
        {
            var keyboard = new InlineKeyboardBuilder()
                .AddButton(new InlineCopyText("Copy", "text to copy"))
                .AddButton(new InlineURL("Site", "https://example.com"))
                .AddButton(new CustomButton("custom"))
                .Build();

            var buttons = keyboard.InlineKeyboard.SelectMany(row => row).ToList();

            buttons.Should().HaveCount(3);
            buttons.Select(x => x.Text).Should().Equal("Copy", "Site", "custom");
        }

        [Test]
        public void GetInlineButtonRejectsNull()
        {
            var act = () => InlineUtils.GetInlineButton(null!);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
