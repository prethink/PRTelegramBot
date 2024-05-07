using PRTelegramBot.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
using PRTelegramBot.Extensions;
using PRTelegramBot.Core;
using PRTelegramBot.Commands.Constants;
using ConsoleExample.Models;
using PRTelegramBot.Models;

namespace PRTelegramBot.Commands
{
    public class StartCommand
    {
        /// <summary>
        /// Обработка команды старт
        /// </summary>
        [ReplyMenuDynamicHandler(nameof(ReplyKeys.RP_START))]
        [RequiredTypeChat(Telegram.Bot.Types.Enums.ChatType.Private)]
        [SlashHandler("/start")]
        public static async Task Start(ITelegramBotClient botClient, Update update)
        {
            await CheckRegister(botClient, update, true);
        }

        /// <summary>
        /// Обработка команды старт с аргументом
        /// </summary>
        [RequiredTypeChat(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task StartWithArguments(ITelegramBotClient botClient, Update update, string arg)
        {
            try
            {
                if (!string.IsNullOrEmpty(arg))
                {
                    await CheckRegister(botClient, update, true, arg);
                }
                else
                {
                    await CheckRegister(botClient, update, true);
                } 
            }
            catch(Exception ex)
            {
                //Обработка исключения
            }

        }

        /// <summary>
        /// Обработка регистрации нового пользователя
        /// </summary>
        /// <param name="showMsg">Показывать сообщение</param>
        /// <param name="refferId">Реф ссылка есть есть</param>
        [RequiredTypeChat(Telegram.Bot.Types.Enums.ChatType.Private)]
        public static async Task CheckRegister(ITelegramBotClient botClient, Update update, bool showMsg, string refferId = null)
        {
            try
            {
                if(update.Message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Private)
                {
                    //TODO:Обработка регистрации нового пользователя
                }
                else
                {
                    string msgUser = $"Регистрация группы или другого объекта {update.GetCacheData<UserCache>()}";
                    //TelegramService.GetInstance().InvokeCommonLog(msgUser, TelegramService.TelegramEvents.GroupAction, ConsoleColor.White);
                }  
            }
            catch(Exception ex)
            {
                //Обработка исключения
            }
        }
    }
}
