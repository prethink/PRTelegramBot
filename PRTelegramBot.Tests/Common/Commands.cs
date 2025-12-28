using PRTelegramBot.Attributes;
using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.Enums;
using static PRTelegramBot.Tests.CoreTests.FindMethodsTests;

namespace PRTelegramBot.Tests.Common
{
    public class Commands
    {
        #region Common methods

        [SlashHandler(CommandComparison.Equals, StringComparison.Ordinal, nameof(TestCommonMethod), "TestTwoArgs")]
        [ReplyMenuHandler(CommandComparison.Contains, StringComparison.Ordinal, nameof(TestCommonMethod), "TestTwoArgs")]
        [InlineCallbackHandler<TestTHeader>(TestTHeader.One, TestTHeader.Two)]
        [ReplyMenuDynamicHandler(CommandComparison.Contains, StringComparison.Ordinal, nameof(KEY_DYNAMIC_REPLY_FOUR))]
        public static async Task TestCommonMethod(IBotContext context) { }

        [SlashHandler(2, nameof(TestCommonMethodTwo))]
        [ReplyMenuHandler(2, nameof(TestCommonMethodTwo))]
        [InlineCallbackHandler<TestTHeader>(2, TestTHeader.Three)]
        [ReplyMenuDynamicHandler(2, nameof(KEY_DYNAMIC_REPLY_FOUR))]
        public static async Task TestCommonMethodTwo(IBotContext context) { }

        [SlashHandler(PRConstants.ALL_BOTS_ID, nameof(TestCommonMethodForAllBot))]
        [ReplyMenuHandler(PRConstants.ALL_BOTS_ID, nameof(TestCommonMethodForAllBot))]
        [InlineCallbackHandler<TestTHeader>(PRConstants.ALL_BOTS_ID, TestTHeader.Eight)]
        [ReplyMenuDynamicHandler(nameof(KEY_DYNAMIC_REPLY_FIVE))]
        public static async Task TestCommonMethodForAllBot(IBotContext context) { }


        #endregion

        #region Reply methods

        [Access(1)]
        [ReplyMenuHandler(nameof(TestAccessMethod))]
        public static async Task TestAccessMethod(IBotContext context) { }

        [ReplyMenuHandler(1, nameof(TestReplyWithCustomId))]
        public static async Task TestReplyWithCustomId(IBotContext context) { }

        [ReplyMenuHandler([2, 1], nameof(TestReplyWithCustomIdTwo))]
        public static async Task TestReplyWithCustomIdTwo(IBotContext context) { }

        [RequireTypeMessage(Telegram.Bot.Types.Enums.MessageType.Photo)]
        [ReplyMenuHandler(nameof(TestTypeMessage))]
        public static async Task TestTypeMessage(IBotContext context) { }

        #endregion

        #region Reply dynamic methods

        [ReplyMenuDynamicHandler(nameof(KEY_DYNAMIC_REPLY_ONE))]
        public static async Task TestDynamicReplyOne(IBotContext context) { }

        [ReplyMenuDynamicHandler(1, nameof(KEY_DYNAMIC_REPLY_TWO))]
        public static async Task TestDynamicReplyWithCustomId(IBotContext context) { }

        [ReplyMenuDynamicHandler([2,1], KEY_DYNAMIC_REPLY_THREE)]
        public static async Task TestDynamicReplyWithCustomIdTwo(IBotContext context) { }

        #endregion

        #region Inline methods

        [InlineCallbackHandler<TestTHeader>(TestTHeader.Four, TestTHeader.Five)]
        public static async Task InlineFive(IBotContext context) { }

        [InlineCallbackHandler<TestTHeader>(1, TestTHeader.Six)]
        public static async Task InlineSix(IBotContext context) { }

        [InlineCallbackHandler<TestTHeader>([2, 1], TestTHeader.Seven)]
        public static async Task InlineSeven(IBotContext context) { }

        #endregion 

        #region Slash methods

        [SlashHandler(nameof(TestSlashMethod))]
        public static async Task TestSlashMethod(IBotContext context) { }

        [SlashHandler(1, nameof(TestSlashMethodTwo))]
        public static async Task TestSlashMethodTwo(IBotContext context) { }

        [SlashHandler([2, 1], nameof(TestSlashMethodThree))]
        public static async Task TestSlashMethodThree(IBotContext context) { }

        #endregion
    }
}
