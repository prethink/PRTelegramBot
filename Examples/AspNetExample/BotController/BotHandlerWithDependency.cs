using AspNetExample.Services;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
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
        public async Task TestMethodWithDependency(IBotContext context)
        {
            await PRTelegramBot.Helpers.Message.Send(context, $"{nameof(TestMethodWithDependency)} {_logger != null}");
        }

        [SlashHandler("/test")]
        public async Task Slash(IBotContext context)
        {
            await PRTelegramBot.Helpers.Message.Send(context, nameof(Slash));
        }

        [ReplyMenuHandler("inline")]
        public async Task InlineTest(IBotContext context)
        {
            var options = new OptionMessage();
            var menuItemns = MenuGenerator.InlineButtons(1, new List<IInlineContent> {
                new InlineCallback("Test", PRTelegramBotCommand.CurrentPage),
                new InlineCallback("TestStatic", PRTelegramBotCommand.NextPage)
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItemns);
            await PRTelegramBot.Helpers.Message.Send(context, nameof(InlineTest), options);
        }

        [ReplyMenuHandler("inlinestatic")]
        public async Task StaticInlineTest(IBotContext context)
        {
            var options = new OptionMessage();
            var menuItemns = MenuGenerator.InlineButtons(1, new List<IInlineContent> {
                new InlineCallback("Test", PRTelegramBotCommand.CurrentPage),
                new InlineCallback("TestStatic", PRTelegramBotCommand.NextPage)
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItemns);
            await PRTelegramBot.Helpers.Message.Send(context, nameof(StaticInlineTest), options);
        }

        [InlineCallbackHandler<PRTelegramBotCommand>(PRTelegramBotCommand.CurrentPage)]
        public async Task InlineHandler(IBotContext context)
        {
            await PRTelegramBot.Helpers.Message.Send(context, nameof(InlineHandler));
        }

        [InlineCallbackHandler<PRTelegramBotCommand>(PRTelegramBotCommand.NextPage)]
        public async static Task InlineHandlerStatic(IBotContext context)
        {
            await PRTelegramBot.Helpers.Message.Send(context, nameof(InlineHandlerStatic));
        }

        /// <summary>
        /// Напишите в чате "stepstart"
        /// Метод регистрирует следующий шаг пользователя
        /// </summary>
        [ReplyMenuHandler("stepstart")]
        public async Task StepStart(IBotContext context)
        {
            string msg = "Тестирование функции пошагового выполнения\nНапишите ваше имя";
            //Регистрация обработчика для последовательной обработки шагов и сохранение данных
            context.Update.RegisterStepHandler(new StepTelegram(StepOne, new StepCache()));
            await PRTelegramBot.Helpers.Message.Send(context, msg);
        }

        /// <summary>
        /// При написание любого текста сообщения или нажатие на любую кнопку из reply для пользователя будет выполнен этот метод.
        /// Метод регистрирует следующий шаг с максимальным времени выполнения
        /// </summary>
        public async Task StepOne(IBotContext context)
        {
            string msg = $"Шаг 1 - Ваше имя {context.Update.Message.Text}" +
                        $"\nВведите дату рождения";
            //Получаем текущий обработчик
            var handler = context.Update.GetStepHandler<StepTelegram>();
            //Записываем имя пользователя в кэш 
            handler!.GetCache<StepCache>().Name = context.Update.Message.Text;
            //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
            handler.RegisterNextStep(StepTwo);
            await PRTelegramBot.Helpers.Message.Send(context, msg);
        }

        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public async Task StepTwo(IBotContext context)
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
            //option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(1, new List<string>(), true, botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "RP_MAIN_MENU"));
            await PRTelegramBot.Helpers.Message.Send(context, msg, option);
        }


        /// <summary>
        /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
        /// </summary>
        public async Task StepThree(IBotContext context)
        {
            //Получение текущего обработчика
            var handler = context.Update.GetStepHandler<StepTelegram>();
            //Получение текущего кэша
            var cache = handler!.GetCache<StepCache>(); ;
            string msg = $"Шаг 3 - Результат: Имя:{cache.Name} дата рождения:{cache.BirthDay}" +
                         $"\nПоследовательность шагов очищена.";
            //Последний шаг
            handler.LastStepExecuted = true;
            await PRTelegramBot.Helpers.Message.Send(context, msg);
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

            await PRTelegramBot.Helpers.Message.Send(context, msg);
        }
    }
}
