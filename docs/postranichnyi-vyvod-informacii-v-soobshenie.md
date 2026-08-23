# Постраничный вывод информации в сообщение

Под постраничным выводом информации я подразумеваю сообщений с inline кнопками вперед и назад, при нажатие на эти кнопки изменяются данные сообщения.

<figure><img src=".gitbook/assets/изображение-22.png" alt=""><figcaption></figcaption></figure>

Создадим перечисление

```csharp
[InlineCommand]
public enum CustomTHeaderTwo
{
    [Description("Пример 1")]
    ExampleOne = 600,
    [Description("Пример 2")]
    ExampleTwo,
    [Description("Пример 3")]
    ExampleThree,
    [Description("Пример страниц")]
    CustomPageHeader,
    [Description("Пример страниц2")]
    CustomPageHeader2,
}
```

Подготовим 2 тестовых списка с текстом сообщений:

```csharp
//Тестовые данные 1
static List<string> pageData = new List<string>()
{
    "Данные страница 1",
    "Данные страница 2",
    "Данные страница 3",
    "Данные страница 4",
    "Данные страница 5"
};

//Тестовые данные 2
static List<string> pageDataTwo = new List<string>()
{
    "TestДанные страница 1",
    "TestДанные страница 2",
    "TestДанные страница 3",
    "TestДанные страница 4",
    "TestДанные страница 5"
};
```

Для генерации постраничного меню используется следующий методы:

```csharp
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
public static InlineKeyboardMarkup GetPageMenu(
    int currentPage,
    int pageCount, 
    InlineKeyboardMarkup addMenu, 
    Enum enumToInt, 
    string nextPageMarker = "➡️", 
    string previousPageMarker = "⬅️", 
    string currentPageMarker = "")

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
public static InlineKeyboardMarkup GetPageMenu(
    int currentPage, 
    int pageCount, 
    InlineKeyboardMarkup addMenu, 
    Enum enumToInt, 
    string nextPageMarker = "➡️", 
    string previousPageMarker = "⬅️", 
    IInlineContent button = null)

/// <summary>
/// Генерирует меню для постраничного вывода
/// </summary>
/// <param name="currentPage">Текущая страница</param>
/// <param name="pageCount">Всего страниц</param>
/// <param name="nextPageMarker">Маркер nextpage</param>
/// <param name="previousPageMarker">Маркер prevpage</param>
/// <param name="currentPageMarker">Маркер currentPage</param>
/// <returns>Постраничное inline menu</returns>
public static InlineKeyboardMarkup GetPageMenu(
    Enum enumToInt, 
    int currentPage, 
    int pageCount, 
    string nextPageMarker = "➡️", 
    string previousPageMarker = "⬅️", 
    string currentPageMarker = "")

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
public static InlineKeyboardMarkup GetPageMenu(
    int currentPage, 
    int pageCount, 
    Enum enumToInt, 
    string nextPageMarker = "➡️", 
    string previousPageMarker = "⬅️", 
    IInlineContent button = null)

/// <summary>
/// Генерирует меню для постраничного вывода
/// </summary>
/// <param name="currentPage">Текущая страница</param>
/// <param name="pageCount">Всего страниц</param>
/// <param name="nextPageMarker">Маркер nextpage</param>
/// <param name="enumToInt">Заголовок команды</param>
/// <param name="previousPageMarker">Маркер prevpage</param>
/// <param name="customButtons">Кнопки обработчики</param>
/// <returns>Постраничное inline menu</returns>
public static InlineKeyboardMarkup GetPageMenu(
    int currentPage, 
    int pageCount, 
    Enum enumToInt, 
    List<IInlineContent> customButtons, 
    string nextPageMarker = "➡️", 
    string previousPageMarker = "⬅️")

```

Данный метод генерировать меню с кнопка вперед и назад, так и с дополнительной кнопкой обработки по центру. Допустим мы с помощью этого может добавлять/удалять объект из избранного.&#x20;

Пример: Данный метод будет генерировать меню с кнопка вперед и назад, так и с дополнительной кнопкой обработки по центру. Допустим мы с помощью этого может добавлять/удалять объект из избранного.

