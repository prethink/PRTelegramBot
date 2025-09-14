using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.EventsArgs;
using Telegram.Bot;

namespace PRTelegramBot.Extensions
{
    /// <summary>
    /// Методы расширения для ITelegramBotClient.
    /// </summary>
    public static class ITelegramBotClientExtension
    {
        #region Методы

        /// <summary>
        /// Проверяет пользователя, является ли он администратором бота.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>True - администратор, False - не администратор.</returns>
        public static async Task<bool> IsAdmin(this IBotContext context)
        {
            return await IsAdmin(context);
        }

        /// <summary>
        /// Проверяет пользователя, является ли он администратором бота.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - администратор, False - не администратор.</returns>
        public static async Task<bool> IsAdmin(this IBotContext context, long userId)
        {
            return await context.Current.Options.AdminManager.HasUser(userId);
        }

        /// <summary>
        /// Проверяет пользователя, присутствует ли в белом списке бота.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>True - есть в списке, False - нет в списке.</returns>
        public static async Task<bool> InWhiteList(this IBotContext context)
        {
            return await InWhiteList(context, context.Update.GetChatId());
        }

        /// <summary>
        /// Проверяет пользователя, присутствует ли в белом списке бота.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>True - есть в списке, False - нет в списке.</returns>
        public static async Task<bool> InWhiteList(this IBotContext context, long userId)
        {
            return await context.Current.Options.WhiteListManager.HasUser(userId);
        }

        /// <summary>
        /// Возвращает список администраторов бота.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Список идентификаторов.</returns>
        public static async Task<List<long>> GetAdminsIds(this IBotContext context)
        {
            return await context.Current.Options.AdminManager.GetUsersIds();
        }

        /// <summary>
        /// Возвращает белый список пользователей.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <returns>Список идентификаторов.</returns>
        public static async Task<List<long>> GetWhiteListIds(this IBotContext context)
        {
            return await context.Current.Options.WhiteListManager.GetUsersIds();
        }

        /// <summary>
        /// Вызов события простого лога.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="msg">Сообщение.</param>
        /// <param name="typeEvent">Тип события.</param>
        /// <param name="color">Цвет.</param>
        public static void InvokeCommonLog(this IBotContext context, string msg, string typeEvent = "", ConsoleColor color = ConsoleColor.Blue)
        {
            context.Current.Events.OnCommonLogInvoke(msg, typeEvent, color);
        }

        /// <summary>
        /// Вызов события логирование ошибок.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="ex">Исключение.</param>
        public static void InvokeErrorLog(this IBotContext context, Exception ex)
        {
            context.Current.Events.OnErrorLogInvoke(new ErrorLogEventArgs(context, ex));
        }

        /// <summary>
        /// Генерация реферальной ссылки.
        /// </summary>
        /// <param name="context">Контекст бота.</param>
        /// <param name="refLink">Текст реферальной ссылки.</param>
        /// <returns>Сгенерированная реферальная ссылка https://t.me/{bot.Username}?start={refLink}.</returns>
        /// <exception cref="ArgumentNullException">Вызывается в случае пустого текста.</exception>
        public async static Task<string> GetGeneratedRefLink(this IBotContext context, string refLink)
        {
            if (string.IsNullOrEmpty(refLink))
                throw new ArgumentNullException(nameof(refLink));

            var bot = await context.BotClient.GetMe();
            return $"https://t.me/{bot.Username}?start={refLink}";
        }

        /// <summary>
        /// Получить значение из конфиг файла по ключу
        /// </summary>
        /// <typeparam name="TBotProvider">Провайдера работы с файлами.</typeparam>
        /// <typeparam name="TReturn">Возращаемый тип.</typeparam>
        /// <param name="context">Контекст бота.</param>
        /// <param name="configKey">Ключ конфига.</param>
        /// <param name="key">Ключ для значения.</param>
        /// <returns>Значение из конфиг файла.</returns>
        public static TReturn GetConfigValue<TBotProvider, TReturn>(this IBotContext context, string configKey, string key)
            where TBotProvider : IBotConfigProvider
        {
            string configPath = context.Current.Options.ConfigPaths[configKey];
            var botConfiguration = Activator.CreateInstance(typeof(TBotProvider)) as IBotConfigProvider;
            botConfiguration.SetConfigPath(configPath);
            return botConfiguration.GetValue<TReturn>(key);
        }

        /// <summary>
        /// Попытаться получить значение из конфиг файла по ключу
        /// </summary>
        /// <typeparam name="TBotProvider">Провайдера работы с файлами.</typeparam>
        /// <typeparam name="TReturn">Возращаемый тип.</typeparam>
        /// <param name="context">Контекст бота.</param>
        /// <param name="configKey">Ключ конфига.</param>
        /// <param name="key">Ключ для значения.</param>
        /// <param name="result">Значение.</param>
        /// <returns>True - значение получено, False - не удалось получить значение.</returns>
        public static bool TryGetConfigValue<TBotProvider, TReturn>(this IBotContext context, string configKey, string key, out TReturn result)
            where TBotProvider : IBotConfigProvider, new()
        {
            result = default(TReturn);
            try
            {
                var botConfiguration = new TBotProvider(); // Создание экземпляра поставщика конфигурации
                string configPath = context.Current.Options?.ConfigPaths?.GetValueOrDefault(configKey);

                if (configPath is null)
                {
                    // Если путь конфигурации не найден, возвращаем false
                    return false;
                }

                botConfiguration.SetConfigPath(configPath); // Установка пути конфигурации
                result = botConfiguration.GetValue<TReturn>(key); // Получение значения конфигурации
                return true; // Успешно получили значение конфигурации
            }
            catch (Exception ex)
            {
                // Обработка ошибки и возврат false
                return false;
            }
        }

        #endregion
    }
}
