using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using PRTelegramBot.Models.Interface;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Core;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.TCommands;
using PRTelegramBot.Models.Enums;

namespace PRTelegramBot.Helpers.TG
{
    /// <summary>
    /// Класс для удобной генерации меню
    /// </summary>
    public static class MenuGenerator
    {
        /// <summary>
        /// Генерирует reply меню для бота
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов</param>
        /// <param name="menu">Коллекция меню</param>
        /// <param name="resizeKeyboard">Изменяет размер по вертикали</param>
        /// <param name="mainMenu">Есть не пусто, добавляет главное меню</param>
        /// <returns>Готовое меню</returns>
        public static ReplyKeyboardMarkup ReplyKeyboard(int maxColumn, List<string> menu, bool resizeKeyboard = true, string mainMenu = "")
        {
            var buttons = ReplyButtons(maxColumn, menu, mainMenu);
            return ReplyKeyboard(buttons, resizeKeyboard);
        }

        /// <summary>
        /// Генерирует reply меню для бота
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов</param>
        /// <param name="keyboardButtons">Коллекция кнопок</param>
        /// <param name="resizeKeyboard">Изменяет размер по вертикали</param>
        /// <param name="mainMenu">Есть не пусто, добавляет главное меню</param>
        /// <returns>Готовое меню</returns>
        public static ReplyKeyboardMarkup ReplyKeyboard(int maxColumn, List<KeyboardButton> keyboardButtons, bool resizeKeyboard = true, string mainMenu = "")
        {
            var buttons = ReplyButtons(maxColumn, keyboardButtons, mainMenu);
            return ReplyKeyboard(buttons, resizeKeyboard);
        }

        /// <summary>
        /// Генерирует reply меню для бота
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="resizeKeyboard">Изменяет размер по вертикали</param>
        /// <param name="mainMenu">Есть не пусто, добавляет главное меню</param>
        /// <returns>Готовое меню</returns>
        public static ReplyKeyboardMarkup ReplyKeyboard(List<List<KeyboardButton>> buttons, bool resizeKeyboard = true, string mainMenu = "")
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
            return replyKeyboardMarkup;
        }

        /// <summary>
        /// Генерирует reply кнокпи для бота
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов</param>
        /// <param name="menu"></param>
        /// <param name="mainMenu">Есть не пусто, добавляет главное меню</param>
        /// <returns>Коллекция кнопок</returns>
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
        /// Генерирует reply кнокпи для бота
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов</param>
        /// <param name="buttons">Кнокпки</param>
        /// <param name="mainMenu">Есть не пусто, добавляет главное меню</param>
        /// <returns>Коллекция кнопок</returns>
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
        /// Объединяет reply кнокпи для бота
        /// </summary>
        /// <param name="buttonsOne">Первая лист кнопок</param>
        /// <param name="buttonsTwo">Второй лист кнопок</param>
        /// <returns>Коллекция кнопок</returns>
        public static List<List<KeyboardButton>> ReplyButtons(List<List<KeyboardButton>> buttonsOne, List<List<KeyboardButton>> buttonsTwo)
        {
            buttonsOne.AddRange(buttonsTwo);
            return buttonsOne;
        }

        /// <summary>
        /// Создает Inline меню для бота
        /// </summary>
        /// <param name="buttons">Коллекция кнопок</param>
        /// <returns> Inline меню для бота</returns>
        public static InlineKeyboardMarkup InlineKeyboard(List<List<InlineKeyboardButton>> buttons)
        {
            InlineKeyboardMarkup Keyboard = new(buttons);
            return Keyboard;
        }

        /// <summary>
        /// Создает Inline меню для бота
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов</param>
        /// <param name="menu">Коллекция кнопок</param>
        /// <returns> Inline меню для бота</returns>
        public static InlineKeyboardMarkup InlineKeyboard(int maxColumn, List<IInlineContent> menu)
        {
            var buttons = InlineButtons(maxColumn, menu);
            return InlineKeyboard(buttons);
        }

