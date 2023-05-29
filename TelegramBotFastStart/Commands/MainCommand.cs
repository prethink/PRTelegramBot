using PRTelegramBot.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
using PRTelegramBot.Extensions;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Core;
using PRTelegramBot.Commands.Constants;

namespace PRTelegramBot.Commands
{
    internal class MainCommand
    {
        [ReplyMenuHandler(true, nameof(ReplyKeys.RP_MENU), nameof(ReplyKeys.RP_MAIN_MENU))]
        [RequiredTypeUpdate(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task MainMenu(ITelegramBotClient botClient, Update update)
        {
            try
            {
                await MainMenu(botClient,  update.GetChatId(), MessageKeys.GetMessage(nameof(MessageKeys.MSG_MAIN_MENU)));
            }
            catch(Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        public static async Task MainMenu(ITelegramBotClient botClient, long telegramId)
        {
            try
            {
                await MainMenu(botClient, telegramId, MessageKeys.GetMessage(nameof(MessageKeys.MSG_MAIN_MENU)));
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

        [RequiredTypeUpdate(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task MainMenu(ITelegramBotClient botClient, long telegramId, string message)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                {
                    message = "Главное меню";
                }

                var option = new OptionMessage();
                option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(1, new List<string>(), true, MessageKeys.GetValueButton(nameof(ReplyKeys.RP_MAIN_MENU)));
                await Common.Message.Send(botClient, telegramId, message, option);
            }
            catch (Exception ex)
            {
                TelegramService.GetInstance().InvokeErrorLog(ex);
            }
        }

    }
}
