﻿using PRTelegramBot.Core;

namespace PRTelegramBot.Tests.CoreTests
{
    internal class CommandsTests
    {
        [TearDown]
        public void Cleanup()
        {
            BotCollection.Instance.ClearBots();
        }

        [Test]
        public void ReplyCommand()
        {
            var bot = new PRBotBuilder("55555:Token").SetBotId(0).Build();
            var replyCommandCount = bot.Handler.MessageFacade.ReplyHandler.CommandCount;
            Assert.AreEqual(4, replyCommandCount);
        }

        [Test]
        public void ReplyDynamicCommand()
        {
            var bot = new PRBotBuilder("55555:Token")
                .SetBotId(0)
                .AddReplyDynamicCommand(nameof(FindMethodsTests.KEY_DYNAMIC_REPLY_ONE), "TestDynamicOne")
                .AddReplyDynamicCommand(nameof(FindMethodsTests.KEY_DYNAMIC_REPLY_FOUR), "TestDynamicTwo")
                .AddReplyDynamicCommand(nameof(FindMethodsTests.KEY_DYNAMIC_REPLY_FIVE), "TestDynamicThree")
                .Build();
            var replyDymanicCommandCount = bot.Handler.MessageFacade.ReplyDynamicHandler.CommandCount;
            Assert.AreEqual(3, replyDymanicCommandCount);
        }

        [Test]
        public void SlashCommand()
        {
            var bot = new PRBotBuilder("55555:Token").SetBotId(0).Build();
            var slashCommandCounts = bot.Handler.MessageFacade.SlashHandler.CommandCount;
            Assert.AreEqual(4, slashCommandCounts);
        }

        [Test]
        public void InlineCommand()
        {
            var bot = new PRBotBuilder("55555:Token").SetBotId(0).Build();
            var inlineCommandsCount = bot.Handler.InlineUpdateHandler.CommandCount;
            Assert.AreEqual(5, inlineCommandsCount);
        }
    }
}
