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
            var replyCommandCount = bot.Handler.Router.ReplyCommandCount;
            Assert.AreEqual(4, replyCommandCount);
        }

        [Test]
        public void ReplyDynamicCommand()
        {
            var bot = new PRBotBuilder("55555:Token").SetBotId(0).Build();
            var replyDymanicCommandCount = bot.Handler.Router.ReplyDynamicCommandCount;
            Assert.AreEqual(3, replyDymanicCommandCount);
        }

        [Test]
        public void SlashCommand()
        {
            var bot = new PRBotBuilder("55555:Token").SetBotId(0).Build();
            var slashCommandCounts = bot.Handler.Router.SlashCommandCount;
            Assert.AreEqual(4, slashCommandCounts);
        }

        [Test]
        public void InlineCommand()
        {
            var bot = new PRBotBuilder("55555:Token").SetBotId(0).Build();
            var inlineCommandsCount = bot.Handler.Router.InlineCommandCount;
            Assert.AreEqual(5, inlineCommandsCount);
        }




    }
}
