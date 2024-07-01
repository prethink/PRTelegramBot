using ConsoleExample.Extension;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.EventsArgs;
using Helpers = PRTelegramBot.Helpers;

namespace ConsoleExample.Examples
{
    public static class ExampleEvent
    {
        public static async Task OnDiceHandle(BotEventArgs e)
        {
            var dice = e.Update.Message.Dice;
            //Обработка данных
        }

        public static async Task OnVideoNoteHandle(BotEventArgs e)
        {
            var videonote = e.Update.Message.VideoNote;
            //Обработка данных
        }

        public static async Task OnGameHandle(BotEventArgs e)
        {
            var game = e.Update.Message.Game;
            //Обработка данных
        }

        public static async Task OnVenueHandle(BotEventArgs e)
        {
            var venue = e.Update.Message.Venue;
            //Обработка данных
        }

        public static async Task OnUnknownHandle(BotEventArgs e)
        {
            //Обработка данных
        }

        public static async Task OnVoiceHandle(BotEventArgs e)
        {
            var voice = e.Update.Message.Voice;
            //Обработка данных
        }

        public static async Task OnStickerHandle(BotEventArgs e)
        {
            var sticker = e.Update.Message.Sticker;
            //Обработка данных
        }

        public static async Task OnPhotoHandle(BotEventArgs e)
        {
            var photo = e.Update.Message.Photo;
            //Обработка данных
        }

        public static async Task OnVideoHandle(BotEventArgs e)
        {
            var video = e.Update.Message.Video;
            //Обработка данных
        }

        public static async Task OnAudioHandle(BotEventArgs e)
        {
            var audio = e.Update.Message.Audio;
            //Обработка данных
        }

        public static async Task OnDocumentHandle(BotEventArgs e)
        {
            var document = e.Update.Message.Document;
            //Обработка данных
        }

        public static async Task OnAccessDenied(BotEventArgs e)
        {
            //Обработка данных
        }

        public static async Task OnWebAppsHandle(BotEventArgs e)
        {
            var webApp = e.Update.Message.WebAppData;
            //Обработка данных
        }

        public static async Task OnPollHandle(BotEventArgs e)
        {
            var poll = e.Update.Message.Poll;
            //Обработка данных
        }

        public static async Task OnContactHandle(BotEventArgs e)
        {
            var contact = e.Update.Message.Contact;
            //Обработка данных
        }

        public static async Task OnLocationHandle(BotEventArgs e)
        {
            var location = e.Update.Message.Location;
            //Обработка данных
        }

        public static async Task OnWrongTypeChat(BotEventArgs e)
        {
            string msg = "Неверный тип чата";
            await Helpers.Message.Send(e.BotClient, e.Update, msg);
        }

        public static async Task OnMissingCommand(BotEventArgs args)
        {
            string msg = "Не найдена команда";
            await Helpers.Message.Send(args.BotClient, args.Update, msg);
        }

        /// <summary>
        /// Событие проверки привилегий пользователя
        /// </summary>
        /// <param name="callback">callback функция выполняется в случае успеха</param>
        /// <param name="mask">Маска доступа</param>
        /// Подписка на событие проверки привелегий <see cref="Program"/>
        public static async Task OnCheckPrivilege(PrivilegeEventArgs e)
        {
            if(!e.Mask.HasValue)
            {
                // Нет маски доступа, выполняем метод.
                await e.ExecuteMethod(e.BotClient, e.Update);
                return;
            }

            // Получаем значение маски требуемого доступа.
            var requiredAccess = e.Mask.Value;

            // Получаем флаги доступа пользователя.
            // Здесь вы на свое усмотрение реализываете логику получение флагов, например можно из базы данных получить.
            var userFlags = e.Update.LoadExampleFlagPrivilege();

            if(requiredAccess.HasFlag(userFlags))
            {
                // Доступ есть, выполняем метод.
                await e.ExecuteMethod(e.BotClient, e.Update);
                return;
            }

            // Доступа нет.
            string errorMsg = "У вас нет доступа к данной функции";
            await Helpers.Message.Send(e.BotClient, e.Update, errorMsg);
            return;

        }

        public static async Task OnUserStartWithArgs(StartEventArgs args)
        {
            string msg = "Пользователь отправил старт с аргументом";
            await Helpers.Message.Send(args.BotClient, args.Update, msg);
        }
        public static async Task OnWrongTypeMessage(BotEventArgs e)
        {
            string msg = "Неверный тип сообщения";
            await Helpers.Message.Send(e.BotClient, e.Update, msg);
        }

        #region Update events

        public static async Task OnUpdateMyChatMember(BotEventArgs args)
        {
            //Обработка информации из myChatHandle
            var myChatHandle = args.Update.MyChatMember;
            try
            {
                if (myChatHandle.NewChatMember.Status == Telegram.Bot.Types.Enums.ChatMemberStatus.Member)
                {
                    if (myChatHandle.NewChatMember.User.Id == args.BotClient.BotId)
                    {
                        await Helpers.Message.Send(args.BotClient, myChatHandle.Chat.Id, "Hello world");
                    }
                    else
                    {
                        //Другие персонажи
                    }
                }
            }
            catch (Exception ex)
            {
                args.Bot.Events.OnErrorLogInvoke(ex);
            }
        }

        public async static Task<UpdateResult> Handler_OnUpdate(BotEventArgs e)
        {
            /*
             Для примера можно рассмотреть зарегистрирован ли пользователь или нет.
                Если зарегистрирован
                    return UpdateResult.Continue; - данный результат позволит продолжить обработку.
                Если не зарегистрирован то вызвать метод регистрации
                    RegisterMethod();
                    return UpdateResult.Stop или return UpdateResult.Handled - позволит прервать текущую обработку и отправить пользователя на регистрацию
             */
            return UpdateResult.Continue;
        }

        public async static Task Handler_OnPostUpdate(BotEventArgs e)
        {
            // Пример. Регистрация последней активности пользователя в боте. Допустим дата и время
        }

        #endregion
    }
}
