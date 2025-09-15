using PRTelegramBot.InlineButtons;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.Enums;
using PRTelegramBot.Models.InlineButtons;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PRTelegramBot.Utils
{
    /// <summary>
    /// Класс для удобной генерации меню.
    /// </summary>
    public static class MenuGenerator
    {
        #region Reply кнопки и меню
        /// <summary>
        /// Генерирует reply меню для бота.
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов.</param>
        /// <param name="menu">Коллекция меню.</param>
        /// <param name="resizeKeyboard">Изменяет размер по вертикали.</param>
        /// <param name="mainMenu">Если значение не пустое добавляет пункт в самый конец меню.</param>
        /// <param name="OneTimeKeyboard">Признак, того что клавиатура будет скрыта после нажатия на кнопку.</param>
        /// <returns>Готовое меню</returns>
        public static ReplyKeyboardMarkup ReplyKeyboard(int maxColumn, List<string> menu, bool resizeKeyboard = true, string mainMenu = "", bool OneTimeKeyboard = false)
        {
            var buttons = ReplyButtons(maxColumn, menu, mainMenu);
            return ReplyKeyboard(buttons, resizeKeyboard, string.Empty, OneTimeKeyboard);
        }

        /// <summary>
        /// Генерирует reply меню для бота.
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов.</param>
        /// <param name="keyboardButtons">Коллекция кнопок.</param>
        /// <param name="resizeKeyboard">Изменяет размер по вертикали.</param>
        /// <param name="mainMenu">Есть не пусто, добавляет главное меню.</param>
        /// <param name="OneTimeKeyboard">Признак, того что клавиатура будет скрыта после нажатия на кнопку.</param>
        /// <returns>Готовое меню</returns>
        public static ReplyKeyboardMarkup ReplyKeyboard(int maxColumn, List<KeyboardButton> keyboardButtons, bool resizeKeyboard = true, string mainMenu = "", bool OneTimeKeyboard = false)
        {
            var buttons = ReplyButtons(maxColumn, keyboardButtons, mainMenu);
            return ReplyKeyboard(buttons, resizeKeyboard, string.Empty, OneTimeKeyboard);
        }

        /// <summary>
        /// Генерирует reply меню для бота.
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="resizeKeyboard">Изменяет размер по вертикали.</param>
        /// <param name="mainMenu">Есть не пусто, добавляет главное меню.</param>
        /// <param name="OneTimeKeyboard">Признак, того что клавиатура будет скрыта после нажатия на кнопку.</param>
        /// <returns>Готовое меню</returns>
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
        /// Генерирует reply кнопки для бота.
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов.</param>
        /// <param name="menu">Меню.</param>
        /// <param name="mainMenu">Есть не пусто, добавляет главное меню.</param>
        /// <returns>Коллекция кнопок.</returns>
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
        /// Генерирует reply кнопки для бота.
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов.</param>
        /// <param name="buttons">Кнопки.</param>
        /// <param name="mainMenu">Есть не пусто, добавляет главное меню.</param>
        /// <returns>Коллекция кнопок.</returns>
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
        /// Объединяет reply кнопки для бота.
        /// </summary>
        /// <param name="buttonsOne">Первая лист кнопок.</param>
        /// <param name="buttonsTwo">Второй лист кнопок.</param>
        /// <returns>Коллекция кнопок.</returns>
        public static List<List<KeyboardButton>> ReplyButtons(List<List<KeyboardButton>> buttonsOne, List<List<KeyboardButton>> buttonsTwo)
        {
            buttonsOne.AddRange(buttonsTwo);
            return buttonsOne;
        }
        #endregion

        #region Inline кнопки и меню
        /// <summary>
        /// Создает Inline меню для бота.
        /// </summary>
        /// <param name="buttons">Коллекция кнопок.</param>
        /// <returns> Inline меню для бота.</returns>
        public static InlineKeyboardMarkup InlineKeyboard(List<List<InlineKeyboardButton>> buttons)
        {
            InlineKeyboardMarkup Keyboard = new(buttons);
            return Keyboard;
        }

        /// <summary>
        /// Создает Inline меню для бота.
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов.</param>
        /// <param name="menu">Коллекция кнопок.</param>
        /// <returns>Inline меню для бота.</returns>
        public static InlineKeyboardMarkup InlineKeyboard(int maxColumn, List<IInlineContent> menu)
        {
            var buttons = InlineButtons(maxColumn, menu);
            return InlineKeyboard(buttons);
        }

        /// <summary>
        /// Создает коллекцию inline кнопок.
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов.</param>
        /// <param name="menu">Коллекция меню.</param>
        /// <returns>Коллекция кнопок.</returns>
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
                    buttons[row].Add(GetInlineButton(item));
                }
                else
                {
                    buttons[row].Add(GetInlineButton(item));
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
        /// Создает inline кнопку.
        /// </summary>
        /// <param name="inlineData">Данные inline кнопки.</param>
        /// <returns>Inline кнопка.</returns>
        public static InlineKeyboardButton GetInlineButton(IInlineContent inlineData)
        {
            return inlineData switch
            {
                InlineCallback inlineCallback => InlineKeyboardButton.WithCallbackData(inlineCallback.GetTextButton(), inlineCallback.GetContent() as string),
                InlinePay inlinePay => InlineKeyboardButton.WithPay(inlinePay.GetTextButton()),
                InlineURL inlineUrl => InlineKeyboardButton.WithUrl(inlineUrl.GetTextButton(), inlineUrl.GetContent() as string),
                InlineWebApp inlineWebApp => InlineKeyboardButton.WithWebApp(inlineWebApp.GetTextButton(), inlineWebApp.GetContent() as WebAppInfo),
                InlineLoginUrl inlineLogin => InlineKeyboardButton.WithLoginUrl(inlineLogin.GetTextButton(), inlineLogin.GetContent() as LoginUrl),
                InlineCallbackGame inlineCallbackGame => InlineKeyboardButton.WithCallbackGame(inlineCallbackGame.GetTextButton()),
                InlineSwitchInlineQuery inlineSwitchInlineQuery => InlineKeyboardButton.WithSwitchInlineQuery(inlineSwitchInlineQuery.GetTextButton(), inlineSwitchInlineQuery.GetContent() as string),
                InlineSwitchInlineQueryCurrentChat inlineSwitchInlineQueryCurrentChat => InlineKeyboardButton.WithSwitchInlineQueryCurrentChat(inlineSwitchInlineQueryCurrentChat.GetTextButton(), inlineSwitchInlineQueryCurrentChat.GetContent() as string),
                InlineSwitchInlineQueryChosenChat inlineSwitchInlineQueryChosenChat => InlineKeyboardButton.WithSwitchInlineQueryChosenChat(inlineSwitchInlineQueryChosenChat.GetTextButton(), inlineSwitchInlineQueryChosenChat.GetContent() as SwitchInlineQueryChosenChat),
                
                _ => throw new NotImplementedException($"{inlineData.GetType()} is not implemented yet.")
            };
        }

        /// <summary>
        /// Создает одно inline меню из нескольких.
        /// </summary>
        /// <param name="keyboards">Массив меню.</param>
        /// <returns> Inline меню для бота.</returns>
        public static InlineKeyboardMarkup UnitInlineKeyboard(params InlineKeyboardMarkup[] keyboards)
        {
            List<IEnumerable<InlineKeyboardButton>> buttons = new();
            foreach (var keyboard in keyboards)
                buttons.AddRange(keyboard.InlineKeyboard);

            InlineKeyboardMarkup Keyboard = new(buttons);
            return Keyboard;
        }

        /// <summary>
        /// Генерирует меню для постраничного вывода.
        /// </summary>
        /// <param name="currentPage">Текущая страница.</param>
        /// <param name="pageCount">Всего страниц.</param>
        /// <param name="nextPageMarker">Маркер nextpage.</param>
        /// <param name="previousPageMarker">Маркер prevpage.</param>
        /// <param name="currentPageMarker">Маркер currentPage.</param>
        /// <param name="addMenu">Дополнительное меню с которым требуется объединить данные.</param>
        /// <returns>Постраничное inline menu.</returns>
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
            return UnitInlineKeyboard(addMenu, pageMenu);
        }

        /// <summary>
        /// Генерирует меню для постраничного вывода.
        /// </summary>
        /// <param name="currentPage">Текущая страница.</param>
        /// <param name="pageCount">Всего страниц.</param>
        /// <param name="nextPageMarker">Маркер nextpage.</param>
        /// <param name="previousPageMarker">Маркер prevpage.</param>
        /// <param name="button">Кнопка обработчик в центре.</param>
        /// <param name="addMenu">Дополнительное меню с которым требуется объединить данные.</param>
        /// <returns>Постраничное inline menu.</returns>
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
            return UnitInlineKeyboard(addMenu, pageMenu);
        }

        /// <summary>
        /// Генерирует меню для постраничного вывода.
        /// </summary>
        /// <param name="currentPage">Текущая страница.</param>
        /// <param name="pageCount">Всего страниц.</param>
        /// <param name="nextPageMarker">Маркер nextpage.</param>
        /// <param name="previousPageMarker">Маркер prevpage.</param>
        /// <param name="currentPageMarker">Маркер currentPage.</param>
        /// <returns>Постраничное inline menu.</returns>
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
        /// Генерирует меню для постраничного вывода.
        /// </summary>
        /// <param name="currentPage">Текущая страница.</param>
        /// <param name="pageCount">Всего страниц.</param>
        /// <param name="nextPageMarker">Маркер nextpage.</param>
        /// <param name="enumToInt">Заголовок команды.</param>
        /// <param name="previousPageMarker">Маркер prevpage.</param>
        /// <param name="button">Кнопка обработчик в центре.</param>
        /// <returns>Постраничное inline menu.</returns>
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
        /// Генерирует меню для постраничного вывода.
        /// </summary>
        /// <param name="currentPage">Текущая страница.</param>
        /// <param name="pageCount">Всего страниц.</param>
        /// <param name="nextPageMarker">Маркер nextpage.</param>
        /// <param name="enumToInt">Заголовок команды.</param>
        /// <param name="previousPageMarker">Маркер prevpage.</param>
        /// <param name="customButtons">Кнопки обработчики.</param>
        /// <returns>Постраничное inline menu.</returns>
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
