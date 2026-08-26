# Создание Inline меню

Перед созданием меню требуется познакомиться со следующими элементами:

* InlineCallback – Метод которые создает Inline кнопку с callback.
* TCommand – Вспомогательный класс для отправки данных в callback.

**InlineCallback** принимает следующие параметры:

* buttonName – название кнопки.
* commandType – команда или тип команды. Принимает перечисление типа THeader, о котором написано ранее.
* data – (не обязательный параметр) данные которые нужно передать в callback.

**TCommand** является базовым классом в котором хранятся данные которые нужно передать через callback. TCommand можно отнаследовать и создать свои переменные которые должны содержать свои данные.

**ВНИМАНИЕ: Максимальный допустимый размер данных для обработки в callback\_data 64байт!**

Чтобы использовать дефолтный конвертер inline меню, но при этом сэкономить несколько байт можно использовать другой сериализатор [ToonSerializedWrapper](../../api/klassy/toonserializerwrapper.md), который реализует интерфейс [IPRSerializer](../../api/interfeisy/iprserializer.md).

<figure><img src="../../.gitbook/assets/изображение (1).png" alt=""><figcaption></figcaption></figure>

Если вы хотите полностью исключить у себя проблему с ограничением в 64 байта, тогда нужно использовать другой конвертер. Для этого доступен [FileInlineConverter ](../../api/klassy/fileinlineconverter.md)реализующий [IInlineMenuConverter](../../api/interfeisy/iinlinemenuconverter.md), который сохраняет данные локально в json файлы в формате файлов "{Ид бота}-{Ид пользователя}-{Ид команды}".

<figure><img src="../../.gitbook/assets/изображение (2).png" alt=""><figcaption></figcaption></figure>

[IPRSerializer ](../../api/interfeisy/iprserializer.md)и [IInlineMenuConverter ](../../api/interfeisy/iinlinemenuconverter.md)поддерживают DI, вы можете их зарегистрировать как зависимости и бот их подтянет у себя.



В PRTelegramBot есть уже несколько готовых TCommand классов:

* CallendarTCommand – используется для передачи даты (DateTime)
* EntityTCommand\<T> – используется для передачи id или другой не большой информации.

Пример создания inline меню

```csharp
public class Commands
{
    /// <summary>
    /// Напишите в чате "Тест"
    /// </summary>
    [ReplyMenuHandler("Тест")]
    public static async Task ExampleReply(IBotContext context)
    {
         /* Создание новой кнопки с callback данными
           * Название кнопки
           * Models.Enums.CustomTHeader.ExampleOne - Заголовок команды
           */
        var exampleItemOne = new InlineCallback("Пример 1", CustomTHeader.ExampleOne);
        /* Создание новой кнопки с callback данными
         * Название кнопки
         * Models.Enums.CustomTHeader.ExampleOne - Заголовок команды
         * new EntityTCommand(2) - Данные которые требуется передать
         */
        var exampleItemTwo = new InlineCallback<EntityTCommand<long>>("Пример 2", CustomTHeader.ExampleTwo, new EntityTCommand<long>(2));
        /* Создание новой кнопки с callback данными
         * Models.Enums.CustomTHeader.ExampleOne - Заголовок команды
         * new EntityTCommand(2) - Данные которые требуется передать
         */
        var exampleItemThree = new InlineCallback<EntityTCommand<long>>("Пример 3", CustomTHeader.ExampleThree, new EntityTCommand<long>(3));
        // Создает inline кнопку с ссылкой
        var url = new InlineURL("Google", "https://google.com");
        // Создаем кнопку для работы с webApp
        var webdata = new InlineWebApp("WA", "https://prethink.github.io/telegram/webapp.html");

        //IInlineContent - реализуют все inline кнопки
        List<IInlineContent> menu = new();

        menu.Add(exampleItemOne);
        menu.Add(exampleItemTwo);
        menu.Add(exampleItemThree);
        menu.Add(url);
        menu.Add(webdata);

        //Генерация меню на основе данных в 1 столбец
        var testMenu = MenuGenerator.InlineKeyboard(1, menu);

        //Создание настроек для передачи в сообщение
        var option = new OptionMessage();
        //Передача меню в настройки
        option.MenuInlineKeyboardMarkup = testMenu;
        string msg = "Пример работы меню";
        await MessageSender.Send(context, msg, option);
    }
}
```

Примеры создания через билдер [InlineKeyboardBuilder](../../api/klassy/inlinekeyboardbuilder.md)

