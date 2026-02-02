using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Attributes;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Services.Messages;

namespace ConsoleExample.Examples.Builders
{
    public class InlineKeyboard
    {

        [ReplyMenuHandler("GInlineK")]
        public static async Task GenerateKeyboard(IBotContext context)
        {
            var keyboard = new InlineKeyboardBuilder();
            var cars = 13;
            for (int i = 0; i < cars; i++)
            {
                keyboard.AddButton(new InlineCallback<EntityTCommand<long>>($"Car {i}", SelectHeader.Car, new EntityTCommand<long> { EntityId = i }), newRow: true);
            }

            var resultKeyboard = keyboard.Build();
            var option = new OptionMessage { MenuInlineKeyboardMarkup = resultKeyboard };
            await MessageSender.Send(context, "Select a car:", option);
        }

        [InlineCallbackHandler<SelectHeader>(SelectHeader.Car)]
        public static async Task ExecuteAsync(IBotContext context)
        {
            var command = context.GetCommandByCallbackOrNull<EntityTCommand<long>>();
            await MessageSender.Send(context, $"Selected {command.Data.EntityId}");
        }
    }
}
