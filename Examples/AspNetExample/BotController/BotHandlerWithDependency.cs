using AspNetExample.Models;
using AspNetExample.Services;
using PRTelegramBot.Attributes;
using PRTelegramBot.Builders.Keyboard;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;
using Telegram.Bot;
using TestDI.Models;

namespace AspNetExample.BotController
{
    [BotHandler]
    public class BotHandlerWithDependency
    {
        private readonly ILogger<BotHandlerWithDependency> _logger;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ServiceTransient serviceTransient;
        private readonly AppDbContext db;

        public BotHandlerWithDependency(ServiceScoped serviceScoped, ServiceTransient serviceTransient, ServiceSingleton serviceSingleton, ILogger<BotHandlerWithDependency> logger, AppDbContext db)
        {
            this.serviceScoped = serviceScoped;
            this.serviceTransient = serviceTransient;
            this.serviceSingleton = serviceSingleton;
            this.db = db;
            _logger = logger;
        }

        [ReplyMenuHandler("Test")]
        public async Task TestMethodWithDependency(IBotContext context)
        {
            var users = db.Users.ToList();
            await MessageSender.Send(context, $"{nameof(TestMethodWithDependency)} {_logger != null}");
        }

        [SlashHandler("/test")]
        public async Task Slash(IBotContext context)
        {
            await MessageSender.Send(context, nameof(Slash));
        }

        [ReplyMenuHandler("inline")]
        public async Task InlineTest(IBotContext context)
        {
            var options = new OptionMessage();
            var exampleItemThree = new InlineCallback<EntityTCommand<string>>("Example with a long text", CustomTHeaderTwo.ExampleThree, new EntityTCommand<string>("There is no doubt that relationship diagrams will be declared a violation of universal ethical and moral standards. There is a debatable point of view stating roughly the following: the key features of the project structure, initiated purely synthetically, have been verified in a timely manner. The significance of these problems is so obvious that a high-technology concept of the social order gives a wide circle of specialists a part in shaping a rethinking of foreign economic policies. Thus, a high-technology concept of the social order plays an important role in shaping experiments that are striking in their scale and grandeur. Cartel agreements do not allow a situation in which thorough studies of competitors, overcoming the difficult economic situation that has developed, are blocked within the bounds of their own rational constraints. Each of us understands the obvious thing: the implementation of the planned targets reveals an urgent need for both self-sufficient and externally dependent conceptual solutions. Equally, the conviction of some opponents unambiguously defines every participant as capable of making their own decisions regarding the highest-priority requirements. Everyday practice shows that the implementation of the planned targets ensures the relevance of the distribution of internal reserves and resources. In their striving to improve the quality of life, they forget that the basic vector of development ensures the relevance of the tasks set by society."));
            var menu = new InlineKeyboardBuilder().AddButton(exampleItemThree).Build();
            options.MenuInlineKeyboardMarkup = menu;
            await MessageSender.Send(context, nameof(InlineTest), options);
        }

        [ReplyMenuHandler("inlinestatic")]
        public async Task StaticInlineTest(IBotContext context)
        {
            var options = new OptionMessage();
            var menuItemns = MenuGenerator.InlineButtons(1, new List<IInlineContent> {
                new InlineCallback("Test", PRTelegramBotCommand.CurrentPage),
                new InlineCallback("TestStatic", PRTelegramBotCommand.NextPage)
            });
            options.MenuInlineKeyboardMarkup = MenuGenerator.InlineKeyboard(menuItemns);
            await MessageSender.Send(context, nameof(StaticInlineTest), options);
        }

        [InlineCallbackHandler<PRTelegramBotCommand>(PRTelegramBotCommand.CurrentPage)]
        public async Task InlineHandler(IBotContext context)
        {
            await MessageSender.Send(context, nameof(InlineHandler));
        }

        [InlineCallbackHandler<PRTelegramBotCommand>(PRTelegramBotCommand.NextPage)]
        public async static Task InlineHandlerStatic(IBotContext context)
        {
            await MessageSender.Send(context, nameof(InlineHandlerStatic));
        }

