using PRTelegramBot.Core;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Models.TCommands;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class InlineCallbackConverterTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            var bot = new PRBotBuilder("55555:Token").Build();
            bot.ReloadHandlers();
        }

        [TearDown]
        public void Cleanup()
        {
            BotCollection.Instance.ClearBots();
        }

        [Test]
        [TestCase(1)]
        [TestCase(10, 2)]
        [TestCase(100, 50)]
        public void EntityTCommandShouldReturnLongAfterConvertation(long exceptedLong, int exceptedCommandId = 0)
        {
            var exceptedCommandType = PRTelegramBotCommand.PickYear;
            var command = new InlineCallback<EntityTCommand<long>>("Пример 2", exceptedCommandType, new EntityTCommand<long>(exceptedLong, exceptedCommandId));
            var json = command.GetContent() as string;
            var exportCommand = InlineCallback<EntityTCommand<long>>.GetCommandByCallbackOrNull(json);
            Assert.AreEqual(exceptedLong, exportCommand.Data.EntityId);
            Assert.AreEqual(exceptedCommandId, exportCommand.Data.HeaderCallbackCommand);
            Assert.AreEqual(exceptedCommandType, exportCommand.CommandType);
        }

        [Test]
        [TestCase("Hello")]
        [TestCase("Test", 2)]
        [TestCase("Hammer", 20)]
        public void EntityTCommandShouldReturnStringAfterConvertation(string exceptedString, int exceptedCommandId = 0)
        {
            var exceptedCommandType = PRTelegramBotCommand.PickYear;
            var command = new InlineCallback<EntityTCommand<string>>("Пример 2", exceptedCommandType, new EntityTCommand<string>(exceptedString, exceptedCommandId));
            var json = command.GetContent() as string;
            var exportCommand = InlineCallback<EntityTCommand<string>>.GetCommandByCallbackOrNull(json);
            Assert.AreEqual(exceptedString, exportCommand.Data.EntityId);
            Assert.AreEqual(exceptedCommandId, exportCommand.Data.HeaderCallbackCommand);
            Assert.AreEqual(exceptedCommandType, exportCommand.CommandType);
        }

        [Test]
        [TestCase(1)]
        [TestCase(10, 2)]
        [TestCase(100, 4)]
        public void PageTCommandShouldReturnIntPageAfterConvertation(int exceptedPage, int exceptedCommandId = 0)
        {
            var exceptedCommandType = PRTelegramBotCommand.PickYear;
            var command = new InlineCallback<PageTCommand>("Пример 2", exceptedCommandType, new PageTCommand(exceptedPage, PRTelegramBotCommand.None, exceptedCommandId));
            var json = command.GetContent() as string;
            var exportCommand = InlineCallback<PageTCommand>.GetCommandByCallbackOrNull(json);
            Assert.AreEqual(exceptedPage, exportCommand.Data.Page);
            Assert.AreEqual(exceptedCommandId, exportCommand.Data.HeaderCallbackCommand);
            Assert.AreEqual(exceptedCommandType, exportCommand.CommandType);
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public void TCommandBaseShouldReturnCommandIdAfterConvertation(int exceptedCommandId)
        {
            var exceptedCommandType = PRTelegramBotCommand.PickYear;
            var command = new InlineCallback<TCommandBase>("Пример 2", exceptedCommandType, new TCommandBase(exceptedCommandId));
            var json = command.GetContent() as string;
            var exportCommand = InlineCallback<TCommandBase>.GetCommandByCallbackOrNull(json);
            Assert.AreEqual(exceptedCommandId, exportCommand.Data.HeaderCallbackCommand);
            Assert.AreEqual(exceptedCommandType, exportCommand.CommandType);
        }

        [Test]
        [TestCase(PRTelegramBotCommand.None)]
        [TestCase(PRTelegramBotCommand.PickYear)]
        [TestCase(PRTelegramBotCommand.CurrentPage)]
        [TestCase(PRTelegramBotCommand.ChangeTo)]
        [TestCase(PRTelegramBotCommand.NextPage)]
        public void NonGenericShouldReturnCommandTypeAfterConvertation(PRTelegramBotCommand exceptedCommand)
        {
            var command = new InlineCallback("Пример 2", exceptedCommand);
            var json = command.GetContent() as string;
            var exportCommand = InlineCallback.GetCommandByCallbackOrNull(json);
            Assert.AreEqual(exceptedCommand, exportCommand.CommandType);
        }

        [Test]
        [TestCase("2020-01-04")]
        [TestCase("2021-03-02")]
        [TestCase("2023-08-07")]
        public void CalendarTCommandShouldReturnDateTimeAfterConvertation(DateTime exceptedDate)
        {
            int exceptedCommandId = 5;
            var exceptedCommandType = PRTelegramBotCommand.PickYear;
            var command = new InlineCallback<CalendarTCommand>("Тест", exceptedCommandType, new CalendarTCommand(exceptedDate, exceptedCommandId));
            var json = command.GetContent() as string;
            var exportCommand = InlineCallback<CalendarTCommand>.GetCommandByCallbackOrNull(json);
            Assert.AreEqual(exceptedDate, exportCommand.Data.Date);
            Assert.AreEqual(exceptedCommandId, exportCommand.Data.HeaderCallbackCommand);
            Assert.AreEqual(exceptedCommandType, exportCommand.CommandType);
        }
    }
}
