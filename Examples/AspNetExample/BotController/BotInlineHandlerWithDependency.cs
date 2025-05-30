﻿using AspNetExample.Services;
using PRTelegramBot.Attributes;
using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using TestDI.Models;

namespace AspNetExample.BotController
{
    [BotHandler]
    public class BotInlineHandlerWithDependency : ICallbackQueryCommandHandler
    {
        private readonly ILogger<BotHandlerWithDependency> _logger;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ServiceTransient serviceTransient;

        public BotInlineHandlerWithDependency(ServiceScoped serviceScoped, ServiceTransient serviceTransient, ServiceSingleton serviceSingleton, ILogger<BotHandlerWithDependency> logger)
        {

            this.serviceScoped = serviceScoped;
            this.serviceTransient = serviceTransient;
            this.serviceSingleton = serviceSingleton;
            _logger = logger;
        }

        public async Task<UpdateResult> Handle(PRBotBase bot, Update update, CallbackQuery updateType)
        {
            await PRTelegramBot.Helpers.Message.Send(bot.botClient, update, $"{nameof(Handle)} {_logger != null}");
            return UpdateResult.Handled;
        }

        [ReplyMenuHandler("InlineClass")]
        public async Task InlineTest(ITelegramBotClient botClient, Update update)
        {
            var options = new OptionMessage();
            var menuItemns = MenuGenerator.InlineButtons(1, new List<IInlineContent> {
                new InlineCallback("Test", ClassTHeader.DefaultTestClass)
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItemns);
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(InlineTest), options);
        }
    }
}
