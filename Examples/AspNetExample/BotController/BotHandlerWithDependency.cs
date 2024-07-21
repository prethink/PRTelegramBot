using AspNetExample.Services;
using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AspNetExample.BotController
{
    [BotHandler]
    public class BotHandlerWithDependency
    {
        private readonly ILogger<BotHandlerWithDependency> _logger;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ServiceTransient serviceTransient;

        public BotHandlerWithDependency(ServiceScoped serviceScoped, ServiceTransient serviceTransient, ServiceSingleton serviceSingleton, ILogger<BotHandlerWithDependency> logger)
        {

            this.serviceScoped = serviceScoped;
            this.serviceTransient = serviceTransient;
            this.serviceSingleton = serviceSingleton;
            _logger = logger;
        }

        [ReplyMenuHandler("Test")]
        public async Task TestMethodWithDependency(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, $"{nameof(TestMethodWithDependency)} {_logger != null}");
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
                new InlineCallback("Test", PRTelegramBotCommand.CurrentPage),
                new InlineCallback("TestStatic", PRTelegramBotCommand.NextPage)
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItemns);
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(InlineTest), options);
        }

        [ReplyMenuHandler("inlinestatic")]
        public async Task StaticInlineTest(ITelegramBotClient botClient, Update update)
        {
            var options = new OptionMessage();
            var menuItemns = MenuGenerator.InlineButtons(1, new List<IInlineContent> {
                new InlineCallback("Test", PRTelegramBotCommand.CurrentPage),
                new InlineCallback("TestStatic", PRTelegramBotCommand.NextPage)
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItemns);
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(StaticInlineTest), options);
        }

        [InlineCallbackHandler<PRTelegramBotCommand>(PRTelegramBotCommand.CurrentPage)]
        public async Task InlineHandler(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(InlineHandler));
        }

        [InlineCallbackHandler<PRTelegramBotCommand>(PRTelegramBotCommand.NextPage)]
        public async static Task InlineHandlerStatic(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(InlineHandlerStatic));
        }


    }
}
