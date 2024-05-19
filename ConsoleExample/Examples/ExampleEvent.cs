using ConsoleExample.Extension;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Helpers = PRTelegramBot.Helpers;

namespace ConsoleExample.Examples
{
    public static class ExampleEvent
    {
        public static async Task OnDiceHandle(ITelegramBotClient botclient, Update update)
        {
            var dice = update.Message.Dice;
            //Обработка данных
        }

        public static async Task OnVideoNoteHandle(ITelegramBotClient botclient, Update update)
        {
            var videonote = update.Message.VideoNote;
            //Обработка данных
        }

        public static async Task OnGameHandle(ITelegramBotClient botclient, Update update)
        {
            var game = update.Message.Game;
            //Обработка данных
        }

        public static async Task OnVenueHandle(ITelegramBotClient botclient, Update update)
        {
            var venue = update.Message.Venue;
            //Обработка данных
        }

        public static async Task OnUnknownHandle(ITelegramBotClient botclient, Update update)
        {
            //Обработка данных
        }

        public static async Task OnVoiceHandle(ITelegramBotClient botclient, Update update)
        {
            var voice = update.Message.Voice;
            //Обработка данных
        }

        public static async Task OnStickerHandle(ITelegramBotClient botclient, Update update)
        {
            var sticker = update.Message.Sticker;
            //Обработка данных
        }

        public static async Task OnPhotoHandle(ITelegramBotClient botclient, Update update)
        {
            var photo = update.Message.Photo;
            //Обработка данных
        }

        public static async Task OnVideoHandle(ITelegramBotClient botclient, Update update)
        {
            var video = update.Message.Video;
            //Обработка данных
        }

        public static async Task OnAudioHandle(ITelegramBotClient botclient, Update update)
        {
            var audio = update.Message.Audio;
            //Обработка данных
        }

        public static async Task OnDocumentHandle(ITelegramBotClient botclient, Update update)
        {
            var document = update.Message.Document;
            //Обработка данных
        }

        public static async Task OnAccessDenied(ITelegramBotClient botclient, Update update)
        {
            //Обработка данных
        }

        public static async Task OnWebAppsHandle(ITelegramBotClient botclient, Update update)
        {
            var webApp = update.Message.WebAppData;
            //Обработка данных
        }

        public static async Task OnPollHandle(ITelegramBotClient botclient, Update update)
        {
            var poll = update.Message.Poll;
            //Обработка данных
        }

        public static async Task OnContactHandle(ITelegramBotClient botclient, Update update)
        {
            var contact = update.Message.Contact;
            //Обработка данных
        }

        public static async Task OnLocationHandle(ITelegramBotClient botclient, Update update)
        {
            var location = update.Message.Location;
            //Обработка данных
        }

        public static async Task OnWrongTypeChat(ITelegramBotClient botclient, Update update)
        {
            string msg = "Неверный тип чата";
            await Helpers.Message.Send(botclient, update, msg);
        }

        public static async Task OnMissingCommand(ITelegramBotClient botclient, Update update)
        {
            string msg = "Не найдена команда";
            await Helpers.Message.Send(botclient, update, msg);
        }

        /// <summary>
        /// Событие проверки привилегий пользователя
        /// </summary>
        /// <param name="callback">callback функция выполняется в случае успеха</param>
        /// <param name="mask">Маска доступа</param>
        /// Подписка на событие проверки привелегий <see cref="Program"/>
        public static async Task OnCheckPrivilege(ITelegramBotClient botclient, Update update, Func<ITelegramBotClient, Update, Task> callback, int? mask = null)
        {
            if(!mask.HasValue)
            {
                // Нет маски доступа, выполняем метод.
                await callback(botclient, update);
                return;
            }

            // Получаем значение маски требуемого доступа.
            var requiredAccess = mask.Value;

            // Получаем флаги доступа пользователя.
            // Здесь вы на свое усмотрение реализываете логику получение флагов, например можно из базы данных получить.
            var userFlags = update.LoadExampleFlagPrivilege();

            if(requiredAccess.HasFlag<UserPrivilege>(userFlags))
            {
                // Доступ есть, выполняем метод.
                await callback(botclient, update);
                return;
            }

            // Доступа нет.
            string errorMsg = "У вас нет доступа к данной функции";
            await Helpers.Message.Send(botclient, update, errorMsg);
            return;

        }

        public static async Task OnUserStartWithArgs(ITelegramBotClient botclient, Update update, string args)
        {
            string msg = "Пользователь отправил старт с аргументом";
            await Helpers.Message.Send(botclient, update, msg);
        }
        public static async Task OnWrongTypeMessage(ITelegramBotClient botclient, Update update)
        {
            string msg = "Неверный тип сообщения";
            await Helpers.Message.Send(botclient, update, msg);
        }
    }
}
