using FluentAssertions;
using PRTelegramBot.Builders;
using PRTelegramBot.Core;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class BotCollectionTests
    {
        [TearDown]
        public void TearDown() => BotCollection.Instance.ClearBots();

        /// <summary>
        /// A bot only learns its own name once <c>getMe</c> comes back, and that call is made at
        /// startup. Looking a bot up before then — or after the call failed — used to throw a
        /// <see cref="NullReferenceException"/> from inside the lookup.
        /// </summary>
        [Test]
        public void LookingUpByNameSurvivesABotThatDoesNotKnowItsNameYet()
        {
            var bot = new PRBotBuilder("5555:Token").SetBotId(4242).Build();
            bot.BotName.Should().BeNull("the bot has not been started, so getMe has not run");

            var found = () => BotCollection.Instance.GetBotOrNull("anything");

            found.Should().NotThrow();
            found().Should().BeNull();
        }
    }
}
