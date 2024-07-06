using ConsoleExample.Models;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot;
using Telegram.Bot.Types;
using Helpers = PRTelegramBot.Helpers;

namespace ConsoleExample.Examples
{
    public class ExampleHandlers
    {
        /// <summary>
        /// callback обработка
        /// Обрабатывает одну точку входа
        /// </summary>
        [InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleOne)]
        public static async Task Inline(ITelegramBotClient botClient, Update update)
        {
            try
            {
                //Попытка преобразовать callback данные к требуемому типу
                var command = InlineCallback.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    string msg = "Выполнена команда callback";
                    await Helpers.Message.Send(botClient, update, msg);
                }
            }
            catch (Exception ex)
            {
                //Обработка исключения
            }
        }

        /// <summary>
        /// callback обработка
        /// Данный метод может обработать несколько точек входа
        /// </summary>
        [InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleTwo, CustomTHeaderTwo.ExampleThree)]
        public static async Task InlineTwo(ITelegramBotClient botClient, Update update)
        {
            try
            {
                //Попытка преобразовать callback данные к требуемому типу
                var command = InlineCallback<EntityTCommand<long>>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    string msg = $"Идентификатор который вы передали {command.Data.EntityId}";
                    if(command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Edit)
                    {
                        await Helpers.Message.Edit(botClient, update, msg);
                    }
                    else
                    {
                        if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Delete)
                        {
                            await botClient.DeleteMessageAsync(update.GetChatIdClass(), update.CallbackQuery.Message.MessageId);
                        }
                        await Helpers.Message.Send(botClient, update, msg);
                    }
                }
            }
            catch (Exception ex)
            {
                //Обработка исключения
            }
        }
    }
}
