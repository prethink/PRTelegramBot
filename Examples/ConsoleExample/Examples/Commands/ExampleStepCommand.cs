using ConsoleExample.Models;
using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Attributes;
using PRTelegramBot.Configs;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;

namespace ConsoleExample.Examples.Commands
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
        public static async Task StepStart(IBotContext context)
        {
            string msg = "Тестирование функции пошагового выполнения\nНапишите ваше имя";
            //Регистрация обработчика для последовательной обработки шагов и сохранение данных
            context.Update.RegisterStepHandler(new StepTelegram(StepOne, new StepCache()));
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// При написание любого текста сообщения или нажатие на любую кнопку из reply для пользователя будет выполнен этот метод.
        /// Метод регистрирует следующий шаг с максимальным времени выполнения
        /// </summary>
        public static async Task StepOne(IBotContext context)
        {
            string msg = $"Шаг 1 - Ваше имя {context.Update.Message.Text}" +
                        $"\nВведите дату рождения";
            //Получаем текущий обработчик
            var handler = context.Update.GetStepHandler<StepTelegram>();
            //Записываем имя пользователя в кэш 
            handler!.GetCache<StepCache>().Name = context.Update.Message.Text;
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            handler.RegisterNextStep(StepTwo);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public static async Task StepTwo(IBotContext context)
        {
            string msg = $"Шаг 2 - дата рождения {context.Update.Message.Text}" +
                         $"\nНапиши любой текст, чтобы увидеть результат";
            //Получаем текущий обработчик
            var handler = context.Update.GetStepHandler<StepTelegram>();
            //Записываем дату рождения
            handler!.GetCache<StepCache>().BirthDay = context.Update.Message.Text;
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            handler.RegisterNextStep(StepThree, DateTime.Now.AddMinutes(1));
            //Настройки для сообщения
            var option = new OptionMessage();
            //Добавление пустого reply меню с кнопкой "Главное меню"
            //Функция является приоритетной, если пользователь нажмет эту кнопку будет выполнена функция главного меню, а не следующего шага.
            option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(1, new List<string>(), true, context.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "RP_MAIN_MENU"));
            await MessageSender.Send(context, msg, option);
        }


        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public static async Task StepThree(IBotContext context)
        {
            //Получение текущего обработчика
            var handler = context.Update.GetStepHandler<StepTelegram>();
            //Получение текущего кэша
            var cache = handler!.GetCache<StepCache>(); ;
            string msg = $"Шаг 3 - Результат: Имя:{cache.Name} дата рождения:{cache.BirthDay}" +
                         $"\nПоследовательность шагов очищена.";
            //Последний шаг
            handler.LastStepExecuted = true;
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Если есть следующий шаг, он будет проигнорирован при выполнение данной команды
        /// Потому что в ReplyMenuHandler значение первого аргумента установлено в true, что значит приоритетная команда
        /// </summary>
        [ReplyMenuHandler("ignorestep")]
        public static async Task IngoreStep(IBotContext context)
        {
            string msg = context.Update.HasStepHandler()
                ? "Следующий шаг проигнорирован"
                : "Следующий шаг отсутствовал";

            await MessageSender.Send(context, msg);
        }


        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.InlineWithStep)]
        public static async Task InlineStepp(IBotContext context)
        {
            try
            {
                //Попытка преобразовать callback данные к требуемому типу
                var command = context.GetCommandByCallbackOrNull();
                if (command != null)
                {
                    string msg = "Регистрация следующего шага, напишите что-нибудь";
                    await MessageSender.Send(context, msg);
                    context.Update.RegisterStepHandler(new StepTelegram(InlineStep, new StepCache()));
                }
            }
            catch (Exception ex)
            {
                //Обработка исключения
            }
        }

        public static async Task InlineStep(IBotContext context)
        {
            string msg = $"Вы ввели данные {context.Update.Message.Text}";
            //Получаем текущий обработчик
            var handler = context.Update.GetStepHandler<StepTelegram>();
            //Записываем имя пользователя в кэш 
            handler!.GetCache<StepCache>().Name = context.Update.Message.Text;
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            context.Update.ClearStepUserHandler();
            await MessageSender.Send(context, msg);
            await ExampleCalendar.PickCalendar(context);
        }
    }
}
