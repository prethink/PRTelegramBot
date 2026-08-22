using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Class that makes generating menus convenient.
    /// </summary>
    public static class MenuGenerator
    {
        #region Reply buttons and menus
        /// <summary>
        /// Generates a reply menu for the bot.
        /// </summary>
        /// <param name="maxColumn">Maximum number of columns.</param>
        /// <param name="menu">Collection of menus.</param>
        /// <param name="resizeKeyboard">Resizes the keyboard vertically.</param>
        /// <param name="mainMenu">If not empty, adds an item at the very end of the menu.</param>
        /// <param name="OneTimeKeyboard">Indicates that the keyboard will be hidden after a button is pressed.</param>
        /// <returns>The generated menu</returns>
        public static ReplyKeyboardMarkup ReplyKeyboard(int maxColumn, List<string> menu, bool resizeKeyboard = true, string mainMenu = "", bool OneTimeKeyboard = false)
        {
            var buttons = ReplyButtons(maxColumn, menu, mainMenu);
            return ReplyKeyboard(buttons, resizeKeyboard, string.Empty, OneTimeKeyboard);
        }

        /// <summary>
        /// Generates a reply menu for the bot.
        /// </summary>
        /// <param name="maxColumn">Maximum number of columns.</param>
        /// <param name="keyboardButtons">Collection of buttons.</param>
        /// <param name="resizeKeyboard">Resizes the keyboard vertically.</param>
        /// <param name="mainMenu">If not empty, adds the main menu.</param>
        /// <param name="OneTimeKeyboard">Indicates that the keyboard will be hidden after a button is pressed.</param>
        /// <returns>The generated menu</returns>
        public static ReplyKeyboardMarkup ReplyKeyboard(int maxColumn, List<KeyboardButton> keyboardButtons, bool resizeKeyboard = true, string mainMenu = "", bool OneTimeKeyboard = false)
        {
            var buttons = ReplyButtons(maxColumn, keyboardButtons, mainMenu);
            return ReplyKeyboard(buttons, resizeKeyboard, string.Empty, OneTimeKeyboard);
        }

        /// <summary>
        /// Generates a reply menu for the bot.
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="resizeKeyboard">Resizes the keyboard vertically.</param>
        /// <param name="mainMenu">If not empty, adds the main menu.</param>
        /// <param name="OneTimeKeyboard">Indicates that the keyboard will be hidden after a button is pressed.</param>
        /// <returns>The generated menu</returns>
        public static ReplyKeyboardMarkup ReplyKeyboard(List<List<KeyboardButton>> buttons, bool resizeKeyboard = true, string mainMenu = "", bool OneTimeKeyboard = false)
        {
            if (!string.IsNullOrEmpty(mainMenu))
            {
                var count = buttons.Count;
                buttons.Add(new List<KeyboardButton>());
                buttons[count].Add(mainMenu);

            }
            ReplyKeyboardMarkup replyKeyboardMarkup = new(buttons)
            {
                ResizeKeyboard = resizeKeyboard
            };
            replyKeyboardMarkup.OneTimeKeyboard = OneTimeKeyboard;
            return replyKeyboardMarkup;
        }

        /// <summary>
        /// Generates reply buttons for the bot.
        /// </summary>
        /// <param name="maxColumn">Maximum number of columns.</param>
        /// <param name="menu">Menu.</param>
        /// <param name="mainMenu">If not empty, adds the main menu.</param>
        /// <returns>Collection of buttons.</returns>
        public static List<List<KeyboardButton>> ReplyButtons(int maxColumn, List<string> menu, string mainMenu = "")
        {
            List<KeyboardButton> buttons = new();
            foreach (var item in menu)
            {
                buttons.Add(new KeyboardButton(item));
            }
            return ReplyButtons(maxColumn, buttons, mainMenu);
        }

        /// <summary>
        /// Generates reply buttons for the bot.
        /// </summary>
        /// <param name="maxColumn">Maximum number of columns.</param>
        /// <param name="buttons">Buttons.</param>
        /// <param name="mainMenu">If not empty, adds the main menu.</param>
        /// <returns>Collection of buttons.</returns>
        public static List<List<KeyboardButton>> ReplyButtons(int maxColumn, List<KeyboardButton> buttons, string mainMenu = "")
        {
            List<List<KeyboardButton>> generateButtons = new();

            int row = 0;
            int currentElement = 0;

            foreach (var item in buttons)
            {
                if (currentElement == 0)
                {
                    generateButtons.Add(new List<KeyboardButton>());
                    generateButtons[row].Add(item);
                }
                else
                {
                    generateButtons[row].Add(item);
                }

                currentElement++;

                if (currentElement >= maxColumn)
                {
                    currentElement = 0;
                    row++;
                }
            }

            if (!string.IsNullOrWhiteSpace(mainMenu))
            {
                generateButtons.Add(new List<KeyboardButton>());
                if (currentElement != 0)
                    row++;
                generateButtons[row].Add(mainMenu);
            }

            return generateButtons;
        }

        /// <summary>
        /// Merges the bot's reply buttons.
        /// </summary>
        /// <param name="buttonsOne">The first list of buttons.</param>
        /// <param name="buttonsTwo">The second list of buttons.</param>
        /// <returns>Collection of buttons.</returns>
        public static List<List<KeyboardButton>> ReplyButtons(List<List<KeyboardButton>> buttonsOne, List<List<KeyboardButton>> buttonsTwo)
        {
            buttonsOne.AddRange(buttonsTwo);
            return buttonsOne;
        }
        #endregion

        #region Inline buttons and menus
        /// <summary>
        /// Creates an inline menu for the bot.
        /// </summary>
        /// <param name="buttons">Collection of buttons.</param>
        /// <returns> An inline menu for the bot.</returns>
        public static InlineKeyboardMarkup InlineKeyboard(List<List<InlineKeyboardButton>> buttons)
        {
            InlineKeyboardMarkup Keyboard = new(buttons);
            return Keyboard;
        }

        /// <summary>
        /// Creates an inline menu for the bot.
        /// </summary>
        /// <param name="maxColumn">Maximum number of columns.</param>
        /// <param name="menu">Collection of buttons.</param>
        /// <returns>An inline menu for the bot.</returns>
        public static InlineKeyboardMarkup InlineKeyboard(int maxColumn, List<IInlineContent> menu)
        {
            var buttons = InlineButtons(maxColumn, menu);
            return InlineKeyboard(buttons);
        }

        /// <summary>
        /// Creates a collection of inline buttons.
        /// </summary>
        /// <param name="maxColumn">Maximum number of columns.</param>
        /// <param name="menu">Collection of menus.</param>
        /// <returns>Collection of buttons.</returns>
        public static List<List<InlineKeyboardButton>> InlineButtons(int maxColumn, List<IInlineContent> menu)
        {
            List<List<InlineKeyboardButton>> buttons = new();

            int row = 0;
            int currentElement = 0;

            foreach (var item in menu)
            {
                if (currentElement == 0)
                {
                    buttons.Add(new List<InlineKeyboardButton>());
                    buttons[row].Add(InlineUtils.GetInlineButton(item));
                }
                else
                {
                    buttons[row].Add(InlineUtils.GetInlineButton(item));
                }

                currentElement++;

                if (currentElement >= maxColumn)
                {
                    currentElement = 0;
                    row++;
                }
            }

            return buttons;
        }

        /// <summary>
        /// Generates a menu for paginated output.
        /// </summary>
        /// <param name="currentPage">Current page.</param>
        /// <param name="pageCount">Total number of pages.</param>
        /// <param name="nextPageMarker">The nextpage marker.</param>
        /// <param name="previousPageMarker">The prevpage marker.</param>
        /// <param name="currentPageMarker">The currentPage marker.</param>
        /// <param name="addMenu">An additional menu the data has to be merged with.</param>
        /// <returns>Paginated inline menu.</returns>
        public static InlineKeyboardMarkup GetPageMenu(
            int currentPage,
            int pageCount, 
            InlineKeyboardMarkup addMenu, 
            Enum enumToInt, 
            string nextPageMarker = "➡️", 
            string previousPageMarker = "⬅️", 
            string currentPageMarker = "")
        {
            var pageMenu = GetPageMenu(enumToInt, currentPage, pageCount, nextPageMarker, previousPageMarker, currentPageMarker);
            return InlineUtils.UnitInlineKeyboard(addMenu, pageMenu);
        }

        /// <summary>
        /// Generates a menu for paginated output.
        /// </summary>
        /// <param name="currentPage">Current page.</param>
        /// <param name="pageCount">Total number of pages.</param>
        /// <param name="nextPageMarker">The nextpage marker.</param>
        /// <param name="previousPageMarker">The prevpage marker.</param>
        /// <param name="button">The handler button placed in the center.</param>
        /// <param name="addMenu">An additional menu the data has to be merged with.</param>
        /// <returns>Paginated inline menu.</returns>
        public static InlineKeyboardMarkup GetPageMenu(
            int currentPage, 
            int pageCount, 
            InlineKeyboardMarkup addMenu, 
            Enum enumToInt, 
            string nextPageMarker = "➡️", 
            string previousPageMarker = "⬅️", 
            IInlineContent button = null)
        {
            var pageMenu = GetPageMenu(currentPage, pageCount, enumToInt, nextPageMarker, previousPageMarker, button);
            return InlineUtils.UnitInlineKeyboard(addMenu, pageMenu);
        }

        /// <summary>
        /// Generates a menu for paginated output.
        /// </summary>
        /// <param name="currentPage">Current page.</param>
        /// <param name="pageCount">Total number of pages.</param>
        /// <param name="nextPageMarker">The nextpage marker.</param>
        /// <param name="previousPageMarker">The prevpage marker.</param>
        /// <param name="currentPageMarker">The currentPage marker.</param>
        /// <returns>Paginated inline menu.</returns>
        public static InlineKeyboardMarkup GetPageMenu(
            Enum enumToInt, 
            int currentPage, 
            int pageCount, 
            string nextPageMarker = "➡️", 
            string previousPageMarker = "⬅️", 
            string currentPageMarker = "")
        {
            IInlineContent button = null;
            if (!string.IsNullOrEmpty(currentPageMarker))
                button = new InlineCallback<PageTCommand>($"{currentPageMarker}({pageCount - currentPage})", PRTelegramBotCommand.NextPage, new PageTCommand(currentPage, enumToInt));

            return GetPageMenu(currentPage, pageCount, enumToInt, nextPageMarker, previousPageMarker, button);
        }

        /// <summary>
        /// Generates a menu for paginated output.
        /// </summary>
        /// <param name="currentPage">Current page.</param>
        /// <param name="pageCount">Total number of pages.</param>
        /// <param name="nextPageMarker">The nextpage marker.</param>
        /// <param name="enumToInt">Command header.</param>
        /// <param name="previousPageMarker">The prevpage marker.</param>
        /// <param name="button">The handler button placed in the center.</param>
        /// <returns>Paginated inline menu.</returns>
        public static InlineKeyboardMarkup GetPageMenu(
            int currentPage, 
            int pageCount, 
            Enum enumToInt, 
            string nextPageMarker = "➡️", 
            string previousPageMarker = "⬅️", 
            IInlineContent button = null)
        {
            List<IInlineContent> buttons = new();

            if (currentPage != 1)
                buttons.Add(new InlineCallback<PageTCommand>($"({pageCount - (pageCount - currentPage + 1)}) {previousPageMarker}", PRTelegramBotCommand.PreviousPage, new PageTCommand(currentPage - 1, enumToInt)));
            if (button is not null)
                buttons.Add(button);

            if (currentPage != pageCount)
                buttons.Add(new InlineCallback<PageTCommand>($"{nextPageMarker} ({pageCount - currentPage})", PRTelegramBotCommand.CurrentPage, new PageTCommand(currentPage + 1, enumToInt)));

            return InlineKeyboard(3, buttons);
        }

        /// <summary>
        /// Generates a menu for paginated output.
        /// </summary>
        /// <param name="currentPage">Current page.</param>
        /// <param name="pageCount">Total number of pages.</param>
        /// <param name="nextPageMarker">The nextpage marker.</param>
        /// <param name="enumToInt">Command header.</param>
        /// <param name="previousPageMarker">The prevpage marker.</param>
        /// <param name="customButtons">Handler buttons.</param>
        /// <returns>Paginated inline menu.</returns>
        public static InlineKeyboardMarkup GetPageMenu(
            int currentPage, 
            int pageCount, 
            Enum enumToInt, 
            List<IInlineContent> customButtons, 
            string nextPageMarker = "➡️", 
            string previousPageMarker = "⬅️")
        {
            List<IInlineContent> buttons = new();

            if (currentPage != 1)
                buttons.Add(new InlineCallback<PageTCommand>($"({pageCount - (pageCount - currentPage + 1)}) {previousPageMarker}", PRTelegramBotCommand.PreviousPage, new PageTCommand(currentPage - 1, enumToInt)));

            if (currentPage != pageCount)
                buttons.Add(new InlineCallback<PageTCommand>($"{nextPageMarker} ({pageCount - currentPage})", PRTelegramBotCommand.CurrentPage, new PageTCommand(currentPage + 1, enumToInt)));

            var pageButtons = InlineButtons(2, buttons);
            var customMenu = InlineButtons(1, customButtons);
            pageButtons.AddRange(customMenu);
            return InlineKeyboard(pageButtons);
        }
        #endregion
    }
}
