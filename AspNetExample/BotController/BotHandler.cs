using PRTelegramBot.Attributes;
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

    [TelegramBotHandler]
    public class BotHandler
    {
        private readonly ILogger<BotHandler> _logger;

        public BotHandler(ILogger<BotHandler> logger)
        {
            _logger = logger;
        }

        [ReplyMenuHandler("Test")]
        public async Task TestMethod(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient,update, $"{nameof(TestMethod)} {_logger != null}");
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

        [ReplyMenuHandler("Test1")]
        public async static Task StaticTestMethod(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(StaticTestMethod));
        }
    }
}
