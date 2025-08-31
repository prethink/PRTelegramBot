using AspNetExample.Services;
using PRTelegramBot.Attributes;
using PRTelegramBot.Configs;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using PRTelegramBot.Extensions;
using TestDI.Models;

namespace AspNetExample.BotController
{
    [BotHandler]
    public class BotHandlerWithDependency
    {
        private readonly ILogger<BotHandlerWithDependency> _logger;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ServiceTransient serviceTransient;
        private readonly AppDbContext db;

        public BotHandlerWithDependency(ServiceScoped serviceScoped, ServiceTransient serviceTransient, ServiceSingleton serviceSingleton, ILogger<BotHandlerWithDependency> logger, AppDbContext db)
        {
            this.serviceScoped = serviceScoped;
            this.serviceTransient = serviceTransient;
            this.serviceSingleton = serviceSingleton;
            this.db = db;
            _logger = logger;
        }

        [ReplyMenuHandler("Test")]
        public async Task TestMethodWithDependency(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, $"{nameof(TestMethodWithDependency)} {_logger != null}");
        }

        [SlashHandler("/test")]
        public async Task Slash(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(Slash));
        }

        [ReplyMenuHandler("inline")]
        public async Task InlineTest(ITelegramBotClient botClient, Update update)
        {
            var options = new OptionMessage();
            var menuItemns = MenuGenerator.InlineButtons(1, new List<IInlineContent> {
                new InlineCallback("Test", PRTelegramBotCommand.CurrentPage),
                new InlineCallback("TestStatic", PRTelegramBotCommand.NextPage)
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItemns);
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(InlineTest), options);
        }

        [ReplyMenuHandler("inlinestatic")]
        public async Task StaticInlineTest(ITelegramBotClient botClient, Update update)
        {
            var options = new OptionMessage();
            var menuItemns = MenuGenerator.InlineButtons(1, new List<IInlineContent> {
                new InlineCallback("Test", PRTelegramBotCommand.CurrentPage),
                new InlineCallback("TestStatic", PRTelegramBotCommand.NextPage)
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItemns);
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(StaticInlineTest), options);
        }

        [InlineCallbackHandler<PRTelegramBotCommand>(PRTelegramBotCommand.CurrentPage)]
        public async Task InlineHandler(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(InlineHandler));
        }

        [InlineCallbackHandler<PRTelegramBotCommand>(PRTelegramBotCommand.NextPage)]
        public async static Task InlineHandlerStatic(ITelegramBotClient botClient, Update update)
        {
            await PRTelegramBot.Helpers.Message.Send(botClient, update, nameof(InlineHandlerStatic));
        }

        /// <summary>
        /// Напишите в чате "stepstart"
        /// Метод регистрирует следующий шаг пользователя
        /// </summary>
        [ReplyMenuHandler("stepstart")]
        public async Task StepStart(ITelegramBotClient botClient, Update update)
        {
            string msg = "Тестирование функции пошагового выполнения\nНапишите ваше имя";
            //Регистрация обработчика для последовательной обработки шагов и сохранение данных
            update.RegisterStepHandler(new StepTelegram(StepOne, new StepCache()));
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// При написание любого текста сообщения или нажатие на любую кнопку из reply для пользователя будет выполнен этот метод.
        /// Метод регистрирует следующий шаг с максимальным времени выполнения
        /// </summary>
        public async Task StepOne(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Шаг 1 - Ваше имя {update.Message.Text}" +
                        $"\nВведите дату рождения";
            //Получаем текущий обработчик
            var handler = update.GetStepHandler<StepTelegram>();
            //Записываем имя пользователя в кэш 
            handler!.GetCache<StepCache>().Name = update.Message.Text;
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            handler.RegisterNextStep(StepTwo);
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public async Task StepTwo(ITelegramBotClient botClient, Update update)
        {
            string msg = $"Шаг 2 - дата рождения {update.Message.Text}" +
                         $"\nНапиши любой текст, чтобы увидеть результат";
            //Получаем текущий обработчик
            var handler = update.GetStepHandler<StepTelegram>();
            //Записываем дату рождения
            handler!.GetCache<StepCache>().BirthDay = update.Message.Text;
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            handler.RegisterNextStep(StepThree, DateTime.Now.AddMinutes(1));
            //Настройки для сообщения
            var option = new OptionMessage();
            //Добавление пустого reply меню с кнопкой "Главное меню"
            //Функция является приоритетной, если пользователь нажмет эту кнопку будет выполнена функция главного меню, а не следующего шага.
            //option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(1, new List<string>(), true, botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "RP_MAIN_MENU"));
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg, option);
        }


        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public async Task StepThree(ITelegramBotClient botClient, Update update)
        {
            //Получение текущего обработчика
            var handler = update.GetStepHandler<StepTelegram>();
            //Получение текущего кэша
            var cache = handler!.GetCache<StepCache>(); ;
            string msg = $"Шаг 3 - Результат: Имя:{cache.Name} дата рождения:{cache.BirthDay}" +
                         $"\nПоследовательность шагов очищена.";
            //Последний шаг
            handler.LastStepExecuted = true;
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
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

            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
    }
}
