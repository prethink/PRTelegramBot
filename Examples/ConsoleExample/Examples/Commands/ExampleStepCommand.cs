using ConsoleExample.Models;
using ConsoleExample.Models.CommandHeaders;
using PRTelegramBot.Attributes;
using PRTelegramBot.Configs;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Services.Messages;
using PRTelegramBot.Utils;

namespace ConsoleExample.Examples.Commands
{
    /// <summary>
    /// Example of step-by-step command execution
    /// </summary>
    public class ExampleStepCommand
    {
        /// <summary>
        /// Send "stepstart" in the chat
        /// Registers the user's next step
        /// </summary>
        [ReplyMenuHandler("stepstart")]
        public static async Task StepStart(IBotContext context)
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
        public static async Task StepOne(IBotContext context)
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
        public static async Task StepTwo(IBotContext context)
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
            option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(1, new List<string>(), true, context.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "RP_MAIN_MENU"));
            await MessageSender.Send(context, msg, option);
        }


        /// <summary>
        /// Send any text to the chat and this command will run if a next step was registered for the user
        /// </summary>
        public static async Task StepThree(IBotContext context)
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


        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.InlineWithStep)]
        public static async Task InlineStepp(IBotContext context)
        {
            try
            {
                //Try to convert the callback data to the required type
                var command = context.GetCommandByCallbackOrNull();
                if (command != null)
                {
                    string msg = "The next step has been registered, type something";
                    await MessageSender.Send(context, msg);
                    context.Update.RegisterStepHandler(new StepTelegram(InlineStep, new StepCache()));
                }
            }
            catch (Exception ex)
            {
                //Exception handling
            }
        }

        public static async Task InlineStep(IBotContext context)
        {
            string msg = $"You entered: {context.Update.Message.Text}";
            //Get the current handler
            var handler = context.Update.GetStepHandler<StepTelegram>();
            //Store the user name in the cache 
            handler!.GetCache<StepCache>().Name = context.Update.Message.Text;
            //Register the next step with a maximum wait of 5 minutes from the moment of registration
            context.Update.ClearStepUserHandler();
            await MessageSender.Send(context, msg);
            await ExampleCalendar.PickCalendar(context);
        }
    }
}
