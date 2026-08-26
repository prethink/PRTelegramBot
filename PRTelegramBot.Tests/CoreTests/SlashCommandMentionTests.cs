using FluentAssertions;
using PRTelegramBot.Core.CommandHandlers;

namespace PRTelegramBot.Tests.CoreTests
{
    /// <summary>
    /// In a group Telegram addresses a command to a bot by name: tapping /get_3 sends
    /// "/get_3@cs2_server_bot".
    /// </summary>
    /// <remarks>
    /// The suffix used to survive into the argument split, so a command declared with '_' as its
    /// separator saw "3@cs2", "server" and "bot" where it expected the single argument "3".
    /// </remarks>
    public class SlashCommandMentionTests
    {
        [Test]
        public void TheMentionComesOffTheCommand()
        {
            SlashCommandHandler.RemoveBotMention("/get_3@cs2_server_bot").Should().Be("/get_3");
        }

        [Test]
        public void TheArgumentSurvivesTheSplit()
        {
            var command = SlashCommandHandler.RemoveBotMention("/get_3@cs2_server_bot");

            command.Split('_').Skip(1).Should().Equal(new[] { "3" },
                "a bot name containing the separator used to be split into arguments of its own");
        }

        [Test]
        public void ACommandWithoutAMentionIsLeftAlone()
        {
            SlashCommandHandler.RemoveBotMention("/get_3").Should().Be("/get_3");
        }

        [Test]
        public void ArgumentsAfterASpaceAreKept()
        {
            SlashCommandHandler.RemoveBotMention("/start@cs2_server_bot payload").Should().Be("/start payload");
        }

        [Test]
        public void AnAtSignInAnArgumentIsNotAMention()
        {
            SlashCommandHandler.RemoveBotMention("/mail user@example.com").Should().Be("/mail user@example.com");
        }

        /// <summary>
        /// The mention is taken off whoever it names. A group can hold several bots, and each
        /// answers what it recognises — the framework does not decide that here.
        /// </summary>
        [Test]
        public void TheMentionComesOffWhoeverItNames()
        {
            SlashCommandHandler.RemoveBotMention("/get_3@some_other_bot").Should().Be("/get_3");
        }

        [Test]
        public void ATrailingAtSignComesOffToo()
        {
            SlashCommandHandler.RemoveBotMention("/get_3@").Should().Be("/get_3");
        }

        [Test]
        public void ACommandWithNothingButAMentionKeepsItsName()
        {
            SlashCommandHandler.RemoveBotMention("/start@cs2_server_bot").Should().Be("/start");
        }
    }
}
