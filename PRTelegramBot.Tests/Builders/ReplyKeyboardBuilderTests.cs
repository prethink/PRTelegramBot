using FluentAssertions;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Extensions;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Tests.Builders
{
    public class ReplyKeyboardBuilderTests
    {
        [Test]
        [TestCase(" ")]
        [TestCase("_")]
        [TestCase("X")]
        public void CreateEmptyButton(string emptyName)
        {
            var keyboard = new ReplyKeyboardBuilder()
                .AddEmptyButton()
                .SetEmptyButtonsName(emptyName)
                .Build();

            keyboard.Keyboard.Should().HaveCount(1);
            keyboard.Keyboard.First().Should().HaveCount(1);
            keyboard.Keyboard.First().First().Text.Should().Be(emptyName);
        }

        [Test]
        public void ClearButtons()
        {
            var keyboardBuilder = new ReplyKeyboardBuilder()
                .AddButton("Test")
                .AddButton("Test")
                .AddButton("Test")
                .AddButton("Test")
                .AddButton("Test")
                .AddButton("Test");

            keyboardBuilder.Clear();
            var keyboard = keyboardBuilder.Build();

            keyboardBuilder.GetAllButtonsCount().Should().Be(0);
            keyboard.Keyboard.Should().BeEmpty();
        }

        [Test]
        public void BuilderShouldSetInputFieldPlaceholder()
        {
            var expectedPlaceholder = "Enter your response here...";
            var keyboard = new ReplyKeyboardBuilder()
                .SetInputFieldPlaceholder(expectedPlaceholder)
                .Build();

            keyboard.InputFieldPlaceholder.Should().Be(expectedPlaceholder);
        }

        [Test]
        public void BuilderShouldSetOneTimeKeyboard()
        {
            var keyboard = new ReplyKeyboardBuilder()
                .SetOneTimeKeyboard(true)
                .Build();

            keyboard.OneTimeKeyboard.Should().Be(true);
        }

        [Test]
        public void BuilderShouldSetPersistent()
        {
            var keyboard = new ReplyKeyboardBuilder()
                .SetPersistent(true)
                .Build();

            keyboard.IsPersistent.Should().Be(true);
        }

        [Test]
        public void BuilderShouldSetSelective()
        {
            var keyboard = new ReplyKeyboardBuilder()
                .SetSelective(true)
                .Build();

            keyboard.Selective.Should().Be(true);
        }

        [Test]
        public void BuilderShouldSetResizeKeyboard()
        {
            var keyboard = new ReplyKeyboardBuilder()
                .SetResizeKeyboard(true)
                .Build();

            keyboard.ResizeKeyboard.Should().Be(true);
        }

        [Test]
        public void Build_ShouldPlaceButtonsInCorrectRows()
        {
            var keyboard = new ReplyKeyboardBuilder()
                .AddButton("Row 1 - Button 1")
                .AddButton("Row 2 - Button 1", true).AddButton("Row 2 - Button 2").AddButton("Row 2 - Button 3")
                .AddRow()
                .AddButton("Row 3 - Button 1")
                .AddRowWithButton("Row 4 - Button 1")
                .AddRowWithButtons(new KeyboardButton("Row 5 - Button 1"), new KeyboardButton("Row 5 - Button 2"), new KeyboardButton("Row 5 - Button 3"))
                .Build();

            var resultKeyboard = keyboard.Keyboard.ToList();

            resultKeyboard.GetRowCount().Should().Be(RowCount(5));

            resultKeyboard[RowIndex(0)].Should().HaveCount(ButtonsCount(1));
            resultKeyboard[RowIndex(1)].Should().HaveCount(ButtonsCount(3));
            resultKeyboard[RowIndex(2)].Should().HaveCount(ButtonsCount(1));
            resultKeyboard[RowIndex(3)].Should().HaveCount(ButtonsCount(1));
            resultKeyboard[RowIndex(4)].Should().HaveCount(ButtonsCount(3));
        }

        [Test]
        public void AddMainMenuButtonTop()
        {
            var keyboard = new ReplyKeyboardBuilder()
                .SetMainMenuButton("Main Menu", MainMenuButtonPosition.Top)
                .AddButton("Button 1")
                .AddButton("Button 2")
                .AddRow()
                .AddButton("Button 3")
                .Build();

            var resultKeyboard = keyboard.Keyboard.ToList();

            resultKeyboard.GetRowCount().Should().Be(RowCount(3));
            resultKeyboard[RowIndex(0)].First().Text.Should().Be("Main Menu");
        }

        [Test]
        public void AddMainMenuButtonBottom()
        {
            var keyboard = new ReplyKeyboardBuilder()
                .SetMainMenuButton("Main Menu", MainMenuButtonPosition.Bottom)
                .AddButton("Button 1")
                .AddButton("Button 2")
                .AddRow()
                .AddButton("Button 3")
                .Build();

            var resultKeyboard = keyboard.Keyboard.ToList();

            resultKeyboard.GetRowCount().Should().Be(RowCount(3));
            resultKeyboard[RowIndex(2)].First().Text.Should().Be("Main Menu");
        }

        private int ButtonsCount(int count) => count;

        private int RowCount(int count) => count;

        private int RowIndex(int index) => index;
    }
}
