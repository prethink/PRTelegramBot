using ConsoleExample.Checkers;
using ConsoleExample.Examples.Events;
using ConsoleExample.Models;
using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Configs;
using PRTelegramBot.Core;
using PRTelegramBot.Models;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Services.Messages;

namespace ConsoleExample.Services
{
    /// <summary>
    /// Initializer for the bot.
    /// </summary>
    public static class Initializer
    {
        /// <summary>
        /// Initializes the events.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public static void InitEvents(PRBotBase bot)
        {
            // Handling an invalid message type
            bot.Events.OnWrongTypeMessage += ExampleEvents.OnWrongTypeMessage;

            // Handling the case where the user sent start with a deeplink
            bot.Events.OnUserStartWithArgs += ExampleEvents.OnUserStartWithArgs;

            // Handling the privilege check
            bot.Events.OnCheckPrivilege += ExampleEvents.OnCheckPrivilege;

            // Handling a command that was not matched
            bot.Events.OnMissingCommand += ExampleEvents.OnMissingCommand;

            // Handling an error raised while a command was running
            bot.Events.OnErrorCommand += ExampleEvents.OnErrorCommand;

            // Handling an invalid chat type
            bot.Events.OnWrongTypeChat += ExampleEvents.OnWrongTypeChat;
        }

        /// <summary>
        /// Initializes the log events.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public static void InitLogEvents(PRBotBase bot)
        {
            // Subscribe to plain logs.
            bot.Events.OnCommonLog += ExampleLogEvents.OnLogCommon;
            // Subscribe to error logs.
            bot.Events.OnErrorLog += ExampleLogEvents.OnLogError;
        }

        /// <summary>
        /// Initializes the events for message-type updates.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public static void InitMessageEvents(PRBotBase bot)
        {
            // Handling locations
            bot.Events.MessageEvents.OnLocationHandle += ExampleMessageEvents.OnLocationHandle;

            // Handling contact data
            bot.Events.MessageEvents.OnContactHandle += ExampleMessageEvents.OnContactHandle;

            // Handling polls
            bot.Events.MessageEvents.OnPollHandle += ExampleMessageEvents.OnPollHandle;

            // Handling WebApps
            bot.Events.MessageEvents.OnWebAppsHandle += ExampleMessageEvents.OnWebAppsHandle;

            // Handling a user who joined the chat from a community
            bot.Events.MessageEvents.OnCommunityChatJoinedHandle += ExampleMessageEvents.OnCommunityChatJoinedHandle;

            // Handling the case where the user is denied access
            bot.Events.OnAccessDenied += ExampleMessageEvents.OnAccessDenied;

            //Handling a message with a document
            bot.Events.MessageEvents.OnDocumentHandle += ExampleMessageEvents.OnDocumentHandle;

            //Handling a message with audio
            bot.Events.MessageEvents.OnAudioHandle += ExampleMessageEvents.OnAudioHandle;

            //Handling a message with a video
            bot.Events.MessageEvents.OnVideoHandle += ExampleMessageEvents.OnVideoHandle;

            //Handling a message with a photo
            bot.Events.MessageEvents.OnPhotoHandle += ExampleMessageEvents.OnPhotoHandle;

            //Handling a message with a sticker
            bot.Events.MessageEvents.OnStickerHandle += ExampleMessageEvents.OnStickerHandle;

            //Handling a message with a voice message
            bot.Events.MessageEvents.OnVoiceHandle += ExampleMessageEvents.OnVoiceHandle;

            //Handling a message of an unknown type
            bot.Events.MessageEvents.OnUnknownHandle += ExampleMessageEvents.OnUnknownHandle;

            //Handling a message with a location
            bot.Events.MessageEvents.OnVenueHandle += ExampleMessageEvents.OnVenueHandle;

            //Handling a message with a game
            bot.Events.MessageEvents.OnGameHandle += ExampleMessageEvents.OnGameHandle;

            //Handling a message with a video note
            bot.Events.MessageEvents.OnVideoNoteHandle += ExampleMessageEvents.OnVideoNoteHandle;

            //Handling a message with a dice
            bot.Events.MessageEvents.OnDiceHandle += ExampleMessageEvents.OnDiceHandle;
        }

        /// <summary>
        /// Initializes the events for the update types.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public static void InitUpdateEvents(PRBotBase bot)
        {
            // Handling before every update 
            bot.Events.UpdateEvents.OnPreUpdate += ExampleUpdateEvents.Handler_OnUpdate;

            // Handling after every update
            bot.Events.UpdateEvents.OnPostUpdate += ExampleUpdateEvents.Handler_OnPostUpdate;

            //Handling an update about a group/chat change
            bot.Events.UpdateEvents.OnMyChatMemberHandle += ExampleUpdateEvents.OnUpdateMyChatMember;

            // Handling a user stopping the generation of a streamed message
            bot.Events.UpdateEvents.OnStoppedMessageGenerationHandle += ExampleUpdateEvents.OnStoppedMessageGeneration;
        }

        /// <summary>
        /// Initializes the new commands.
        /// </summary>
        /// <param name="bot">Bot.</param>
        public static void InitCommands(PRBotBase bot)
        {
            bot.Register.AddInlineCommand(AddCustomTHeader.TestAddCommand, async (context) =>
            {
                await MessageSender.Send(context, "Testing the TestAddCommand method");
            });

            bot.Register.AddInlineCommand(AddCustomTHeader.TestAddCommandTwo, async (context) =>
            {
                await MessageSender.Send(context, "Testing the TestAddCommandTwo method");
            });
        }

        /// <summary>
        /// Gets the list of dynamic commands from the json file.
        /// </summary>
        /// <returns>The commands as key-value pairs.</returns>
        public static Dictionary<string, string> GetDynamicCommands()
        {
            var botJsonProvider = new BotConfigJsonProvider(".\\Configs\\commands.json");
            return botJsonProvider.GetKeysAndValues();
        }

        /// <summary>
        /// Gets the checkers for the commands.
        /// </summary>
        /// <returns>The list of checkers.</returns>
        public static List<InternalChecker> GetCommandChekers()
        {
            var checkerReplyCommand = new InternalChecker(CommandType.Reply, new ReplyExampleChecker());
            var adminChecker = new InternalChecker(new List<CommandType>() { CommandType.Reply, CommandType.NextStep, CommandType.Inline, CommandType.ReplyDynamic, CommandType.Slash }, new AdminExampleChecker());
            return new List<InternalChecker>() { checkerReplyCommand, adminChecker };
        }

        /// <summary>
        /// Gets the list of configuration file paths.
        /// </summary>
        /// <returns>Paths to the files as key-value pairs.</returns>
        public static Dictionary<string, string> GetConfigPaths()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json");
            dictionary.Add(ExampleConstants.MESSAGES_FILE_KEY, ".\\Configs\\messages.json");
            return dictionary;
        }
    }
}
