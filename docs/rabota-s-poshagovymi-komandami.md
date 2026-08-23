# Работа с пошаговыми командами

PRTelegramBot бот предоставляет функционал пошагового выполнения команд. Допустим нужно чтобы пользователь прошел по следующим этапам:

* Ввод имени&#x20;
* Ввод даты рождения

Для создание следующего шага используется класс **StepTelegram**.

```csharp
/// <summary>
/// Позволяет выполнить пользователю команды пошагово.
/// </summary>
public sealed class StepTelegram : IExecuteStep
{
    #region Свойства и константы

    /// <summary>
    /// Ссылка на метод который должен быть выполнен.
    /// </summary>
    public Func<IBotContext, Task> CommandDelegate { get; set; }

    /// <summary>
    /// До какого времени команду можно выполнить.
    /// </summary>
    public DateTime? ExpiredTime { get; set; }

    /// <summary>
    /// Кэш данных.
    /// </summary>
    private ITelegramCache cache { get; set; }

    #endregion

    #region IExecuteStep

    /// <inheritdoc/>
    public bool LastStepExecuted { get; set; }

    /// <inheritdoc/>
    public bool IgnoreBasicCommands { get; set; }

    /// <inheritdoc/>
    public async Task<ExecuteStepResult> ExecuteStep(IBotContext context)
    {
        if (ExpiredTime is not null && DateTime.Now > ExpiredTime)
        {
            context.Update.ClearStepUserHandler();
            return ExecuteStepResult.ExpiredTime;
        }

        try
            {
                await CommandDelegate.Invoke(context);
                return ExecuteStepResult.Success;
            }
            catch (Exception ex)
            {
                context.Current.Events.OnErrorLogInvoke(new ErrorLogEventArgs(context, ex));
                return ExecuteStepResult.Failure;
            }
        }

        /// <inheritdoc/>
        public Func<IBotContext, Task> GetExecuteMethod()
        {
            return CommandDelegate;
        }

        /// <inheritdoc/>
        public bool CanExecute()
        {
            return ExpiredTime is null || DateTime.Now < ExpiredTime;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Регистрация следующего шага.
        /// </summary>
        /// <param name="nextStep">Метод для следующей обработки.</param>
        public void RegisterNextStep(Func<IBotContext, Task> nextStep)
        {
            RegisterNextStep(nextStep, null);
        }

        /// <summary>
        /// Регистрация следующего шага.
        /// </summary>
        /// <param name="nextStep">Метод для следующей обработки.</param>
        /// <param name="addTime">До какого времени команду можно выполнить</param>
        public void RegisterNextStep(Func<IBotContext, Task> nextStep, TimeSpan addTime)
        {
            RegisterNextStep(nextStep, DateTime.Now.Add(addTime));
        }

        /// <summary>
        /// Регистрация следующего шага.
        /// </summary>
        /// <param name="nextStep">Метод для следующей обработки.</param>
        /// <param name="expiriedTime"> До какого времени команду можно выполнить.</param>
        public void RegisterNextStep(Func<IBotContext, Task> nextStep, DateTime? expiriedTime)
        {
            RegisterNextStep(nextStep, expiriedTime, false);
        }

        /// <summary>
        /// Регистрация следующего шага.
        /// </summary>
        /// <param name="nextStep">Метод для следующей обработки.</param>
        /// <param name="expiriedTime"> До какого времени команду можно выполнить.</param>
        /// <param name="ignoreBasicCommands">Игнорировать базовые команды при выполнение шагов.</param>
        public void RegisterNextStep(Func<IBotContext, Task> nextStep, DateTime? expiriedTime, bool ignoreBasicCommands)
        {
            CommandDelegate = nextStep;
            ExpiredTime = expiriedTime;
            IgnoreBasicCommands = ignoreBasicCommands;
        }

        /// <summary>
        /// Получение текущего кэша
        /// </summary>
        /// <typeparam name="T">Класс для хранения кэша</typeparam>
        /// <returns>Кэш</returns>
        public T GetCache<T>()
        {
            return cache is T resultCache ? resultCache : default;
        }

        #endregion

        #region Конструкторы класса

        /// <summary>
        /// Создать новый следующий шаг.
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        public StepTelegram(Func<IBotContext, Task> command)
            : this(command, null, null) { }

        /// <summary>
        /// Создать новый следующий шаг.
        /// </summary>
        /// <param name="command">Команда для выполнения.</param>
        /// <param name="cache">Кэш.</param>
        public StepTelegram(Func<IBotContext, Task> command, ITelegramCache cache)
            : this(command, null, cache, false) { }

        /// <summary>
        /// Создать новый следующий шаг.
        /// </summary>
        /// <param name="command">Команда для выполнения.</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды после чего команда будет проигнорирована.</param>
        public StepTelegram(Func<IBotContext, Task> command, DateTime expiriedTime)
            : this(command, expiriedTime, null, false) { }

        /// <summary>
        /// Создать новый следующий шаг.
        /// </summary>
        /// <param name="command">Команда для выполнения.</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды после чего команда будет проигнорирована.</param>
        /// <param name="cache">Кэш.</param>
        public StepTelegram(Func<IBotContext, Task> command, DateTime? expiriedTime, ITelegramCache cache)
            : this(command, expiriedTime, cache, false) { }

        /// <summary>
        /// Создать новый следующий шаг.
        /// </summary>
        /// <param name="command">Команда для выполнения.</param>
        /// <param name="expiriedTime">Максимальный срок выполнения команды после чего команда будет проигнорирована.</param>
        /// <param name="cache">Кэш.</param>
        /// <param name="ignoreBasicCommands">Игнорировать базовые команды при выполнение шагов.</param>
        public StepTelegram(Func<IBotContext, Task> command, DateTime? expiriedTime, ITelegramCache cache, bool ignoreBasicCommands)
        {
            this.cache = cache;
            IgnoreBasicCommands = ignoreBasicCommands;
            CommandDelegate = command;
            ExpiredTime = expiriedTime;
        }

        #endregion
    }
```

