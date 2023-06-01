using PRTelegramBot.Attributes;
using PRTelegramBot.Core;
using PRTelegramBot.Models.CallbackCommands;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Models.InlineButtons;
using Helpers = PRTelegramBot.Helpers;
using Header = PRTelegramBot.Models.Enums.Header;

namespace ConsoleExample.Examples
{
    public class ExampleHandlers
    {
        /// <summary>
        /// callback обработка
        /// Обрабатывает одну точку входа
        /// </summary>
        [InlineCallbackHandler(Header.ExampleOne)]
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
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        /// <summary>
        /// callback обработка
        /// Данный метод может обработать несколько точек входа
        /// </summary>
        [InlineCallbackHandler(Header.ExampleTwo, Header.ExampleThree)]
        public static async Task InlineTwo(ITelegramBotClient botClient, Update update)
        {
            try
            {
                //Попытка преобразовать callback данные к требуемому типу
                var command = InlineCallback<EntityTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    string msg = $"Идентификатор который вы передали {command.Data.EntityId}";
                    await Helpers.Message.Send(botClient, update, msg);
                }
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }
    }
}
