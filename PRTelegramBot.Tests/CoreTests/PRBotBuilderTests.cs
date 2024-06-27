using FluentAssertions;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Core.Factory;
using PRTelegramBot.Models.Enums;

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
        public void BuilderShouldCreateInstance()
        {
            var builder = new PRBotBuilder(TOKEN);
            var bot = builder.Build();

            Assert.That(bot, Is.Not.Null);
        }

        [Test]
        public void BuilderShouldCreateInstanceWithToken()
        {
            var builder = new PRBotBuilder(TOKEN);
            var bot = builder.Build();

            Assert.AreEqual(TOKEN, bot.Options.Token);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void BuilderShouldCreateInstanceWithBotId(long exceptedBotId)
        {
            var builder = new PRBotBuilder(TOKEN)
                            .SetBotId(exceptedBotId);
            var bot = builder.Build();

            Assert.AreEqual(exceptedBotId, bot.Options.BotId);
        }

        [Test]
        [TestCase(1)]
        public void BuilderShouldCreateInstanceWithNextBotId(int exceptedBotId)
        {
            var builder = new PRBotBuilder(TOKEN)
                            .SetBotId(BotCollection.GetNextId());
            var bot = builder.Build();

            Assert.AreEqual(exceptedBotId, bot.Options.BotId);
        }

        [Test]
        [TestCase(654745634)]
        [TestCase(3523532)]
        [TestCase(2151612)]
        public async Task BuilderShouldCreateBotWithAdmin(long telegramId)
        {
            var builder = new PRBotBuilder(TOKEN).AddAdmin(telegramId);
            var bot = builder.Build();

            var userIds = await bot.Options.AdminManager.GetUsersIds();
            userIds.Should().BeEquivalentTo(new[] { telegramId });
        }

        [Test]
        [TestCase(654745634)]
        [TestCase(3523532)]
        [TestCase(2151612)]
        public async Task BuilderShouldCreateBotWithAdmins(long telegramId)
        {
            var builder = new PRBotBuilder(TOKEN).AddAdmins(new List<long>() { telegramId });
            var bot = builder.Build();
            var userIds = await bot.Options.AdminManager.GetUsersIds();
            userIds.Should().BeEquivalentTo(new[] { telegramId });
        }

        [Test]
        [TestCase(654745634)]
        [TestCase(3523532)]
        [TestCase(2151612)]
        public async Task BuilderShouldCreateBotWithUserWhiteList(long telegramId)
        {
            var builder = new PRBotBuilder(TOKEN).AddUserWhiteList(telegramId);
            var bot = builder.Build();
            var userIds = await bot.Options.WhiteListManager.GetUsersIds();
            userIds.Should().BeEquivalentTo(new[] { telegramId });
        }

        [Test]
        [TestCase(654745634)]
        [TestCase(3523532)]
        [TestCase(2151612)]
        public async Task BuilderShouldCreateBotWithUsersWhiteList(long telegramId)
        {
            var builder = new PRBotBuilder(TOKEN).AddUsersWhiteList(new List<long>() { telegramId });
            var bot = builder.Build();
            var userIds = await bot.Options.WhiteListManager.GetUsersIds();
            userIds.Should().BeEquivalentTo(new[] { telegramId });
        }

        [Test]
        [TestCase("Main", "Привет")]
        [TestCase("Test", "Тест")]
        [TestCase("Reply", "Бест")]
        public void BuilderShouldCreateBotWithReplyDynamicCommand(string key, string command)
        {
            var builder = new PRBotBuilder(TOKEN).AddReplyDynamicCommand(key, command);
            var bot = builder.Build();

            bot.Options.ReplyDynamicCommands.ContainsKey(key).Should().BeTrue();    
            Assert.IsTrue(bot.Options.ReplyDynamicCommands.GetValueOrDefault(key) == command);    
        }

        [Test]
        [TestCase("Main", "Привет")]
        [TestCase("Test", "Тест")]
        [TestCase("Reply", "Бест")]
        public void BuilderShouldCreateBotWithReplyDynamicCommands(string key, string command)
        {
            var builder = new PRBotBuilder(TOKEN).AddReplyDynamicCommands(new Dictionary<string, string>() { { key, command } });
            var bot = builder.Build();

            bot.Options.ReplyDynamicCommands.ContainsKey(key).Should().BeTrue();
            Assert.IsTrue(bot.Options.ReplyDynamicCommands.GetValueOrDefault(key) == command);
        }

        [Test]
        [TestCase("Main", "D:\\Unity")]
        [TestCase("Test", "D:\\Test")]
        [TestCase("Reply", "D:\\Vaer")]
        public void BuilderShouldCreateBotWithConfigPath(string key, string path)
        {
            var builder = new PRBotBuilder(TOKEN).AddConfigPath(key, path);
            var bot = builder.Build();

            bot.Options.ConfigPaths.ContainsKey(key).Should().BeTrue();
            Assert.IsTrue(bot.Options.ConfigPaths.GetValueOrDefault(key) == path);
        }

        [Test]
        [TestCase("Main", "D:\\Unity")]
        [TestCase("Test", "D:\\Test")]
        [TestCase("Reply", "D:\\Vaer")]
        public void BuilderShouldCreateBotWithConfigPaths(string key, string path)
        {
            var builder = new PRBotBuilder(TOKEN).AddConfigPaths(new Dictionary<string, string>() { { key, path } });
            var bot = builder.Build();

            bot.Options.ConfigPaths.ContainsKey(key).Should().BeTrue();
            Assert.IsTrue(bot.Options.ConfigPaths.GetValueOrDefault(key) == path);
        }

        [Test]
        public void BuilderShouldCreateBotWithClearUpdatesOnStart([Values] bool exceptedFlag)
        {
            var builder = new PRBotBuilder(TOKEN).SetClearUpdatesOnStart(exceptedFlag);
            var bot = builder.Build();

            Assert.IsTrue(bot.Options.ClearUpdatesOnStart == exceptedFlag);
        }

        #region WebHook

        [Test]
        public void PRBuilderShouldCreateBotWithDataRetrievalMethodWebhook()
        {
            var bot = new PRBotBuilder(TOKEN).UseFactory(new PRBotWebHookFactory()).Build();
            Assert.AreEqual(DataRetrievalMethod.WebHook, bot.DataRetrieval);
        }

        [Test]
        public void PRBuilderShouldCreateWebhookInstance()
        {
            var bot = new PRBotBuilder(TOKEN).UseFactory(new PRBotWebHookFactory()).Build();
            Assert.IsTrue(bot.GetType().Equals(typeof(PRBotWebHook)));
        }

        [Test]
        public void PRBuilderShouldCreateWebhookInstanceWithWebHookOptions()
        {
            var bot = new PRBotBuilder(TOKEN).UseFactory(new PRBotWebHookFactory()).Build();
            Assert.IsTrue(bot.Options.GetType().Equals(typeof(WebHookTelegramOptions)));
        }

        #endregion
    }
}
