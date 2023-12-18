using PRTelegramBot.Attributes;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Models.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using TestDI.Controllers;

namespace TestDI.BotController
{

    public class BotHandlerWithDependency
    {
        private readonly ILogger<BotHandlerWithDependency> _logger;

        public BotHandlerWithDependency(ILogger<BotHandlerWithDependency> logger)
        {
            _logger = logger;
        }

        [ReplyMenuHandler("Test")]
        public async Task TestMethodWithDependency(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient,update, $"{nameof(TestMethodWithDependency)} {_logger != null}");
        }

        [SlashHandler("/test")]
        public async Task Slash(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(Slash));
        }

        [ReplyMenuHandler("inline")]
        public async Task InlineTest(ITelegramBotClient botClient, Update update)
        {
            var options = new OptionMessage();
            var menuItemns = MenuGenerator.InlineButtons(1, new List<IInlineContent> { 
                new InlineCallback("Test", THeader.CurrentPage), 
                new InlineCallback("TestStatic", THeader.NextPage) 
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItemns );
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(InlineTest), options);
        }

        [ReplyMenuHandler("inlinestatic")]
        public async Task StaticInlineTest(ITelegramBotClient botClient, Update update)
        {
            var options = new OptionMessage();
            var menuItemns = MenuGenerator.InlineButtons(1, new List<IInlineContent> {
                new InlineCallback("Test", THeader.CurrentPage),
                new InlineCallback("TestStatic", THeader.NextPage)
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItemns);
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(StaticInlineTest), options);
        }

        [InlineCallbackHandler<THeader>(THeader.CurrentPage)]
        public async Task InlineHandler(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(InlineHandler));
        }

        [InlineCallbackHandler<THeader>(THeader.NextPage)]
        public async static Task InlineHandlerStatic(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(InlineHandlerStatic));
        }


    }
}
