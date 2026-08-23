# События логирования

Используя экземпляр PRBot можно вызывать 2 события логирования. На текущий момент это:

* InvokeCommonLog - логирование простых событий;
* InvokeErrorLog - логирование ошибок.

Пример как подписаться на события логов

```csharp
var telegram = new PRBotBuilder("Token")
                    .SetBotId(0)
                    .AddAdmin(1111111)
                    .SetClearUpdatesOnStart(true)
                    .Build();

//Подписка на простые логи
telegram.Events.OnCommonLog += Telegram_OnLogCommon;
//Подписка на логи с ошибками
telegram.Events.OnErrorLog += Telegram_OnLogError;
```



Вызов событий через ITelegramBotClient botclient

Простые логи

```csharp
context.InvokeCommonLog("Записать в обычные логи");
```

Логи ошибок

<pre class="language-csharp"><code class="lang-csharp">/// &#x3C;param name="ex">Исключение&#x3C;/param>
<strong>context.InvokeErrorLog(new Exception("что-то пошло не так"));
</strong></code></pre>
