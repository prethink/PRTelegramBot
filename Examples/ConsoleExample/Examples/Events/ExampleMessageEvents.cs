using PRTelegramBot.Models.EventsArgs;

namespace ConsoleExample.Examples.Events
{
    public static class ExampleMessageEvents
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
    }
}
