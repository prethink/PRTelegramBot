# MenuGenerator

## Reply кнопки и меню

```csharp
/// <summary>
/// Генерирует reply меню для бота
/// </summary>
/// <param name="maxColumn">Максимальное количество столбцов</param>
/// <param name="menu">Коллекция меню</param>
/// <param name="resizeKeyboard">Изменяет размер по вертикали</param>
/// <param name="mainMenu">Если значение не пустое добавляет пункт в самый конец меню</param>
/// <returns>Готовое меню</returns>
public static ReplyKeyboardMarkup ReplyKeyboard(int maxColumn, List<string> menu, bool resizeKeyboard = true, string mainMenu = "")

/// <summary>
/// Генерирует reply меню для бота
/// </summary>
/// <param name="maxColumn">Максимальное количество столбцов</param>
/// <param name="keyboardButtons">Коллекция кнопок</param>
/// <param name="resizeKeyboard">Изменяет размер по вертикали</param>
/// <param name="mainMenu">Если значение не пустое добавляет пункт в самый конец меню</param>
/// <returns>Готовое меню</returns>
public static ReplyKeyboardMarkup ReplyKeyboard(int maxColumn, List<KeyboardButton> keyboardButtons, bool resizeKeyboard = true, string mainMenu = "")

/// <summary>
/// Генерирует reply меню для бота
/// </summary>
/// <param name="buttons"></param>
/// <param name="resizeKeyboard">Изменяет размер по вертикали</param>
/// <param name="mainMenu">Если значение не пустое добавляет пункт в самый конец меню</param>
/// <returns>Готовое меню</returns>
public static ReplyKeyboardMarkup ReplyKeyboard(List<List<KeyboardButton>> buttons, bool resizeKeyboard = true, string mainMenu = "")

/// <summary>
/// Генерирует reply кнокпи для бота
/// </summary>
/// <param name="maxColumn">Максимальное количество столбцов</param>
/// <param name="menu"></param>
/// <param name="mainMenu">Если значение не пустое добавляет пункт в самый конец меню</param>
/// <returns>Коллекция кнопок</returns>
public static List<List<KeyboardButton>> ReplyButtons(int maxColumn, List<string> menu, string mainMenu = "")

/// <summary>
/// Генерирует reply кнокпи для бота
/// </summary>
/// <param name="maxColumn">Максимальное количество столбцов</param>
/// <param name="buttons">Кнокпки</param>
/// <param name="mainMenu">Если значение не пустое добавляет пункт в самый конец меню</param>
/// <returns>Коллекция кнопок</returns>
public static List<List<KeyboardButton>> ReplyButtons(int maxColumn, List<KeyboardButton> buttons, string mainMenu = "")

/// <summary>
/// Объединяет reply кнопки для бота
/// </summary>
/// <param name="buttonsOne">Первая лист кнопок</param>
/// <param name="buttonsTwo">Второй лист кнопок</param>
/// <returns>Коллекция кнопок</returns>
public static List<List<KeyboardButton>> ReplyButtons(List<List<KeyboardButton>> buttonsOne, List<List<KeyboardButton>> buttonsTwo)
```

## Inline кнопки и меню

