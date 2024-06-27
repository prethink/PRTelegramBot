using PRTelegramBot.Core;
using PRTelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

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
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Обновление из telegram.</param>
        /// <returns>True - администратор, False - не администратор.</returns>
        public static async Task<bool> IsAdmin(this ITelegramBotClient botClient, Update update)
        {
            return await IsAdmin(botClient, update.GetChatId());
        }

        /// <summary>
        /// Проверяет пользователя, является ли он администратором бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - администратор, False - не администратор.</returns>
        public static async Task<bool> IsAdmin(this ITelegramBotClient botClient, long userId)
        {
            var botData = GetBotDataOrNull(botClient);
            return botData != null && await botData.Options.AdminManager.HasUser(userId);
        }

        /// <summary>
        /// Проверяет пользователя, присутствует ли в белом списке бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Обновление из telegram.</param>
        /// <returns>True - есть в списке, False - нет в списке.</returns>
        public static async Task<bool> InWhiteList(this ITelegramBotClient botClient, Update update)
        {
            return await InWhiteList(botClient, update.GetChatId());
        }

        /// <summary>
        /// Проверяет пользователя, присутствует ли в белом списке бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>True - есть в списке, False - нет в списке.</returns>
        public static async Task<bool> InWhiteList(this ITelegramBotClient botClient, long userId)
        {
            var botData = GetBotDataOrNull(botClient);
            return botData != null && await botData.Options.WhiteListManager.HasUser(userId);
        }

        /// <summary>
        /// Возращает список администраторов бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Список идентификаторов.</returns>
        public static async Task<List<long>> GetAdminsIds(this ITelegramBotClient botClient)
        {
            var botData = GetBotDataOrNull(botClient);
            return botData != null ? await botData.Options.AdminManager.GetUsersIds() : new List<long>();
        }

        /// <summary>
        /// Возращает белый список пользователей.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Список идентификаторов.</returns>
        public static async Task<List<long>> GetWhiteListIds(this ITelegramBotClient botClient)
        {
            var botData = GetBotDataOrNull(botClient);
            return botData != null ? await botData.Options.WhiteListManager.GetUsersIds() : new List<long>();
        }

        /// <summary>
        /// Получить экземпляр класса бота.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <returns>Экземпляр класса или null.</returns>
        public static PRBotBase GetBotDataOrNull(this ITelegramBotClient botClient)
        {
            return BotCollection.Instance.GetBotByTelegramIdOrNull(botClient.BotId);
        }

        /// <summary>
        /// Вызов события простого лога.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="msg">Сообщение.</param>
        /// <param name="typeEvent">Тип события.</param>
        /// <param name="color">Цвет.</param>
        public static void InvokeCommonLog(this ITelegramBotClient botClient, string msg, string typeEvent = "", ConsoleColor color = ConsoleColor.Blue)
        {
            var bot = GetBotDataOrNull(botClient);
            bot?.Events.OnCommonLogInvoke(msg, typeEvent, color);
        }

        /// <summary>
        /// Вызов события логирование ошибок.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="ex">Исключение.</param>
        public static void InvokeErrorLog(this ITelegramBotClient botClient, Exception ex)
        {
            var bot = GetBotDataOrNull(botClient);
            bot?.Events.OnErrorLogInvoke(ex);
        }

        /// <summary>
        /// Вызов события логирование ошибок.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="ex">Исключение.</param>
        /// <param name="update">обновление.</param>
        public static void InvokeErrorLog(this ITelegramBotClient botClient, Exception ex, Update update)
        {
            var bot = GetBotDataOrNull(botClient);
            bot?.Events.OnErrorLogInvoke(ex, update);
        }

        /// <summary>
        /// Генерация реферальной ссылки.
        /// </summary>
        /// <param name="botClient">Бот.</param>
        /// <param name="refLink">Текст реферальной ссылки.</param>
        /// <returns>Сгенерированная реферальная ссылка https://t.me/{bot.Username}?start={refLink}.</returns>
        /// <exception cref="ArgumentNullException">Вызывается в случае пустого текста.</exception>
        public async static Task<string> GetGeneratedRefLink(this ITelegramBotClient botClient, string refLink)
        {
            if (string.IsNullOrEmpty(refLink))
                throw new ArgumentNullException(nameof(refLink));

            var bot = await botClient.GetMeAsync();
            return $"https://t.me/{bot.Username}?start={refLink}";
        }

        /// <summary>
        /// Получить значение из конфиг файла по ключу
        /// </summary>
        /// <typeparam name="TBotProvider">Провайдера работы с файлами.</typeparam>
        /// <typeparam name="TReturn">Возращаемый тип.</typeparam>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="configKey">Ключ конфига.</param>
        /// <param name="key">Ключ для значения.</param>
        /// <returns>Значение из конфиг файла.</returns>
        public static TReturn GetConfigValue<TBotProvider, TReturn>(this ITelegramBotClient botClient, string configKey, string key)
            where TBotProvider : IBotConfigProvider
        {
            string configPath = botClient.GetBotDataOrNull().Options.ConfigPaths[configKey];
            var botConfiguration = Activator.CreateInstance(typeof(TBotProvider)) as IBotConfigProvider;
            botConfiguration.SetConfigPath(configPath);
            return botConfiguration.GetValue<TReturn>(key);
        }

        /// <summary>
        /// Попытаться получить значение из конфиг файла по ключу
        /// </summary>
        /// <typeparam name="TBotProvider">Провайдера работы с файлами.</typeparam>
        /// <typeparam name="TReturn">Возращаемый тип.</typeparam>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="configKey">Ключ конфига.</param>
        /// <param name="key">Ключ для значения.</param>
        /// <param name="result">Значение.</param>
        /// <returns>True - значение получено, False - не удалось получить значение.</returns>
        public static bool TryGetConfigValue<TBotProvider, TReturn>(this ITelegramBotClient botClient, string configKey, string key, out TReturn result)
            where TBotProvider : IBotConfigProvider, new()
        {
            result = default(TReturn);
            try
            {
                var botConfiguration = new TBotProvider(); // Создание экземпляра поставщика конфигурации
                string configPath = botClient.GetBotDataOrNull()?.Options?.ConfigPaths?.GetValueOrDefault(configKey);

                if (configPath == null)
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