Для постраничного вывода информации используется метод расширения **GetPaged**:

```csharp
/// <summary>
/// Вывод данных постранично
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="query">Коллекция данных</param>
/// <param name="page">Страница</param>
/// <param name="pageSize">Размер страницы</param>
/// <returns>Страница данных с доп информацией</returns>
public static async Task<PagedResult<T>> GetPaged<T>(this IList<T> query,
                                 int page, int pageSize) where T : class
{
    var result = new PagedResult<T>();
    result.CurrentPage = page;
    result.PageSize = pageSize;
    result.RowCount = query.Count();
 
 
     /// <summary>
    /// Помогает разбить данные постранично.
    /// </summary>
    public static class PageExtension
    {
        #region Методы

        /// <summary>
        /// Вывод данных постранично.
        /// </summary>
        /// <typeparam name="T">Тип.</typeparam>
        /// <param name="query">Коллекция данных.</param>
        /// <param name="page">Страница.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <returns>Страница данных с доп информацией.</returns>
        public static async Task<PagedResult<T>> GetPaged<T>(this IEnumerable<T> query, int page, int pageSize)
            where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        #endregion
    }
```

За его основу была взята эта статья [по работе постраничным выводом из Entity framework](https://www.codingame.com/playgrounds/5363/paging-with-entity-framework-core)

Ниже представлен полный пример.

```csharp
public class ExamplePage
{
    //Тестовые данные 1
    static List<string> pageData = new List<string>()
        {
            "Данные страница 1",
            "Данные страница 2",
            "Данные страница 3",
            "Данные страница 4",
            "Данные страница 5"
        };

    //Тестовые данные 2
    static List<string> pageDataTwo = new List<string>()
        {
            "TestДанные страница 1",
            "TestДанные страница 2",
            "TestДанные страница 3",
            "TestДанные страница 4",
            "TestДанные страница 5"
        };

    /// <summary>
    /// Напишите в чате "pages"
    /// </summary>
    [ReplyMenuHandler("pages")]
    public static async Task ExamplePages(IBotContext context)
    {
        //Беру текст для первого сообщения
        string msg = pageData[0];
        //Получаю контент с 1 страницы с размером страницы 1
        var data = await pageData.GetPaged<string>(1, 1);
        //Генерирую меню постраничного вывода с заголовком
        var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeaderTwo.CustomPageHeader);
        var option = new OptionMessage();
        option.MenuInlineKeyboardMarkup = generateMenu;
        var message = await MessageSender.Send(botClient, update, msg, option);
    }

    /// <summary>
    /// Напишите в чате "pagestwo"
    /// </summary>
    [ReplyMenuHandler("pagestwo")]
    public static async Task ExamplePagesTwo(IBotContext context)
    {
        //Беру текст для первого сообщения
        string msg = pageDataTwo[0];
        //Получаю контент с 1 страницы с размером страницы 1
        var data = await pageDataTwo.GetPaged<string>(1, 1);
        //Генерирую меню постраничного вывода с заголовком
        var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeaderTwo.CustomPageHeader2);
        var option = new OptionMessage();
        option.MenuInlineKeyboardMarkup = generateMenu;

        var message = await MessageSender.Send(botClient, update, msg, option);
    }

    /// <summary>
    /// callback обработка постраничного вывода
    /// Обрабатывает одну точку входа
    /// </summary>
    [InlineCallbackHandler<PRTelegramBotCommand>(PRTelegramBotCommand.NextPage, PRTelegramBotCommand.PreviousPage, PRTelegramBotCommand.CurrentPage)]
    public static async Task InlinenPage(IBotContext context)
    {
        try
        {
            //Попытка преобразовать callback данные к требуемому типу
            if (update.CallbackQuery?.Data != null)
            {
                var command = InlineCallback<PageTCommand>.GetCommandByCallbackOrNull(context);
                if (command != null)
                {
                    //Получаю заголовок из данных
                    CustomTHeaderTwo header = (CustomTHeaderTwo)command.Data.Header;
                    //обрабатываю данные по заголовку
                    if(header == CustomTHeaderTwo.CustomPageHeader)
                    {
                        //Получаю номер страницы и указываю размер страницы
                        var data = await pageData.GetPaged<string>(command.Data.Page, 1);
                        //Генерирую постраничное меню
                        var button = new InlineCallback("⭐", CustomTHeader.CustomButton);
                        var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeaderTwo.CustomPageHeader,button: button);
                        //Получаю результат из постраничного вывода
                        var pageResult = data.Results;
                        var option = new OptionMessage();
                        option.MenuInlineKeyboardMarkup = generateMenu;
                        string msg = "";
                        if (pageResult.Count > 0)
                        {
                            msg = pageResult.FirstOrDefault();
                        }
                        else
                        {
                            msg = "Нечего не найдено";
                        }
                        //Редактирую текущую страницу
                        await MessageEditor.Edit(context, msg, option);
                    }
                    //обрабатываю данные по заголовку
                    else if (header == CustomTHeaderTwo.CustomPageHeader2)
                    {
                        //Получаю номер страницы и указываю размер страницы
                        var data = await pageDataTwo.GetPaged<string>(command.Data.Page, 1);
                        //Генерирую постраничное меню
                        var generateMenu = MenuGenerator.GetPageMenu(data.CurrentPage, data.PageCount, CustomTHeaderTwo.CustomPageHeader2);
                        //Получаю результат из постраничного вывода
                        var pageResult = data.Results;
                        var option = new OptionMessage();
                        option.MenuInlineKeyboardMarkup = generateMenu;
                        string msg = "";
                        if (pageResult.Count > 0)
                        {
                            msg = pageResult.FirstOrDefault();
                        }
                        else
                        {
                            msg = "Нечего не найдено";
                        }
                        //Редактирую текущую страницу
                        await MessageEditor.Edit(context, msg, option);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            //Обработка исключения
        }
    }

    [InlineCallbackHandler<CustomTHeader>(CustomTHeader.CustomButton)]
    public static async Task FavoriteMessage(IBotContext context)
    {
        string msg = "Меню";
        //Создаем настройки сообщения
        var option = new OptionMessage();
        //Создаем список для меню
        var menuList = new List<KeyboardButton>();
        //Добавляем кнопку с текстом
        menuList.Add(new KeyboardButton("Кнопка 1"));
        //Добавляем кнопку с запросом на контакт пользователя
        menuList.Add(KeyboardButton.WithRequestContact("Отправить свой контакт"));
        //Добавляем кнопку с запросом на локацию пользователя
        menuList.Add(KeyboardButton.WithRequestLocation("Отправить свою локацию"));
        //Добавляем кнопку с запросом отправки чата боту
        menuList.Add(KeyboardButton.WithRequestChat("Отправить группу боту", new KeyboardButtonRequestChat(2, true) ));
        //Добавляем кнопку с запросом отправки пользователя боту
        menuList.Add(KeyboardButton.WithRequestUsers("Отправить пользователя боту", new KeyboardButtonRequestUsers() { RequestId = 1 }));
        //Добавляем кнопку с отправкой опроса
        menuList.Add(KeyboardButton.WithRequestPoll("Отправить свою голосование"));
        //Добавляем кнопку с запросом работы с WebApp
        menuList.Add(KeyboardButton.WithWebApp("WebApp", new WebAppInfo() { Url = "https://prethink.github.io/telegram/webapp.html" }));

        //Генерируем reply меню
        //1 столбец, коллекция пунктов меню, вертикальное растягивание меню, пункт в самом низу по умолчанию
        var menu = MenuGenerator.ReplyKeyboard(1, menuList, true, "Главное меню");
        //Добавляем в настройки меню
        option.MenuReplyKeyboardMarkup = menu;
        await MessageSender.Send(context, msg, option);
    }
}
```

Результаты работы:

<figure><img src=".gitbook/assets/Без имени.png" alt=""><figcaption></figcaption></figure>

<figure><img src=".gitbook/assets/изображение-23.png" alt=""><figcaption></figcaption></figure>

<figure><img src=".gitbook/assets/изображение-24.png" alt=""><figcaption></figcaption></figure>
