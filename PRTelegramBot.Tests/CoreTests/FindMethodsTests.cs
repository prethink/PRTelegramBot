using PRTelegramBot.Attributes;
using PRTelegramBot.Core;
using PRTelegramBot.Utils;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class FindMethodsTests
    {
        [TearDown]
        public void Cleanup()
        {
            BotCollection.Instance.ClearBots();
        }

        public const string KEY_DYNAMIC_REPLY_ONE      = "";
        public const string KEY_DYNAMIC_REPLY_TWO      = "";
        public const string KEY_DYNAMIC_REPLY_THREE    = "dynamic three";
        public const string KEY_DYNAMIC_REPLY_FOUR     = "dynamic four";
        public const string KEY_DYNAMIC_REPLY_FIVE     = "dynamic five";

        public enum TestTHeader
        {
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
        }

        [Test]
        [TestCase(0, 3)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        public void FindReplyMethods(int botId, int exceptedMethodsCount)
        {
            MethodInfo[] replyMethods = ReflectionUtils.FindStaticMessageMenuHandlers(botId);
            Assert.AreEqual(exceptedMethodsCount, replyMethods.Length);
        }

        [Test]
        [TestCase(0, 3)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        public void FindReplyDictionaryMethods(int botId, int exceptedMethodsCount)
        {
            var botBuilder = new PRBotBuilder("55555:Token")
                            .AddReplyDynamicCommand(nameof(KEY_DYNAMIC_REPLY_ONE), "dynamic one")
                            .AddReplyDynamicCommand(nameof(KEY_DYNAMIC_REPLY_TWO), "dynamic two")
                            .AddReplyDynamicCommand(KEY_DYNAMIC_REPLY_THREE, "dynamic three example")
                            .AddReplyDynamicCommand(KEY_DYNAMIC_REPLY_ONE, "dynamic foure example");

            var bot = botBuilder.Build();
            var botWithIdOne = botBuilder.SetBotId(BotCollection.GetNextId()).Build();
            var botWithIdTwo = botBuilder.SetBotId(BotCollection.GetNextId()).Build();

            MethodInfo[] replyMethods = ReflectionUtils.FindStaticMessageMenuDictionaryHandlers(botId);
            Assert.AreEqual(exceptedMethodsCount, replyMethods.Length);
        }

        [Test]
        [TestCase(0, 3)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        public void FindInlineMethods(int botId, int exceptedMethodsCount)
        {
            MethodInfo[] inlineMethods = ReflectionUtils.FindStaticInlineMenuHandlers(botId);
            Assert.AreEqual(exceptedMethodsCount, inlineMethods.Length);
        }

        [Test]
        [TestCase(0, 3)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        public void FindSlashMethods(int botId, int exceptedMethodsCount)
        {
            MethodInfo[] slashCommandMethods = ReflectionUtils.FindStaticSlashCommandHandlers(botId);
            Assert.AreEqual(exceptedMethodsCount, slashCommandMethods.Length);
        }

        #region Common methods

        [SlashHandler(nameof(TestCommonMethod), "TestTwoArgs")]
        [ReplyMenuHandler(nameof(TestCommonMethod), "TestTwoArgs")]
        [InlineCallbackHandler<TestTHeader>(TestTHeader.One, TestTHeader.Two)]
        [ReplyMenuDynamicHandler(nameof(KEY_DYNAMIC_REPLY_FOUR))]
        public static async Task TestCommonMethod(ITelegramBotClient botClient, Update update) { }

        [SlashHandler(2, nameof(TestCommonMethodTwo))]
        [ReplyMenuHandler(2, nameof(TestCommonMethodTwo))]
        [InlineCallbackHandler<TestTHeader>(2, TestTHeader.Three)]
        [ReplyMenuDynamicHandler(2, nameof(KEY_DYNAMIC_REPLY_FOUR))]
        public static async Task TestCommonMethodTwo(ITelegramBotClient botClient, Update update) { }

        [SlashHandler(-1, nameof(TestCommonMethodForAllBot))]
        [ReplyMenuHandler(-1, nameof(TestCommonMethodForAllBot))]
        [InlineCallbackHandler<TestTHeader>(-1, TestTHeader.Eight)]
        [ReplyMenuDynamicHandler(-1, nameof(KEY_DYNAMIC_REPLY_FIVE))]
        public static async Task TestCommonMethodForAllBot(ITelegramBotClient botClient, Update update) { }


        #endregion

        #region Reply methods

        [ReplyMenuHandler(nameof(TestReplyOne))]
        public static async Task TestReplyOne(ITelegramBotClient botClient, Update update) { }

        [ReplyMenuHandler(1, nameof(TestReplyWithCustomId))]
        public static async Task TestReplyWithCustomId(ITelegramBotClient botClient, Update update) { }

        [ReplyMenuHandler(2, nameof(TestReplyWithCustomIdTwo))]
        public static async Task TestReplyWithCustomIdTwo(ITelegramBotClient botClient, Update update) { }

        #endregion

        #region Reply dynamic methods

        [ReplyMenuDynamicHandler(nameof(KEY_DYNAMIC_REPLY_ONE))]
        public static async Task TestDynamicReplyOne(ITelegramBotClient botClient, Update update) { }

        [ReplyMenuDynamicHandler(1, nameof(KEY_DYNAMIC_REPLY_TWO))]
        public static async Task TestDynamicReplyWithCustomId(ITelegramBotClient botClient, Update update) { }

        [ReplyMenuDynamicHandler(2, KEY_DYNAMIC_REPLY_THREE)]
        public static async Task TestDynamicReplyWithCustomIdTwo(ITelegramBotClient botClient, Update update) { }

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
