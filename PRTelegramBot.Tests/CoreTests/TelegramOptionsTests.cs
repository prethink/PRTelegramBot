using PRTelegramBot.Configs;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class TelegramOptionsTests
    {
        [Test]
        public void TelegramOptionsClone()
        {
            var options = new TelegramOptions();
            options.Token = "5555:TOKEN";
            options.ClearUpdatesOnStart = true;
            options.BotId = 10;
            options.Admins.Add(5555);
            options.WhiteListUsers.Add(6666);
            options.ReplyDynamicCommands.Add("Test", "Test");
            options.ConfigPaths.Add("Test", "Test");

            var cloneOptions = options.Clone() as TelegramOptions;
            Assert.AreEqual(options.Token, cloneOptions.Token);
            Assert.AreEqual(options.ClearUpdatesOnStart, cloneOptions.ClearUpdatesOnStart);
            Assert.AreEqual(options.BotId, cloneOptions.BotId);
            Assert.AreEqual(options.Admins, cloneOptions.Admins);
            Assert.AreEqual(options.WhiteListUsers, cloneOptions.WhiteListUsers);
            Assert.AreEqual(options.ReplyDynamicCommands, cloneOptions.ReplyDynamicCommands);
            Assert.AreEqual(options.ConfigPaths, cloneOptions.ConfigPaths);
        }
    }
}
