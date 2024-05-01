using PRTelegramBot.Attributes;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class FindMethodsTests
    {
        public enum TestTHeader
        {
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
        }

        [Test]
        [TestCase(0, 2)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void FindReplyMethods(int botId, int exceptedMethodsCount)
        {
            MethodInfo[] replyMethods = ReflectionUtils.FindStaticMessageMenuHandlers(botId);
            Assert.AreEqual(exceptedMethodsCount, replyMethods.Length);
        }

        [Test]
        [TestCase(0, 0)]
        public void FindReplyDictionaryMethods(int botId, int exceptedMethodsCount)
        {
            MethodInfo[] replyMethods = ReflectionUtils.FindStaticMessageMenuDictionaryHandlers(botId);
            Assert.AreEqual(exceptedMethodsCount, replyMethods.Length);
        }

        [Test]
        [TestCase(0, 2)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void FindInlineMethods(int botId, int exceptedMethodsCount)
        {
            MethodInfo[] inlineMethods = ReflectionUtils.FindStaticInlineMenuHandlers(botId);
            Assert.AreEqual(exceptedMethodsCount, inlineMethods.Length);
        }

        [Test]
        [TestCase(0, 2)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void FindSlashMethods(int botId, int exceptedMethodsCount)
        {
            MethodInfo[] slashCommandMethods = ReflectionUtils.FindStaticSlashCommandHandlers(botId);
            Assert.AreEqual(exceptedMethodsCount, slashCommandMethods.Length);
        }

        #region Common methods

        [SlashHandler(nameof(TestCommonMethod), "TestTwoArgs")]
        [ReplyMenuHandler(nameof(TestCommonMethod), "TestTwoArgs")]
        [InlineCallbackHandler<TestTHeader>(TestTHeader.One, TestTHeader.Two)]
        public static async Task TestCommonMethod(ITelegramBotClient botClient, Update update) { }

        [SlashHandler(2, nameof(TestCommonMethodTwo))]
        [ReplyMenuHandler(2, nameof(TestCommonMethodTwo))]
        [InlineCallbackHandler<TestTHeader>(2, TestTHeader.Three)]
        public static async Task TestCommonMethodTwo(ITelegramBotClient botClient, Update update) { }

        #endregion

        #region Reply methods

        [ReplyMenuHandler(nameof(TestReplyOne))]
        public static async Task TestReplyOne(ITelegramBotClient botClient, Update update) { }

        [ReplyMenuHandler(1, nameof(TestReplyWithCustomId))]
        public static async Task TestReplyWithCustomId(ITelegramBotClient botClient, Update update) { }

        [ReplyMenuHandler(2, nameof(TestReplyWithCustomIdTwo))]
        public static async Task TestReplyWithCustomIdTwo(ITelegramBotClient botClient, Update update) { }

        #endregion

        #region Inline methods

        [InlineCallbackHandler<TestTHeader>(TestTHeader.Four, TestTHeader.Five)]
        public static async Task InlineFive(ITelegramBotClient botClient, Update update) { }

        [InlineCallbackHandler<TestTHeader>(1, TestTHeader.Six)]
        public static async Task InlineSix(ITelegramBotClient botClient, Update update) { }

        [InlineCallbackHandler<TestTHeader>(2, TestTHeader.Seven)]
        public static async Task InlineSeven(ITelegramBotClient botClient, Update update) { }

        #endregion 

        #region Slash methods

        [SlashHandler(nameof(TestSlashMethod))]
        public static async Task TestSlashMethod(ITelegramBotClient botClient, Update update) { }

        [SlashHandler(1, nameof(TestSlashMethodTwo))]
        public static async Task TestSlashMethodTwo(ITelegramBotClient botClient, Update update) { }

        [SlashHandler(2, nameof(TestSlashMethodThree))]
        public static async Task TestSlashMethodThree(ITelegramBotClient botClient, Update update) { }

        #endregion

    }
}
