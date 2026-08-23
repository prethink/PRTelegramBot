# Динамические reply команды из json файла

Библиотека предоставляет возможность работать с динамическими reply командами. Это значит, что значения можно сохранить в json файле, и потом изменять название кнопок без перекомпиляции приложения.

Для работы с динамическими командами используется атрибут **ReplyMenuDynamicHandlerAttribute**

```csharp
/// <summary>
/// Конструктор.
/// </summary>
/// <param name="botId">Идентификатор бота.</param>
/// <param name="botIds">Идентификаторы ботов.</param>
/// <param name="commandComparison">Как сравнивать команду.</param>
/// <param name="stringComparison">Как сравнивать строку.</param>
/// <param name="commands">Команды.</param>
public ReplyMenuDynamicHandlerAttribute(params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long botId, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long[] botIds, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(CommandComparison commandComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long botId, CommandComparison commandComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long[] botIds, CommandComparison commandComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(StringComparison stringComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long botId, StringComparison stringComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long[] botIds, StringComparison stringComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
public ReplyMenuDynamicHandlerAttribute(long botId, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands) 
public ReplyMenuDynamicHandlerAttribute(long[] botIds, CommandComparison commandComparison, StringComparison stringComparison, params string[] commands)
```

Пример:\
\
Создадим json файл commands.json. Формат файла key:value. В нем будут находится команды.

```json
{
  "DYNAMIC_COMMANT_EXAMPLE": "Динамическая команда",
  "MAIN_MENU": "Главное меню",
  "MENU": "Меню",
  "RP_START": "Start"
}
```

Создание бота с загрузкой динамических команд.

<pre class="language-csharp"><code class="lang-csharp"><strong>// json провайдер для динамических команд
</strong><strong>var botJsonProvider = new BotConfigJsonProvider(".\\Configs\\commands.json");
</strong><strong>// Получаем команды в формате ключ:значение.
</strong>var dynamicCommands = botJsonProvider.GetKeysAndValues();

// В билдере при создание бота используем AddReplyDynamicCommands и передаем туда динамические команды
var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .AddReplyDynamicCommands(dynamicCommands)
                    .Build();
</code></pre>

Создаем метод обработки динамической команды:

```csharp
/// <summary>
/// Команда отработает для бота с botId 0.
/// Команда отработает при написание в чат значения по ключу "DYNAMIC_COMMANT_EXAMPLE" из файла commands.json.
/// Настройка конфигурационных файла при создание экземпляра PRBot <see cref="Program"/>
/// "DYNAMIC_COMMANT_EXAMPLE": "Динамическая команда"
/// </summary>
[ReplyMenuDynamicHandler(nameof(ExampleConstants.DYNAMIC_COMMANT_EXAMPLE))]
public static async Task ExampleReplyDynamicCommand(IBotContext context)
{
	/*
	 *  Создание провайдера работы с json файлом commands.json
	 *  var botJsonProvider = new BotConfigJsonProvider(".\\Configs\\commands.json");
	 *  
	 *  Выгрузка всех команд в формате ключ:значение
	 *  var dynamicCommands = botJsonProvider.GetKeysAndValues();
	 *
	 *  var telegram = new PRBotBuilder("")
	 *                      .AddReplyDynamicCommands(dynamicCommands)
	 *                      .Build();
	 * 
	 * .AddReplyDynamicCommands(dynamicCommands) - добавляет в список все динамические команды.
	 * 
	 * [ReplyMenuDynamicHandler(nameof(ExampleConstants.DYNAMIC_COMMANT_EXAMPLE))] - работа динамической команды по ключу DYNAMIC_COMMANT_EXAMPLE
	 */

	string msg = nameof(ExampleReplyDynamicCommand);
	await Helpers.Message.Send(context, msg);
}

```

Пишем в боте сообщение "Динамическая команда" и получаем результат выполнения.

<figure><img src="../../.gitbook/assets/изображение (28).png" alt=""><figcaption></figcaption></figure>

Теперь можно без всякой компиляции, просто отредактировать файл commands.json, поменять название команды и она будет выполнена.

```json
{
  "DYNAMIC_COMMANT_EXAMPLE": "Тес команда",
  "MAIN_MENU": "Главное меню",
  "MENU": "Меню",
  "RP_START": "Start"
}
```

<figure><img src="../../.gitbook/assets/изображение (29).png" alt=""><figcaption></figcaption></figure>
