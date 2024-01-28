using PRTelegramBot.Attributes;
using Telegram.Bot.Types;
using Telegram.Bot;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Commands.Constants;
using Helpers = PRTelegramBot.Helpers;
using ConsoleExample.Models;
using PRTelegramBot.Utils;
using PRTelegramBot.Models.InlineButtons;
using System.Globalization;

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
            //Регистрация обработчика для последовательной обработки шагов и сохранение данных
            update.RegisterStepHandler(new StepTelegram(StepOne, new StepCache()));
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// При написание любого текста сообщения или нажатие на любую кнопку из reply для пользователя будет выполнен этот метод.
        /// Метод регистрирует следующий шаг с максимальным времени выполнения
        /// </summary>
        public static async Task StepOne(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Шаг 1 - Ваше имя {update.Message.Text}" +
                        $"\nВведите дату рождения";
            //Получаем текущий обработчик
            var handler = update.GetStepHandler<StepTelegram>();
            //Записываем имя пользователя в кэш 
            handler!.GetCache<StepCache>().Name = update.Message.Text;
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            handler.RegisterNextStep(StepTwo, DateTime.Now.AddMinutes(5));
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public static async Task StepTwo(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Шаг 2 - дата рождения {update.Message.Text}" +
                         $"\nНапиши любой текст, чтобы увидеть результат";
            //Получаем текущий обработчик
            var handler = update.GetStepHandler<StepTelegram>();
            //Записываем дату рождения
            handler!.GetCache<StepCache>().BirthDay = update.Message.Text;
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            handler.RegisterNextStep(StepThree, DateTime.Now.AddMinutes(5));
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
        public static async Task StepThree(ITelegramBotClient botClient, Update update)
        {
            //Получение текущего обработчика
            var handler = update.GetStepHandler<StepTelegram>();
            //Получение текущего кэша
            var cache = handler!.GetCache<StepCache>(); ;
            string msg = $"Шаг 3 - Результат: Имя:{cache.Name} дата рождения:{cache.BirthDay}" +
                         $"\nПоследовательность шагов очищена.";
            //Последний шаг
            update.ClearStepUserHandler();
            await Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Если есть следующий шаг, он будет проигнорирован при выполнение данной команды
        /// Потому что в ReplyMenuHandler значение первого аргумента установлено в true, что значит приоритетная команда
        /// </summary>
        [ReplyMenuHandler("ignorestep")]
        public static async Task IngoreStep(ITelegramBotClient botClient, Update update)
        {
            string msg = update.HasStepHandler() 
                ? "Следующий шаг проигнорирован" 
                : "Следующий шаг отсутствовал";
            
            await Helpers.Message.Send(botClient, update, msg);
        }


        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.InlineWithStepp)]
        public static async Task InlineStepp(ITelegramBotClient botClient, Update update)
        {
            try
            {
                //Попытка преобразовать callback данные к требуемому типу
                var command = InlineCallback.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
                if (command != null)
                {
                    string msg = "Регистрация следующего шага, напишите что-нибудь";
                    await Helpers.Message.Send(botClient, update, msg);
                    update.RegisterStepHandler(new StepTelegram(InlineStep, new StepCache()));
                }
            }
            catch (Exception ex)
            {
                //Обработка исключения
            }
        }

        public static async Task InlineStep(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Вы ввели данные {update.Message.Text}";
            //Получаем текущий обработчик
            var handler = update.GetStepHandler<StepTelegram>();
            //Записываем имя пользователя в кэш 
            handler!.GetCache<StepCache>().Name = update.Message.Text;
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            update.ClearStepUserHandler();
            await Helpers.Message.Send(botClient, update, msg);
            await ExampleCalendar.PickCalendar(botClient, update);
        }
    }
}
