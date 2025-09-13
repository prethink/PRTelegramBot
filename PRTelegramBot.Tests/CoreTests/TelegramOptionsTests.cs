using FluentAssertions;
using PRTelegramBot.Core;
using PRTelegramBot.Core.Factories;
using PRTelegramBot.Core.Factory;
using PRTelegramBot.Tests.TestModels;
using PRTelegramBot.Tests.TestModels.TestHandlers;
using PRTelegramBot.Tests.UtilsTests;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class TelegramOptionsTests
    {
        [TearDown]
        public void Cleanup()
        {
            BotCollection.Instance.ClearBots();
        }

        [Test]
        public void PRBotDefaultMessageHandlersShouldBeThree()
        {
            var bot = new PRBotBuilder(CommonUtils.TEST_TOKEN).Build();
            bot.Options.MessageHandlers.Count.Should().Be(3);
        }

        [Test]
        public void PRBotWebHookDefaultMessageHandlersShouldBeThree()
        {
            var bot = new PRBotBuilder(CommonUtils.TEST_TOKEN).UseFactory(new PRBotWebHookFactory()).Build();
            bot.Options.MessageHandlers.Count.Should().Be(3);
        }

        [Test]
        public void PRBotPollingHookDefaultMessageHandlersShouldBeThree()
        {
            var bot = new PRBotBuilder(CommonUtils.TEST_TOKEN).UseFactory(new PRBotPollingFactory()).Build();
            bot.Options.MessageHandlers.Count.Should().Be(3);
        }

        [Test]
        public void PRBotDefaultCallbackQueryHandlersShouldBeOne()
        {
            var bot = new PRBotBuilder(CommonUtils.TEST_TOKEN).Build();
            bot.Options.CallbackQueryHandlers.Count.Should().Be(2);
        }

        [Test]
        public void PRBotWebHookDefaultCallbackQueryHandlersShouldBeOne()
        {
            var bot = new PRBotBuilder(CommonUtils.TEST_TOKEN).UseFactory(new PRBotWebHookFactory()).Build();
            bot.Options.CallbackQueryHandlers.Count.Should().Be(2);
        }

        [Test]
        public void PRBotPollingDefaultCallbackQueryHandlersShouldBeOne()
        {
            var bot = new PRBotBuilder(CommonUtils.TEST_TOKEN).UseFactory(new PRBotPollingFactory()).Build();
            bot.Options.CallbackQueryHandlers.Count.Should().Be(2);
        }

        [Test]
        public void WhenAddNewHandlerMessageHandlersShouldBeFour()
        {
            var bot = new PRBotBuilder(CommonUtils.TEST_TOKEN)
                .AddMessageCommandHandlers(new MessageTestHandler())
                .Build();
            bot.Options.MessageHandlers.Count.Should().Be(4);
        }

        [Test]
        public void WhenAddNewHandlerCallbackQueryHandlersShouldBeTwo()
        {
            var bot = new PRBotBuilder(CommonUtils.TEST_TOKEN)
                .AddCallbackQueryCommandHandlers(new CallbackQueryTestHandler())
                .Build();
            bot.Options.CallbackQueryHandlers.Count.Should().Be(3);
        }

        [Test]
        public void AddInlineClassHandlerWhenTypeImplementedInterfaceNotShouldBeException()
        {
            var bot = new PRBotBuilder(CommonUtils.TEST_TOKEN)
                .AddInlineClassHandler(TestTHeader.Class, typeof(TestInlineClassHandler))
                .Build();

            bot.Options.CommandOptions.InlineClassHandlers.Count.Should().Be(1);
            bot.InlineClassHandlerInstances.Count.Should().Be(1);
        }

        [Test]
        public void AddInlineClassHandlerWhenTypeNotImplementedInterfaceShouldBeException()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                var bot = new PRBotBuilder(CommonUtils.TEST_TOKEN)
                    .AddInlineClassHandler(TestTHeader.Class, typeof(TestClass))
                    .Build();
            });
        }
    }
}
