using FluentAssertions;
using PRTelegramBot.Builders;
using PRTelegramBot.Core;
using PRTelegramBot.Tests.TestModels.TestHandlers;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.CoreTests
{
    /// <summary>
    /// The whole path a slash command takes: message text, through the dispatcher, into
    /// <c>GetSlashArgs</c>.
    /// </summary>
    internal class SlashCommandArgsFlowTests
    {
        private PRBotBase bot { get; set; } = null!;

        [OneTimeSetUp]
        public void SetUp()
        {
            bot = new PRBotBuilder("5555:Token").SetBotId(SlashArgsTestHandler.SlashArgsBotId).Build();
            bot.ReloadHandlers();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            BotCollection.Instance.ClearBots();
        }

        [SetUp]
        public void Reset() => SlashArgsTestHandler.Reset();

        private static Update CommandUpdate(string text)
        {
            return new Update
            {
                Message = new Message
                {
                    Id = 1,
                    Text = text,
                    Chat = new Chat { Id = 555555 },
                    From = new User { Id = 111 }
                }
            };
        }

        [Test]
        public async Task ArgumentsArriveWithoutTheBotMention()
        {
            await bot.Handler.HandleUpdateAsync(bot.BotClient, CommandUpdate("/argtest_3@cs2_server_bot"), CancellationToken.None);

            SlashArgsTestHandler.LastArgs.Should().Equal(new[] { "3" },
                "the bot name carries the separator, and used to be split into arguments of its own");
        }

        [Test]
        public async Task ArgumentsArriveFromACommandWithoutAMention()
        {
            await bot.Handler.HandleUpdateAsync(bot.BotClient, CommandUpdate("/argtest_3"), CancellationToken.None);

            SlashArgsTestHandler.LastArgs.Should().Equal(new[] { "3" });
        }

        [Test]
        public async Task SeveralArgumentsAreKept()
        {
            await bot.Handler.HandleUpdateAsync(bot.BotClient, CommandUpdate("/argtest_3_7@cs2_server_bot"), CancellationToken.None);

            SlashArgsTestHandler.LastArgs.Should().Equal(new[] { "3", "7" });
        }
    }
}