Для работы с шагами используется следующие методы расширения [StepExtension](api/metody-rasshireniya/stepextension.md):

Пользователь может прервать пошаговое выполнение команд в любой момент, для этого ему требуется вызвать ранее зарегистрированную команду. Допустим написать в чате **Тест**, в наших примерам можно было увидеть, что используется команда **Тест**.

Для хранение кэша (промежуточных данных)  создадим класс **StepCache**.

```csharp
public class StepCache : ITelegramCache
{
    public string Name { get; set; }
    public string BirthDay { get; set; }
    public bool ClearData()
    {
        this.BirthDay = string.Empty; 
        this.Name = string.Empty;
        return true;
    }
}
```

Пример

```csharp
/// <summary>
/// Пример работы пошагового выполнения команд
/// </summary>
public class ExampleStepCommand
{
    /// <summary>
    /// Напишите в чате "stepstart"
    /// Метод регистрирует следующий шаг пользователя
    /// </summary>
    [ReplyMenuHandler("stepstart")]
    public static async Task StepStart(IBotContext context)
    {
        string msg = "Тестирование функции пошагового выполнения\nНапишите ваше имя";
        //Регистрация обработчика для последовательной обработки шагов и сохранение данных
        context.RegisterStepHandler(new StepTelegram(StepOne, new StepCache()));
        await Helpers.Message.Send(context, msg);
    }

    /// <summary>
    /// При написание любого текста сообщения или нажатие на любую кнопку из reply для пользователя будет выполнен этот метод.
    /// Метод регистрирует следующий шаг с максимальным времени выполнения
    /// </summary>
    public static async Task StepOne(IBotContext context)
    {
        string msg = $"Шаг 1 - Ваше имя {update.Message.Text}" +
                    $"\nВведите дату рождения";
        //Получаем текущий обработчик
        var handler = context.GetStepHandler<StepTelegram>();
        //Записываем имя пользователя в кэш 
        handler!.GetCache<StepCache>().Name = update.Message.Text;
        //Регистрация следующего шага
        handler.RegisterNextStep(StepTwo);
        await Helpers.Message.Send(context, msg);
    }

    /// <summary>
    /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
    /// </summary>
    public static async Task StepTwo(IBotContext context)
    {
        string msg = $"Шаг 2 - дата рождения {context.Update.Message.Text}" +
                     $"\nНапиши любой текст, чтобы увидеть результат";
        //Получаем текущий обработчик
        var handler = context.GetStepHandler<StepTelegram>();
        //Записываем дату рождения
        handler!.GetCache<StepCache>().BirthDay = context.Update.Message.Text;
        //Регистрация следующего шага с максимальным ожиданием выполнения этого шага 5 минут от момента регистрации
        handler.RegisterNextStep(StepThree, DateTime.Now.AddMinutes(5));
        //Настройки для сообщения
        var option = new OptionMessage();
        //Добавление пустого reply меню с кнопкой "Главное меню"
        //Функция является приоритетной, если пользователь нажмет эту кнопку будет выполнена функция главного меню, а не следующего шага.
        option.MenuReplyKeyboardMarkup = MenuGenerator.ReplyKeyboard(1, new List<string>(), true, new DictionaryJSON().GetButton(nameof(ReplyKeys.RP_MAIN_MENU)));
        await Helpers.Message.Send(context, msg, option);
    }


    /// <summary>
    /// Напишите в чат любой текст и будет выполнена эта команда если у пользователя был записан следующий шаг
    /// </summary>
    public static async Task StepThree(IBotContext context)
    {
        //Получение текущего обработчика
        var handler = context.GetStepHandler<StepTelegram>();
        //Получение текущего кэша
        var cache = handler!.GetCache<StepCache>();
        string msg = $"Шаг 3 - Результат: Имя:{cache.Name} дата рождения:{cache.BirthDay}" +
                     $"\nПоследовательность шагов очищена.";
        //Последний шаг
        context.ClearStepUserHandler();
        await Helpers.Message.Send(context, msg);
    }

    /// <summary>
    /// Если есть следующий шаг, он будет проигнорирован при выполнение данной команды
    /// Потому что в ReplyMenuHandler значение первого аргумента установлено в true, что значит приоритетная команда
    /// </summary>
    [ReplyMenuHandler("ignorestep")]
    public static async Task IngoreStep(IBotContext context)
    {
        string msg = context.HasStepHandler() 
            ? "Следующий шаг проигнорирован" 
        : "Следующий шаг отсутствовал";
    
        await Helpers.Message.Send(context, msg);
    }
}
```



## Завершение пошагового выполнение команд на последнем шаге

Начиная с версии 0.6 есть возможность взвести флаг, который оповестит, что это последний шаг в системе и пошаговое выполнение нужно завершить.

Пример:

```csharp
var handler = context.GetStepHandler<StepTelegram>();
handler.LastStepExecuted = true;
```

## Игнорирование базовых команд при выполнение пошаговых команд

Начиная с версии 0.6 есть возможность взвести флаг, который оповестит систему, что в данный момент нужно игнорировать все команды, кроме пошаговых.

Пример:

```csharp
var handler = context.GetStepHandler<StepTelegram>();
handler.IgnoreBasicCommands = true;
```
