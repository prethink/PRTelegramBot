using Telegram.Bot;
using PRTelegramBot.Core;

namespace PRTelegramBot.Helpers.TG
{
    /// <summary>
    /// Хелпер для работы с группами
    /// </summary>
    internal class GroupHelper
    {
        /// <summary>
        /// Проверяет находится ли пользователь в группе
        /// </summary>
        /// <param name="botClient">Телеграм бот клиент</param>
        /// <param name="groupId">Идентификатор группы</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>True/false</returns>
        public static async Task<bool> IsMemberGroup(ITelegramBotClient botClient, long groupId, long userId)
        {
            try
            {
                var data = await botClient.GetChatMemberAsync(groupId, userId);
                return data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Member ||
                        data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Administrator ||
                        data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Creator ||
                        data.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Administrator;
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
                return false;
            }
        }
    }
}
