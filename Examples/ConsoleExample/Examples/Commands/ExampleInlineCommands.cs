using ConsoleExample.Models;
using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Attributes;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Configs;
using PRTelegramBot.Extensions;
using PRTelegramBot.InlineButtons;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Services.Messages;
using Telegram.Bot;
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
        public static async Task InlineMenu(IBotContext context)
        {
            /*
             *  В program.cs создается экземпляр бота:
             *   
             *  var telegram = new PRBotBuilder(string.Empty)
                    .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
             *      .Build();
             *  
             *  AddConfigPath - добавляет путь для конфигурационного файла.
             *  ExampleConstants.BUTTONS_FILE_KEY - ключ 
             *  ".\\Configs\\buttons.json" - путь до конфигурационного файла.
             *  
             */

            /*
             *  context.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE")
             *  BotConfigJsonProvider - провайдер который работает с json файлами.
             *  string - возращаемый тип.
             *  ExampleConstants.BUTTONS_FILE_KEY - ключ конфига.
             *  IN_EXAMPLE_ONE - ключ текста кнопки из json файла buttons.json
             * 
             */

            /* Создание новой кнопки с callback данными
             * context`.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE") - Название кнопки из json
             * CustomTHeaderTwo.ExampleOne - Заголовок команды
             */
            var exampleItemOne = new InlineCallback(context.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE"), CustomTHeaderTwo.ExampleOne);
            /* Создание новой кнопки с callback данными
             * InlineKeys.IN_EXAMPLE_TWO - Название кнопки из константы
             * CustomTHeaderTwo.ExampleTwo - Заголовок команды
             * new EntityTCommand(2) - Данные которые требуется передать
             */
            var exampleItemTwo = new InlineCallback<EntityTCommand<long>>("Пример с большим числом", CustomTHeaderTwo.ExampleTwo, new EntityTCommand<long>(2_000_000_000_000_000_000));
            /* Создание новой кнопки с callback данными
             * CustomTHeaderTwo.ExampleThree - Заголовок команды
             * new EntityTCommand(3) - Данные которые требуется передать
             */

            var exampleItemThree = new InlineCallback<EntityTCommand<string>>("Пример с большим текстом", CustomTHeaderTwo.ExampleThree, new EntityTCommand<string>("И нет сомнений, что диаграммы связей будут объявлены нарушающими общечеловеческие нормы этики и морали. Имеется спорная точка зрения, гласящая примерно следующее: ключевые особенности структуры проекта, инициированные исключительно синтетически, своевременно верифицированы. Значимость этих проблем настолько очевидна, что высокотехнологичная концепция общественного уклада обеспечивает широкому кругу (специалистов) участие в формировании переосмысления внешнеэкономических политик. Таким образом, высокотехнологичная концепция общественного уклада играет важную роль в формировании экспериментов, поражающих по своей масштабности и грандиозности. Картельные сговоры не допускают ситуации, при которой тщательные исследования конкурентов, превозмогая сложившуюся непростую экономическую ситуацию, заблокированы в рамках своих собственных рациональных ограничений. Каждый из нас понимает очевидную вещь: реализация намеченных плановых заданий выявляет срочную потребность как самодостаточных, так и внешне зависимых концептуальных решений. Равным образом, убеждённость некоторых оппонентов однозначно определяет каждого участника как способного принимать собственные решения касаемо первоочередных требований. Повседневная практика показывает, что реализация намеченных плановых заданий обеспечивает актуальность распределения внутренних резервов и ресурсов. В своём стремлении повысить качество жизни, они забывают, что базовый вектор развития обеспечивает актуальность поставленных обществом задач."));

            var inlineStep = new InlineCallback("Inline Step", CustomTHeader.InlineWithStep);

            //Команды который добавлены после запуска бота
            var exampleAddCommand = new InlineCallback("Команда добавленная динамически 1", AddCustomTHeader.TestAddCommand);
            var exampleAddCommandTwo = new InlineCallback("Команда добавленная динамически 2", AddCustomTHeader.TestAddCommandTwo);

            // Создает inline кнопку с ссылкой
            var url = new InlineURL("Google", "https://google.com");
            // Создаем кнопку для работы с webApp
            var webdata = new InlineWebApp("WebApp", "https://prethink.github.io/telegram/webapp.html");

            var keyboard = new InlineKeyboardBuilder()
                .AddButton(exampleItemOne)
                .AddButton(exampleItemTwo, newRow:true)
                .AddButton(exampleItemThree, newRow: true)
                .AddButton(exampleAddCommand, newRow: true)
                .AddRow()
                .AddButton(exampleAddCommandTwo)
                .AddButton(inlineStep)
                .AddRow()
                .AddButton(url)
                .AddButton(webdata)
                .Build();

            //Создание настроек для передачи в сообщение
            var option = new OptionMessage();
            //Передача меню в настройки
            option.MenuInlineKeyboardMarkup = keyboard;
            string msg = "Пример работы меню";
            //Отправка сообщение с меню
            await MessageSender.Send(context, msg, option);
        }

        /// <summary>
        /// callback обработка
        /// Обрабатывает одну точку входа
        /// </summary>
        [InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleOne)]
        public static async Task Inline(IBotContext context)
        {
            try
            {
                //Попытка преобразовать callback данные к требуемому типу
                var command = context.GetCommandByCallbackOrNull();
                if (command != null)
                {
                    string msg = "Выполнена команда callback";
                    await MessageSender.Send(context, msg);
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
        [InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleTwo)]
        public static async Task InlineTwo(IBotContext context)
        {
            try
            {
                //Попытка преобразовать callback данные к требуемому типу
                var command = context.GetCommandByCallbackOrNull<EntityTCommand<long>>();
                if (command != null)
                {
                    string msg = $"Идентификатор который вы передали {command.Data.EntityId}";
                    if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Edit)
                    {
                        await MessageEditor.Edit(context, msg);
                    }
                    else
                    {
                        if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Delete)
                        {
                            await context.BotClient.DeleteMessage(context.Update.GetChatIdClass(), context.Update.CallbackQuery.Message.MessageId);
                        }
                        await MessageSender.Send(context, msg);
                    }
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
        [InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleThree)]
        public static async Task InlineThree(IBotContext context)
        {
            try
            {
                //Попытка преобразовать callback данные к требуемому типу
                var command = context.GetCommandByCallbackOrNull<EntityTCommand<string>>();
                if (command != null)
                {
                    string msg = $"Идентификатор который вы передали {command.Data.EntityId}";
                    if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Edit)
                    {
                        await MessageEditor.Edit(context, msg);
                    }
                    else
                    {
                        if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Delete)
                        {
                            await context.BotClient.DeleteMessage(context.Update.GetChatIdClass(), context.Update.CallbackQuery.Message.MessageId);
                        }
                        await MessageSender.Send(context, msg);
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
