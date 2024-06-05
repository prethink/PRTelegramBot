using PRTelegramBot.Attributes;
using PRTelegramBot.Models.Enums;
using static PRTelegramBot.Tests.CoreTests.FindMethodsTests;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace PRTelegramBot.Tests.Common
{
    public class Commands
    {
        #region Common methods

        [SlashHandler(CommandComparison.Equals, StringComparison.Ordinal, nameof(TestCommonMethod), "TestTwoArgs")]
        [ReplyMenuHandler(CommandComparison.Contains, StringComparison.Ordinal, nameof(TestCommonMethod), "TestTwoArgs")]
        [InlineCallbackHandler<TestTHeader>(TestTHeader.One, TestTHeader.Two)]
        [ReplyMenuDynamicHandler(CommandComparison.Contains, StringComparison.Ordinal, nameof(KEY_DYNAMIC_REPLY_FOUR))]
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
