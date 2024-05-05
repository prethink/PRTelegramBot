using PRTelegramBot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class PRBotBuilderTests
    {
        private const string TOKEN = "555555:tokensfasfasfasfasfasfasfaza";

        [TearDown]
        public void Cleanup()
        {
            BotCollection.Instance.ClearBots();
        }

        [Test]
        public void BuildInstancePRBot()
        {
            var builder = new PRBotBuilder(TOKEN);
            var bot = builder.Build();

            Assert.IsNotNull(bot);
        }

        [Test]
        public void TokenBuilderShouldBeInstance()
        {
            var builder = new PRBotBuilder(TOKEN);
            var bot = builder.Build();

            Assert.AreEqual(TOKEN, bot.Options.Token);
        }
    }
}