        /// <summary>
        /// callback handling
        /// This method can handle several entry points
        /// </summary>
        [InlineCallbackHandler<CustomTHeaderTwo>(CustomTHeaderTwo.ExampleThree)]
        public static async Task InlineThree(IBotContext context)
        {
            try
            {
                //Try to convert the callback data to the required type
                var command = context.GetCommandByCallbackOrNull<EntityTCommand<string>>();
                if (command != null)
                {
                    string msg = $"The identifier you passed: {command.Data.EntityId}";
                    if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Edit)
                    {
                        await MessageEditor.Edit(context, msg);
                    }
                    else
                    {
                        if (command.Data.GetActionWithLastMessage() == ActionWithLastMessage.Delete)
                        {
                            await context.BotClient.DeleteMessage(context.Update.GetChatIdClass(), context.Update.CallbackQuery.Message.MessageId);
                        }
                        await MessageSender.Send(context, msg);
                    }
                }
            }
            catch (Exception ex)
            {
                //Exception handling
            }
        }

        /// <summary>
        /// Send "stepstart" in the chat
        /// Registers the user's next step
        /// </summary>
        [ReplyMenuHandler("stepstart")]
        public async Task StepStart(IBotContext context)
        {
            string msg = "Testing the step-by-step execution feature\nEnter your name";
            //Register a handler for sequential step processing and data storage
            context.Update.RegisterStepHandler(new StepTelegram(StepOne, new StepCache()));
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// This method runs for the user on any text message or any reply button press.
        /// Registers the next step with a maximum execution time
        /// </summary>
        public async Task StepOne(IBotContext context)
        {
            string msg = $"Step 1 - Your name: {context.Update.Message.Text}" +
                        $"\nEnter your date of birth";
            //Get the current handler
            var handler = context.Update.GetStepHandler<StepTelegram>();
            //Store the user name in the cache 
            handler!.GetCache<StepCache>().Name = context.Update.Message.Text;
            //Register the next step with a maximum wait of 5 minutes from the moment of registration
            handler.RegisterNextStep(StepTwo);
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// Send any text to the chat and this command will run if a next step was registered for the user
        /// </summary>
        public async Task StepTwo(IBotContext context)
        {
            string msg = $"Step 2 - Date of birth: {context.Update.Message.Text}" +
                         $"\nType any text to see the result";
            //Get the current handler
            var handler = context.Update.GetStepHandler<StepTelegram>();
            //Store the date of birth
            handler!.GetCache<StepCache>().BirthDay = context.Update.Message.Text;
            //Register the next step with a maximum wait of 5 minutes from the moment of registration
            handler.RegisterNextStep(StepThree, DateTime.Now.AddMinutes(1));
            //Options for the message
            var option = new OptionMessage();
            //Add an empty reply menu with a "Main menu" button
            //This method is a priority one: if the user presses this button, the main menu method runs instead of the next step.
            //option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(1, new List<string>(), true, botClient.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "RP_MAIN_MENU"));
            await MessageSender.Send(context, msg, option);
        }


        /// <summary>
        /// Send any text to the chat and this command will run if a next step was registered for the user
        /// </summary>
        public async Task StepThree(IBotContext context)
        {
            //Get the current handler
            var handler = context.Update.GetStepHandler<StepTelegram>();
            //Get the current cache
            var cache = handler!.GetCache<StepCache>(); ;
            string msg = $"Step 3 - Result: Name: {cache.Name}, date of birth: {cache.BirthDay}" +
                         $"\nThe step sequence has been cleared.";
            //Last step
            handler.LastStepExecuted = true;
            await MessageSender.Send(context, msg);
        }

        /// <summary>
        /// If a next step exists, it is ignored while this command runs
        /// Because the first argument of ReplyMenuHandler is set to true, which marks the command as a priority command
        /// </summary>
        [ReplyMenuHandler("ignorestep")]
        public static async Task IngoreStep(IBotContext context)
        {
            string msg = context.Update.HasStepHandler()
                ? "The next step was ignored"
                : "There was no next step";

            await MessageSender.Send(context, msg);
        }
    }
}