        /// <summary>
        /// Создает коллекцию inline кнопок
        /// </summary>
        /// <param name="maxColumn">Максимальное количество столбцов</param>
        /// <param name="menu">Коллекция меню</param>
        /// <returns>Коллекция кнопок</returns>
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
        /// Создает inline кнопку
        /// </summary>
        /// <param name="inlineData">Данные inline кнопки</param>
        /// <returns>Inline кнопка</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static InlineKeyboardButton GetInlineButton(IInlineContent inlineData)
        {
            if (inlineData is InlineCallback)
            {
                return InlineKeyboardButton.WithCallbackData(inlineData.GetTextButton(), inlineData.GetContent() as string);
            }
            else if (inlineData is InlineURL)
            {
                return InlineKeyboardButton.WithUrl(inlineData.GetTextButton(), inlineData.GetContent() as string);
            }
            else if (inlineData is InlineWebApp)
            {
                return InlineKeyboardButton.WithWebApp(inlineData.GetTextButton(), inlineData.GetContent() as WebAppInfo);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Создает одно inline меню из нескольких
        /// </summary>
        /// <param name="keyboards">Массив меню</param>
        /// <returns> Inline меню для бота</returns>
        public static InlineKeyboardMarkup UnitInlineKeyboard(params InlineKeyboardMarkup[] keyboards)
        {
            List<IEnumerable<InlineKeyboardButton>> buttons = new();
            foreach (var keyboard in keyboards)
            {
                buttons.AddRange(keyboard.InlineKeyboard);
            }
            InlineKeyboardMarkup Keyboard = new(buttons);
            return Keyboard;
        }

        /// <summary>
        /// Генерирует меню для постраничного вывода
        /// </summary>
        /// <param name="currentPage">Текущая страница</param>
        /// <param name="pageCount">Всего страниц</param>
        /// <param name="nextPageMarker">Маркер nextpage</param>
        /// <param name="previousPageMarker">Маркер prevpage</param>
        /// <param name="currentPageMarker">Маркер currentPage</param>
        /// <param name="addMenu">Дополнительное меню с которым требуется объединить данные</param>
        /// <returns>Постраничное inline menu</returns>
        public static InlineKeyboardMarkup GetPageMenu(int currentPage, int pageCount, InlineKeyboardMarkup addMenu, Enum enumToInt, string nextPageMarker = "➡️", string previousPageMarker = "⬅️", string currentPageMarker = "")
        {
            var pageMenu = GetPageMenu(enumToInt, currentPage, pageCount, nextPageMarker, previousPageMarker, currentPageMarker);
            return UnitInlineKeyboard(addMenu,pageMenu);
        }

        /// <summary>
        /// Генерирует меню для постраничного вывода
        /// </summary>
        /// <param name="currentPage">Текущая страница</param>
        /// <param name="pageCount">Всего страниц</param>
        /// <param name="nextPageMarker">Маркер nextpage</param>
        /// <param name="previousPageMarker">Маркер prevpage</param>
        /// <param name="button">Кнопка обработчик в центре</param>
        /// <param name="addMenu">Дополнительное меню с которым требуется объединить данные</param>
        /// <returns>Постраничное inline menu</returns>
        public static InlineKeyboardMarkup GetPageMenu(int currentPage, int pageCount, InlineKeyboardMarkup addMenu, Enum enumToInt, string nextPageMarker = "➡️", string previousPageMarker = "⬅️", IInlineContent button = null)
        {
            var pageMenu = GetPageMenu(currentPage, pageCount, enumToInt, nextPageMarker, previousPageMarker, button);
            return UnitInlineKeyboard(addMenu, pageMenu);
        }

        /// <summary>
        /// Генерирует меню для постраничного вывода
        /// </summary>
        /// <param name="currentPage">Текущая страница</param>
        /// <param name="pageCount">Всего страниц</param>
        /// <param name="nextPageMarker">Маркер nextpage</param>
        /// <param name="previousPageMarker">Маркер prevpage</param>
        /// <param name="currentPageMarker">Маркер currentPage</param>
        /// <returns>Постраничное inline menu</returns>
        public static InlineKeyboardMarkup GetPageMenu(Enum enumToInt, int currentPage, int pageCount, string nextPageMarker = "➡️", string previousPageMarker = "⬅️", string currentPageMarker = "")
        {
            IInlineContent button = null;
            if(!string.IsNullOrEmpty(currentPageMarker))
            {
                button = new InlineCallback<PageTCommand>($"{currentPageMarker}({pageCount - currentPage})", THeader.NextPage, new PageTCommand(currentPage, enumToInt));
            }
            return GetPageMenu(currentPage, pageCount, enumToInt, nextPageMarker, previousPageMarker, button);
        }

        /// <summary>
        /// Генерирует меню для постраничного вывода
        /// </summary>
        /// <param name="currentPage">Текущая страница</param>
        /// <param name="pageCount">Всего страниц</param>
        /// <param name="nextPageMarker">Маркер nextpage</param>
        /// <param name="enumToInt">Заголовок команды</param>
        /// <param name="previousPageMarker">Маркер prevpage</param>
        /// <param name="button">Кнопка обработчик в центре</param>
        /// <returns>Постраничное inline menu</returns>
        public static InlineKeyboardMarkup GetPageMenu(int currentPage, int pageCount, Enum enumToInt, string nextPageMarker = "➡️", string previousPageMarker = "⬅️", IInlineContent button = null)
        {
            List<IInlineContent> buttons = new();

            if (currentPage != 1)
            {
                buttons.Add(new InlineCallback<PageTCommand>($"({pageCount - (pageCount - currentPage + 1)}) {previousPageMarker}", THeader.PreviousPage, new PageTCommand(currentPage - 1, enumToInt)));
            }
            if (button != null)
            {
                buttons.Add(button);
            }

            if (currentPage != pageCount)
            {
                buttons.Add(new InlineCallback<PageTCommand>($"{nextPageMarker} ({pageCount - currentPage})", THeader.CurrentPage, new PageTCommand(currentPage + 1, enumToInt)));
            }

            return InlineKeyboard(3, buttons);
        }
    }
}
