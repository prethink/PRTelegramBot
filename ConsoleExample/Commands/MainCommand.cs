using ConsoleExample.Models;
using PRTelegramBot.Attributes;
using PRTelegramBot.Configs;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PRTelegramBot.Commands
{
    internal class MainCommand
    {
        [ReplyMenuDynamicHandler("MENU", "MAIN_MENU")]
        [RequiredTypeChat(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task MainMenu(ITelegramBotClient botClient, Update update)
        {
            try
            {
                await MainMenu(botClient,  update.GetChatId(), botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.MESSAGES_FILE_KEY, "MSG_MAIN_MENU"));
            }
            catch(Exception ex)
            {
                //Обработка исключения
            }
        }

        public static async Task MainMenu(ITelegramBotClient botClient, long telegramId)
        {
            try
            {
                await MainMenu(botClient, telegramId, botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.MESSAGES_FILE_KEY, "MSG_MAIN_MENU"));
            }
            catch (Exception ex)
            {
                //Обработка исключения
            }
        }

        [RequiredTypeChat(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task MainMenu(ITelegramBotClient botClient, long telegramId, string message)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                {
                    message = "Главное меню";
                }

                var option = new OptionMessage();
                option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(1, new List<string>(), true, botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "RP_MAIN_MENU"));
                await Helpers.Message.Send(botClient, telegramId, message, option);
            }
            catch (Exception ex)
            {
                //Обработка исключения
            }
        }

    }
}
