using PRTelegramBot.Core;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class CommandsTests
    {
        [TearDown]
        public void Cleanup()
        {
            BotCollection.Instance.ClearBots();
        }

        [Test]
        public void ReplyCommand()
        {
            var bot = new PRBotBuilder("55555:Token").SetBotId(0).Build();
            bot.ReloadHandlers();
            var replyCommandCount = ((Handler)(bot.Handler)).ReplyCommandsStore.CommandCount;
            Assert.AreEqual(5, replyCommandCount);
        }

        [Test]
        public void ReplyDynamicCommand()
        {
            var bot = new PRBotBuilder("55555:Token")
                .SetBotId(0)
                .AddReplyDynamicCommand(nameof(FindMethodsTests.KEY_DYNAMIC_REPLY_ONE), "TestDynamicOne")
                .AddReplyDynamicCommand(nameof(FindMethodsTests.KEY_DYNAMIC_REPLY_FOUR), "TestDynamicTwo")
                .AddReplyDynamicCommand(nameof(FindMethodsTests.KEY_DYNAMIC_REPLY_FIVE), "TestDynamicThree")
                .Build();
            bot.ReloadHandlers();
            var replyDymanicCommandCount = ((Handler)(bot.Handler)).ReplyDynamicCommandsStore.CommandCount;
            Assert.AreEqual(3, replyDymanicCommandCount);
        }

        [Test]
        public void SlashCommand()
        {
            var bot = new PRBotBuilder("55555:Token").SetBotId(0).Build();
            bot.ReloadHandlers();
            var slashCommandCounts = ((Handler)(bot.Handler)).SlashCommandsStore.CommandCount;
            Assert.AreEqual(4, slashCommandCounts);
        }

        [Test]
        public void InlineCommand()
        {
            var bot = new PRBotBuilder("55555:Token").SetBotId(0).Build();
            bot.ReloadHandlers();
            var inlineCommandsCount = ((Handler)(bot.Handler)).CallbackQueryCommandsStore.CommandCount;
            Assert.AreEqual(12, inlineCommandsCount);
        }
    }
}
