using Telegram.Bot.Types;
using Helpers = PRTelegramBot.Helpers;
using CallbackId = PRTelegramBot.Models.Enums.THeader;
using PRTelegramBot.Core;
using PRTelegramBot.Models.Enums;
using ConsoleExample.Extension;
using Telegram.Bot;
using PRTelegramBot.Configs;

namespace ConsoleExample.Examples
{
    public static class ExampleEvent
    {
        public static async Task OnDiceHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var dice = update.Message.Dice;
            //Обработка данных
        }

        public static async Task OnVideoNoteHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var videonote = update.Message.VideoNote;
            //Обработка данных
        }

        public static async Task OnGameHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var game = update.Message.Game;
            //Обработка данных
        }

        public static async Task OnVenueHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var venue = update.Message.Venue;
            //Обработка данных
        }

        public static async Task OnUnknownHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            //Обработка данных
        }

        public static async Task OnVoiceHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var voice = update.Message.Voice;
            //Обработка данных
        }

        public static async Task OnStickerHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var sticker = update.Message.Sticker;
            //Обработка данных
        }

        public static async Task OnPhotoHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var photo = update.Message.Photo;
            //Обработка данных
        }

        public static async Task OnVideoHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var video = update.Message.Video;
            //Обработка данных
        }

        public static async Task OnAudioHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var audio = update.Message.Audio;
            //Обработка данных
        }

        public static async Task OnDocumentHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var document = update.Message.Document;
            //Обработка данных
        }

        public static async Task OnAccessDenied(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            //Обработка данных
        }

        public static async Task OnWebAppsHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var webApp = update.Message.WebAppData;
            //Обработка данных
        }

        public static async Task OnPollHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var poll = update.Message.Poll;
            //Обработка данных
        }

        public static async Task OnContactHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var contact = update.Message.Contact;
            //Обработка данных
        }

        public static async Task OnLocationHandle(Telegram.Bot.ITelegramBotClient botclient, Update update)
        {
            var location = update.Message.Location;
            //Обработка данных
        }

        public static async Task OnWrongTypeChat(Telegram.Bot.ITelegramBotClient botclient, Telegram.Bot.Types.Update update)
        {
            string msg = "Неверный тип чата";
            await PRTelegramBot.Helpers.Message.Send(botclient, update, msg);
        }

        public static async Task OnMissingCommand(Telegram.Bot.ITelegramBotClient botclient, Telegram.Bot.Types.Update update)
        {
            string msg = "Не найдена команда";
            await PRTelegramBot.Helpers.Message.Send(botclient, update, msg);
        }

        /// <summary>
        /// Событие проверки привилегий пользователя
        /// </summary>
        /// <param name="callback">callback функция выполняется в случае успеха</param>
        /// <param name="flags">Флаги которые должны присуствовать</param>
        public static async Task OnCheckPrivilege(Telegram.Bot.ITelegramBotClient botclient, Telegram.Bot.Types.Update update, Func<ITelegramBotClient, Update, Task> callback, int? flags = null)
        {
            if(flags != null)
            { 
                var flag = flags.Value;
                //Проверяем флаги через int
                if(update.GetIntPrivilege().Contains(flag))
                {
                    await callback(botclient, update);
                    return;
                }

                //Проверяем флаги через enum UserPrivilage
                if (((UserPrivilege)flag).HasFlag(update.GetFlagPrivilege()))
                {
                    await callback(botclient, update);
                    return;
                }

                string errorMsg = "У вас нет доступа к данной функции";
                await PRTelegramBot.Helpers.Message.Send(botclient, update, errorMsg);
                return;
            }
            string msg = "Проверка привилегий";
            await PRTelegramBot.Helpers.Message.Send(botclient, update, msg);
        }

        public static async Task OnUserStartWithArgs(Telegram.Bot.ITelegramBotClient botclient, Telegram.Bot.Types.Update update, string args)
        {
            string msg = "Пользователь отправил старт с аргументом";
            await PRTelegramBot.Helpers.Message.Send(botclient, update, msg);
        }
        public static async Task OnWrongTypeMessage(Telegram.Bot.ITelegramBotClient botclient, Telegram.Bot.Types.Update update)
        {
            string msg = "Неверный тип сообщения";
            await PRTelegramBot.Helpers.Message.Send(botclient, update, msg);
        }
    }
}
