using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Attributes;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Models.TCommands;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ConsoleExample.Examples.Commands
{
    internal class ExampleInlineConfirmation
    {
        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает если пользователь напишет InlineConfirm.
        /// Так же при проверки будет проигнорирован регистр команды.
        /// </summary>
        [ReplyMenuHandler("InlineConfirm")]
        public static async Task InlineConfirm(ITelegramBotClient botClient, Update update)
        {
            //Кнопка для которой нужно создать подтверждение.
            var exampleInlineCallback = new InlineCallback<EntityTCommand<long>>("Кнопка с подтверждением", CustomTHeaderTwo.ExampleThree, new EntityTCommand<long>(3, ActionWithLastMessage.Delete));
            //Обертка кнопки.
            var exampleWithConfirmation = new InlineCallbackWithConfirmation(exampleInlineCallback, ActionWithLastMessage.Delete);

            //Создание нового меню.
            List<IInlineContent> menu = new() { exampleWithConfirmation };
            var testMenu = MenuGenerator.InlineKeyboard(1, menu);
            var option = new OptionMessage();

            //Передача меню в настройки
            option.MenuInlineKeyboardMarkup = testMenu;
            string msg = "InlineCallback с подтверждением";
            //Отправка сообщение с меню
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg, option);
        }

        /// <summary>
        /// Пример обработки inline класса.
        /// </summary>
        [ReplyMenuHandler("InlineClass")]
        public static async Task InlineClass(ITelegramBotClient botClient, Update update)
        {
            var exampleInlineCallback = new InlineCallback<StringTCommand>("Test1", ClassTHeader.DefaultTestClass, new StringTCommand("Test1"));
            var exampleInlineCallbackTwo = new InlineCallback<StringTCommand>("Test2", ClassTHeader.DefaultTestClass, new StringTCommand("Test2"));
            var exampleInlineCallbackThree = new InlineCallback<StringTCommand>("Test3", ClassTHeader.DefaultTestClass, new StringTCommand("Test3"));

            //Создание нового меню.
            List<IInlineContent> menu = new() { exampleInlineCallback, exampleInlineCallbackTwo, exampleInlineCallbackThree };
            var testMenu = MenuGenerator.InlineKeyboard(1, menu);
            var option = new OptionMessage();

            //Передача меню в настройки
            option.MenuInlineKeyboardMarkup = testMenu;
            string msg = "InlineClass";

            //Отправка сообщение с меню
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg, option);
        }

        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает если пользователь напишет InlineConfirm.
        /// Так же при проверки будет проигнорирован регистр команды.
        /// </summary>
        [ReplyMenuHandler("InlineConfirmWithBack")]
        [InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleBack)]
        public static async Task InlineConfirmWithBack(ITelegramBotClient botClient, Update update)
        {
            //Кнопка для которой нужно создать подтверждение.
            var exampleInlineCallback = new InlineCallback<EntityTCommand<long>>("Кнопка с подтвержением", CustomTHeaderTwo.ExampleThree, new EntityTCommand<long>(3, ActionWithLastMessage.Delete));
            //Кнопка обработчик назад или кастомная.
            var exampleBack = new InlineCallback("Назад", CustomTHeaderTwo.ExampleBack);

            //Обертка кнопки.
            var exampleWithConfirmation = new InlineCallbackWithConfirmation(exampleInlineCallback, ActionWithLastMessage.Edit, exampleBack);

            //Создание нового меню.
            List<IInlineContent> menu = new() { exampleWithConfirmation };
            var testMenu = MenuGenerator.InlineKeyboard(1, menu);
            var option = new OptionMessage();

            //Передача меню в настройки
            option.MenuInlineKeyboardMarkup = testMenu;
            string msg = "InlineCallback с подтверждением и обработкой кнопки назад или кастомной";
            //Отправка сообщение с меню
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
                await PRTelegramBot.Helpers.Message.Edit(botClient, update, msg, option);
            else
                await PRTelegramBot.Helpers.Message.Send(botClient, update, msg, option);
        }
    }
}
