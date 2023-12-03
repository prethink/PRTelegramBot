using Telegram.Bot.Types;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Models;
using Telegram.Bot;

namespace PRTelegramBot.Extensions
{
    public static class User
    {
        /// <summary>
        /// Получает ChatId в зависимости от типа сообщений
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <returns>ChatId</returns>
        /// <exception cref="Exception">Не найден тип сообщения</exception>
        public static long GetChatId(this Update update)
        {
            if (update.Message != null)
                return update.Message.Chat.Id;

            if (update.CallbackQuery != null)
                return update.CallbackQuery.Message.Chat.Id;


            throw new Exception("Не удалось получить чат ID");
        }

        /// <summary>
        /// Получает идентификатор сообщения
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <returns>Идентификатор сообщения</returns>
        /// <exception cref="Exception">Не найден тип сообщения</exception>
        public static int GetMessageId(this Update update)
        {
            var data = update.GetCacheData<TelegramCache>();

            if (update.CallbackQuery != null)
                return update.CallbackQuery.Message.MessageId;

            if (data?.LastMessage?.MessageId > 0)
            {
                var messageId = data.LastMessage.MessageId;
                return messageId;
            }

            throw new Exception("Не удалось получить ID чата");
        }

        /// <summary>
        /// Проверка что пользователь администратор из конфига
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <returns>true/false</returns>
        public static bool IsAdmin(this Update update, Core.PRBot service)
        {
            try
            {
                var telegramId = update.GetChatId();
                var admins = service.Config.Admins;
                return admins != null ? admins.Contains(telegramId) : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Информация о пользователе
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <returns>Информация о пользователе</returns>
        public static string GetInfoUser(this Update update)
        {
            string result = "";

            result += update?.Message?.Chat?.Id + " ";
            result += string.IsNullOrEmpty(update?.Message?.Chat?.FirstName) ? "" : update.Message.Chat.FirstName + " ";
            result += string.IsNullOrEmpty(update?.Message?.Chat?.LastName) ? "" : update.Message.Chat.LastName + " ";
            result += string.IsNullOrEmpty(update?.Message?.Chat?.Username) ? "" : update.Message.Chat.Username + " ";

            result += update?.CallbackQuery?.Message?.Chat?.Id + " ";
            result += string.IsNullOrEmpty(update?.CallbackQuery?.Message?.Chat?.FirstName) ? "" : update.CallbackQuery.Message.Chat.FirstName + " ";
            result += string.IsNullOrEmpty(update?.CallbackQuery?.Message?.Chat?.LastName) ? "" : update.CallbackQuery.Message.Chat.LastName + " ";
            result += string.IsNullOrEmpty(update?.CallbackQuery?.Message?.Chat?.Username) ? "" : update.CallbackQuery.Message.Chat.Username + " ";

            return result;
        }

        /// <summary>
        /// Получает реферальную ссылку пользователя по его ID
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <param name="copy">использовать html разметку для возможности копирования ссылки</param>
        /// <returns>Реферальная ссылка</returns>
        public async static Task<string> GetRefLink(this Update update,ITelegramBotClient botClient, bool copy = false)
        {
            var bot = await botClient.GetMeAsync();
            if (copy)
            {
                return $"<code>https://t.me/{bot.Username}?start={update.GetChatId()}</code>";
            }
            else
            {
                return $"https://t.me/{bot.Username}?start={update.GetChatId()}";
            }

        }
    }
}
