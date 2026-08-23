using FluentAssertions;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.InlineButtons;

namespace PRTelegramBot.Tests.Builders
{
    public class InlineKeyboardBuilderTests
    {
        private static IInlineContent Button(string text) => new InlineURL(text, "https://example.com");

        [Test]
        public void AddButtonPutsButtonsOnOneRowByDefault()
        {
            var keyboard = new InlineKeyboardBuilder()
                .AddButton(Button("one"))
                .AddButton(Button("two"))
                .Build();

            keyboard.InlineKeyboard.Should().HaveCount(1);
            keyboard.InlineKeyboard.First().Should().HaveCount(2);
        }

        [Test]
        public void AddButtonWithNewRowStartsANewRow()
        {
            var keyboard = new InlineKeyboardBuilder()
                .AddButton(Button("one"))
                .AddButton(Button("two"), newRow: true)
                .Build();

            keyboard.InlineKeyboard.Should().HaveCount(2);
            keyboard.InlineKeyboard.First().Should().HaveCount(1);
            keyboard.InlineKeyboard.Last().Should().HaveCount(1);
        }

        [Test]
        public void AddRowWithButtonsCreatesASeparateRow()
        {
            var keyboard = new InlineKeyboardBuilder()
                .AddButton(Button("one"))
                .AddRowWithButtons(Button("two"), Button("three"))
                .Build();

            keyboard.InlineKeyboard.Should().HaveCount(2);
            keyboard.InlineKeyboard.Last().Should().HaveCount(2);
        }

        [Test]
        public void ClearRemovesEveryButton()
        {
            var builder = new InlineKeyboardBuilder()
                .AddButton(Button("one"))
                .AddRowWithButton(Button("two"));

            builder.Clear();

            builder.GetAllButtonsCount().Should().Be(0);
            builder.Build().InlineKeyboard.Should().BeEmpty();
        }

        [Test]
        public void GetAllButtonsCountCountsEveryRow()
        {
            var builder = new InlineKeyboardBuilder()
                .AddButton(Button("one"))
                .AddButton(Button("two"))
                .AddRowWithButtons(Button("three"), Button("four"), Button("five"));

            builder.GetAllButtonsCount().Should().Be(5);
        }

        [Test]
        public void GetRowReturnsTheButtonsOfThatRow()
        {
            var builder = new InlineKeyboardBuilder()
                .AddButton(Button("one"))
                .AddRowWithButtons(Button("two"), Button("three"));

            builder.GetRow(1).Select(x => x.GetButtonName()).Should().BeEquivalentTo("two", "three");
        }

        [Test]
        public void GetColumnReturnsTheButtonsOfThatColumn()
        {
            var builder = new InlineKeyboardBuilder()
                .AddButton(Button("a1"))
                .AddButton(Button("a2"))
                .AddRowWithButtons(Button("b1"), Button("b2"));

            builder.GetColumn(0).Select(x => x.GetButtonName()).Should().BeEquivalentTo("a1", "b1");
        }

        [Test]
        public void GetRowCountMatchesTheNumberOfRows()
        {
            var builder = new InlineKeyboardBuilder()
                .AddButton(Button("one"))
                .AddRowWithButton(Button("two"))
                .AddRowWithButton(Button("three"));

            builder.GetRowCount().Should().Be(3);
        }

        [Test]
        public void GetColumnCountMatchesTheWidestRow()
        {
            var builder = new InlineKeyboardBuilder()
                .AddButton(Button("one"))
                .AddRowWithButtons(Button("two"), Button("three"), Button("four"));

            builder.GetColumnCount().Should().Be(3);
        }

        [Test]
        public void GetRowWithInvalidIndexReturnsEmpty()
        {
            var builder = new InlineKeyboardBuilder().AddButton(Button("one"));

            builder.GetRow(5).Should().BeEmpty();
        }

        [Test]
        public void BuildOnAnEmptyBuilderProducesAnEmptyKeyboard()
        {
            new InlineKeyboardBuilder().Build().InlineKeyboard.Should().BeEmpty();
        }
    }
}