<pre class="language-csharp"><code class="lang-csharp"><strong>/// &#x3C;summary>
</strong>/// Создает Inline меню
/// &#x3C;/summary>
/// &#x3C;param name="buttons">Коллекция кнопок&#x3C;/param>
/// &#x3C;returns> Inline меню для бота&#x3C;/returns>
public static InlineKeyboardMarkup InlineKeyboard(List&#x3C;List&#x3C;InlineKeyboardButton>> buttons)

/// &#x3C;summary>
/// Создает Inline меню
/// &#x3C;/summary>
/// &#x3C;param name="maxColumn">Максимальное количество столбцов&#x3C;/param>
/// &#x3C;param name="menu">Коллекция кнопок&#x3C;/param>
/// &#x3C;returns> Inline меню для бота&#x3C;/returns>
public static InlineKeyboardMarkup InlineKeyboard(int maxColumn, List&#x3C;IInlineContent> menu)

/// &#x3C;summary>
/// Создает коллекцию inline кнопок
/// &#x3C;/summary>
/// &#x3C;param name="maxColumn">Максимальное количество столбцов&#x3C;/param>
/// &#x3C;param name="menu">Коллекция меню&#x3C;/param>
/// &#x3C;returns>Коллекция кнопок&#x3C;/returns>
public static List&#x3C;List&#x3C;InlineKeyboardButton>> InlineButtons(int maxColumn, List&#x3C;IInlineContent> menu)

/// &#x3C;summary>
/// Создает inline кнопку
/// &#x3C;/summary>
/// &#x3C;param name="inlineData">Данные inline кнопки&#x3C;/param>
/// &#x3C;returns>Inline кнопка&#x3C;/returns>
/// &#x3C;exception cref="NotImplementedException">&#x3C;/exception>
public static InlineKeyboardButton GetInlineButton(IInlineContent inlineData)

/// &#x3C;summary>
/// Создает одно inline меню из нескольких
/// &#x3C;/summary>
/// &#x3C;param name="keyboards">Массив меню&#x3C;/param>
/// &#x3C;returns> Inline меню для бота&#x3C;/returns>
public static InlineKeyboardMarkup UnitInlineKeyboard(params InlineKeyboardMarkup[] keyboards)

/// &#x3C;summary>
/// Генерирует меню для постраничного вывода
/// &#x3C;/summary>
/// &#x3C;param name="currentPage">Текущая страница&#x3C;/param>
/// &#x3C;param name="pageCount">Всего страниц&#x3C;/param>
/// &#x3C;param name="nextPageMarker">Маркер nextpage&#x3C;/param>
/// &#x3C;param name="previousPageMarker">Маркер prevpage&#x3C;/param>
/// &#x3C;param name="currentPageMarker">Маркер currentPage&#x3C;/param>
/// &#x3C;param name="addMenu">Дополнительное меню с которым требуется объединить данные&#x3C;/param>
/// &#x3C;returns>Постраничное inline menu&#x3C;/returns>
public static InlineKeyboardMarkup GetPageMenu(int currentPage, int pageCount, InlineKeyboardMarkup addMenu, Enum enumToInt, string nextPageMarker = "➡️", string previousPageMarker = "⬅️", string currentPageMarker = "")

/// &#x3C;summary>
/// Генерирует меню для постраничного вывода
/// &#x3C;/summary>
/// &#x3C;param name="currentPage">Текущая страница&#x3C;/param>
/// &#x3C;param name="pageCount">Всего страниц&#x3C;/param>
/// &#x3C;param name="nextPageMarker">Маркер nextpage&#x3C;/param>
/// &#x3C;param name="previousPageMarker">Маркер prevpage&#x3C;/param>
/// &#x3C;param name="button">Кнопка обработчик в центре&#x3C;/param>
/// &#x3C;param name="addMenu">Дополнительное меню с которым требуется объединить данные&#x3C;/param>
/// &#x3C;returns>Постраничное inline menu&#x3C;/returns>
public static InlineKeyboardMarkup GetPageMenu(int currentPage, int pageCount, InlineKeyboardMarkup addMenu, Enum enumToInt, string nextPageMarker = "➡️", string previousPageMarker = "⬅️", IInlineContent button = null)

/// &#x3C;summary>
/// Генерирует меню для постраничного вывода
/// &#x3C;/summary>
/// &#x3C;param name="currentPage">Текущая страница&#x3C;/param>
/// &#x3C;param name="pageCount">Всего страниц&#x3C;/param>
/// &#x3C;param name="nextPageMarker">Маркер nextpage&#x3C;/param>
/// &#x3C;param name="previousPageMarker">Маркер prevpage&#x3C;/param>
/// &#x3C;param name="currentPageMarker">Маркер currentPage&#x3C;/param>
/// &#x3C;returns>Постраничное inline menu&#x3C;/returns>
public static InlineKeyboardMarkup GetPageMenu(Enum enumToInt, int currentPage, int pageCount, string nextPageMarker = "➡️", string previousPageMarker = "⬅️", string currentPageMarker = "")

/// &#x3C;summary>
/// Генерирует меню для постраничного вывода
/// &#x3C;/summary>
/// &#x3C;param name="currentPage">Текущая страница&#x3C;/param>
/// &#x3C;param name="pageCount">Всего страниц&#x3C;/param>
/// &#x3C;param name="nextPageMarker">Маркер nextpage&#x3C;/param>
/// &#x3C;param name="enumToInt">Заголовок команды&#x3C;/param>
/// &#x3C;param name="previousPageMarker">Маркер prevpage&#x3C;/param>
/// &#x3C;param name="button">Кнопка обработчик в центре&#x3C;/param>
/// &#x3C;returns>Постраничное inline menu&#x3C;/returns>
public static InlineKeyboardMarkup GetPageMenu(int currentPage, int pageCount, Enum enumToInt, string nextPageMarker = "➡️", string previousPageMarker = "⬅️", IInlineContent button = null)

/// &#x3C;summary>
/// Генерирует меню для постраничного вывода
/// &#x3C;/summary>
/// &#x3C;param name="currentPage">Текущая страница&#x3C;/param>
/// &#x3C;param name="pageCount">Всего страниц&#x3C;/param>
/// &#x3C;param name="nextPageMarker">Маркер nextpage&#x3C;/param>
/// &#x3C;param name="enumToInt">Заголовок команды&#x3C;/param>
/// &#x3C;param name="previousPageMarker">Маркер prevpage&#x3C;/param>
/// &#x3C;param name="customButtons">Кнопки обработчики&#x3C;/param>
/// &#x3C;returns>Постраничное inline menu&#x3C;/returns>
public static InlineKeyboardMarkup GetPageMenu(int currentPage, int pageCount, Enum enumToInt, List&#x3C;IInlineContent> customButtons, string nextPageMarker = "➡️", string previousPageMarker = "⬅️")
</code></pre>
