using System.Reflection;
using FluentAssertions;
using PRTelegramBot.Core;
using PRTelegramBot.Core.Events;
using PRTelegramBot.Core.UpdateHandlers;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;

namespace PRTelegramBot.Tests.CoverageGuards
{
    /// <summary>
    /// Guards that fail when Telegram.Bot is updated and the framework has not caught up.
    /// </summary>
    /// <remarks>
    /// Updating the Telegram.Bot package brings new <see cref="MessageType"/> and
    /// <see cref="UpdateType"/> values, and new parameters on the send requests. Those
    /// additions are silent: the code keeps compiling and the new kinds of update simply
    /// never reach anyone. Several were missed across versions 22.8 to 22.10 and only
    /// surfaced when the enums were compared against the dispatcher by hand.
    ///
    /// When one of these tests fails, it is not a broken test — it is a to-do list.
    /// </remarks>
    public class TelegramBotApiCoverageTests
    {
        /// <summary>
        /// Text messages are routed through the command pipeline in
        /// <c>UpdateMessageCommands</c>, not through the event dictionary.
        /// </summary>
        private static readonly MessageType[] MessageTypesHandledElsewhere = { MessageType.Text };

        /// <summary>
        /// Message updates are routed to the command pipeline and the message events,
        /// so there is no single "OnMessageHandle" event for them.
        /// </summary>
        private static readonly UpdateType[] UpdateTypesHandledElsewhere = { UpdateType.Message };

        /// <summary>
        /// Invoke methods that are called directly instead of through the dictionary.
        /// </summary>
        private static readonly string[] InvokersCalledDirectly = { "OnTextHandleInvoke" };

        private static PRBotBase CreateBot()
        {
            return new PRBotDummy(opt =>
            {
                opt.Client = new TelegramBotClient("35425:token");
                opt.Token = "35425:token";
                opt.BotId = 1;
            }, null);
        }

        private static MessageUpdateDispatcher CreateDispatcher() => new(CreateBot());

        [Test]
        public void EveryMessageTypeIsRoutedToAnEvent()
        {
            var dispatcher = CreateDispatcher();

            var missing = Enum.GetValues<MessageType>()
                .Except(MessageTypesHandledElsewhere)
                .Where(type => !dispatcher.TypeMessage.ContainsKey(type))
                .ToList();

            missing.Should().BeEmpty(
                "every MessageType needs an event. Declare it in MessageEvents and register it in " +
                "MessageUpdateDispatcher.UpdateEventLink. Missing: {0}",
                string.Join(", ", missing));
        }

        [Test]
        public void EveryUpdateTypeHasAnEvent()
        {
            var declared = typeof(UpdateEvents)
                .GetEvents(BindingFlags.Public | BindingFlags.Instance)
                .Select(x => x.Name)
                .ToHashSet();

            var missing = Enum.GetValues<UpdateType>()
                .Except(UpdateTypesHandledElsewhere)
                .Where(type => !declared.Contains($"On{type}Handle"))
                .ToList();

            missing.Should().BeEmpty(
                "every UpdateType needs an On{{Type}}Handle event in UpdateEvents, raised from " +
                "Handler. Missing: {0}",
                string.Join(", ", missing));
        }

        /// <summary>
        /// Declaring an event is not enough — it has to be registered in the dispatcher,
        /// or it never fires. <c>OnPaidMessagePriceChangedHandle</c> stayed declared and
        /// unwired for a whole release.
        /// </summary>
        [Test]
        public void EveryMessageEventIsActuallyRaised()
        {
            var dispatcher = CreateDispatcher();

            var wired = dispatcher.TypeMessage.Values
                .Select(x => x.Method.Name)
                .ToHashSet();

            var declared = typeof(MessageEvents)
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name.StartsWith("On") && x.Name.EndsWith("Invoke"))
                .Select(x => x.Name)
                .Except(InvokersCalledDirectly)
                .ToList();

            var dead = declared.Where(name => !wired.Contains(name)).ToList();

            dead.Should().BeEmpty(
                "these message events are declared but never raised, so subscribers of them would " +
                "wait forever. Register them in MessageUpdateDispatcher.UpdateEventLink. Dead: {0}",
                string.Join(", ", dead));
        }

        /// <summary>
        /// Catches new send parameters appearing in the Bot API.
        /// </summary>
        /// <remarks>
        /// This test does not judge whether a parameter belongs on <c>OptionMessage</c> — some are
        /// supplied separately, and some are irrelevant. It only makes a new parameter impossible
        /// to miss. When it fails, decide what to do with the new one and then add it to this list.
        /// </remarks>
        [Test]
        public void SendMessageHasNoUnreviewedParameters()
        {
            var known = new[]
            {
                // supplied by the caller, not through OptionMessage
                "ChatId", "Text",
                // mapped from OptionMessage
                "ParseMode", "Entities", "LinkPreviewOptions", "ReplyParameters", "ReplyMarkup",
                "MessageThreadId", "DisableNotification", "ProtectContent",
                "BusinessConnectionId", "MessageEffectId", "AllowPaidBroadcast",
                "DirectMessagesTopicId", "SuggestedPostParameters",
                // reviewed and deliberately not exposed
                "ReceiverUserId", "CallbackQueryId"
            };

            var actual = typeof(SendMessageRequest)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Select(x => x.Name)
                .ToList();

            var unreviewed = actual.Except(known).ToList();

            unreviewed.Should().BeEmpty(
                "Telegram.Bot added send parameters that nobody has looked at yet. Decide whether " +
                "OptionMessage should expose them, then list them in this test. New: {0}",
                string.Join(", ", unreviewed));
        }

        /// <summary>
        /// The counterpart of the check above: makes sure a parameter this test claims to know
        /// about has not disappeared from the Bot API.
        /// </summary>
        [Test]
        public void EveryOptionMessagePropertyStillExistsOnTheRequest()
        {
            var forwarded = new[]
            {
                "BusinessConnectionId", "MessageEffectId", "AllowPaidBroadcast",
                "DirectMessagesTopicId", "SuggestedPostParameters"
            };

            var actual = typeof(SendMessageRequest)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(x => x.Name)
                .ToHashSet();

            var gone = forwarded.Where(name => !actual.Contains(name)).ToList();

            gone.Should().BeEmpty(
                "OptionMessage forwards parameters that no longer exist on SendMessageRequest. " +
                "Removed: {0}",
                string.Join(", ", gone));
        }
    }
}
