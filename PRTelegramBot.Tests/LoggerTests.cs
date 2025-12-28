using Microsoft.Extensions.Logging;
using Moq;
using PRTelegramBot.Builders;
using PRTelegramBot.Core;
using PRTelegramBot.Models.EventsArgs;

namespace PRTelegramBot.Tests
{
    public class LoggerTests
    {
        private Mock<ILogger<LoggerTests>> logMock;
        private Mock<ILoggerFactory> logFactoryMock;

        [SetUp]
        public void SetUpTest()
        {
            logMock = new Mock<ILogger<LoggerTests>>();
            logFactoryMock = new Mock<ILoggerFactory>();
            logFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(logMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            BotCollection.Instance.ClearBots();
        }

        [Test]
        public async Task DefaultErrorLogEventShouldInvoked()
        {
            var bot = new PRBotBuilder("5555:Token").Build();
            var update = TestDataFactory.CreateUpdateTypeShippingQuery();
            bool errorEventCalled = false;
            Exception capturedException = null;

            bot.Events.OnErrorLog += (args) =>
            {
                errorEventCalled = true;
                capturedException = args.Exception;
                return Task.CompletedTask;
            };

            Task ThrowingHandler(BotEventArgs e)
            {
                bot.GetLogger<LoggerTests>().LogError("Test");
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnShippingQueryHandle += ThrowingHandler;

            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, CancellationToken.None);

            Assert.IsTrue(errorEventCalled, "Error event was not triggered.");
            Assert.IsNotNull(capturedException, "Test.");

            bot.Events.UpdateEvents.OnShippingQueryHandle -= ThrowingHandler;
        }

        [Test]
        public async Task DefaultCommonLogEventShouldInvoked()
        {
            var bot = new PRBotBuilder("5555:Token").Build();
            var update = TestDataFactory.CreateUpdateTypeShippingQuery();
            bool errorEventCalled = false;
            string logMessage = string.Empty;

            bot.Events.OnCommonLog += (args) =>
            {
                errorEventCalled = true;
                logMessage = args.Message;
                return Task.CompletedTask;
            };

            Task ThrowingHandler(BotEventArgs e)
            {
                bot.GetLogger<LoggerTests>().LogInformation("Test");
                return Task.CompletedTask;
            }

            bot.Events.UpdateEvents.OnShippingQueryHandle += ThrowingHandler;

            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, CancellationToken.None);

            Assert.IsTrue(errorEventCalled, "Log event was not triggered.");
            Assert.IsNotNull(logMessage, "Test.");

            bot.Events.UpdateEvents.OnShippingQueryHandle -= ThrowingHandler;
        }

        [Test]
        public async Task LogDebug_Should_Invoke_LogEvent()
        {
            var bot = new PRBotBuilder("5555:Token")
                .SetBotId(99)
                .SetLoggerFactory(logFactoryMock.Object)
                .Build();

            var update = TestDataFactory.CreateUpdateTypeShippingQuery();

            bot.Events.UpdateEvents.OnShippingQueryHandle += e =>
            {
                bot.GetLogger<LoggerTests>().LogDebug("DebugTest");
                return Task.CompletedTask;
            };


            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, CancellationToken.None);


            logMock.VerifyLog(LogLevel.Debug, "DebugTest");
        }


        [Test]
        public async Task LogInformation_Should_Invoke_LogEvent()
        {
            var bot = new PRBotBuilder("5555:Token")
                .SetBotId(99)
                .SetLoggerFactory(logFactoryMock.Object)
                .Build();
            var update = TestDataFactory.CreateUpdateTypeShippingQuery();

            bot.Events.UpdateEvents.OnShippingQueryHandle += e =>
            {
                bot.GetLogger<LoggerTests>().LogInformation("InfoTest");
                return Task.CompletedTask;
            };


            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, CancellationToken.None);


            logMock.VerifyLog(LogLevel.Information, "InfoTest");
        }


        [Test]
        public async Task LogWarning_Should_Invoke_LogEvent()
        {
            var bot = new PRBotBuilder("5555:Token")
                .SetBotId(99)
                .SetLoggerFactory(logFactoryMock.Object)
                .Build();

            var update = TestDataFactory.CreateUpdateTypeShippingQuery();

            bot.Events.UpdateEvents.OnShippingQueryHandle += e =>
            {
                bot.GetLogger<LoggerTests>().LogWarning("WarnTest");
                return Task.CompletedTask;
            };


            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, CancellationToken.None);


            logMock.VerifyLog(LogLevel.Warning, "WarnTest");
        }


        [Test]
        public async Task LogError_Should_Invoke_LogEvent()
        {
            var bot = new PRBotBuilder("5555:Token")
                .SetBotId(99)
                .SetLoggerFactory(logFactoryMock.Object)
                .Build();
            var update = TestDataFactory.CreateUpdateTypeShippingQuery();


            bot.Events.UpdateEvents.OnShippingQueryHandle += e =>
            {
                bot.GetLogger<LoggerTests>().LogError("ErrorTest");
                return Task.CompletedTask;
            };


            await bot.Handler.HandleUpdateAsync(bot.BotClient, update, CancellationToken.None);


            logMock.VerifyLog(LogLevel.Error, "ErrorTest");
        }
    }

    public static class LoggerMockExtensions
    {
        public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel logLevel, string? expectedMessage = null, Times? times = null)
        {
            loggerMock.Verify(x =>
            x.Log(
            logLevel,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((state, _) => expectedMessage == null || state.ToString() == expectedMessage),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times ?? Times.Once());
        }
    }
}

