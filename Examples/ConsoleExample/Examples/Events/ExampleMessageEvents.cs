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
            //Data handling
        }

        public static async Task OnVideoNoteHandle(BotEventArgs e)
        {
            var videonote = e.Context.Update.Message.VideoNote;
            //Data handling
        }

        public static async Task OnGameHandle(BotEventArgs e)
        {
            var game = e.Context.Update.Message.Game;
            //Data handling
        }

        public static async Task OnVenueHandle(BotEventArgs e)
        {
            var venue = e.Context.Update.Message.Venue;
            //Data handling
        }

        public static async Task OnUnknownHandle(BotEventArgs e)
        {
            //Data handling
        }

        public static async Task OnVoiceHandle(BotEventArgs e)
        {
            var voice = e.Context.Update.Message.Voice;
            //Data handling
        }

        public static async Task OnStickerHandle(BotEventArgs e)
        {
            var sticker = e.Context.Update.Message.Sticker;
            //Data handling
        }

        public static async Task OnPhotoHandle(BotEventArgs e)
        {
            var photo = e.Context.Update.Message.Photo;
            //Data handling
        }

        public static async Task OnVideoHandle(BotEventArgs e)
        {
            var video = e.Context.Update.Message.Video;
            //Data handling
        }

        public static async Task OnAudioHandle(BotEventArgs e)
        {
            var audio = e.Context.Update.Message.Audio;
            //Data handling
        }

        public static async Task OnDocumentHandle(BotEventArgs e)
        {
            var document = e.Context.Update.Message.Document;
            //Data handling
        }

        public static async Task OnAccessDenied(BotEventArgs e)
        {
            //Data handling
        }

        public static async Task OnWebAppsHandle(BotEventArgs e)
        {
            var webApp = e.Context.Update.Message.WebAppData;
            //Data handling
        }

        public static async Task OnPollHandle(BotEventArgs e)
        {
            var poll = e.Context.Update.Message.Poll;
            //Data handling
        }

        public static async Task OnContactHandle(BotEventArgs e)
        {
            await Task.Delay(5000);
            var contact = e.Context.Update.Message.Contact;
            var bot = CurrentScope.Bot;
            var context = CurrentScope.Context;
            //Data handling
        }

        public static async Task OnLocationHandle(BotEventArgs e)
        {
            var location = e.Context.Update.Message.Location;
            //Data handling
        }

        /// <summary>
        /// Bot API 10.3. A user joined this chat through a community it belongs to.
        /// </summary>
        public static async Task OnCommunityChatJoinedHandle(BotEventArgs e)
        {
            var community = e.Context.Update.Message.CommunityChatJoined?.Community;
            //Data handling: greet them, or record which community they came from
        }
    }
}