```csharp
[ReplyMenuHandler("InlineMenu")]
public static async Task InlineMenu(IBotContext context)
{
    /*
     *  В program.cs создается экземпляр бота:
     *   
     *  var telegram = new PRBotBuilder(string.Empty)
            .AddConfigPath(ExampleConstants.BUTTONS_FILE_KEY, ".\\Configs\\buttons.json")
     *      .Build();
     *  
     *  AddConfigPath - добавляет путь для конфигурационного файла.
     *  ExampleConstants.BUTTONS_FILE_KEY - ключ 
     *  ".\\Configs\\buttons.json" - путь до конфигурационного файла.
     *  
     */

    /*
     *  context.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE")
     *  BotConfigJsonProvider - провайдер который работает с json файлами.
     *  string - возращаемый тип.
     *  ExampleConstants.BUTTONS_FILE_KEY - ключ конфига.
     *  IN_EXAMPLE_ONE - ключ текста кнопки из json файла buttons.json
     * 
     */

    /* Создание новой кнопки с callback данными
     * context`.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE") - Название кнопки из json
     * CustomTHeaderTwo.ExampleOne - Заголовок команды
     */
    var exampleItemOne = new InlineCallback(context.GetConfigValue<BotConfigJsonProvider, string>(ExampleConstants.BUTTONS_FILE_KEY, "IN_EXAMPLE_ONE"), CustomTHeaderTwo.ExampleOne);
    /* Создание новой кнопки с callback данными
     * InlineKeys.IN_EXAMPLE_TWO - Название кнопки из константы
     * CustomTHeaderTwo.ExampleTwo - Заголовок команды
     * new EntityTCommand(2) - Данные которые требуется передать
     */
    var exampleItemTwo = new InlineCallback<EntityTCommand<long>>("Пример с большим числом", CustomTHeaderTwo.ExampleTwo, new EntityTCommand<long>(2_000_000_000_000_000_000));
    /* Создание новой кнопки с callback данными
     * CustomTHeaderTwo.ExampleThree - Заголовок команды
     * new EntityTCommand(3) - Данные которые требуется передать
     */

    var exampleItemThree = new InlineCallback<EntityTCommand<string>>("Пример с большим текстом", CustomTHeaderTwo.ExampleThree, new EntityTCommand<string>("И нет сомнений, что диаграммы связей будут объявлены нарушающими общечеловеческие нормы этики и морали. Имеется спорная точка зрения, гласящая примерно следующее: ключевые особенности структуры проекта, инициированные исключительно синтетически, своевременно верифицированы. Значимость этих проблем настолько очевидна, что высокотехнологичная концепция общественного уклада обеспечивает широкому кругу (специалистов) участие в формировании переосмысления внешнеэкономических политик. Таким образом, высокотехнологичная концепция общественного уклада играет важную роль в формировании экспериментов, поражающих по своей масштабности и грандиозности. Картельные сговоры не допускают ситуации, при которой тщательные исследования конкурентов, превозмогая сложившуюся непростую экономическую ситуацию, заблокированы в рамках своих собственных рациональных ограничений. Каждый из нас понимает очевидную вещь: реализация намеченных плановых заданий выявляет срочную потребность как самодостаточных, так и внешне зависимых концептуальных решений. Равным образом, убеждённость некоторых оппонентов однозначно определяет каждого участника как способного принимать собственные решения касаемо первоочередных требований. Повседневная практика показывает, что реализация намеченных плановых заданий обеспечивает актуальность распределения внутренних резервов и ресурсов. В своём стремлении повысить качество жизни, они забывают, что базовый вектор развития обеспечивает актуальность поставленных обществом задач."));

    var inlineStep = new InlineCallback("Inline Step", CustomTHeader.InlineWithStep);

    //Команды который добавлены после запуска бота
    var exampleAddCommand = new InlineCallback("Команда добавленная динамически 1", AddCustomTHeader.TestAddCommand);
    var exampleAddCommandTwo = new InlineCallback("Команда добавленная динамически 2", AddCustomTHeader.TestAddCommandTwo);

    // Создает inline кнопку с ссылкой
    var url = new InlineURL("Google", "https://google.com");
    // Создаем кнопку для работы с webApp
    var webdata = new InlineWebApp("WebApp", "https://prethink.github.io/telegram/webapp.html");

    var keyboard = new InlineKeyboardBuilder()
        .AddButton(exampleItemOne)
        .AddButton(exampleItemTwo, newRow:true)
        .AddButton(exampleItemThree, newRow: true)
        .AddButton(exampleAddCommand, newRow: true)
        .AddRow()
        .AddButton(exampleAddCommandTwo)
        .AddButton(inlineStep)
        .AddRow()
        .AddButton(url)
        .AddButton(webdata)
        .Build();

    //Создание настроек для передачи в сообщение
    var option = new OptionMessage();
    //Передача меню в настройки
    option.MenuInlineKeyboardMarkup = keyboard;
    string msg = "Пример работы меню";
    //Отправка сообщение с меню
    await MessageSender.Send(context, msg, option);
}
```

<figure><img src="../../.gitbook/assets/изображение (3).png" alt=""><figcaption></figcaption></figure>

## Кнопки, которые ничего не делают

В Bot API 10.3 у кнопки появилось третье состояние. `InlineDisabled` рисуется серой, и нажатие на неё не отправляет ничего — обработчик не вызывается.

```csharp
var keyboard = new InlineKeyboardBuilder()
    .AddButton(new InlineCallback("Шаг 1 — пройден", MyHeader.StepOne))
    .AddRowWithButton(new InlineDisabled("Шаг 2 — сначала закончите первый"))
    .AddRowWithButton(new InlineDisabled("Шаг 3 — закрыт"))
    .Build();
```

Смысл в вёрстке. Раньше временно недоступный пункт оставлял два варианта: убрать кнопку — и меню съезжает под пальцем пользователя между двумя сообщениями, либо оставить её живой и объяснять отказ уже после нажатия. Отключённая кнопка остаётся на месте и объясняет причину собственной подписью.

Полезной нагрузки она не несёт — подпись и есть вся кнопка, — поэтому маршрутизировать нечего и заголовок объявлять не нужно.
