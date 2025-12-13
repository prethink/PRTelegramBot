using PRTelegramBot.Core.BotScope;
using PRTelegramBot.Models;
using PRTelegramBot.Models.EventsArgs;

namespace ConsoleExample.Examples.Events
{
    public static class ExampleMessageEvents
    {
        public static async Task OnDiceHandle(BotEventArgs e)
        {
            var dice = e.Context.Update.Message.Dice;
            //Обработка данных
        }

        public static async Task OnVideoNoteHandle(BotEventArgs e)
        {
            var videonote = e.Context.Update.Message.VideoNote;
            //Обработка данных
        }

        public static async Task OnGameHandle(BotEventArgs e)
        {
            var game = e.Context.Update.Message.Game;
            //Обработка данных
        }

        public static async Task OnVenueHandle(BotEventArgs e)
        {
            var venue = e.Context.Update.Message.Venue;
            //Обработка данных
        }

        public static async Task OnUnknownHandle(BotEventArgs e)
        {
            //Обработка данных
        }

        public static async Task OnVoiceHandle(BotEventArgs e)
        {
            var voice = e.Context.Update.Message.Voice;
            //Обработка данных
        }

        public static async Task OnStickerHandle(BotEventArgs e)
        {
            var sticker = e.Context.Update.Message.Sticker;
            //Обработка данных
        }

        public static async Task OnPhotoHandle(BotEventArgs e)
        {
            var photo = e.Context.Update.Message.Photo;
            //Обработка данных
        }

        public static async Task OnVideoHandle(BotEventArgs e)
        {
            var video = e.Context.Update.Message.Video;
            //Обработка данных
        }

        public static async Task OnAudioHandle(BotEventArgs e)
        {
            var audio = e.Context.Update.Message.Audio;
            //Обработка данных
        }

        public static async Task OnDocumentHandle(BotEventArgs e)
        {
            var document = e.Context.Update.Message.Document;
            //Обработка данных
        }

        public static async Task OnAccessDenied(BotEventArgs e)
        {
            //Обработка данных
        }

        public static async Task OnWebAppsHandle(BotEventArgs e)
        {
            var webApp = e.Context.Update.Message.WebAppData;
            //Обработка данных
        }

        public static async Task OnPollHandle(BotEventArgs e)
        {
            var poll = e.Context.Update.Message.Poll;
            //Обработка данных
        }

        public static async Task OnContactHandle(BotEventArgs e)
        {
            await Task.Delay(5000);
            var contact = e.Context.Update.Message.Contact;
            var bot = CurrentScope.Bot;
            var context = CurrentScope.Context;
            //Обработка данных
        }

        public static async Task OnLocationHandle(BotEventArgs e)
        {
            var location = e.Context.Update.Message.Location;
            //Обработка данных
        }
    }
}
