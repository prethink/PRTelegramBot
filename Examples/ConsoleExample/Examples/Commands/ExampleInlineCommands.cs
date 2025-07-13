using ConsoleExample.Models;
using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Attributes;
using PRTelegramBot.Configs;
using PRTelegramBot.Extensions;
using PRTelegramBot.InlineButtons;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Helpers = PRTelegramBot.Helpers;

namespace ConsoleExample.Examples.Commands
{
    internal class ExampleInlineCommands
    {
        /// <summary>
        /// Команда отработает для бота с botId 0.
        /// Команда отработает при написание в чат "InlineMenu".
        /// Пример с генерацией inline меню
        /// Настройка конфигурационных файла при создание экземпляра PRBot <see cref="Program"/>
        /// </summary>
        [ReplyMenuHandler("InlineMenu")]
        public static async Task InlineMenu(ITelegramBotClient botClient, Update update)
        {
            /*
             *  В program.cs создается экземпляр бота:
             *   
             *  var telegram = new PRBotBuilder("")
                    .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
             *      .Build();
             *  
             *  AddConfigPath - добавляет путь для конфигурационного файла.
             *  ExampleConstants.BUTTONS_FILE_KEY - ключ 
             *  ".\\Configs\\buttons.json" - путь до конфигурационного файла.
             *  
             */

            /*
             *  botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE")
             *  BotConfigJsonProvider - провайдер который работает с json файлами.
             *  string - возращаемый тип.
             *  ExampleConstants.BUTTONS_FILE_KEY - ключ конфига.
             *  IN_EXAMPLE_ONE - ключ текста кнопки из json файла buttons.json
             * 
             */

            /* Создание новой кнопки с callback данными
             * botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE") - Название кнопки из json
             * CustomTHeaderTwo.ExampleOne - Заголовок команды
             */
            var exampleItemOne = new InlineCallback(botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE"), CustomTHeaderTwo.ExampleOne);
            /* Создание новой кнопки с callback данными
             * InlineKeys.IN_EXAMPLE_TWO - Название кнопки из константы
             * CustomTHeaderTwo.ExampleTwo - Заголовок команды
             * new EntityTCommand(2) - Данные которые требуется передать
             */
            var exampleItemTwo = new InlineCallback<EntityTCommand<long>>("Пример 2", CustomTHeaderTwo.ExampleTwo, new EntityTCommand<long>(2));
            /* Создание новой кнопки с callback данными
             * CustomTHeaderTwo.ExampleThree - Заголовок команды
             * new EntityTCommand(3) - Данные которые требуется передать
             */
            var exampleItemThree = new InlineCallback<EntityTCommand<long>>("Пример 3", CustomTHeaderTwo.ExampleThree, new EntityTCommand<long>(3));

            var inlineStep = new InlineCallback("Inline Step", CustomTHeader.InlineWithStep);

            //Команды который добавлены после запуска бота
            var exampleAddCommand = new InlineCallback("Команда добавленная динамически 1", AddCustomTHeader.TestAddCommand);
            var exampleAddCommandTwo = new InlineCallback("Команда добавленная динамически 2", AddCustomTHeader.TestAddCommandTwo);

            // Создает inline кнопку с ссылкой
            var url = new InlineURL("Google", "https://google.com");
            // Создаем кнопку для работы с webApp
            var webdata = new InlineWebApp("WebApp", "https://prethink.github.io/telegram/webapp.html");

            //IInlineContent - реализуют все inline кнопки
            List<IInlineContent> menu = new();
            menu.Add(exampleItemOne);
            menu.Add(exampleItemTwo);
            menu.Add(exampleItemThree);
            menu.Add(exampleAddCommand);
            menu.Add(exampleAddCommandTwo);
            menu.Add(inlineStep);
            menu.Add(url);
            menu.Add(webdata);

            //Генерация меню на основе данных в 1 столбец
            var testMenu = MenuGenerator.InlineKeyboard(1, menu);

            //Создание настроек для передачи в сообщение
            var option = new OptionMessage();
            //Передача меню в настройки
            option.MenuInlineKeyboardMarkup = testMenu;
            string msg = "Пример работы меню";
            //Отправка сообщение с меню
            await Helpers.Message.Send(botClient, update, msg, option);
        }

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
                    if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Edit)
                    {
                        await Helpers.Message.Edit(botClient, update, msg);
                    }
                    else
                    {
                        if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Delete)
                        {
                            await botClient.DeleteMessage(update.GetChatIdClass(), update.CallbackQuery.Message.MessageId);
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
