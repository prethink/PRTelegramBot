using FluentAssertions;
using PRTelegramBot.Builders;
using PRTelegramBot.Core;
using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;

namespace PRTelegramBot.Tests.CoreTests
{
    /// <summary>
    /// Walks the confirmation button through the same steps the framework takes at runtime:
    /// the button is built, turned into a keyboard, and the callback data it carries is parsed
    /// back and looked up the way <c>InlineConfirmation.ActionWithConfirmation</c> does.
    /// </summary>
    internal class InlineConfirmationFlowTests
    {
        private PRBotBase botInstance;
        private IBotContext context;

        [SetUp]
        public void SetUp()
        {
            botInstance = new PRBotBuilder("55555:Token").Build();
            botInstance.ReloadHandlers();
            context = TestDataFactory.CreateBotContext();
        }

        [TearDown]
        public void Cleanup()
        {
            BotCollection.Instance.ClearBots();
        }

        private static InlineCallbackWithConfirmation BuildConfirmation()
        {
            var target = new InlineCallback<EntityTCommand<long>>(
                "Button with confirmation",
                PRTelegramBotCommand.PickDate,
                new EntityTCommand<long>(3, ActionWithLastMessage.Delete));

            return new InlineCallbackWithConfirmation(target, ActionWithLastMessage.Delete);
        }

        [Test]
        public void ConfirmationButtonSurvivesTheRoundTripThroughTheMenu()
        {
            using (new BotDataScope(context, botInstance))
            {
                var confirmation = BuildConfirmation();

                var keyboard = MenuGenerator.InlineKeyboard(1, new List<IInlineContent> { confirmation });
                var callbackData = keyboard.InlineKeyboard.Single().Single().CallbackData;

                callbackData.Should().NotBeNullOrEmpty();

                var parsed = InlineCallback<EntityTCommand<string>>.GetCommandByCallbackOrNull(callbackData!);

                parsed.Should().NotBeNull();
                parsed.CommandType.Should().Be(PRTelegramBotCommand.CallbackWithConfirmation);
                parsed.Data.EntityId.Should().NotBeNullOrEmpty();
            }
        }

        [Test]
        public void PendingConfirmationIsFoundByTheIdCarriedInTheCallbackData()
        {
            using (new BotDataScope(context, botInstance))
            {
                var confirmation = BuildConfirmation();

                var keyboard = MenuGenerator.InlineKeyboard(1, new List<IInlineContent> { confirmation });
                var callbackData = keyboard.InlineKeyboard.Single().Single().CallbackData;
                var parsed = InlineCallback<EntityTCommand<string>>.GetCommandByCallbackOrNull(callbackData!);

                var found = InlineCallbackWithConfirmation.TryGetPending(parsed.Data.EntityId, out var pending);

                found.Should().BeTrue("the confirmation was registered when the button was built");
                pending.Should().BeSameAs(confirmation);
                pending!.YesCallback.Should().NotBeNull();
                pending.NoCallback.Should().NotBeNull();
            }
        }

        [Test]
        public void AnsweringNoForgetsThePendingConfirmation()
        {
            using (new BotDataScope(context, botInstance))
            {
                var confirmation = BuildConfirmation();

                var keyboard = MenuGenerator.InlineKeyboard(1, new List<IInlineContent> { confirmation });
                var callbackData = keyboard.InlineKeyboard.Single().Single().CallbackData;
                var id = InlineCallback<EntityTCommand<string>>.GetCommandByCallbackOrNull(callbackData!).Data.EntityId;

                InlineCallbackWithConfirmation.TryGetPending(id, out _).Should().BeTrue();

                InlineCallbackWithConfirmation.Complete(id);

                InlineCallbackWithConfirmation.TryGetPending(id, out var afterwards).Should().BeFalse();
                afterwards.Should().BeNull();
            }
        }

        [Test]
        public void TheNoButtonCarriesTheSameIdAsTheConfirmation()
        {
            using (new BotDataScope(context, botInstance))
            {
                var confirmation = BuildConfirmation();

                var keyboard = MenuGenerator.InlineKeyboard(1, new List<IInlineContent> { confirmation });
                var confirmationId = InlineCallback<EntityTCommand<string>>
                    .GetCommandByCallbackOrNull(keyboard.InlineKeyboard.Single().Single().CallbackData!)
                    .Data.EntityId;

                // This is the menu InlineConfirmation.ActionWithConfirmation builds after the
                // confirmation button is pressed.
                confirmation.YesCallback.ButtonName = confirmation.YesButton;
                var answerKeyboard = MenuGenerator.InlineKeyboard(
                    2,
                    new List<IInlineContent> { confirmation.YesCallback, confirmation.NoCallback });

                var answers = answerKeyboard.InlineKeyboard.SelectMany(row => row).ToList();
                answers.Should().HaveCount(2);

                var noData = InlineCallback<EntityTCommand<string>>.GetCommandByCallbackOrNull(answers[1].CallbackData!);

                noData.CommandType.Should().Be(PRTelegramBotCommand.CallbackWithConfirmationResultNo);
                noData.Data.EntityId.Should().Be(confirmationId, "otherwise pressing No cannot clear the right entry");
            }
        }
    }
}
