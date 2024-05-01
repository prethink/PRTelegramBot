using PRTelegramBot.Core;
using PRTelegramBot.Extensions;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class PRBotTests
    {
        private const long BotOneId = 123456;
        private const long BotTwoId = 654321;
        private const long BotThreeId = 321654;

        private const string BotOneToken = $"123456:gdafgderafgerwtw4tywest";
        private const string BotTwoToken = $"654321:gdafgderafgerwtw4tywest";
        private const string BotThreeToken = $"321654:gdafgderafgerwtw4tywest";

        private const long UserId = 5555555;

        [TearDown]
        public void Cleanup()
        {
            BotCollection.Instance.ClearBots();
        }

        [Test]
        public void BotHasUserInAdminList()
        {
            var bot = new PRBot(config =>
            {
                config.Token = BotOneToken;
                config.Admins.Add(UserId);
            });

            Assert.True(bot.IsAdmin(UserId));
        }

        [Test]
        public void BotHasUserInWhiteList()
        {
            var bot = new PRBot(config =>
            {
                config.Token = BotOneToken;
                config.WhiteListUsers.Add(UserId);
            });

            Assert.True(bot.InWhiteList(UserId));
        }

        [Test]
        public void FindOneBotFromBotCollectionFromId()
        {
            var botOne = new PRBot(config => { config.Token = BotOneToken; });

            var botFromCollection = BotCollection.Instance.GetBotByTelegramIdOrNull(BotOneId);
            Assert.IsNotNull(botFromCollection);
        }

        [Test]
        public void FindBotFromBotCollectionFromId()
        {
            var botOne = new PRBot(config => { config.Token = BotOneToken; });
            var botTwo = new PRBot(config => { config.Token = BotTwoToken; });
            var botThree = new PRBot(config => { config.Token = BotThreeToken; });

            var botFromCollection = BotCollection.Instance.GetBotByTelegramIdOrNull(BotTwoId);
            Assert.IsNotNull(botFromCollection);
        }

        [Test]
        public void GetAllBotsFromBotCollection()
        {
            var botOne = new PRBot(config => { config.Token = BotOneToken; });
            var botTwo = new PRBot(config => { config.Token = BotTwoToken; });
            var botThree = new PRBot(config => { config.Token = BotThreeToken; });

            var botFromCollection = BotCollection.Instance.GetBots();
            var exceptedBotCount = 3;
            Assert.AreEqual(exceptedBotCount, botFromCollection.Count);
        }
    }
}
