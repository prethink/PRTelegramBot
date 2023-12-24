using PRTelegramBot.Attributes;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Commands.Constants;
using static PRTelegramBot.Models.StepTelegram;
using PRTelegramBot.Helpers;
using Helpers = PRTelegramBot.Helpers;
using CallbackId = PRTelegramBot.Models.Enums.THeader;
using ConsoleExample.Models;
using ConsoleExample.Commands.Constants;
using PRTelegramBot.Models.Interface;
using PRTelegramBot.Utils;

namespace ConsoleExample.Examples
{
    /// <summary>
    /// Пример работы пошагового выполнения команд
    /// </summary>
    public class ExampleStepCommand
    {
        /// <summary>
        /// Напишите в чате "stepstart"
        /// Метод регистрирует следующий шаг пользователя
        /// </summary>
        [ReplyMenuHandler("stepstart")]
        public static async Task StepStart(ITelegramBotClient botClient, Update update)
        {
            string msg = "Тестирование функции пошагового выполнения\nНапишите ваше имя";
            //Регистрация следующего шага пользователя
            update.RegisterNextStep(new StepTelegram(StepOne, new TestParams()));
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// При написание любого текста сообщения или нажатие на любую кнопку из reply для пользователя будет выполнен этот метод.
        /// Метод регистрирует следующий шаг с максимальным времени выполнения
        /// </summary>
        public static async Task StepOne(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            string msg = $"Шаг 1 - Ваше имя {update.Message.Text}" +
                        $"\nВведите дату рождения";
            var data = args as TestParams;
            data.DataOne = $"Имя: {update.Message.Text}";
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            update.RegisterNextStep(new StepTelegram(StepTwo, DateTime.Now.AddMinutes(5), data));
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public static async Task StepTwo(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            string msg = $"Шаг 2 - дата рождения {update.Message.Text}" +
                         $"\nНапиши любой текст, чтобы увидеть результат";
            var data = args as TestParams;
            data.DataTwo = $"дата рождения {update.Message.Text}";
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            update.RegisterNextStep(new StepTelegram(StepThree, DateTime.Now.AddMinutes(5), data));

            //Настройки для сообщения
            var option = new OptionMessage();
            //Добавление пустого reply меню с кнопкой "Главное меню"
            //Функция является приоритетной, если пользователь нажмет эту кнопку будет выполнена функция главного меню, а не следующего шага.
            option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(1, new List<string>(), true, new DictionaryJSON().GetButton(nameof(ReplyKeys.RP_MAIN_MENU)));
            await Helpers.Message.Send(botClient, update, msg, option);
        }


        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public static async Task StepThree(ITelegramBotClient botClient, Update update, CustomParameters args)
        {
            var data = args as TestParams;
            string msg = $"Шаг 3 - Результат:\n{data.DataOne}\n{data.DataTwo}" +
                         $"\nПоследовательность шагов очищена.";
            //Последний шаг
            update.ClearStepUser();
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Если есть следующий шаг, он будет проигнорирован при выполнение данной команды
        /// Потому что в ReplyMenuHandler значение первого аргумента установлено в true, что значит приоритетная команда
        /// </summary>
        [ReplyMenuHandler("ignorestep")]
        public static async Task IngoreStep(ITelegramBotClient botClient, Update update)
        {
            string msg = "";
            if (update.HasStep())
            {
                msg = "Следующий шаг проигнорирован";
            }
            else
            {
                msg = "Следующий шаг отсутствовал";
            }
            
            await Helpers.Message.Send(botClient, update, msg);
        }
    }
}
