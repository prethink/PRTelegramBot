using PRTelegramBot.Attributes;
using PRTelegramBot.Builders;
using PRTelegramBot.Core;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Tests.Common;
using PRTelegramBot.Utils;
using System.Reflection;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class FindMethodsTests
    {
        [TearDown]
        public void Cleanup()
        {
            BotCollection.Instance.ClearBots();
        }

        public const string KEY_DYNAMIC_REPLY_ONE      =  "";
        public const string KEY_DYNAMIC_REPLY_TWO      =  "";
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
        [TestCase(0, 4)]
        [TestCase(1, 3)]
        [TestCase(2, 3)]
        public void FindReplyMethods(int botId, int exceptedMethodsCount)
        {
            MethodInfo[] replyMethods = ReflectionUtils.FindStaticReplyCommandHandlers(botId);
            Assert.AreEqual(exceptedMethodsCount, replyMethods.Length);
        }

        [Test]
        [TestCase(0, 3)]
        [TestCase(1, 2)]
        [TestCase(2, 2)]
        public void FindReplyDictionaryMethods(int botId, int exceptedMethodsCount)
        {
            var botBuilder = new PRBotBuilder("55555:Token")
                            .AddReplyDynamicCommand(nameof(KEY_DYNAMIC_REPLY_ONE), "dynamic one")
                            .AddReplyDynamicCommand(nameof(KEY_DYNAMIC_REPLY_TWO), "dynamic two")
                            .AddReplyDynamicCommand(KEY_DYNAMIC_REPLY_THREE, "dynamic three example")
                            .AddReplyDynamicCommand(KEY_DYNAMIC_REPLY_ONE, "dynamic foure example");

            var bot = botBuilder.Build();
            var botWithIdOne = botBuilder.SetBotId(BotCollection.Instance.GetNextId()).Build();
            var botWithIdTwo = botBuilder.SetBotId(BotCollection.Instance.GetNextId()).Build();

            MethodInfo[] replyMethods = ReflectionUtils.FindStaticDynamicReplyCommandHandlers(botId);
            Assert.AreEqual(exceptedMethodsCount, replyMethods.Length);
        }

        [Test]
        [TestCase(0, 10)]
        [TestCase(1, 10)]
        [TestCase(2, 10)]
        public void FindInlineMethods(int botId, int exceptedMethodsCount)
        {
            MethodInfo[] inlineMethods = ReflectionUtils.FindStaticInlineCommandHandlers(botId);
            Assert.AreEqual(exceptedMethodsCount, inlineMethods.Length);
        }

        [Test]
        [TestCase(0, 3)]
        [TestCase(1, 3)]
        [TestCase(2, 3)]
        public void FindSlashMethods(int botId, int exceptedMethodsCount)
        {
            MethodInfo[] slashCommandMethods = ReflectionUtils.FindStaticSlashCommandHandlers(botId);
            Assert.AreEqual(exceptedMethodsCount, slashCommandMethods.Length);
        }

        [Test]
        [TestCase(0, nameof(Commands.TestCommonMethod), CommandComparison.Contains, StringComparison.Ordinal)]
        [TestCase(2, nameof(Commands.TestCommonMethodTwo), CommandComparison.Equals, StringComparison.OrdinalIgnoreCase)]
        public void ReplyComparisonCommands(long botId, string methodName, CommandComparison exceptedCommandComparison, StringComparison exceptedStringComparison)
        {
            MethodInfo[] replyMethods = ReflectionUtils.FindStaticReplyCommandHandlers(botId);
            var method = replyMethods.FirstOrDefault(x => x.Name == methodName);
            var attribute = method.GetCustomAttribute<ReplyMenuHandlerAttribute>();

            Assert.AreEqual(exceptedCommandComparison, attribute.CommandComparison);
            Assert.AreEqual(exceptedStringComparison, attribute.StringComparison);
        }

        [Test]
        [TestCase(0, nameof(Commands.TestCommonMethod), CommandComparison.Contains, StringComparison.Ordinal)]
        [TestCase(2, nameof(Commands.TestCommonMethodTwo), CommandComparison.Equals, StringComparison.OrdinalIgnoreCase)]
        public void ReplyDynamicComparisonCommands(long botId, string methodName, CommandComparison exceptedCommandComparison, StringComparison exceptedStringComparison)
        {
            MethodInfo[] replyMethods = ReflectionUtils.FindStaticReplyCommandHandlers(botId);
            var method = replyMethods.FirstOrDefault(x => x.Name == methodName);
            var attribute = method.GetCustomAttribute<ReplyMenuDynamicHandlerAttribute>();

            Assert.AreEqual(exceptedCommandComparison, attribute.CommandComparison);
            Assert.AreEqual(exceptedStringComparison, attribute.StringComparison);
        }

        [Test]
        [TestCase(0, nameof(Commands.TestCommonMethod), CommandComparison.Equals, StringComparison.Ordinal)]
        [TestCase(2, nameof(Commands.TestCommonMethodTwo), CommandComparison.Contains, StringComparison.OrdinalIgnoreCase)]
        public void SlashComparisonCommands(long botId, string methodName, CommandComparison exceptedCommandComparison, StringComparison exceptedStringComparison)
        {
            MethodInfo[] replyMethods = ReflectionUtils.FindStaticReplyCommandHandlers(botId);
            var method = replyMethods.FirstOrDefault(x => x.Name == methodName);
            var attribute = method.GetCustomAttribute<SlashHandlerAttribute>();

            Assert.AreEqual(exceptedCommandComparison, attribute.CommandComparison);
            Assert.AreEqual(exceptedStringComparison, attribute.StringComparison);
        }
    }
}
