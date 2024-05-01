using Telegram.Bot.Types;

namespace PRTelegramBot.Extensions
{
    public static class UpdateExtension
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

            throw new Exception("Failed to obtain chat id");
        }

        /// <summary>
        /// Получает идентификатор сообщения
        /// </summary>
        /// <param name="update">Обновление полученное с телеграма</param>
        /// <returns>Идентификатор сообщения</returns>
        /// <exception cref="Exception">Не найден тип сообщения</exception>
        public static int GetMessageId(this Update update)
        {
            //var data = update.GetCacheData<TelegramCache>();

            if (update.CallbackQuery != null)
                return update.CallbackQuery.Message.MessageId;

            //if (data?.LastMessage?.MessageId > 0)
            //{
            //    var messageId = data.LastMessage.MessageId;
            //    return messageId;
            //}

            throw new Exception("Failed to obtain message id");
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
    }
}
