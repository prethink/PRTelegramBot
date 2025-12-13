using PRTelegramBot.Builders;
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
        public async Task BotHasUserInAdminList()
        {
            var bot = new PRBotBuilder(BotOneToken).AddAdmin(UserId).Build();
            await bot.Initialize();
            Assert.True(await bot.IsAdmin(UserId));
        }

        [Test]
        public async Task BotHasUserInWhiteList()
        {
            var bot = new PRBotBuilder(BotOneToken).AddUserWhiteList(UserId).Build();
            await bot.Initialize();
            Assert.True(await bot.InWhiteList(UserId));
        }

        [Test]
        public void FindOneBotFromBotCollectionFromId()
        {
            var botOne = new PRBotBuilder(BotOneToken).Build();
            var botFromCollection = BotCollection.Instance.GetBotByTelegramIdOrNull(BotOneId);
            Assert.IsNotNull(botFromCollection);
        }

        [Test]
        public void FindBotFromBotCollectionFromId()
        {
            var botOne      = new PRBotBuilder(BotOneToken).SetBotId(0).Build();
            var botTwo      = new PRBotBuilder(BotTwoToken).SetBotId(1).Build();
            var botThree    = new PRBotBuilder(BotThreeToken).SetBotId(2).Build();

            var botFromCollection = BotCollection.Instance.GetBotByTelegramIdOrNull(BotTwoId);
            Assert.IsNotNull(botFromCollection);
        }

        [Test]
        public void GetAllBotsFromBotCollection()
        {
            var botOne = new PRBotBuilder(BotOneToken).SetBotId(0).Build();
            var botTwo = new PRBotBuilder(BotTwoToken).SetBotId(1).Build();
            var botThree = new PRBotBuilder(BotThreeToken).SetBotId(2).Build();

            var botFromCollection = BotCollection.Instance.GetBots();
            var exceptedBotCount = 3;
            Assert.AreEqual(exceptedBotCount, botFromCollection.Count());
        }
    }
}
